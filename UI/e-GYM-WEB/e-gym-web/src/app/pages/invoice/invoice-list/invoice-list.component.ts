import { ActivatedRoute, Router } from '@angular/router';
import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api-service/api.service';
import { MatchingTypes, StringMatchTypeEnum, MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { map } from 'rxjs';
@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.scss']
})
export class InvoiceListComponent implements OnInit {
  public invoiceList: any[] = [];
  public searchBy: any;
  public filterBy: any;
  public filterFieldList: any[] = [{
    description: 'Nome do aluno',
    value: 'student.user.name',
    type: "text"
  }, {
    description: 'Data de vencimento',
    value: 'dueDate',
    type: "date"
  }, {
    description: 'Data de referência',
    value: 'referentToDate',
    type: "date"
  }];

  constructor(private apiService: ApiService, private router: Router) {
    let state = this.router.getCurrentNavigation().extras.state;
    this.filterBy = this.filterFieldList[0];

    if (state) {
      this.searchBy = state.studentName;
      this.loadInvoices(this.filterFieldList[0], state.studentName);
    } else {
      this.loadInvoices();
    }
  }

  ngOnInit(): void {
  }

  public search() {
    this.loadInvoices(this.filterBy, this.searchBy);
  }
  
  public seeDetails(id){
    this.router.navigate([`payment/invoice/${id}`]);
  }

  private loadInvoices(filterBy?: any, searchBy?: string) {
    let queryBuilder: QueryBuilder = new QueryBuilder("listInvoice");

    if (filterBy && searchBy) {
      let queryFilter = queryBuilder.CreateFilter();
      let matchType: MatchingTypes = StringMatchTypeEnum.CONTAINS;

      if (filterBy.type === "date") {
        matchType = MatchTypeEnum.IN;
      }

      if (!filterBy.value.includes(".")) {
        queryFilter.AddCondition(filterBy.value, matchType, searchBy.toString());
      } else {
        let splittedFilterBy = filterBy.value.split(".");

        let splitFilter = queryFilter.AddEntity(splittedFilterBy[0]);
        splittedFilterBy.forEach((item, index) => {
          if (index == 0) {
            return;
          }

          if (index < splittedFilterBy.length - 1) {
            splitFilter = splitFilter.AddEntity(item);
            return;
          }
          console.log(typeof (searchBy))
          splitFilter.AddCondition(item, matchType, searchBy.toString());
        })
      }
    }

    queryBuilder.AddColumn("id").AddColumn("totalValue").AddColumn("referentToDate").AddColumn("dueDate")
      .AddEntity("invoiceStatus").AddColumn("id").AddColumn("description");
    queryBuilder.AddEntity("student").AddColumn("id").AddEntity("user").AddColumn("name");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result: any) => {
      this.invoiceList = result.items;
    });

  }
}
