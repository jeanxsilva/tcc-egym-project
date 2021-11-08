import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ApiService } from 'src/app/services/api-service/api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {
  @ViewChild('removeModal') removeModal: TemplateRef<any>;

  constructor(private router: Router, private ngbModal: NgbModal, private apiService: ApiService) { }

  ngOnInit(): void {
  }

  editEmployee(employee: any) {
    console.log(employee)
    this.router.navigate([`employee/edit/${employee.id}`]);
  }

  insertEmployee() {
    this.router.navigate(['employee/create']);
  }

  deleteEmployee(employee) {
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
        this.apiService.SendToAPI("Employee", "Delete", employee).subscribe((result) => {
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
