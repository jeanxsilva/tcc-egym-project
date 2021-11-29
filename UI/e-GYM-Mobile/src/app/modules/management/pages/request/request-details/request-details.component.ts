import { ApiService } from './../../../../../services/api-service/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { PaymentReversalStatusEnum, RequestCategoryEnum, RequestStatusEnum } from 'src/app/models/Enums';

@Component({
  selector: 'app-request-details',
  templateUrl: './request-details.component.html',
  styleUrls: ['./request-details.component.scss'],
})
export class RequestDetailsComponent implements OnInit {
  public requestId: number;
  public request: any;

  constructor(private activatedRoute: ActivatedRoute, private apiService: ApiService, private router: Router) {
    this.activatedRoute.params.subscribe(param => {
      this.requestId = parseInt(param['id']);
      this.loadRequest();
    });
  }

  ngOnInit() { }

  get RequestCategoryEnum(): typeof RequestCategoryEnum {
    return RequestCategoryEnum;
  }

  get RequestStatusEnum(): typeof RequestStatusEnum {
    return RequestStatusEnum;
  }

  get paymentReversalStatusEnum(): typeof PaymentReversalStatusEnum {
    return PaymentReversalStatusEnum;
  }

  private loadRequest() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listStudentRequest");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.requestId);
    queryBuilder.AddColumn("id").AddColumn("registerDateTime").AddColumn("note").AddColumn("isPaid").AddColumn("wasCanceled")
    queryBuilder.AddEntity("student").AddColumn("id").AddEntity("user").AddColumn("id").AddColumn("name").AddColumn("lastName");
    queryBuilder.AddEntity("requestStatus").AddColumn("id").AddColumn("description");
    queryBuilder.AddEntity("requestCategory").AddColumn("id").AddColumn("description");
    queryBuilder.AddEntity("closedByUser").AddColumn("id").AddColumn("name").AddColumn("lastName");
    queryBuilder.AddEntity("invoice").AddColumn("id");

    let paymentReversalBuilder = queryBuilder.AddEntity("paymentReversal").AddColumn("id").AddColumn("reason").AddColumn("lastModifiedDateTime");
    paymentReversalBuilder.AddEntity("authorizedByUser").AddColumn("id").AddColumn("name");
    paymentReversalBuilder.AddEntity("createdByUser").AddColumn("id").AddColumn("name");
    paymentReversalBuilder.AddEntity("finishedByUser").AddColumn("id").AddColumn("name");
    paymentReversalBuilder.AddEntity("payment").AddColumn("id").AddColumn("paymentDateTime").AddEntity("paidByUser").AddColumn("id").AddColumn("name");
    paymentReversalBuilder.AddEntity("paymentReversalStatus").AddColumn("id").AddColumn("description");

    let movementBuilder = paymentReversalBuilder.AddEntity("paymentMovements").AddColumn("id").AddColumn("registerDateTime").AddColumn("isCurrent").AddColumn("note");
    movementBuilder.AddEntity("paymentReversalStatus").AddColumn("id").AddColumn("description");
    movementBuilder.AddEntity("registeredByUser").AddColumn("id").AddColumn("name");


    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.request = result.items[0];
    });
  }

  public seeInvoice(invoiceId: number) {
    this.router.navigate(['invoice', invoiceId]);
  }
}
