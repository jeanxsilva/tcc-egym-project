import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { formatTime } from 'src/app/models/Converters';
import { InvoiceStatusEnum } from 'src/app/models/Enums';
import { ApiService } from 'src/app/services/api-service/api.service';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { ListMatchTypeEnum, MatchTypeEnum, OperatorEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.scss'],
})
export class RegistrationFormComponent implements OnInit {
  public selectedRegistrations = new Set();
  public modalityClasses: any[];
  public userId: number;
  public student: any;
  public newRegistrations = [];
  public paymentTypes: any[] = [];
  public paymentType: any;
  public dueDay: any;
  public alreadyRegistered: any[] = [];

  constructor(private router: Router, private apiService: ApiService, private authService: AuthService, private alertController: AlertController) {
    this.selectedRegistrations.clear();
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile.User.Id;

      this.loadStudent();
      this.loadPaymentType();
    });
  }

  ngOnInit() {
    this.loadModalityClasses();
  }

  public getRegistration(modalityClass) {
    return Array.from(this.selectedRegistrations).filter((o: any) => o.modalityClassId == modalityClass.id);
  }
  public undoRegistration(registration) {
    this.selectedRegistrations.delete(registration);
  }

  public setRegistration(addingClass: any) {
    let registration = {
      id: 0,
      studentRegistrationId: this.student.id,
      modalityClassId: addingClass.id,
      modalityClass: addingClass,
      isValid: false,
      dueDay: "5",
      modalityPaymentTypeId: 1
    };

    if (this.student) {
      if (this.getRegistration(addingClass).length == 0) {
        this.selectedRegistrations.add(registration);
      } else {
        let registration = this.getRegistration(addingClass)[0];
        this.selectedRegistrations.delete(registration);
      }
    }
  }

  private loadStudent() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRegistration");
    queryBuilder.CreateFilter().AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id")
      .AddEntity("user")
      .AddColumn("name");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.student = result.items[0];
      this.loadRegistrations();
    });
  }

  private loadPaymentType() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listModalityPaymentType");
    queryBuilder.AddColumn("id").AddColumn("description");
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.paymentTypes = result.items;
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
    queryFilter.AddEntity("studentRegistration").AddCondition("id", MatchTypeEnum.EQUALS, this.student.id)
    queryBuilder.AddColumn("id").AddColumn("registerDateTime").AddColumn("isValid");
    queryBuilder.AddEntity("modalityClass").AddColumn("id");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.alreadyRegistered = result.items.map(register => {
        return register.modalityClass.id;
      });
    });
  }

  public loadModalityClasses() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listModalityClass");
    queryBuilder.AddColumn("id")
      .AddColumn("startTime")
      .AddColumn("endTime")
      .AddEntity("instructor")
      .AddEntity("user")
      .AddColumn("name");
    queryBuilder.AddEntity("modality")
      .AddColumn("id")
      .AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.modalityClasses = result.items;
      this.modalityClasses.map(modalityClass => {
        modalityClass.startTime = formatTime(modalityClass.startTime);
        modalityClass.endTime = formatTime(modalityClass.endTime);
      });
    });
  }

  public save() {
    if (this.student && this.selectedRegistrations.size != 0) {
      let entity = { id: this.student.id, registrationModalityClasses: [] };

      Array.from(this.selectedRegistrations).forEach((registration: any) => {
        entity.registrationModalityClasses.push(registration);
      });

      this.apiService.SendToAPI("StudentRegistration", "ChangeRegistration", entity).subscribe((result: any) => {
        console.log(result);
        this.router.navigate(['invoices']);
      }, err => { console.error(err); });
    }
  }
}