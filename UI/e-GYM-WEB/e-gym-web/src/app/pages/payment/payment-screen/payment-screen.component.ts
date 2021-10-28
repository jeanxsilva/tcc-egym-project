import { ApiService } from 'src/app/services/api-service/api.service';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

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

  ngAfterViewInit(){
    this.updateForm();
  }

  private updateForm(){
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

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.paymentTypeList = result.items;
    });
  }

  savePayment(){
    let entity = this.formPayment.value;
    this.apiService.SendToAPI("Payment", "Insert", entity).subscribe(result => console.log(result));
    if(this.paymentIsConfirmed){
      this.close(true);
    }
  }

  dismiss() {
    this.activeModal.dismiss();
  }
  
  close(reason) {
    this.activeModal.close(reason);
  }
}