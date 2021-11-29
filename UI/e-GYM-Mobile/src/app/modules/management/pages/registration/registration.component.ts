import { QueryBuilder } from './../../../../services/query-builder/query-builder';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { ApiService } from 'src/app/services/api-service/api.service';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { ListMatchTypeEnum, MatchTypeEnum, OperatorEnum } from 'src/app/services/query-builder/enums';
import { InvoiceStatusEnum } from 'src/app/models/Enums';
import { formatTime } from 'src/app/models/Converters';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements OnInit {
  public registrations: any[];
  public userId: number;
  public student: any;

  constructor(private router: Router, private apiService: ApiService, private authService: AuthService, private alertController: AlertController) {
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile.User.Id;

      this.loadStudent();
      this.loadRegistrations();
    });
  }

  ngOnInit() { }

  private loadStudent() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRegistration");
    queryBuilder.CreateFilter().AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id")
      .AddEntity("user")
      .AddColumn("name");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      if (result) {
        this.student = result.items[0];
      }
    });
  }

  private loadRegistrations() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listRegistrationModalityClass");
    let queryFilter = queryBuilder.CreateFilter();

    queryFilter.AddOperator(OperatorEnum.OR)
      .AddCondition("isValid", MatchTypeEnum.EQUALS, true)
      .AddEntityList("invoiceDetails", ListMatchTypeEnum.SOME)
      .AddEntity("invoice")
      .AddEntity("invoiceStatus")
      .AddCondition("id", MatchTypeEnum.EQUALS, InvoiceStatusEnum.Generated) // OR have invoice generated;
    queryFilter.AddEntity("studentRegistration").AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);

    queryBuilder.AddColumn("id").AddColumn("registerDateTime").AddColumn("isValid");
    queryBuilder.AddEntity("modalityPaymentType").AddColumn("id");
    queryBuilder.AddEntity("invoiceDetails").AddEntity("invoice").AddColumn("id");
    let classBuilder = queryBuilder.AddEntity("modalityClass").AddColumn("id");
    classBuilder.AddEntity("instructor")
      .AddEntity("user")
      .AddColumn("name");
    classBuilder.AddColumn("id")
      .AddColumn("startTime")
      .AddColumn("endTime")
      .AddEntity("modality")
      .AddColumn("id")
      .AddColumn("description")

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.registrations = result.items;
      this.registrations.map(register => {
        register.modalityClass.startTime = formatTime(register.modalityClass.startTime);
        register.modalityClass.endTime = formatTime(register.modalityClass.endTime);
      });
    });
  }

  public seeInvoice(registration) {
    let invoice = registration?.invoiceDetails[0].invoice;

    console.log(invoice)
    if (invoice) {
      this.router.navigate(['invoice', invoice.id]);
    }
  }

  public registerClass() {
    this.router.navigate(['registration/insert']);
  }

  public isInRegistration(classe) {
    let isInRegistration = false;
    if (this.registrations && this.registrations.length > 0) {
      this.registrations.forEach(registration => {
        if (registration.modalityClass.id == classe.id) {
          isInRegistration = true;
        }
      });
    }

    return isInRegistration;
  }

  public cancelRegistration(cancelingRegistration) {
    this.save(cancelingRegistration);
  }

  async presentAlertConfirm(registration) {
    const alert = await this.alertController.create({
      cssClass: '',
      header: 'Cancelar matricula',
      message: 'Deseja mesmo cancelar a matricula?',
      buttons: [
        {
          text: 'Voltar',
          role: 'cancel',
          cssClass: 'btn-danger',
          handler: () => {
          }
        }, {
          text: 'Confirmar',
          cssClass: 'btn-success',
          handler: () => {
            this.cancelRegistration(registration);
          }
        }
      ]
    });

    await alert.present();
  }

  public save(cancelingRegistration: any) {
    if (this.student && cancelingRegistration) {
      let entity = { id: this.student.id, registrationModalityClasses: [] };

      cancelingRegistration.invoiceDetails = null;
      cancelingRegistration.isValid = false;
      cancelingRegistration.modalityPaymentTypeId = cancelingRegistration.modalityPaymentType.id;
      cancelingRegistration.studentRegistrationId = this.student.id;
      cancelingRegistration.modalityClassId = cancelingRegistration.modalityClass.id;

      entity.registrationModalityClasses.push(cancelingRegistration);

      this.apiService.SendToAPI("StudentRegistration", "ChangeRegistration", entity).subscribe((result: any) => {
        // window.location.reload();
        if (result) {
          this.loadRegistrations();
        }
      }, err => { console.error(err); });
    }
  }
}