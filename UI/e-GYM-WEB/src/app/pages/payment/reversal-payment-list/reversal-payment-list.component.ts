import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api-service/api.service';

@Component({
  selector: 'app-reversal-payment-list',
  templateUrl: './reversal-payment-list.component.html',
  styleUrls: ['./reversal-payment-list.component.scss']
})
export class ReversalPaymentListComponent implements OnInit {

  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
  }

  seeDetails(id: number){
    this.router.navigate([`payment/reversal/${id}`]);
  }
}
