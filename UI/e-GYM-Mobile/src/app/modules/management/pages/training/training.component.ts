import { RequestStatusEnum, RequestCategoryEnum } from './../../../../models/Enums';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { ApiService } from 'src/app/services/api-service/api.service';
import { Component, ContentChild, OnInit, TemplateRef } from '@angular/core';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { AlertController } from '@ionic/angular';

@Component({
  selector: 'app-training',
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.scss'],
})
export class TrainingComponent implements OnInit {
  public training: any;
  public days: any[] = [];
  public userId: number;
  public student: any;
  public trainingRequests: any[] = [];

  constructor(private apiService: ApiService, private authService: AuthService, private router: Router, private alertController: AlertController) {
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile.User.Id;

      this.loadTraining();
      this.loadStudent();
      this.loadRequests();
    });
  }

  ngOnInit() { }

  private loadStudent() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRegistration");
    queryBuilder.CreateFilter().AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.student = result.items[0];
    });
  }

  private loadRequests() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRequest");
    let queryFilter = queryBuilder.CreateFilter();
    queryFilter.AddEntity("student").AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryFilter.AddEntity("requestStatus").AddCondition("id", MatchTypeEnum.EQUALS, RequestStatusEnum.Opened);
    queryFilter.AddEntity("requestCategory").AddCondition("id", MatchTypeEnum.EQUALS, RequestCategoryEnum.Training);
    queryBuilder.AddColumn("id")
    queryBuilder.AddColumn("registerDateTime")
    queryBuilder.AddEntity("requestStatus").AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.trainingRequests = result.items;
    });
  }

  private loadTraining() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listTrainingPlan");
    queryBuilder.CreateFilter().AddCondition("isActive", MatchTypeEnum.EQUALS, true).AddEntity("specificToStudent").AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id")
      .AddColumn("description")
      .AddColumn("registerDateTime")
      .AddColumn("note")
      .AddColumn("isActive");
    let trainingPlanExerciseBuilder = queryBuilder.AddEntity("trainingPlanExercises")
      .AddColumn("id")
      .AddColumn("dayOfWeek")
      .AddColumn("order")
      .AddColumn("isCombined")
      .AddColumn("repetition");
    trainingPlanExerciseBuilder.AddEntity("exercise").AddColumn("description");
    trainingPlanExerciseBuilder.AddEntity("combinedExercise").AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.training = result.items[0];
      this.training?.trainingPlanExercises.forEach(item => {
        let day = this.days.find(o => o.number === item.dayOfWeek)

        if (!day) {
          this.days.push({ number: item.dayOfWeek, trainingPlanExercises: [item] });
          return;
        }

        day.trainingPlanExercises.push(item);
      });
    });
  }

  public listTraining() {
    this.router.navigate(['training/historic']);
  }

  public requestNewTraining() {
    if (this.trainingRequests.length == 0) {
      let entity: any = {
        id: 0,
        studentId: this.student.id,
        note: '',
      };

      this.apiService.SendToAPI("StudentRequest", "RequestChangeTraining", entity).subscribe(result => {
        console.log(result);
        if (result && result.HasError == false) {
          this.router.navigate(['requests']);
        }
      });
    }
  }

  public cancelRequest(requestId: number) {
    this.apiService.SendToAPI("StudentRequest", "CancelRequest", requestId).subscribe(result => {
      console.log(result);
    });
  }

  async presentAlertConfirm() {
    const alert = await this.alertController.create({
      cssClass: '',
      header: 'Troca de treino',
      message: 'Deseja mesmo solicitar uma troca de treino?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          cssClass: 'btn-danger',
          handler: () => {
          }
        }, {
          text: 'Confirmar',
          cssClass: 'btn-success',
          handler: () => {
            this.requestNewTraining();
          }
        }
      ]
    });

    await alert.present();
  }
}