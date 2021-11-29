import { RequestStatusEnum } from './../../../../models/Enums';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { AuthService } from './../../../../services/auth-service.ts/auth-service.service';
import { QueryBuilder } from './../../../../services/query-builder/query-builder';
import { ApiService } from './../../../../services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.scss'],
})
export class RequestComponent implements OnInit {
  public openedRequests: any[] = [];
  public requests: any[] = [];
  public userId: number;

  constructor(private apiService: ApiService, private authService: AuthService, private alertController: AlertController, private router: Router) {
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile.User.Id;

      this.loadRequests();
    });
  }

  ngOnInit() { }

  get RequestStatusEnum(): typeof RequestStatusEnum {
    return RequestStatusEnum;
  }

  private loadRequests() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRequest");
    queryBuilder.CreateFilter().AddEntity("student").AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id").AddColumn("registerDateTime").AddColumn("note").AddColumn("isPaid").AddColumn("wasCanceled")
    queryBuilder.AddEntity("requestStatus").AddColumn("id").AddColumn("description");
    queryBuilder.AddEntity("requestCategory").AddColumn("id").AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      if(result){
        this.openedRequests = result.items.filter(o => o.requestStatus.id == RequestStatusEnum.Opened);
        this.requests = result.items.filter(o => o.requestStatus.id != RequestStatusEnum.Opened);
      }
    });
  }

  public cancelRequest(requestId: number) {
    this.apiService.SendToAPI("StudentRequest", "CancelRequest", requestId).subscribe(result => {
      console.log(result);
      this.loadRequests();
    })
  }

  async presentAlertConfirm(request) {
    const alert = await this.alertController.create({
      cssClass: '',
      header: 'Cancelar requisição',
      message: 'Deseja mesmo cancelar a requisição?',
      buttons: [
        {
          text: 'Voltar',
          role: 'cancel',
          cssClass: 'btn-danger',
          handler: () => {
          }
        }, {
          text: 'Confirmar',
          cssClass: 'btn-success',
          handler: () => {
            this.cancelRequest(request.id);
          }
        }
      ]
    });

    await alert.present();
  }

  public seeDetails(requestId: number) {
    this.router.navigate(['request', requestId]);
  }
}