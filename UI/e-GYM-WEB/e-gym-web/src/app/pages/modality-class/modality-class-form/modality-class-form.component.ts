import { MatchTypeEnum } from './../../../services/query-builder/enums';
import { Employee } from './../../employee/employee-form/employee-form.component';
import { User } from './../../student/student-form/student-form.component';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApiService } from 'src/app/services/api-service/api.service';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { Modality } from '../../modality/modality-form/modality-form.component';

@Component({
  selector: 'app-modality-class-form',
  templateUrl: './modality-class-form.component.html',
  styleUrls: ['./modality-class-form.component.scss']
})
export class ModalityClassFormComponent implements OnInit {
  public isNew: boolean = true;
  public formModalityClass: FormGroup;
  public modalities: Modality[];
  public instructors: Employee[];
  public modalityClassId: number;
  public modalityClass: ModalityClass = new ModalityClass();

  constructor(private formBuilder: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute, private apiService: ApiService) {
    this.activatedRoute.params.subscribe(result => {
      if (result && result.id) {
        this.modalityClassId = result.id;
        this.isNew = false;

        this.loadModalityClass();
      }
    });

    this.formModalityClass = this.formBuilder.group({
      id: [0],
      modalityId: [null, Validators.required],
      startTime: [null, Validators.required],
      endTime: [null, Validators.required],
      instructorId: [null, Validators.required],
      totalVacancies: [null, Validators.required],
      totalActiveMembers: [0],
      companyUnitId: [0]
    });
  }

  ngOnInit(): void {
    this.loadInstructors();
    this.loadModalities();

    this.updateForm();
  }

  public updateForm() {
    this.formModalityClass.patchValue({
      id: this.modalityClass.id,
      modalityId: this.modalityClass.modality.id,
      startTime: this.modalityClass.startTime,
      endTime: this.modalityClass.endTime,
      instructorId: this.modalityClass.instructor.id,
      totalVacancies: this.modalityClass.totalVacancies,
      totalActiveMembers: this.modalityClass.totalActiveMembers,
      companyUnitId: this.modalityClass.companyUnit.id
    });
  }
  
  public loadModalityClass() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listModalityClass");
    queryBuilder.AddColumn("id").AddColumn("startTime").AddColumn("endTime").AddColumn("totalVacancies").AddColumn("totalActiveMembers");
    queryBuilder.AddEntity("modality").AddColumn("id");
    queryBuilder.AddEntity("instructor").AddColumn("id");
    queryBuilder.AddEntity("companyUnit").AddColumn("id");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.modalityClassId);
    
    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      console.log(result);
      this.modalityClass = result.items[0];
    }, err => {
      console.error(err);
    });
  }
  
  public loadInstructors() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listEmployee");
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
    this.router.navigate(['modality/classes']);
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