import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PaymentReversalStatusEnum } from 'src/app/models/Enums';
import { ApiService } from 'src/app/services/api-service/api.service';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { User } from '../../student/student-form/student-form.component';
import { Payment } from '../payment-details/payment-details.component';
import { PaymentMovementFormComponent } from '../payment-movement-form/payment-movement-form.component';

@Component({
  selector: 'app-reversal-payment-details',
  templateUrl: './reversal-payment-details.component.html',
  styleUrls: ['./reversal-payment-details.component.scss']
})
export class ReversalPaymentDetailsComponent implements OnInit {
  public reversalId: number = 0;
  public reversal: any;

  constructor(private activatedRoute: ActivatedRoute, private apiService: ApiService, private router: Router, private ngbModal: NgbModal) {
    this.activatedRoute.params.subscribe(result => {
      if (result && result.id) {
        this.reversalId = parseInt(result.id);
        this.loadReversal();
      }
    });
  }

  ngOnInit(): void {
  }

  get paymentReversalStatusEnum(): typeof PaymentReversalStatusEnum {
    return PaymentReversalStatusEnum;
  }

  public loadReversal() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPaymentReversal");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.reversalId);
    queryBuilder.AddColumn("id")
      .AddColumn("reason")
      .AddColumn("lastModifiedDateTime")
    queryBuilder.AddEntity("authorizedByUser").AddColumn("id").AddColumn("name")
    queryBuilder.AddEntity("createdByUser").AddColumn("id").AddColumn("name")
    queryBuilder.AddEntity("finishedByUser").AddColumn("id").AddColumn("name")
    queryBuilder.AddEntity("payment").AddColumn("id").AddColumn("paymentDateTime").AddEntity("paidByUser").AddColumn("id").AddColumn("name");
    queryBuilder.AddEntity("paymentReversalStatus").AddColumn("id").AddColumn("description");
    let movementBuilder = queryBuilder.AddEntity("paymentMovements").AddColumn("id").AddColumn("registerDateTime").AddColumn("isCurrent").AddColumn("note")
    movementBuilder.AddEntity("paymentReversalStatus").AddColumn("id").AddColumn("description")
    movementBuilder.AddEntity("registeredByUser").AddColumn("id").AddColumn("name");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.reversal = result.items[0];
    });
  }

  public seePaymentDetails() {
    this.router.navigate([`payment/${this.reversal.payment.id}`]);
  }

  public openModal(reason?) {
    let modal = this.ngbModal.open(PaymentMovementFormComponent, { centered: true, size: 'md' });
    modal.componentInstance['paymentReversalId'] = this.reversalId;
    modal.result.then(result => {
      if(result && result === true){
        this.router.navigate(['payment/reversals']);
      }
    });
  }
}

export class PaymentReversal {
  public id: number;
  public reason: string;
  public authorizedByUserId: number;
  public createdByUserId: number;
  public finishedByUserId: number;
  public paymentId: number;
  public paymentReversalStatusId: number;
  public lastModifiedDateTime: Date;
  public authorizedByUser: User = new User();
  public createdByUser: User = new User();
  public finishedByUser: User = new User();
  public payment: Payment = new Payment();
  public paymentReversalStatus: PaymentReversalStatus;
}

export class PaymentReversalStatus {
  public id: number;
  public description: string;
}