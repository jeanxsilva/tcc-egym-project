import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ApiService } from 'src/app/services/api-service/api.service';

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
    let modal = this.ngbModal.open(this.removeModal, { centered: true });

    modal.result.then((result) => {
      if (result && result == "Confirm") {
        console.log(result);

        this.apiService.SendToAPI("Employee", "Delete", employee).subscribe((result) => {
          console.log(result);
        }, (err) => {
          console.error(err);
        });
      }
    }, err => {
      console.log(err)
    });
  }

}
