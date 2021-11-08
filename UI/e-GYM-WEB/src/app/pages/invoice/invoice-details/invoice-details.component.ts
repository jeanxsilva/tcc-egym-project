import { InvoiceStatusEnum } from './../../../models/Enums';
import { PaymentScreenComponent } from './../../payment/payment-screen/payment-screen.component';
import { ApiService } from 'src/app/services/api-service/api.service';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-invoice-details',
  templateUrl: './invoice-details.component.html',
  styleUrls: ['./invoice-details.component.scss']
})
export class InvoiceDetailsComponent implements OnInit {
  public invoice: any;
  public redirectToSchedule: boolean = false;
  @ViewChild('modalPayment') modalPayment: TemplateRef<any>;

  constructor(private activatedRoute: ActivatedRoute, private apiService: ApiService, private router: Router, private ngbModal: NgbModal) {
    this.activatedRoute.params.subscribe((result) => {
      if (result.id) {
        this.loadDetails(parseInt(result.id));
      }
    });

    let state = this.router.getCurrentNavigation().extras.state;
    if (state) {
      this.redirectToSchedule = state.redirectToSchedule;
    }
  }

  ngOnInit(): void {
  }

  get invoiceStatusEnum(): typeof InvoiceStatusEnum {
    return InvoiceStatusEnum;
  }

  public receivePayment() {
    let active = this.ngbModal.open(PaymentScreenComponent, { centered: true, size: 'lg' });
    active.componentInstance['invoice'] = this.invoice;
    active.result.then(hasSuccess => {
      if (hasSuccess) {
        if (this.redirectToSchedule) {
          this.router.navigate([`assesment/schedule/register`], {
            state: {
              student: this.invoice.student
            }
          });
        } else {
          this.router.navigate(['dashboard']);
        }
      }
    });
  }

  public seePayment() {
    this.router.navigate(['payment', this.invoice.payments[0].id]);
  }

  public loadDetails(id: number) {
    let queryBuilder: QueryBuilder = new QueryBuilder("listInvoice");
    let queryFilter = queryBuilder.CreateFilter();
    queryFilter.AddCondition("id", MatchTypeEnum.EQUALS, id);

    queryBuilder.AddColumn("id").AddColumn("totalValue").AddColumn("referentToDate").AddColumn("dueDate").AddColumn("note").AddColumn("isByRequest");
    queryBuilder.AddEntity("payments").AddColumn("id");
    queryBuilder.AddEntity("invoiceDetails").AddColumn("description").AddColumn("price");
    queryBuilder.AddEntity("invoiceStatus").AddColumn("id").AddColumn("description");
    queryBuilder.AddEntity("student").AddColumn("code").AddEntity("user").AddColumn("id").AddColumn("name").AddColumn("lastName");
    let studentRequestBuilder = queryBuilder.AddEntity("studentRequests").AddColumn("id").AddColumn("note");
    studentRequestBuilder.AddEntity("requestCategory").AddColumn("id").AddColumn("description");
    studentRequestBuilder.AddEntity("closedByUser").AddColumn("id").AddColumn("name");
    console.log(queryBuilder.GetQuery().ToString());

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result) => {
      this.invoice = result.items[0];
    });
  }
}