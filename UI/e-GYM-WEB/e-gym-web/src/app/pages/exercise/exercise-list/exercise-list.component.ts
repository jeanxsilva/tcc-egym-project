import { ICrudBase } from './../../../models/CrudBase';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-exercise-list',
  templateUrl: './exercise-list.component.html',
  styleUrls: ['./exercise-list.component.scss']
})
export class ExerciseListComponent implements OnInit, ICrudBase{

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
