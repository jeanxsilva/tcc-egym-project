import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ICrudBase } from 'src/app/models/CrudBase';

@Component({
  selector: 'app-modality-list',
  templateUrl: './modality-list.component.html',
  styleUrls: ['./modality-list.component.scss']
})
export class ModalityListComponent implements OnInit, ICrudBase {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  insert() {
    console.log("Inserir");
  }

  edit(entity: any) {
    this.router.navigate([`exercise/edit/${entity.id}`]);
  }

  remove(entity: any) {
    console.log(entity);
  }
}
