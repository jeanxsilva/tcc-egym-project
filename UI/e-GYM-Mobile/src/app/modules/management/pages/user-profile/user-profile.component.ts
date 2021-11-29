import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApiService } from './../../../../services/api-service/api.service';
import { QueryBuilder } from './../../../../services/query-builder/query-builder';
import { AuthService } from './../../../../services/auth-service.ts/auth-service.service';
import { Component, OnInit } from '@angular/core';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ToastController } from '@ionic/angular';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
})
export class UserProfileComponent implements OnInit {
  public userId: number;
  public user: any;
  public isEditing: boolean = false;
  public formEntity: FormGroup;
  public address: string;

  constructor(private authService: AuthService, private apiService: ApiService, private formBuilder: FormBuilder, private httpClient: HttpClient, private toastController: ToastController) {
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile?.User?.Id;

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

  private loadUser() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listUser");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id")
      .AddColumn("registerCode")
      .AddColumn("name")
      .AddColumn("lastName")
      .AddColumn("birthday")
      .AddColumn("email")
      .AddColumn("phone")
      .AddColumn("genre")
      .AddColumn("addressCode")
      .AddColumn("contactPhone")
      .AddColumn("addressNumber")
      .AddColumn("addressCity").AddEntity("companyUnit").AddColumn("id");

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      if (result && result.items) {
        this.user = result.items[0];

        this.updateForm();
        this.getAddress();
      }
    });
  }

  public async getAddress() {
    var re = /^([\d]{8})/;

    if (re.test(this.formEntity.get('addressCode').value)) {
      this.formEntity.get('addressCode').setValue(this.formEntity.get('addressCode').value.replace(re, "$1"));
    } else {
      console.log(this.formEntity.get('addressCode').value)
      const toast = await this.toastController.create({
        message: 'CEP inválido!',
        position: 'bottom',
        color: 'danger',
        duration: 1000
      });

      await toast.present();
      return;
    }
    let cep = this.formEntity.get('addressCode').value;

    //TODO: Transformar o valor do cep em numeros sequenciais sem pontuação.
    this.httpClient.get(`https://viacep.com.br/ws/${cep}/json/`).subscribe((result: any) => {
      if (result) {
        this.address = result.logradouro;
      }
    });
  }

  public save() {
    let entity = this.formEntity.value;
    console.log(entity);
    this.apiService.SendToAPI("User", "Save", entity).subscribe(result => {
      console.log(result);
      if (result && result.HasError == false) {
        this.isEditing = false;
        this.loadUser();
      }
    }, err => console.error(err));
  }

  public edit() {
    this.isEditing = !this.isEditing;
  }
}