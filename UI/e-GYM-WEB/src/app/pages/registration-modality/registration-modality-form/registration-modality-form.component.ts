import { InvoiceStatusEnum } from './../../../models/Enums';
import { ListMatchTypeEnum, SortEnum } from './../../../services/query-builder/enums';
import { Router } from '@angular/router';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ApiService } from 'src/app/services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
import { Student } from '../../student/student-form/student-form.component';
import { MatchTypeEnum, OperatorEnum } from 'src/app/services/query-builder/enums';
import { formatTime } from 'src/app/models/Converters';
import Swal from 'sweetalert2';
import { IFormBase } from 'src/app/models/CrudBase';

@Component({
  selector: 'app-registration-modality-form',
  templateUrl: './registration-modality-form.component.html',
  styleUrls: ['./registration-modality-form.component.scss']
})
export class RegistrationModalityFormComponent implements OnInit, IFormBase {
  public student: Student = null;
  public students: Student[] = new Array<Student>();
  public registrations: any = null;
  public classes: any[];
  public formEntity: FormGroup;
  public selectedRegistrations = new Set();
  public canceledRegistrations = new Set();
  public paymentTypes: any[] = [];

  constructor(private apiService: ApiService, private formBuilder: FormBuilder, private router: Router) {
    this.formEntity = this.formBuilder.group({
      id: [0],
      registrationModalityClasses: this.formBuilder.array([])
    });
  }

  ngOnInit(): void {
    this.loadStudents();
    this.loadClasses();
    this.loadPaymentType();
  }

  public isAdded(classe) {
    let addeds = Array.from(this.selectedRegistrations);
    return addeds.length > 0 && addeds.filter((o: any) => o.modalityClassId == classe.id) != null;
  }

  public undoRegistration(registration) {
    this.selectedRegistrations.delete(registration);
  }

  public setRegistration(addingClass: any) {
    let registration = {
      id: 0,
      studentRegistrationId: this.student.id,
      modalityClassId: addingClass.id,
      modalityClass: addingClass,
      isValid: false,
      dueDay: new Date().getDate(),
      modalityPaymentTypeId: 1
    };

    if (this.student) {
      if (!this.selectedRegistrations.has(registration)) {
        this.selectedRegistrations.add(registration);
      }
    }
  }

