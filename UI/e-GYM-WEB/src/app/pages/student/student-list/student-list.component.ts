import { ApiService } from 'src/app/services/api-service/api.service';
import { Router } from '@angular/router';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.scss']
})
export class StudentListComponent implements OnInit {
  @ViewChild('removeModal') removeModal: TemplateRef<any>;

  constructor(private router: Router, private ngbModal: NgbModal, private apiService: ApiService) { }

  ngOnInit(): void {
  }

  editStudent(student: any) {
    console.log(student)
    this.router.navigate([`student/edit/${student.id}`]);
  }

  insertStudent() {
    this.router.navigate(['student/create']);
  }

  deleteStudent(student) {
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
        this.apiService.SendToAPI("StudentRegistration", "DeleteStudent", student).subscribe((result) => {
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