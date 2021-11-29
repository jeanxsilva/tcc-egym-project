import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { AlertController } from '@ionic/angular';
import { ApiService } from 'src/app/services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { Router } from '@angular/router';
import { InvoiceStatusEnum } from 'src/app/models/Enums';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss'],
})
export class InvoiceComponent implements OnInit {
  public userId: number;
  public invoices: any[] = [];
  public openedInvoices: any[] = [];

  constructor(private apiService: ApiService, private alertController: AlertController, private router: Router, private authService: AuthService) {
    this.authService.GetUserLogged().then(userProfile => {
      this.userId = userProfile.User.Id;

      this.loadInvoices();
    });
  }

  ngOnInit() { }

  get InvoiceStatusEnum(): typeof InvoiceStatusEnum {
    return InvoiceStatusEnum;
  }

  private loadInvoices() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listInvoice");
    queryBuilder.CreateFilter().AddEntity("student").AddEntity("user").AddCondition("id", MatchTypeEnum.EQUALS, this.userId);
    queryBuilder.AddColumn("id")
      .AddColumn("totalValue")
      .AddColumn("referentToDate")
      .AddColumn("dueDate")
      .AddColumn("note");
    queryBuilder.AddEntity("invoiceStatus").AddColumn("id").AddColumn("description");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.openedInvoices = result.items.filter(o => {
        return o.invoiceStatus.id == InvoiceStatusEnum.Generated;
      });

      this.invoices = result.items.filter(o => {
        return o.invoiceStatus.id != InvoiceStatusEnum.Generated;
      });
    });
  }

  public seeDetails(invoiceId: number) {
    this.router.navigate(['invoice', invoiceId]);
  }
}