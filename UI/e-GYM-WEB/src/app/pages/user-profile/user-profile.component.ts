import { IFormBase } from './../../models/CrudBase';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { QueryBuilder } from './../../services/query-builder/query-builder';
import { Component, OnInit } from '@angular/core';
import { Ability, AbilityBuilder } from '@casl/ability';
import { ApiService } from 'src/app/services/api-service/api.service';
import { User } from '../student/student-form/student-form.component';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit, IFormBase {
  public user: User = new User();
  public userId: number = 1;
  public formEntity: FormGroup;
  public isEditing: boolean = false;
  public address: string = '';

  constructor(private apiService: ApiService, private formBuilder: FormBuilder, private http: HttpClient) { }

  cancel() {
    this.isEditing = false;
  }

  save() {
    let entity = this.formEntity.value;
    this.apiService.SendToAPI("User", "Save", entity).subscribe(result => {
      this.isEditing = false;
      console.log(result);
      this.loadUser();
    });
  }

  ngOnInit() {
    this.formEntity = this.formBuilder.group({
      id: [0],
      registerCode: ['', Validators.required],
      name: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      birthday: ['', Validators.required],
      genre: [false],
      phone: ['', Validators.required],
      contactPhone: [''],
      addressCode: ['', Validators.required],
      addressNumber: [''],
      addressCity: [''],
      companyUnitId: [''],
    });

    this.loadUser();
  }

  public edit() {
    this.isEditing = !this.isEditing;
  }

  public updateForm() {
    this.formEntity.patchValue({
      id: this.user.id,
      registerCode: this.user.registerCode,
      name: this.user.name,
      lastName: this.user.lastName,
      email: this.user.email,
      birthday: formatDate(this.user.birthday, 'yyyy-MM-dd', 'en'),
      genre: this.user.genre,
      phone: this.user.phone,
      contactPhone: this.user.contactPhone,
      addressCode: this.user.addressCode,
      addressNumber: this.user.addressNumber,
      addressCity: this.user.addressCity,
      companyUnitId: this.user.companyUnit.id,
    });
  }

  public loadUser() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listUser");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id").AddColumn("name").AddColumn("lastName")
      .AddColumn("birthday").AddColumn("genre").AddColumn("registerCode")
      .AddColumn("addressCode").AddColumn("addressNumber").AddColumn("addressCity")
      .AddColumn("email").AddColumn("phone").AddColumn("contactPhone");
    queryBuilder.AddEntity("companyUnit").AddColumn("id");
    queryBuilder.AddEntity("userProfile").AddColumn("id").AddEntity("userLevel").AddColumn("description");

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.user = result.items[0];

      this.updateForm();
      this.loadAddress();
    });
  }

  public loadAddress() {
    let cep = this.user.addressCode;

    //TODO: Transformar o valor do cep em numeros sequenciais sem pontuação.
    this.http.get(`https://viacep.com.br/ws/${cep}/json/`).subscribe((result: any) => {
      if (result) {
        this.address = result.logradouro;
      }
    });
  }
}