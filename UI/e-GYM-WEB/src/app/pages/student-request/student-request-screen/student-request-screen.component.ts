import { RequestCategoryEnum, RequestStatusEnum } from './../../../models/Enums';
import { ApiService } from 'src/app/services/api-service/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { IFormBase } from 'src/app/models/CrudBase';
import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';

@Component({
  selector: 'app-student-request-screen',
  templateUrl: './student-request-screen.component.html',
  styleUrls: ['./student-request-screen.component.scss']
})
export class StudentRequestScreenComponent implements OnInit {
  public request: any;
  public requestId: number;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private apiService: ApiService) {
    this.activatedRoute.params.subscribe(param => {
      if (param['id']) {
        this.requestId = parseInt(param['id']);

        this.loadRequest();
      }
    });
  }

  ngOnInit(): void {
  }

  get RequestCategoryEnum(): typeof RequestCategoryEnum {
    return RequestCategoryEnum;
  }

  get RequestStatusEnum(): typeof RequestStatusEnum {
    return RequestStatusEnum;
  }

  public finishRequest(requestId: number) {
    this.apiService.SendToAPI("StudentRequest", "FinishRequest", requestId, "requestStatusEnum=" + RequestStatusEnum.Deffered).subscribe(result => {
      this.router.navigate(['student-requests']);
    }, err => {
      console.error(err);
    });
  }

  public refuseRequest(requestId: number) {
    this.apiService.SendToAPI("StudentRequest", "FinishRequest", requestId, "requestStatusEnum=" + RequestStatusEnum.Dismissed).subscribe(result => {
      this.router.navigate(['student-requests']);
    }, err => {
      console.error(err);
    });
  }

  private loadRequest() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRequest");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.requestId);
    queryBuilder.AddColumn("id").AddColumn("registerDateTime").AddColumn("note").AddColumn("isPaid").AddColumn("wasCanceled")
    queryBuilder.AddEntity("student").AddColumn("id").AddEntity("user").AddColumn("id").AddColumn("name").AddColumn("lastName");
    queryBuilder.AddEntity("requestStatus").AddColumn("id").AddColumn("description");
    queryBuilder.AddEntity("requestCategory").AddColumn("id").AddColumn("description");
    queryBuilder.AddEntity("closedByUser").AddColumn("id").AddColumn("name").AddColumn("lastName");
    queryBuilder.AddEntity("paymentReversal").AddColumn("id");
    queryBuilder.AddEntity("invoice").AddColumn("id");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.request = result.items[0];
    });
  }

  seeReversal(reversalPaymentId: number) {
    this.router.navigate(['payment/reversal', reversalPaymentId]);
  }

  seeInvoice(invoiceId: number) {
    this.router.navigate(['invoice', invoiceId]);
  }


}
