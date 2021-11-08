import { ApiService } from './../../../services/api-service/api.service';
import { Router } from '@angular/router';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import Swal from 'sweetalert2';
import { Observable } from 'rxjs';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';

@Component({
  selector: 'app-training-plan-list',
  templateUrl: './training-plan-list.component.html',
  styleUrls: ['./training-plan-list.component.scss']
})
export class TrainingPlanListComponent implements OnInit {
  
  constructor(private router: Router, private apiService: ApiService) { }

  ngOnInit(): void {
  }

  public seeDetails(id) {
    this.router.navigate(['training', id]);
  }

  public editPlan(plan) {
    this.router.navigate(['training/edit', plan.id]);
  }

  public deletePlan(plan) {
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
        this.apiService.SendToAPI("TrainingPlan", "Delete", plan).subscribe((result) => {
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

  public insertPlan() {
    this.router.navigate(['training/create']);
  }
}