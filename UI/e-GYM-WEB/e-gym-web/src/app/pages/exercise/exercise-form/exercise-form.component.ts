import { ApiService } from './../../../services/api-service/api.service';
import { QueryBuilder } from './../../../services/query-builder/query-builder';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';

@Component({
  selector: 'app-exercise-form',
  templateUrl: './exercise-form.component.html',
  styleUrls: ['./exercise-form.component.scss']
})
export class ExerciseFormComponent implements OnInit {
  public isNew: boolean = true;
  public formExercise: FormGroup;
  public exerciseId: number;
  public exercise: Exercise = new Exercise();
  public categories: ExerciseCategory[];

  constructor(private formBuilder: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute, private apiService: ApiService) {
    this.activatedRoute.params.subscribe(result => {
      if (result && result.id) {
        this.exerciseId = result.id;
        this.isNew = false;

        this.loadExercise();
      }
    });

    this.formExercise = this.formBuilder.group({
      id: [0],
      description: ["", Validators.required],
      exerciseCategoryId: [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadCategories();

    this.updateForm();
  }

  public updateForm() {
    this.formExercise.patchValue({
      id: this.exercise.id,
      description: this.exercise.description,
      exerciseCategoryId: this.exercise.exerciseCategory.id
    });
  }

  public loadExercise() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listExercise");
    queryBuilder.AddColumn("id").AddColumn("description")
      .AddEntity("exerciseCategory").AddColumn("id").AddColumn("description");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.exerciseId);

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      console.log(result);
      this.exercise = result.items[0];
    }, err => {
      console.error(err);
    });
  }

  public loadCategories() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listExerciseCategory");
    queryBuilder.AddColumn("id").AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      console.log(result);
      this.categories = result.items;
    }, err => {
      console.error(err);
    });
  }

  public save() {
    this.router.navigate(['exercises']);
  }
}

export class Exercise {
  id: number;
  description: string;
  exerciseCategoryId: number;
  exerciseCategory: ExerciseCategory;
}

export class ExerciseCategory {
  id: number;
  description: string;
}