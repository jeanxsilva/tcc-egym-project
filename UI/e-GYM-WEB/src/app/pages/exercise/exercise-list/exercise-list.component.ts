import { ICrudBase } from './../../../models/CrudBase';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ApiService } from 'src/app/services/api-service/api.service';

@Component({
  selector: 'app-exercise-list',
  templateUrl: './exercise-list.component.html',
  styleUrls: ['./exercise-list.component.scss']
})
export class ExerciseListComponent implements OnInit, ICrudBase {

  constructor(private router: Router, private apiService: ApiService) { }

  ngOnInit(): void {
  }

  insert() {
    this.router.navigate([`exercise/create`]);
  }

  edit(entity: any) {
    this.router.navigate([`exercise/edit/`, entity.id]);
  }

  remove(entity: any) {
    Swal.fire({
      title: 'Tem certeza que deseja remover?',
      text: 'Não sera possivel recuperar este registro!',
      icon: 'warning',
      showCancelButton: true,
      customClass:{
        cancelButton: 'bg-default',
        confirmButton: 'bg-danger'
      },
      confirmButtonText: 'Sim, remover!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.value) {
        this.apiService.SendToAPI("Exercise", "Delete", entity).subscribe((result) => {
          console.log(result);
          if (result.HasError === false) {
            Swal.fire(
              'Removido!',
              'O arquivo foi removido com sucesso.',
              'success'
            );
          }
        }, (err) => {
          console.error(err);
        });
      }
    });
  }
}