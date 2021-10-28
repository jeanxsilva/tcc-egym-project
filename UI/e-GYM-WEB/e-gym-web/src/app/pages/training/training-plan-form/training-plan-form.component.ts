import { Sort } from './../../../models/SortInfo';
import { ListMatchTypeEnum, NumberMatchTypeEnum, SortEnum } from './../../../services/query-builder/enums';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api-service/api.service';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { Student } from '../../student/student-form/student-form.component';
import { FilterField } from 'src/app/services/query-builder/filter/filter';

@Component({
  selector: 'app-training-plan-form',
  templateUrl: './training-plan-form.component.html',
  styleUrls: ['./training-plan-form.component.scss']
})
export class TrainingPlanFormComponent implements OnInit {
  public isNew: boolean = true;
  public trainingPlanId: number;
  public addingTrainingPlan: {
    id: number,
    isCombined: boolean,
    repetition: string,
    exerciseId: number,
    exercise: any,
    combinedExerciseId: number,
    combinedExercise: any
  } = {
      id: 0,
      isCombined: false,
      repetition: '3x12',
      exerciseId: 0,
      exercise: {},
      combinedExerciseId: 0,
      combinedExercise: null
    };
  @ViewChild('scrolling') scrolling: ElementRef;
  public formTrainingPlan: FormGroup;
  public student: Student;
  public studentList: Student[];
  public trainingPlan: any;
  public lastPhysicalAssesment: any;
  public currentDay: number = 1;
  public exerciseCategories: any[] = [];
  public exerciseCategory: any;
  public exercises: any[] = [];
  public exercise: any;
  public days = [{
    number: 1,
    description: 'Dia 1'
  },]

  constructor(private apiService: ApiService, private router: Router, private activatedRoute: ActivatedRoute, private formBuilder: FormBuilder, private http: HttpClient) {
    this.activatedRoute.params.subscribe((result) => {
      if (result && result.id) {
        this.isNew = false;
        this.trainingPlanId = parseInt(result.id);

        this.loadTrainingPlan();
      }
    });

    this.formTrainingPlan = this.formBuilder.group({
      id: [0],
      description: [''],
      registerDateTime: [new Date()],
      note: [''],
      specificToStudentId: [0],
      trainingPlanExercises: this.formBuilder.array([])
    });
  }

  get trainingPlanExercises() {
    return this.formTrainingPlan.get('trainingPlanExercises') as FormArray;
  }

  ngOnInit(): void {
    if (this.isNew) {
      this.loadStudents();
    }

    this.loadExerciseCategories();
  }

  public onChangeStudent() {
    console.log(this.student )
    if(this.student && this.student.physicalAssesments.length > 0){
      let assessments = [...this.student.physicalAssesments];
      assessments.sort((a, b) => {
        return a.id < b.id ? -1 : a.id > b.id ? 1 : 0;
      });
      
      this.lastPhysicalAssesment = assessments[this.student.physicalAssesments.length - 1];
    }
  }

  public addDay() {
    this.currentDay = this.days.length + 1;
    this.days.push({ number: this.currentDay, description: `Dia ${this.currentDay}` });
    console.log(this.scrolling)

    setTimeout(() => {
      let scroll = this.scrolling.nativeElement.scrollWidth - 146;
      this.scrolling.nativeElement.scrollTo(scroll, 0);
    }, 0)
  }

  public addTrainingPlanExercise() {
    if (!this.addingTrainingPlan.exercise || this.addingTrainingPlan.exercise.id == null) {
      console.log("ERROR");
      return;
    }

    this.trainingPlanExercises.push(
      this.formBuilder.group({
        id: 0,
        dayOfWeek: [this.currentDay],
        order: [1],
        isCombined: [this.addingTrainingPlan.isCombined],
        repetition: [this.addingTrainingPlan.repetition],
        exerciseId: [this.addingTrainingPlan.exercise.id],
        exercise: [this.addingTrainingPlan.exercise],
        exerciseCategory: [this.exerciseCategory],
        combinedExerciseId: [this.addingTrainingPlan.combinedExercise?.id],
        combinedExercise: [this.addingTrainingPlan.combinedExercise]
      })
    );
  }

  private updateForm() {
    this.formTrainingPlan.patchValue({
      id: this.trainingPlan.id,
      description: this.trainingPlan.description,
      registerDateTime: this.trainingPlan.registerDateTime,
      note: this.trainingPlan.note,
      trainingPlanExercises: [this.trainingPlan.trainingPlanExercises]
    });
  }

  public save() {
    if(!this.student){
      return;
    }
    
    let entity = this.formTrainingPlan.value;
    entity.specificToStudentId = this.student.id;

    this.apiService.SendToAPI("TrainingPlan", "Save", entity).subscribe(result => {
      console.log(result);
    })
  }

  public cancel() {

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
        this.student = this.trainingPlan.studentRegistration;
      }

      this.updateForm();
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }

  private loadStudents() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRegistration");
    let queryFilter = queryBuilder.CreateFilter();

    queryFilter.AddConditionWithList("physicalAssesments", ListMatchTypeEnum.SOME, new FilterField("id", NumberMatchTypeEnum.GREATER_THAN, 0));
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
      this.studentList = result.items;
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }

  private loadExerciseCategories() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listExerciseCategory");
    queryBuilder.AddSort("id", SortEnum.ASC);

    queryBuilder.AddColumn("id")
      .AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result) => {
      this.exerciseCategories = result.items;
      this.exerciseCategory = this.exerciseCategories[0];
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }

  public loadExercises() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listExercise");
    queryBuilder.CreateFilter().AddEntity("exerciseCategory").AddCondition("id", MatchTypeEnum.EQUALS, this.exerciseCategory.id);
    queryBuilder.AddSort("id", SortEnum.ASC);

    queryBuilder.AddColumn("id")
      .AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result) => {
      this.exercises = result.items;
      this.addingTrainingPlan.exercise = this.exercises[0];
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }

}