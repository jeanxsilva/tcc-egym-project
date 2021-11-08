import { Sort } from './../../../models/SortInfo';
import { ListMatchTypeEnum, NumberMatchTypeEnum, SortEnum } from './../../../services/query-builder/enums';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api-service/api.service';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { Student } from '../../student/student-form/student-form.component';
import { FilterField } from 'src/app/services/query-builder/filter/filter';
import Swal from 'sweetalert2';
import { IFormBase } from 'src/app/models/CrudBase';

@Component({
  selector: 'app-training-plan-form',
  templateUrl: './training-plan-form.component.html',
  styleUrls: ['./training-plan-form.component.scss']
})
export class TrainingPlanFormComponent implements OnInit, IFormBase {
  @ViewChild('scrolling') scrolling: ElementRef;

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
      exercise: null,
      combinedExerciseId: 0,
      combinedExercise: null
    };
  public formEntity: FormGroup;
  public student: Student;
  public studentId: number;
  public studentList: Student[];
  public lastPhysicalAssesment: any;
  public currentDay: number = 1;
  public exerciseCategories: any[] = [];
  public exerciseCategory: any;
  public combinedExerciseCategory: any;
  public exercises: any[] = [];
  public exercise: any;
  public days = [{
    number: 1,
    description: 'Dia 1'
  },]

  constructor(private apiService: ApiService, private router: Router, private activatedRoute: ActivatedRoute, private formBuilder: FormBuilder, private http: HttpClient) {
    this.activatedRoute.queryParams.subscribe(param => {
      if (param && param.id) {
        this.studentId = param.id;
      }
    });


    this.formEntity = this.formBuilder.group({
      id: [0],
      description: ['', Validators.required],
      registerDateTime: [new Date()],
      note: [''],
      specificToStudentId: [0],
      trainingPlanExercises: this.formBuilder.array([])
    });
  }

  get trainingPlanExercises() {
    return this.formEntity.get('trainingPlanExercises') as FormArray;
  }

  get hasThisDay() {
    let has = false;

    if (this.trainingPlanExercises.value.length != 0) {
      this.trainingPlanExercises.value.forEach(exercise => {
        has = exercise.dayOfWeek == this.currentDay;
      });
    }

    return has;
  }

  ngOnInit(): void {
    this.loadStudents();
    this.loadExerciseCategories();
  }

  public onChangeStudent() {
    if (this.student && this.student.physicalAssesments.length > 0) {
      let assessments = [...this.student.physicalAssesments];
      assessments.sort((a, b) => {
        return a.id < b.id ? -1 : a.id > b.id ? 1 : 0;
      });

      this.lastPhysicalAssesment = assessments[this.student.physicalAssesments.length - 1];
    }
  }

  public addDay() {
    if (!this.hasThisDay) {
      return;
    }

    this.currentDay = this.days.length + 1;
    this.days.push({ number: this.currentDay, description: `Dia ${this.currentDay}` });

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

    if (!this.addingTrainingPlan.isCombined) {
      this.addingTrainingPlan.combinedExercise = null;
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

    this.addingTrainingPlan.isCombined = false;
    this.addingTrainingPlan.combinedExercise = null;
  }

  public removeTrainingPlanExercise(index: number) {
    this.trainingPlanExercises.removeAt(index);
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
      this.studentList = result.items;

      if (this.studentId) {
        let student = this.studentList.filter(o => o.id == this.studentId);

        if (student.length > 0) {
          this.student = student[0];
        }
      }
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

      this.loadExercises(this.exerciseCategory.id);
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }

  public loadExercises(categoryId: number) {
    let queryBuilder: QueryBuilder = new QueryBuilder("listExercise");
    queryBuilder.CreateFilter().AddEntity("exerciseCategory").AddCondition("id", MatchTypeEnum.EQUALS, categoryId);
    queryBuilder.AddSort("description", SortEnum.ASC);

    queryBuilder.AddColumn("id")
      .AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result) => {
      this.exercises = result.items;
      this.addingTrainingPlan.exercise = result.items[0];
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }

  public save() {
    if (!this.student || this.formEntity.invalid) {
      return;
    }

    let entity = this.formEntity.value;
    entity.specificToStudentId = this.student.id;

    this.apiService.SendToAPI("TrainingPlan", "Save", entity).subscribe(result => {
      this.formEntity.markAsPristine();
      this.router.navigate(['trainings']);
    })
  }

  public cancel() {
    this.router.navigate(['trainings']);
  }
}