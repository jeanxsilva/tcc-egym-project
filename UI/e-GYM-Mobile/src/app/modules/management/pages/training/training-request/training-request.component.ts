import { AuthService } from './../../../../../services/auth-service.ts/auth-service.service';
import { ApiService } from './../../../../../services/api-service/api.service';
import { ModalController } from '@ionic/angular';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';

@Component({
  selector: 'app-training-request',
  templateUrl: './training-request.component.html',
  styleUrls: ['./training-request.component.scss'],
})
export class TrainingRequestComponent implements OnInit {
  public formEntity: FormGroup;
  public userId: number;
  public student: any;

  constructor(private formBuilder: FormBuilder, private modalController: ModalController, private apiService: ApiService, private authService: AuthService) {
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile.User.Id;

      this.loadStudent();
    });

    this.formEntity = this.formBuilder.group({
      id: [0],
      studentId: [],
      note: [''],
    });
  }

  ngOnInit() { }

  private updateForm() {
    this.formEntity.patchValue({
      id: 0,
      studentId: this.student.id,
      note: '',
    })
  }

  private loadStudent() {
    let queryBuilder: QueryBuilder = new QueryBuilder("loadStudentRegistration");
    queryBuilder.CreateFilter().AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id");

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.student = result.items[0]; 
      
      this.updateForm();
    });
  }

  public dismiss() {
    this.modalController.dismiss({
      'dismissed': true
    });
  }

  public save() {
    let entity = this.formEntity.value;

    this.apiService.SendToAPI("StudentRequest", "RequestChangeTraining", entity).subscribe(result => {
      console.log(result);
    });
  }
}