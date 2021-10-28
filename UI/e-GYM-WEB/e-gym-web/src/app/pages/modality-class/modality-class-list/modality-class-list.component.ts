import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ICrudBase } from 'src/app/models/CrudBase';

@Component({
  selector: 'app-modality-class-list',
  templateUrl: './modality-class-list.component.html',
  styleUrls: ['./modality-class-list.component.scss']
})
export class ModalityClassListComponent implements OnInit, ICrudBase{

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
