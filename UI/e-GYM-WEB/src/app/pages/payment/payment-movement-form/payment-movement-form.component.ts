import { ApiService } from 'src/app/services/api-service/api.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PaymentReversal } from './../reversal-payment-details/reversal-payment-details.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { PaymentReversalStatusEnum } from 'src/app/models/Enums';

@Component({
  selector: 'app-payment-movement-form',
  templateUrl: './payment-movement-form.component.html',
  styleUrls: ['./payment-movement-form.component.scss']
})
export class PaymentMovementFormComponent implements OnInit {
  public formMovement: FormGroup;
  public paymentReversalId: number;
  public updateType: number = 5;
  public finishReason: number = 10;

  constructor(private formBuilder: FormBuilder, private activeModal: NgbActiveModal, private apiService: ApiService) {
  }

  get paymentReversalStatusEnum(): typeof PaymentReversalStatusEnum {
    return PaymentReversalStatusEnum;
  }

  ngOnInit(): void {
    this.formMovement = this.formBuilder.group({
      id: [0],
      paymentReversalStatusId: [0],
      registeredByUserId: [0],
      note: ['', Validators.required],
      paymentReversalId: [this.paymentReversalId, Validators.required]
    });
  }

  public save() {
    if (this.formMovement.valid) {
      let entity = this.formMovement.value;

      if (this.updateType != 0) {
        entity.paymentReversalStatusId = this.updateType;
      } else {
        entity.paymentReversalStatusId = this.finishReason;
      }

      console.log(entity);
      this.apiService.SendToAPI("PaymentMovement", "Save", entity).subscribe((result: any) => {
        console.log(result);
        if(result && result.Result == true){
          this.activeModal.close(true);
        }
      }, err => {
        console.error(err);
      });
    }
  }

  public cancel() {
    this.activeModal.dismiss();
  }
}
