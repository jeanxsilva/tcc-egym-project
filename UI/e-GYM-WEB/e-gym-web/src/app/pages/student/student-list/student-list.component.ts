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
  constructor(private router: Router, private ngbModal: NgbModal) { }

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
    console.log(this.removeModal)
    let modal = this.ngbModal.open(this.removeModal, { centered: true });
    modal.result.then((result) => {
      console.log(result);
    });
  }

}