  public loadPaymentType() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listModalityPaymentType");
    queryBuilder.AddColumn("id").AddColumn("description");
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.paymentTypes = result.items;
    })
  }

  public isInRegistration(classe) {
    let isInRegistration = false;

    if (this.student) {
      if (this.registrations && this.registrations.length > 0) {
        this.registrations.forEach(registration => {
          if (registration.modalityClass.id == classe.id) {
            isInRegistration = true;
          }
        });
      }
    }

    return isInRegistration;
  }

  public cancelRegistration(cancelingRegistration) {
    console.log(cancelingRegistration);
    if (this.student) {
      if (!this.canceledRegistrations.has(cancelingRegistration)) {
        this.canceledRegistrations.add(cancelingRegistration);
      } else {
        this.canceledRegistrations.delete(cancelingRegistration);
      }
    }
  }

  public onChangeStudent() {
    this.selectedRegistrations = new Set();
    this.loadRegistrations();
  }

  private loadStudents() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRegistration");
    queryBuilder.AddColumn("id").AddColumn("code").AddEntity("user").AddColumn("id").AddColumn("name").AddColumn("lastName");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.students = result.items;
    });
  }

  get InvoiceStatusEnum() {
    return typeof InvoiceStatusEnum;
  }

  private loadRegistrations() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listRegistrationModalityClass");
    let queryFilter = queryBuilder.CreateFilter();
    queryFilter.AddOperator(OperatorEnum.OR)
      .AddCondition("isValid", MatchTypeEnum.EQUALS, true)
      .AddEntityList("invoiceDetails", ListMatchTypeEnum.SOME)
      .AddEntity("invoice")
      .AddEntity("invoiceStatus")
      .AddCondition("id", MatchTypeEnum.EQUALS, InvoiceStatusEnum.Generated) // OR have invoice generated;
    queryFilter.AddEntity("studentRegistration").AddCondition("id", MatchTypeEnum.EQUALS, this.student.id)
    queryBuilder.AddColumn("id").AddColumn("registerDateTime").AddColumn("isValid");
    queryBuilder.AddEntity("modalityPaymentType").AddColumn("id");
    queryBuilder.AddEntity("invoiceDetails")
      .AddColumn("id")
      .AddEntity("invoice")
      .AddEntity("invoiceStatus")
      .AddColumn("id")
    let classBuilder = queryBuilder.AddEntity("modalityClass").AddColumn("id");
    classBuilder.AddEntity("instructor")
      .AddEntity("user")
      .AddColumn("name");
    classBuilder.AddColumn("id")
      .AddColumn("totalVacancies")
      .AddColumn("totalActiveMembers")
      .AddColumn("startTime")
      .AddColumn("endTime")
      .AddEntity("modality")
      .AddColumn("id")
      .AddColumn("description")
      .AddColumn("price")
      .AddColumn("daysInWeek");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.registrations = result.items;
      this.registrations.map(register => {
        let lastInvoice = register.invoiceDetails.sort((a, b) => a.id - b.id);
        register.invoiceDetails = [lastInvoice];
        register.modalityClass.startTime = formatTime(register.modalityClass.startTime);
        register.modalityClass.endTime = formatTime(register.modalityClass.endTime);
      });
      console.log(this.registrations)
    });
  }

  private loadClasses() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listModalityClass");
    queryBuilder.AddColumn("id").AddEntity("instructor")
      .AddEntity("user")
      .AddColumn("name");
    queryBuilder.AddColumn("id")
      .AddColumn("totalVacancies")
      .AddColumn("totalActiveMembers")
      .AddColumn("startTime")
      .AddColumn("endTime")
      .AddEntity("modality")
      .AddColumn("id")
      .AddColumn("description")
      .AddColumn("price")
      .AddColumn("daysInWeek");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.classes = result.items;
      this.classes = this.classes.map(classeOrig => {
        let classe = Object.assign({}, classeOrig);
        classe.startTime = formatTime(classe.startTime);
        classe.endTime = formatTime(classe.endTime);
        return classe;
      });
    });
  }

  public cancel() {
    if (this.formEntity.dirty) {
      Swal.fire({
        title: 'Tem certeza que deseja cancelar as alterações?',
        text: 'Todos os dados modificados serão perdidos!',
        icon: 'warning',
        showCancelButton: true,
        customClass: {
          cancelButton: 'bg-translucent-default',
          confirmButton: 'bg-warning'
        },
        confirmButtonText: 'Sim, cancelar alterações!',
        cancelButtonText: 'Não, continuar as alterações'
      }).then((result) => {
        if (result.value) {
          this.router.navigate(['dashboard']);
        }
      });
    } else {
      this.router.navigate(['dashboard']);
    }
  }

  public save() {
    if (this.student && (this.canceledRegistrations.size != 0 || this.selectedRegistrations.size != 0)) {

      let entity = this.formEntity.value;
      entity.id = this.student.id;
      entity.registrationModalityClasses = [];

      let arrayCanceleds = [];
      Array.from(this.canceledRegistrations).forEach(item => {
        arrayCanceleds.push(Object.assign({}, item));
      });

      arrayCanceleds.forEach((item: any) => {
        let registration: any = {};
        registration.id = item.id
        registration.isValid = false;
        registration.modalityPaymentTypeId = item.modalityPaymentType.id;
        registration.studentRegistrationId = this.student.id;
        registration.modalityClassId = item.modalityClass.id;

        entity.registrationModalityClasses.push(registration);
      });

      Array.from(this.selectedRegistrations).forEach((classe: any) => {
        let newRegistration = classe

        entity.registrationModalityClasses.push(newRegistration);
      });

      console.log(entity);

      // this.apiService.SendToAPI("StudentRegistration", "ChangeRegistration", entity).subscribe((result: any) => {
      //   this.formEntity.markAsPristine();
      //   window.location.reload();
      // }, err => { console.error(err); });
    }
  }
}