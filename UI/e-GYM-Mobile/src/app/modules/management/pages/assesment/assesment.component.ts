import { LoaderService } from './../../../../services/loader-service/loader-service.service';
import { AssesmentRequestComponent } from './assesment-request/assesment-request.component';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalController, AlertController } from '@ionic/angular';
import { ApiService } from 'src/app/services/api-service/api.service';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';

@Component({
  selector: 'app-assesment',
  templateUrl: './assesment.component.html',
  styleUrls: ['./assesment.component.scss'],
})
export class AssesmentComponent implements OnInit {
  public assesments: any[] = [];
  public assesmentScheduleds: any[] = [];
  public userId: number;

  constructor(private apiService: ApiService, private authService: AuthService, private router: Router, public modalController: ModalController, private alertController: AlertController, private loaderService: LoaderService) {
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile.User.Id;

      this.loadAssesments();
      this.loadAssesmentScheduleds();
    });
  }

  ngOnInit() { }

  public loadAssesments() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPhysicalAssesment");
    queryBuilder.CreateFilter().AddEntity("student").AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id")
      .AddColumn("studentGoal")
      .AddColumn("registerDateTime");
    queryBuilder.AddEntity("registeredByEmployee")
      .AddColumn("id")
      .AddEntity("user")
      .AddColumn("name");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.assesments = result.items;
    });
  }

  public loadAssesmentScheduleds() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPhysicalAssesmentScheduled");
    queryBuilder.CreateFilter().AddCondition("wasAnswered", MatchTypeEnum.EQUALS, false)
      .AddCondition("wasCanceled", MatchTypeEnum.EQUALS, false)
      .AddEntity("studentRegistration").AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id")
      .AddColumn("note")
      .AddColumn("scheduledToDate")
      .AddColumn("registerDateTime");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.assesmentScheduleds = result.items;
    });
  }

  public async requestAssesment() {
    const modal = await this.modalController.create({
      component: AssesmentRequestComponent,
      cssClass: ''
    });

    return await modal.present();
  }

  public cancelScheduled(scheduledId: number) {
    this.apiService.SendToAPI("StudentRequest", "CancelSchedule", scheduledId).subscribe(result => {
      console.log(result);
    });
  }

  public seeDetails(assesmentId: number) {
    this.router.navigate(['assesment', assesmentId]);
  }

  async presentAlertConfirm(assesmentScheduled) {
    const alert = await this.alertController.create({
      cssClass: '',
      header: 'Cancelar agendamento',
      message: 'Deseja mesmo cancelar o agendamento?',
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
            this.cancelScheduled(assesmentScheduled.id);
          }
        }
      ]
    });

    await alert.present();
  }
}