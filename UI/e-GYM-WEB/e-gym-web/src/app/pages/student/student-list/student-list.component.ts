import { ApiService } from 'src/app/services/api-service/api.service';
import { Router } from '@angular/router';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

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
    let modal = this.ngbModal.open(this.removeModal, { centered: true });

    modal.result.then((result) => {
      if (result && result == "Confirm") {
        console.log(result);

        this.apiService.SendToAPI("StudentRegistration", "DeleteStudent", student).subscribe((result) => {
          console.log(result);
        }, (err) => {
          console.error(err);
        });
      }
    }, err =>{
      console.log(err)
    });
  }
}