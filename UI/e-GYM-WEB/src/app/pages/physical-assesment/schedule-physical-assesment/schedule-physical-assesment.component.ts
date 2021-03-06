import { QueryBuilder } from './../../../services/query-builder/query-builder';
import { Router } from '@angular/router';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Student } from '../../student/student-form/student-form.component';
import { ApiService } from 'src/app/services/api-service/api.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';
import { IFormBase } from 'src/app/models/CrudBase';

@Component({
  selector: 'app-schedule-physical-assesment',
  templateUrl: './schedule-physical-assesment.component.html',
  styleUrls: ['./schedule-physical-assesment.component.scss']
})
export class SchedulePhysicalAssesmentComponent implements OnInit, IFormBase {
  public student: Student;
  public studentList: Student[];
  public formEntity: FormGroup;
  public dateAdded: any;
  public message: any;
  @ViewChild('warningModal') warningModal: TemplateRef<any>;

  constructor(private router: Router, private formBuilder: FormBuilder, private apiService: ApiService, private ngbModal: NgbModal) {
    let state = this.router.getCurrentNavigation().extras.state;
    if (state) {
      this.student = state.student;
    }

    this.formEntity = this.formBuilder.group({
      id: [0],
      studentRegistration: [new Student(), Validators.required],
      studentRegistrationId: [0],
      registerDateTime: [new Date()],
      wasAnswered: [false],
      wasCanceled: [false],
      scheduledToDate: [null, Validators.required],
      note: ['']
    });
  }

  ngOnInit(): void {
    this.loadStudents();
    this.updateForm();
  }

  private loadStudents() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRegistration");
    queryBuilder.AddColumn("id").AddColumn("code").AddEntity("user").AddColumn("name");

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.studentList = result.items;
    });
  }

  private updateForm() {
    this.formEntity.patchValue({
      id: 0,
      studentRegistration: this.student,
      studentRegistrationId: this.student?.id,
      registerDateTime: new Date(),
      wasAnswered: false,
      wasCanceled: false,
      scheduledToDate: null,
      note: ''
    });
  }

  public handleEvent(clickInfo) {
    if (this.dateAdded && clickInfo.event.id === this.dateAdded.id) {
      clickInfo.event.remove();
      this.dateAdded = null;
      this.formEntity.get('scheduledToDate').patchValue(null);
    }
  }

  public addEvent(selectInfo) {
    const calendarApi = selectInfo.view.calendar;
    let student = this.formEntity.get('studentRegistration').value;

    this.message = null;
    if (!student) {
      this.message = "?? necess??rio selecionar um aluno antes de inserir uma data.";
      calendarApi.unselect();
      return;
    } else if (this.dateAdded != null) {
      this.message = "S?? ?? possivel selecionar uma data por agendamento.";
      calendarApi.unselect();
      return;
    }

    if (new Date(selectInfo.startStr) < new Date()) {
      this.message = "N??o ?? possivel adicionar uma data anterior a atual.";
      calendarApi.unselect();
      return;
    }

    let newEvent = {
      id: student.id.toString(),
      title: `${student.user.name}`,
      start: selectInfo.startStr,
      end: selectInfo.endStr
    };

    calendarApi.unselect();
    calendarApi.addEvent(newEvent);
    this.dateAdded = newEvent;
    this.formEntity.get('scheduledToDate').patchValue(newEvent.start);
  }

  public save() {
    if (this.formEntity.valid) {
      let entity = this.formEntity.value;
      entity.studentRegistrationId = entity.studentRegistration.id;

      if (entity.scheduledToDate != null) {
        entity.scheduledToDate = this.dateAdded.start;

        this.apiService.SendToAPI("PhysicalAssesmentScheduled", "Save", entity).subscribe(result => {
          if (result.HasError === false) {
            this.formEntity.markAsPristine();
            this.router.navigate(['assessment/scheduleds']);
          }
        });
      }
    }
  }

  public cancel() {
    this.router.navigate(['assessment/scheduleds']);
  }
}