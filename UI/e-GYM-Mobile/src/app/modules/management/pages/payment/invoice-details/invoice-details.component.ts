import { AlertController } from '@ionic/angular';
import { ApiService } from './../../../../../services/api-service/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { InvoiceStatusEnum, PaymentReversalStatusEnum } from 'src/app/models/Enums';

@Component({
  selector: 'app-invoice-details',
  templateUrl: './invoice-details.component.html',
  styleUrls: ['./invoice-details.component.scss'],
})
export class InvoiceDetailsComponent implements OnInit {
  public invoiceId: number;
  public invoice: any;

  constructor(private activatedRoute: ActivatedRoute, private apiService: ApiService, private alertController: AlertController, private router: Router) {
    this.activatedRoute.params.subscribe(param => {
      this.invoiceId = parseInt(param.id);

      this.loadInvoice();
    });
  }

  ngOnInit() { }

  get InvoiceStatusEnum(): typeof InvoiceStatusEnum {
    return InvoiceStatusEnum;
  }

  get canReversal(): boolean {
    if (this.invoice && this.invoice.payments && this.invoice.payments.length > 0) {
      let daysInMinutes = (1440) * 8;
      let dateNow: Date = new Date();
      let canUntil: Date = new Date(new Date(this.invoice.payments[0].paymentDateTime).setMinutes(daysInMinutes));
      let can = (this.hasReversalCanceled && canUntil > dateNow) && this.invoice.payments[0].isValid;

      return can;
    }

    return false;
  }

  get hasReversalCanceled() {
    let can = false;

    if (this.invoice.payments[0].paymentReversals.length == 0) {
      can = true;
    }
    else {
      this.invoice.payments[0].paymentReversals.forEach(reversal => {
        can = reversal.paymentReversalStatus.id == PaymentReversalStatusEnum.Canceled;
      });
    }

    return can;
  }

  private loadInvoice() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listInvoice");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.invoiceId);
    queryBuilder.AddColumn("id")
      .AddColumn("totalValue")
      .AddColumn("referentToDate")
      .AddColumn("dueDate")
      .AddColumn("note")
      .AddEntity("invoiceStatus")
      .AddColumn("id")
      .AddColumn("description");
    queryBuilder.AddEntity("invoiceDetails")
      .AddColumn("id")
      .AddColumn("description")
      .AddColumn("price")
    let paymentBuilder = queryBuilder.AddEntity("payments")
      .AddColumn("id")
      .AddColumn("isValid")
      .AddColumn("paymentDateTime");
    paymentBuilder.AddEntity("paymentReversals").AddColumn("id").AddEntity("paymentReversalStatus").AddColumn("id");
    paymentBuilder.AddEntity("paymentType").AddColumn("id").AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.invoice = result.items[0];
    })
  }

  public requestReversal(paymentId, reason) {
    let entity = {
      paymentId: paymentId,
      reason: reason
    };

    this.apiService.SendToAPI("StudentRequest", "RequestReversalPayment", entity).subscribe(result => {
      this.router.navigate(['requests']);
      console.log(result);
    });
  }

  async presentAlertConfirm(payment) {
    const alert = await this.alertController.create({
      cssClass: '',
      header: 'Solicitar estorno',
      message: 'Deseja mesmo efetuar o pedido de estorno?',
      inputs: [
        {
          name: 'reason',
          placeholder: 'Breve explicação do motivo',
          type: 'text'
        }
      ],

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
          handler: (data: any) => {
            this.requestReversal(payment.id, data.reason);
          }
        }
      ]
    });

    await alert.present();
  }
}