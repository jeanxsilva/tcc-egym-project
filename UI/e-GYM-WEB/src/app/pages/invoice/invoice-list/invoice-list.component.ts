import { ActivatedRoute, Router } from '@angular/router';
import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api-service/api.service';
import { MatchingTypes, StringMatchTypeEnum, MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.scss']
})
export class InvoiceListComponent implements OnInit {
  public invoiceList: any[] = [];
  
  constructor(private apiService: ApiService, private router: Router) {
    let state = this.router.getCurrentNavigation().extras.state;

    if (state) {
      state.studentName;
    }
  }

  ngOnInit(): void {
  }

  public seeDetails(id) {
    this.router.navigate([`payment/invoice/${id}`]);
  }
}
