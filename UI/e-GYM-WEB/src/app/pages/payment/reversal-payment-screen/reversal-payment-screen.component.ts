import { PaymentReversalStatusEnum, PaymentTypeEnum } from './../../../models/Enums';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ApiService } from 'src/app/services/api-service/api.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { Employee } from '../../employee/employee-form/employee-form.component';

@Component({
  selector: 'app-reversal-payment-screen',
  templateUrl: './reversal-payment-screen.component.html',
  styleUrls: ['./reversal-payment-screen.component.scss']
})
export class ReversalPaymentScreenComponent implements OnInit {
  public formReversal: FormGroup;
  public paymentId: number;
  public paymentType: any;
  public employees: Employee[] = new Array<Employee>();

  constructor(private formBuilder: FormBuilder, private apiService: ApiService, private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.formReversal = this.formBuilder.group({
      id: [0],
      reason: [''],
      paymentId: [this.paymentId],
      authorizedByUserId: [null],
      paymentReversalStatusId: [PaymentReversalStatusEnum.Opened]
    });

    this.loadEmployees();
    this.loadPaymentType();
  }

  private loadPaymentType() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPayment");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.paymentId);
    queryBuilder.AddColumn("id").AddEntity("paymentType").AddColumn("id").AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      console.log(result);
      this.paymentType = result.items[0]?.paymentType;
    });
  }

  private loadEmployees() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listEmployee");
    queryBuilder.AddColumn("id").AddEntity("user").AddColumn("id").AddColumn("name");
    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      console.log(result);
      this.employees = result.items;
    });
  }

  get paymentTypeEnum(): typeof PaymentTypeEnum {
    return PaymentTypeEnum;
  }

  public save() {
    if (this.formReversal.valid) {
      let entity = this.formReversal.value;
      this.apiService.SendToAPI("PaymentReversal", "Save", entity).subscribe((result: any) => {
        console.log(result);
        if (result && result.Result == true) {
          this.activeModal.close(true);
        }
      });
    }
  }

  public dismiss() {
    this.activeModal.dismiss();
  }
}