import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api-service/api.service';
import { MatchTypeEnum, ListMatchTypeEnum, NumberMatchTypeEnum, SortEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { Student } from '../../student/student-form/student-form.component';
import { print } from 'html-to-printer'

@Component({
  selector: 'app-training-details',
  templateUrl: './training-details.component.html',
  styleUrls: ['./training-details.component.scss']
})
export class TrainingDetailsComponent implements OnInit {
  @ViewChild('scrolling') scrolling: ElementRef;
  @ViewChild('printable') printable: ElementRef;

  public trainingPlanId: number;
  public student: Student;
  public studentList: Student[];
  public trainingPlan: any;
  public days: any = [];

  constructor(private apiService: ApiService, private router: Router, private activatedRoute: ActivatedRoute, private formBuilder: FormBuilder, private http: HttpClient) {
    this.activatedRoute.params.subscribe((result) => {
      if (result && result.id) {
        this.trainingPlanId = parseInt(result.id);

        this.loadTrainingPlan();
      }
    });
  }

  ngOnInit(): void {
    this.loadStudents();
  }

  get trainingPlanExercises() {
    return this.trainingPlan.trainingPlanExercises;
  }

  public generateTraining() {
    this.router.navigate(['training/create'], {
      queryParams: { id: this.trainingPlan.specificToStudent.id }
    });
  }

  public print() {
    print(`
      <div style="border: 1px solid #E9ECEF;font-weight: 600; text-align: center;">
        <h4>${this.student.user.name} ${this.student.user.lastName}</h4>
      </div>
      ${this.printable.nativeElement.innerHTML}
    `)

    return true;
  }

  private loadTrainingPlan() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listTrainingPlan");
    let queryFilter = queryBuilder.CreateFilter();

    queryFilter.AddCondition("id", MatchTypeEnum.EQUALS, this.trainingPlanId);

    let studentBuilder = queryBuilder.AddEntity("specificToStudent").AddColumn("id")
      .AddColumn("code");
    studentBuilder.AddEntity("user")
      .AddColumn("id")
      .AddColumn("name")
      .AddColumn("lastName");
    let physicalAssessmentBuilder = studentBuilder.AddEntity("physicalAssesments")
      .AddColumn("id")
      .AddColumn("studentGoal")
      .AddColumn("registerDateTime");
    physicalAssessmentBuilder.AddEntity("registeredByEmployee").AddEntity("user").AddColumn("name");
    physicalAssessmentBuilder.AddEntity("studentCaracteristics")
      .AddColumn("id")
      .AddColumn("weight")
      .AddColumn("height")
      .AddColumn("triceps")
      .AddColumn("chest")
      .AddColumn("subaxillary")
      .AddColumn("subscapular")
      .AddColumn("abdominal")
      .AddColumn("suprailiac")
      .AddColumn("thigh")
      .AddColumn("leanMass")
      .AddColumn("fatMass")
      .AddColumn("fatPercentage")
      .AddColumn("bodyMassIndex")
      .AddColumn("ageAtMoment")
      .AddColumn("basalMetabolicRate")
      .AddColumn("bodyDensity");

    let trainingPlanExerciseBuilder = queryBuilder.AddColumn("id")
      .AddColumn("description")
      .AddColumn("registerDateTime")
      .AddColumn("note")
      .AddEntity("trainingPlanExercises")
      .AddColumn("id")
      .AddColumn("dayOfWeek")
      .AddColumn("order")
      .AddColumn("isCombined")
      .AddColumn("repetition");
    trainingPlanExerciseBuilder.AddEntity("exercise")
      .AddColumn("id")
      .AddColumn("description")
      .AddEntity("exerciseCategory")
      .AddColumn("id")
      .AddColumn("description");
    trainingPlanExerciseBuilder.AddEntity("combinedExercise")
      .AddColumn("id")
      .AddColumn("description")
      .AddEntity("exerciseCategory")
      .AddColumn("id")
      .AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result) => {
      this.trainingPlan = result.items[0];

      if (this.trainingPlan) {
        this.student = this.trainingPlan.specificToStudent;
        this.trainingPlan.trainingPlanExercises.forEach(item => {
          let filtered = this.days.filter(o => o.number === item.dayOfWeek);

          if (filtered.length == 0) {
            this.days.push({ number: item.dayOfWeek, description: `Dia ${item.dayOfWeek}` });
          }
        })
      }
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }

  private loadStudents() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRegistration");
    let queryFilter = queryBuilder.CreateFilter();

    queryFilter.AddEntityList("physicalAssesments", ListMatchTypeEnum.SOME).AddCondition("id", NumberMatchTypeEnum.GREATER_THAN, 0);
    queryBuilder.AddSort("id", SortEnum.ASC);

    queryBuilder.AddColumn("id")
      .AddColumn("code")
      .AddEntity("user")
      .AddColumn("id")
      .AddColumn("name")
      .AddColumn("lastName");

    let physicalAssessmentBuilder = queryBuilder.AddEntity("physicalAssesments")
      .AddColumn("id")
      .AddColumn("studentGoal")
      .AddColumn("registerDateTime");
    physicalAssessmentBuilder.AddEntity("registeredByEmployee").AddEntity("user").AddColumn("name");
    physicalAssessmentBuilder.AddEntity("studentCaracteristics")
      .AddColumn("id")
      .AddColumn("weight")
      .AddColumn("height")
      .AddColumn("triceps")
      .AddColumn("chest")
      .AddColumn("subaxillary")
      .AddColumn("subscapular")
      .AddColumn("abdominal")
      .AddColumn("suprailiac")
      .AddColumn("thigh")
      .AddColumn("leanMass")
      .AddColumn("fatMass")
      .AddColumn("fatPercentage")
      .AddColumn("bodyMassIndex")
      .AddColumn("ageAtMoment")
      .AddColumn("basalMetabolicRate")
      .AddColumn("bodyDensity");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result) => {
      console.log(result)
      this.studentList = result.items;
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }
}
