import { FormBuilder, FormArray, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/services/api-service/api.service';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { formatDate, Time } from '@angular/common';
import { HttpClient } from '@angular/common/http'
import * as moment from 'moment';
import 'moment/locale/pt-br';
import { Modality } from '../../modality/modality-form/modality-form.component';
import Swal from 'sweetalert2';
import { IFormBase } from 'src/app/models/CrudBase';
import { formatTime } from 'src/app/models/Converters';

@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.scss']
})
export class StudentFormComponent implements OnInit, IFormBase {
  public isNew: boolean = true;
  public formEntity: FormGroup;
  public student: Student = new Student();
  public street: string = "";
  public modalityClasses: Array<ModalityClass> = [];
  public selectedModalities;
  public registered: number[] = [];
  public dateNow: string = formatDate(new Date(), "dd", 'pt-br');

  constructor(private apiService: ApiService, private router: Router, private activatedRoute: ActivatedRoute, private formBuilder: FormBuilder, private http: HttpClient) {
    this.student.user = new User();
    this.loadModalities();

    this.activatedRoute.params.subscribe((result) => {
      if (result && result.id) {
        this.isNew = false;
        this.loadStudent(parseInt(result.id));
      }
    });
  }

  ngOnInit(): void {
    this.formEntity = this.formBuilder.group({
      id: [this.student.id],
      user: this.formBuilder.group({
        id: [this.student.user.id],
        name: [this.student.user.name, Validators.required],
        lastName: [this.student.user.lastName, Validators.required],
        birthday: [this.student.user.birthday],
        email: [this.student.user.email, Validators.required],
        genre: [this.student.user.genre, Validators.required],
        phone: [this.student.user.phone, Validators.required],
        registerCode: [this.student.user.registerCode, Validators.required],
        contactPhone: [this.student.user.contactPhone],
        addressCode: [this.student.user.addressCode, Validators.required],
        addressNumber: [this.student.user.addressNumber],
        addressCity: [this.student.user.addressCity]
      }),
      registrationModalityClasses: this.formBuilder.array([], Validators.required)
    });
  }

  private loadModalities() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listModalityClass");
    queryBuilder.AddColumn("startTime");
    queryBuilder.AddColumn("endTime");
    queryBuilder.AddEntity("instructor")
      .AddEntity("user")
      .AddColumn("name");
    queryBuilder.AddColumn("id")
      .AddColumn("totalVacancies")
      .AddColumn("totalActiveMembers")
      .AddEntity("modality")
      .AddColumn("id")
      .AddColumn("description")
      .AddColumn("price")
      .AddColumn("daysInWeek");
    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result: any) => {
      this.modalityClasses = result.items;
      this.modalityClasses = this.modalityClasses.map(classe => {
        let modifiedClass = Object.assign({}, classe);
        modifiedClass.startTime = formatTime(classe.startTime);
        modifiedClass.endTime = formatTime(classe.endTime);
        return modifiedClass;
      })
      console.log(result)
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }
  private loadStudent(id: number) {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRegistration");
    let queryFilter = queryBuilder.CreateFilter();
    queryFilter.AddCondition("id", MatchTypeEnum.EQUALS, id);
    queryBuilder.AddColumn("id")
      .AddColumn("code")
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
      .AddColumn("addressCity");

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result) => {
      console.log(result)
      this.student = result.items[0];
      this.updateForm();
    },
      (err) => {
        console.error(err);
        this.router.navigate(['dashboard']);
      });
  }


  get registrationModalityClasses(): FormArray {
    return this.formEntity.get('registrationModalityClasses') as FormArray;
  }

  private updateForm() {
    this.formEntity.patchValue({
      id: this.student.id,
      user: {
        id: this.student.user.id,
        name: this.student.user.name,
        lastName: this.student.user.lastName,
        birthday: formatDate(this.student.user.birthday, 'yyyy-MM-dd', 'en'),
        email: this.student.user.email,
        genre: this.student.user.genre.toString(),
        phone: this.student.user.phone,
        registerCode: this.student.user.registerCode,
        contactPhone: this.student.user.contactPhone,
        addressCode: this.student.user.addressCode,
        addressNumber: this.student.user.addressNumber,
        addressCity: this.student.user.addressCity
      },
    });
  }

  public save() {
    if (this.formEntity.valid) {
      let entity = this.formEntity.value;

      this.apiService.SendToAPI("StudentRegistration", "SaveStudent", entity).subscribe((result: any) => {
        if (result.HasError == false) {
          this.formEntity.markAsPristine();
          if (this.isNew) {
            this.router.navigate(['payment/invoices'], {
              state: {
                studentName: result.Result.User.Name
              }
            });
          } else {
            this.router.navigate(['student']);
          }
        }
      }, (err) => {
        console.error(err);
      });
    }
  }

  public cancel() {
    this.router.navigate(['student']);
  }

  public addRegistrationModality(modalityClass) {
    this.registrationModalityClasses.push(
      this.formBuilder.group({
        id: 0,
        modalityClass: modalityClass,
        modalityClassId: modalityClass.id,
        isValid: false,
        dueDay: 5,
        groupEnumerator: 0,
        modalityPaymentTypeId: 1
      })
    );

    this.registered.push(modalityClass.id);
  }

  public removeRegistrationModality(index: number) {
    this.registered.splice(this.registered.indexOf(this.registrationModalityClasses.at(index).value.modalityClass.id), 1);
    this.registrationModalityClasses.removeAt(index);
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
    //TODO: Transformar o valor do cep em numeros sequenciais sem pontuação.
    this.http.get(`https://viacep.com.br/ws/${cep}/json/`).subscribe((result: any) => {
      if (result) {
        this.formEntity.get('user.addressCity').patchValue(result.localidade)
        this.street = result.logradouro;
      }
    });
  }
}

export class Student {
  id: number = 0;
  actualTrainingPlanId: number = 0;
  code: string = "";
  user: User = new User();
  registrationModalityClasses: RegistrationModalityClass[] = []
  physicalAssesments: any[] = [];
}

export class User {
  public id: number = 0;
  public name: string = '';
  public lastName: string = '';
  public registerCode: string = '';
  public birthday: string = '';
  public email: string = '';
  public genre: boolean = false;
  public phone: string = '';
  public companyUnitId?: number;
  public companyUnit?: any;
  public contactPhone: string = '';
  public addressCode: string = '';
  public addressNumber: string = '';
  public addressCity: string = '';
  public userProfile: UserProfile = new UserProfile();
}

export class UserProfile {
  public id: number = 0;
  public login: string = '';
  public userId: number = 0;
  public userLevelId: number = 0;
  public userLevel: UserLevel = new UserLevel();
  public userStateId: number = 0;
}

export class UserLevel {
  public id: number = 0;
  public description: string = '';
}

export class ModalityClass {
  public id: number = 0;
  public startTime: string;
  public endTime: string;
  public totalVacancies: number;
  public totalActiveMembers: number;
  public instructor: User;
  public modality: Modality;
}

export class RegistrationModalityClass {
  public id: number;
  public studentRegistration: Student;
  public modalityClassId: number;
  public modalityClass: ModalityClass;
  public registerDateTime: Date;
  public isValid: boolean;
  public dueDay: number;
  public modalityPaymentTypeId: number;
}