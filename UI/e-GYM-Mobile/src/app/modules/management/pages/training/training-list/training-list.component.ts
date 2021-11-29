import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api-service/api.service';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';

@Component({
  selector: 'app-training-list',
  templateUrl: './training-list.component.html',
  styleUrls: ['./training-list.component.scss'],
})
export class TrainingListComponent implements OnInit {
  public trainings: any[] = [];
  public olderTrainings: any[] = [];
  public userId: number;

  constructor(private apiService: ApiService, private authService: AuthService, private router: Router) {
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile.User.Id;

      this.loadTrainings();
    });
  }

  ngOnInit() { }

  private loadTrainings() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listTrainingPlan");
    queryBuilder.CreateFilter().AddEntity("specificToStudent").AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);;
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

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      if (result && result.items) {
        result.items.forEach(item => {
          if (item.isActive) {
            this.trainings.push(item);
          } else {
            this.olderTrainings.push(item);
          }
        });
      }
    });
  }

  public seeDetails(trainingPlanId: number) {
    this.router.navigate(['training/details', trainingPlanId]);
  }
}
