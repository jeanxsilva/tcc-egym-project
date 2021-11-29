import { ApiService } from './../../../../../services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';

@Component({
  selector: 'app-training-details',
  templateUrl: './training-details.component.html',
  styleUrls: ['./training-details.component.scss'],
})
export class TrainingDetailsComponent implements OnInit {
  public training: any;
  public trainingPlanId: number;
  public days: any[] = [];

  constructor(private apiService: ApiService, private authService: AuthService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(param => {
      this.trainingPlanId = parseInt(param.id);

      this.loadTraining();
    });
  }

  ngOnInit() { }

  private loadTraining() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listTrainingPlan");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.trainingPlanId);
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
      this.training = result.items[0];
      this.training.trainingPlanExercises.forEach(item => {
        let day = this.days.find(o => o.number === item.dayOfWeek)

        if (!day) {
          this.days.push({ number: item.dayOfWeek, trainingPlanExercises: [item] });
          return;
        }

        day.trainingPlanExercises.push(item);
      });
    });
  }
}