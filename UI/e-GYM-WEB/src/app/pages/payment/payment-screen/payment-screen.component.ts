import { ApiService } from 'src/app/services/api-service/api.service';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { PaymentTypeEnum } from 'src/app/models/Enums';

@Component({
  selector: 'app-payment-screen',
  templateUrl: './payment-screen.component.html',
  styleUrls: ['./payment-screen.component.scss']
})
export class PaymentScreenComponent implements OnInit {
  public invoice: any;
  public formPayment: FormGroup;
  public paymentTypeList: any[] = [];
  public paymentIsConfirmed: boolean = false;

  constructor(private activeModal: NgbActiveModal, private router: Router, private location: Location, private formBuilder: FormBuilder, private apiService: ApiService) {
    this.formPayment = this.formBuilder.group({
      id: [0],
      isValid: [false],
      paymentTypeId: [0, Validators.required],
      paymentDateTime: [''],
      invoiceId: [0, Validators.required],
      paidByUserId: [0],
      companyUnitId: [0],
      receivedByUserId: [0],
    });
  }

  ngOnInit(): void {
    this.loadPaymentType();
  }

  ngAfterViewInit() {
    this.updateForm();
  }

  private updateForm() {
    this.formPayment.patchValue({
      id: 0,
      isValid: false,
      paymentTypeId: 0,
      paymentDateTime: new Date(),
      invoiceId: this.invoice.id,
      paidByUserId: this.invoice.student.user.id,
      companyUnitId: 0,
      receivedByUserId: 0,
    });
  }
  private loadPaymentType() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPaymentType").AddColumn("id").AddColumn("description");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.NOT_EQUALS, PaymentTypeEnum.TICKET);
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.paymentTypeList = result.items;
    });
  }

  savePayment() {
    if (this.formPayment.valid && this.paymentIsConfirmed) {
      let entity = this.formPayment.value;

      this.apiService.SendToAPI("Payment", "Save", entity).subscribe(result => {
        if (result.HasError === false) {
          this.close(true);
        }
      }, err => {
        console.error(err);
        this.close(false);
      });
    }
  }

  public generateTicket() {
    this.paymentIsConfirmed = true;
  }

  dismiss() {
    this.activeModal.dismiss();
  }

  close(reason) {
    this.activeModal.close(reason);
  }
}