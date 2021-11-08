import { PaymentReversalStatusEnum } from './../../../models/Enums';
import { ReversalPaymentScreenComponent } from './../reversal-payment-screen/reversal-payment-screen.component';
import { ApiService } from 'src/app/services/api-service/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { User } from '../../student/student-form/student-form.component';
import { CompanyUnit } from '../../modality-class/modality-class-form/modality-class-form.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';

@Component({
  selector: 'app-payment-details',
  templateUrl: './payment-details.component.html',
  styleUrls: ['./payment-details.component.scss']
})
export class PaymentDetailsComponent implements OnInit {
  public paymentId: number = 0;
  public payment: Payment = new Payment();

  constructor(private activatedRoute: ActivatedRoute, private apiService: ApiService, private router: Router, private ngbModal: NgbModal) {
    this.activatedRoute.params.subscribe(result => {
      if (result && result.id) {
        this.paymentId = parseInt(result.id);
        this.loadPayment();
      }
    });
  }

  ngOnInit(): void {
  }

  get canReversal(): boolean {
    let daysInMinutes = (1440) * 8;
    let dateNow: Date = new Date();
    let canUntil: Date = new Date(new Date(this.payment.paymentDateTime).setMinutes(daysInMinutes));
    let can = (this.payment.paymentReversals.length == 0 && canUntil > dateNow) && this.payment.isValid;

    return can;
  }

  get paymentReversalStatusEnum(): typeof PaymentReversalStatusEnum {
    return PaymentReversalStatusEnum;
  }

  public seeReversalDetails() {
    if (this.payment.paymentReversals.length != 0) {
      let reversal = this.payment.paymentReversals[0];
      this.router.navigate([`payment/reversal/${reversal.id}`]);
    }
  }

  public seeInvoiceDetails() {
    if (this.payment.invoice != null) {
      this.router.navigate([`payment/invoice/${this.payment.invoice.id}`]);
    }
  }

  private loadPayment() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPayment");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.paymentId);
    queryBuilder.AddColumn("id").AddColumn("paymentDateTime").AddColumn("isValid");
    let invoiceBuilder = queryBuilder.AddEntity("invoice").AddColumn("id")
      .AddColumn("totalValue")
      .AddColumn("referentToDate")
      .AddColumn("dueDate")
      .AddColumn("note")
      .AddColumn("isByRequest");

    let studentRequestBuilder = invoiceBuilder.AddEntity("studentRequests").AddColumn("id").AddColumn("note");
    studentRequestBuilder.AddEntity("requestCategory").AddColumn("id").AddColumn("description");
    studentRequestBuilder.AddEntity("closedByUser").AddColumn("id").AddColumn("name");

    invoiceBuilder.AddEntity("invoiceDetails")
      .AddColumn("id")
      .AddColumn("description")
      .AddColumn("price");
    queryBuilder.AddEntity("paymentType").AddColumn("id").AddColumn("description");
    queryBuilder.AddEntity("paidByUser").AddColumn("id").AddColumn("name");
    queryBuilder.AddEntity("receivedByUser").AddColumn("id").AddColumn("name");
    queryBuilder.AddEntity("paymentReversals").AddColumn("id")
      .AddColumn("reason")
      .AddColumn("lastModifiedDateTime")
      .AddEntity("paymentReversalStatus").AddColumn("id").AddColumn("description");;

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.payment = result.items[0];
    });
  }

  public openReversalModal() {
    let modal = this.ngbModal.open(ReversalPaymentScreenComponent, { centered: true, size: 'md' });
    modal.componentInstance['paymentId'] = this.paymentId;
    modal.result.then(result => {
      if (result && result === true) {
        this.router.navigate(['payment/reversals']);
      }
    });
  }
}

export class Payment {
  public id: number = 0;
  public isValid: boolean;
  public paymentTypeId: number = 0;
  public paymentDateTime?;
  public invoiceId: number = 0;
  public paidByUserId: number = 0;
  public companyUnitId: number = 0;
  public receivedByUserId: number = 0;
  public companyUnit?: CompanyUnit = new CompanyUnit();
  public invoice?;
  public paidByUser?: User = new User();
  public paymentType?;
  public receivedByUser?: User = new User();
  public paymentReversals?;
}