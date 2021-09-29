import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.scss']
})
export class TablesComponent implements OnInit {

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
  }
  rows = [
    { name: 'Austin', gender: 'Male', company: 'Swimlane' },
    { name: 'Dany', gender: 'Male', company: 'KFC' },
    { name: 'Molly', gender: 'Female', company: 'Burger King'}
  ];
  columns = [{ prop: 'name' }, { name: 'Gender' }, { name: 'Company' }, { name: 'Editar' }, {name: 'Excluir', sortable: false}];
}
