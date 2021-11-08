import { NumberMatchTypeEnum } from './../../../services/query-builder/enums';
import { formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api-service/api.service';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { User } from '../../student/student-form/student-form.component';
import Swal from 'sweetalert2';
import { IFormBase } from 'src/app/models/CrudBase';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.scss']
})
export class EmployeeFormComponent implements OnInit, IFormBase {
  public isNew: boolean = true;
  public formEntity: FormGroup;
  public employee: Employee = new Employee();
  public shifts: Shift[];
  public levels: any[];
  public street: string = "";
  public dateNow: string = formatDate(new Date(), "dd", 'pt-br');

  constructor(private apiService: ApiService, private router: Router, private activatedRoute: ActivatedRoute, private formBuilder: FormBuilder, private http: HttpClient) {
    this.employee.user = new User();
    this.loadShifts();
    this.loadUserLevels();

    this.activatedRoute.params.subscribe((result) => {
      if (result && result.id) {
        this.isNew = false;
        this.loadEmployee(parseInt(result.id));
      }
    });
  }

  ngOnInit(): void {
    this.formEntity = this.formBuilder.group({
      id: [this.employee.id],
      user: this.formBuilder.group({
        id: [this.employee.user.id],
        name: [this.employee.user.name, Validators.required],
        lastName: [this.employee.user.lastName, Validators.required],
        birthday: [this.employee.user.birthday],
        email: [this.employee.user.email, Validators.required],
        genre: [this.employee.user.genre, Validators.required],
        phone: [this.employee.user.phone, Validators.required],
        registerCode: [this.employee.user.registerCode, Validators.required],
        contactPhone: [this.employee.user.contactPhone],
        addressCode: [this.employee.user.addressCode, Validators.required],
        addressNumber: [this.employee.user.addressNumber],
        addressCity: [this.employee.user.addressCity],
        userProfile: this.formBuilder.group({
          userLevelId: [this.employee.user.userProfile.userLevel.id, Validators.required]
        })
      }),
      shiftId: [this.employee.shift.id, Validators.required]
    });
  }

  private loadShifts() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listShift");
    queryBuilder.AddColumn("id")
      .AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result: any) => {
      this.shifts = result.items;
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }

  private loadEmployee(id: number) {
    let queryBuilder: QueryBuilder = new QueryBuilder("listEmployee");
    let queryFilter = queryBuilder.CreateFilter();
    queryFilter.AddCondition("id", MatchTypeEnum.EQUALS, id);
    queryBuilder.AddEntity("shift").AddColumn("id").AddColumn("description");
    queryBuilder.AddColumn("id")
      .AddEntity("user")
      .AddColumn("id")
      .AddColumn("name")
      .AddColumn("lastName")
      .AddColumn("registerCode")
      .AddColumn("birthday")
      .AddColumn("email")
      .AddColumn("phone")
      .AddColumn("genre")
      .AddColumn("addressCode")
      .AddColumn("contactPhone")
      .AddColumn("addressNumber")
      .AddColumn("addressCity")
      .AddEntity("userProfile")
      .AddEntity("userLevel")
      .AddColumn("id");

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result) => {
      console.log(result)
      this.employee = result.items[0];
      this.updateForm();
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }

  private updateForm() {
    this.formEntity.patchValue({
      id: this.employee.id,
      user: {
        id: this.employee.user.id,
        name: this.employee.user.name,
        lastName: this.employee.user.lastName,
        birthday: formatDate(this.employee.user.birthday, 'yyyy-MM-dd', 'pt-BR'),
        email: this.employee.user.email,
        genre: this.employee.user.genre.toString(),
        phone: this.employee.user.phone,
        registerCode: this.employee.user.registerCode,
        contactPhone: this.employee.user.contactPhone,
        addressCode: this.employee.user.addressCode,
        addressNumber: this.employee.user.addressNumber,
        addressCity: this.employee.user.addressCity
      },
      shiftId: this.employee.shift.id
    });
  }

  private loadUserLevels() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listUserLevel");
    queryBuilder.AddColumn("id").AddColumn("description");
    queryBuilder.CreateFilter().AddCondition("id", NumberMatchTypeEnum.LESS_THAN, 50);

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result) => {
      this.levels = result.items;
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }

  public save() {
    let entity = this.formEntity.value;
    console.log(entity)
    
    this.apiService.SendToAPI("Employee", "Save", entity).subscribe((result: any) => {
      if (result.HasError == false) {
        this.formEntity.markAsPristine();
        this.router.navigate(['employees']);
      }
    }, (err) => {
      console.error(err);
    });
  }

  public cancel() {
    this.router.navigate(['employees']);
  }

  public fieldIsRequired(fieldName: string) {
    let field = this.formEntity.get(fieldName);

    if (field) {
      const validator = field.validator({} as AbstractControl);

      return validator && validator.required
    }

    return false;
  }

  public searchAddress() {
    let cep = this.formEntity.get('user.addressCode').value;

    this.http.get(`https://viacep.com.br/ws/${cep}/json/`).subscribe((result: any) => {
      if (result) {
        this.formEntity.get('user.addressCity').patchValue(result.localidade)
        this.street = result.logradouro;
      }
    });
  }
}

export class Employee {
  id: number = 0;
  user: User = new User();
  shift: Shift = new Shift();
}

export class Shift {
  id: number = 0;
  description: string;
}