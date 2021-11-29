import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { QueryBuilder } from './../../../../services/query-builder/query-builder';
import { ApiService } from 'src/app/services/api-service/api.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { AlertController } from '@ionic/angular';
import { PaymentReversalStatusEnum } from 'src/app/models/Enums';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss'],
})
export class PaymentComponent implements OnInit {
  public paymentReversals: any[] = [];
  public payments: any[] = [];
  public userId: number;

  constructor(private router: Router, private apiService: ApiService, private authService: AuthService, private alertController: AlertController) {
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile.User.Id;

      this.loadPayments();
      this.loadReversals();
    });
  }

  ngOnInit() { }

  get PaymentReversalStatusEnum(): typeof PaymentReversalStatusEnum {
    return PaymentReversalStatusEnum;
  }

  private loadPayments() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPayment");
    queryBuilder.CreateFilter().AddEntity("invoice").AddEntity("student").AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);

    queryBuilder.AddColumn("id")
      .AddColumn("isValid")
      .AddColumn("paymentDateTime")
      .AddEntity("paymentType").AddColumn("description");
    queryBuilder.AddEntity("invoice").AddColumn("id");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.payments = result.items;
    });
  }

  private loadReversals() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPaymentReversal");
    let queryFilter = queryBuilder.CreateFilter();
    queryFilter.AddEntity("payment").AddEntity("invoice").AddEntity("student").AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryFilter.AddEntity("paymentReversalStatus").AddCondition("id", MatchTypeEnum.EQUALS, PaymentReversalStatusEnum.Opened);
    queryBuilder.AddColumn("id").AddColumn("reason").AddColumn("lastModifiedDateTime").AddEntity("paymentReversalStatus").AddColumn("id").AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.paymentReversals = result.items;
    });
  }

  public cancelReversal(paymentReversalId: number) {
    this.apiService.SendToAPI("PaymentReversal", "CancelReversal", paymentReversalId).subscribe(result => {
      console.log(result);
    });
  }

  public seeDetails(invoiceId: number) {
    this.router.navigate(['invoice', invoiceId]);
  }

  async presentAlertConfirm(paymentReversal) {
    const alert = await this.alertController.create({
      cssClass: '',
      header: 'Cancelar estorno',
      message: 'Deseja mesmo cancelar o pedido de estorno?',
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
            this.cancelReversal(paymentReversal.id);
          }
        }
      ]
    });

    await alert.present();
  }
}