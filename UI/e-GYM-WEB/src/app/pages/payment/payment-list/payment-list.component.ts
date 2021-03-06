import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-payment-list',
  templateUrl: './payment-list.component.html',
  styleUrls: ['./payment-list.component.scss']
})
export class PaymentListComponent implements OnInit {
  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
  }

  public seeDetails(id: number) {
    this.router.navigate([`payment/${id}`]);
  }
}