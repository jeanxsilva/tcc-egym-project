import { ApiService } from './../../../services/api-service/api.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ICrudBase } from 'src/app/models/CrudBase';
import Swal from 'sweetalert2';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-modality-class-list',
  templateUrl: './modality-class-list.component.html',
  styleUrls: ['./modality-class-list.component.scss']
})
export class ModalityClassListComponent implements OnInit, ICrudBase {
  public refresh: Subject<boolean> = new Subject<boolean>();

  constructor(private router: Router, private apiService: ApiService) { }

  ngOnInit(): void {
  }

  insert() {
    this.router.navigate([`modality/class/create`]);
  }

  edit(entity: any) {
    this.router.navigate([`modality/class/edit/${entity.id}`]);
  }

  remove(entity: any) {
    Swal.fire({
      title: 'Tem certeza que deseja remover?',
      text: 'Não sera possivel recuperar este registro!',
      icon: 'warning',
      showCancelButton: true,
      customClass: {
        cancelButton: 'bg-default',
        confirmButton: 'bg-danger'
      },
      confirmButtonText: 'Sim, remover!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.value) {
        this.apiService.SendToAPI("ModalityClass", "Delete", entity).subscribe((result) => {
          console.log(result);
          if (result.HasError === false) {
            Swal.fire(
              'Removido!',
              'O arquivo foi removido com sucesso.',
              'success'
            );

            this.refresh.next(true);
          }
        }, (err) => {
          console.error(err);
        });
      }
    });
    console.log(entity);
  }
}