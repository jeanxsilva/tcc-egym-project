import { AuthService } from './../../../../../services/auth-service.ts/auth-service.service';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from './../../../../../services/query-builder/query-builder';
import { ApiService } from './../../../../../services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalController } from '@ionic/angular';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-assesment-request',
  templateUrl: './assesment-request.component.html',
  styleUrls: ['./assesment-request.component.scss'],
})
export class AssesmentRequestComponent implements OnInit {
  public formEntity: FormGroup;
  public scheduledToDate: string = this.datePipe.transform(new Date(), "yyyy-MM-dd HH:mm");
  public scheduledHours: any[] = [];
  public minDate = {
    hours: '6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22',
    days: this.getDaysAvailable(),
    months: this.getMonthsAvailable(),
    year: new Date().getFullYear()
  };
  public student: any;
  public userId: number;
  public message: string;

  constructor(public modalController: ModalController, private formBuilder: FormBuilder, private apiService: ApiService, private datePipe: DatePipe, private authService: AuthService) {
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile.User.Id;

      this.loadStudent();
    });

    this.formEntity = this.formBuilder.group({
      id: [0],
      studentId: [0],
      note: ['', Validators.required],
      physicalAssesmentScheduled: ['']
    });

    this.hoursScheduled();
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

  private getMonthsAvailable() {
    let monthsAvailable = '';

    for (let i = new Date().getMonth() + 1; i <= 12; i++) {
      monthsAvailable += ',' + i;
    }

    return monthsAvailable;
  }

  private getDaysAvailable() {
    let daysAvailable = '';
    for (let i = new Date().getUTCDate(); i <= 31; i++) {
      daysAvailable += ',' + i;
    }
    console.log(daysAvailable)
    return daysAvailable;
  }

  public onChangeScheduled() {
    let scheduled = new Date(this.scheduledToDate).getHours() + ':' + new Date(this.scheduledToDate).getMinutes();

    if (this.scheduledHours.includes(scheduled)) {
      this.message = 'Não é possivel selecionar o horário atual pois conflita com outro horário.';
    }
  }

  public hoursScheduled() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPhysicalAssesmentScheduled");
    queryBuilder.CreateFilter().AddCondition("wasAnswered", MatchTypeEnum.EQUALS, true).AddCondition("wasCanceled", MatchTypeEnum.EQUALS, false);
    queryBuilder.AddColumn("scheduledToDate");

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      result.items.forEach((item, index) => {
        if (new Date(item.scheduledToDate).toDateString() === new Date().toDateString()) {
          this.scheduledHours.push(new Date(item.scheduledToDate).getHours() + ':' + new Date(item.scheduledToDate).getMinutes());
        }
      })
    });
  }

  public dismiss() {
    this.modalController.dismiss({
      'dismissed': true
    });
  }

  public save() {
    let entity = this.formEntity.value;
    entity.studentId = this.student.id;
    entity.physicalAssesmentScheduled = {
      id: 0,
      scheduledToDate: this.scheduledToDate,
      studentRegistrationId: this.student.id
    };

    this.apiService.SendToAPI("StudentRequest", "RequestPhysicalAssesment", entity).subscribe(result => {
      console.log(result);
    });
  }
}