import { Router } from '@angular/router';
import { ApiService } from './../../../services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';

@Component({
  selector: 'app-physical-assesment-list',
  templateUrl: './physical-assesment-list.component.html',
  styleUrls: ['./physical-assesment-list.component.scss']
})
export class PhysicalAssesmentListComponent implements OnInit {
  public physicalAssesmentList = [];

  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
  }

  seeDetails(id: number){
    this.router.navigate(['assessment', id]);
  }
}