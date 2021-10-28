import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from 'src/app/services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { Student } from '../../student/student-form/student-form.component';

@Component({
  selector: 'app-physical-assesment-screen',
  templateUrl: './physical-assesment-screen.component.html',
  styleUrls: ['./physical-assesment-screen.component.scss']
})
export class PhysicalAssesmentScreenComponent implements OnInit {
  public schedule: any;
  public formPhysicalAssesment: FormGroup;
  public scheduledId: number;
  public wasCalculated: boolean = false;

  constructor(private apiService: ApiService, private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router) {
    this.activatedRoute.params.subscribe(result => {
      if (result && result.scheduledId) {
        this.scheduledId = parseInt(result.scheduledId);
        this.loadSchedule();
      }
    });

    this.formPhysicalAssesment = this.formBuilder.group({
      id: [0],
      studentId: [0],
      studentGoal: [''],
      scheduledPhysicalAssesmentId: [0, Validators.required],
      studentCaracteristic: this.formBuilder.group({
        id: [0],
        studentRegistrationId: [0],
        weight: [0, Validators.required],
        height: [0, Validators.required],
        triceps: [0, Validators.required],
        chest: [0, Validators.required],
        subaxillary: [0, Validators.required],
        subscapular: [0, Validators.required],
        abdominal: [0, Validators.required],
        suprailiac: [0, Validators.required],
        thigh: [0, Validators.required],
        leanMass: [0],
        fatMass: [0],
        fatPercentage: [0],
        bodyMassIndex: [0],
        ageAtMoment: [0, Validators.required],
        basalMetabolicRate: [0],
        bodyDensity: [0]
      }),
    });
  }

  ngOnInit(): void {
  }

  private updateForm() {
    this.formPhysicalAssesment.patchValue({
      id: 0,
      studentId: this.schedule.studentRegistration.id,
      studentGoal: '',
      scheduledPhysicalAssesmentId: this.schedule.id,
      studentCaracteristic: {
        id: 0,
        studentRegistrationId: this.schedule.studentRegistration.id,
        weight: null,
        height: null,
        triceps: null,
        chest: null,
        subaxillary: null,
        subscapular: null,
        abdominal: null,
        suprailiac: null,
        thigh: null,
        leanMass: 0,
        fatMass: 0,
        fatPercentage: 0,
        bodyMassIndex: 0,
        ageAtMoment: null,
        basalMetabolicRate: 0,
        bodyDensity: 0
      },
    });
  }

  private loadSchedule() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPhysicalAssesmentScheduled");
    let queryFilter = queryBuilder.CreateFilter();
    queryFilter.AddCondition("id", MatchTypeEnum.EQUALS, this.scheduledId);
    queryBuilder.AddColumn("id").AddEntity("studentRegistration").AddColumn("id").AddColumn("code").AddEntity("user").AddColumn("id").AddColumn("name").AddColumn("genre");

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.schedule = result.items[0];

      this.updateForm();
    }, err => {
      console.error(err);
    });
  }

  get studentCaracteristic() {
    return this.formPhysicalAssesment.get('studentCaracteristic');
  }

  get imcResult(){
    return this.studentCaracteristic.value.bodyMassIndex;
  }

  public calculateResult() {
    this.apiService.SendToAPI("PhysicalAssesment", "CalculateCaracteristics", this.studentCaracteristic.value).subscribe((result: any) => {
      if (result.HasError == false) {
        let studentCaracteristic = result.Result;
        
        this.studentCaracteristic.patchValue({
          leanMass: studentCaracteristic.LeanMass,
          fatMass: studentCaracteristic.FatMass,
          fatPercentage: studentCaracteristic.FatPercentage,
          bodyMassIndex: studentCaracteristic.BodyMassIndex,
          basalMetabolicRate: studentCaracteristic.BasalMetabolicRate,
          bodyDensity: studentCaracteristic.BodyDensity
        });
        this.wasCalculated = true;
      }
    });
  }

  public cancel() {
    this.router.navigate(['assessments']);
  }

  public save() {
    let entity = this.formPhysicalAssesment.value;
    entity.studentCaracteristics = this.formPhysicalAssesment.get('studentCaracteristic').value;
    console.log(entity)
    if (this.formPhysicalAssesment.valid) {
      this.apiService.SendToAPI("PhysicalAssesment", "Save", entity).subscribe(result => {
        console.log(result);
      });
    }
  }
}