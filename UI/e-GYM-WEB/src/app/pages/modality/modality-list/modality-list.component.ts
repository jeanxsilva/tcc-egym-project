import { ApiService } from './../../../services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ICrudBase } from 'src/app/models/CrudBase';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-modality-list',
  templateUrl: './modality-list.component.html',
  styleUrls: ['./modality-list.component.scss']
})
export class ModalityListComponent implements OnInit, ICrudBase {

  constructor(private router: Router, private apiService: ApiService) { }

  ngOnInit(): void {
  }

  insert() {
    this.router.navigate([`modality/create`]);
  }

  edit(entity: any) {
    this.router.navigate([`modality/edit/${entity.id}`]);
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
        this.apiService.SendToAPI("Modality", "Delete", entity).subscribe((result) => {
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
    console.log(entity);
  }
}
