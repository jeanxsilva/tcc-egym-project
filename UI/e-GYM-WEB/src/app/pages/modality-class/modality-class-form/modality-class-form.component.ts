import { MatchTypeEnum } from './../../../services/query-builder/enums';
import { Employee } from './../../employee/employee-form/employee-form.component';
import { User } from './../../student/student-form/student-form.component';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApiService } from 'src/app/services/api-service/api.service';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { Modality } from '../../modality/modality-form/modality-form.component';
import { formatDate } from '@angular/common';
import { formatTime } from 'src/app/models/Converters';
import Swal from 'sweetalert2';
import { IFormBase } from 'src/app/models/CrudBase';

@Component({
  selector: 'app-modality-class-form',
  templateUrl: './modality-class-form.component.html',
  styleUrls: ['./modality-class-form.component.scss']
})
export class ModalityClassFormComponent implements OnInit, IFormBase {
  public isNew: boolean = true;
  public formEntity: FormGroup;
  public modalities: Modality[];
  public instructors: Employee[];
  public modalityClassId: number;
  public modalityClass: ModalityClass = new ModalityClass();

  constructor(private formBuilder: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute, private apiService: ApiService) {
    this.activatedRoute.params.subscribe(result => {
      if (result && result.id) {
        this.modalityClassId = parseInt(result.id);
        this.isNew = false;

        this.loadModalityClass();
      }
    });

    this.formEntity = this.formBuilder.group({
      id: [0],
      modalityId: [null, Validators.required],
      startTime: [null, Validators.required],
      endTime: [null, Validators.required],
      instructorId: [null],
      totalVacancies: [null, [Validators.required]],
      totalActiveMembers: [0],
      companyUnitId: [0]
    });
  }

  ngOnInit(): void {
    this.loadInstructors();
    this.loadModalities();
  }

  public updateForm() {
    this.formEntity.patchValue({
      id: this.modalityClass.id,
      modalityId: this.modalityClass.modality.id,
      startTime: formatTime(this.modalityClass.startTime),
      endTime: formatTime(this.modalityClass.endTime),
      instructorId: this.modalityClass.instructor.id,
      totalVacancies: this.modalityClass.totalVacancies,
      totalActiveMembers: this.modalityClass.totalActiveMembers,
      companyUnitId: this.modalityClass.companyUnit.id
    });
  }

  public loadModalityClass() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listModalityClass");
    queryBuilder.AddColumn("id").AddColumn("startTime").AddColumn("endTime").AddColumn("totalVacancies").AddColumn("totalActiveMembers");
    queryBuilder.AddEntity("modality").AddColumn("id").AddColumn("description");
    queryBuilder.AddEntity("instructor").AddColumn("id");
    queryBuilder.AddEntity("companyUnit").AddColumn("id");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.modalityClassId);

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      console.log(result);
      this.modalityClass = result.items[0];
      // console.log(formatDate("PT23H", "hh:mm:ss", "pt-BR"))
      this.updateForm();
    }, err => {
      console.error(err);
    });
  }

  public loadInstructors() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listEmployee");
    queryBuilder.CreateFilter().AddEntity("user").AddEntity("userProfile").AddEntity("userLevel").AddCondition("id", MatchTypeEnum.EQUALS, 1);
    queryBuilder.AddColumn("id").AddEntity("user").AddColumn("name");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      console.log(result);
      this.instructors = result.items;
    }, err => {
      console.error(err);
    });
  }

  public loadModalities() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listModality");
    queryBuilder.AddColumn("id").AddColumn("description");
    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      console.log(result);
      this.modalities = result.items;
    }, err => {
      console.error(err);
    });
  }

  save() {
    if (this.formEntity.valid) {
      let entity = this.formEntity.value;
      this.apiService.SendToAPI("ModalityClass", "Save", entity).subscribe((result: any) => {
        console.log(result);
        if (result.HasError == false) {
          this.formEntity.markAsPristine();
          this.router.navigate(['modality/classes']);
        }
      }, err => {
        console.error(err);
      });
    }
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
          this.router.navigate(['modality/classes']);
        }
      });
    } else {
      this.router.navigate(['modality/classes']);
    }
  }
}

export class ModalityClass {
  id: number = 0;
  modalityId: number = 0;
  modality: Modality = new Modality();
  startTime: string = "";
  endTime: string = "";
  instructorId: number = 0;
  instructor: Employee = new Employee();
  totalVacancies: number = 0;
  totalActiveMembers: number = 0;
  companyUnitId: number = 0;
  companyUnit: CompanyUnit = new CompanyUnit();
}

export class CompanyUnit {
  id: number;
  description: string;
  companyId: number;
  company: Company = new Company();
  registerCode: string;
  postalCode: string;
  phone: number;
  email: string;
  userContactId: number;
  userContact: User = new User();
}

export class Company {
  id: number;
  description: string;
  registerCode: string;
}