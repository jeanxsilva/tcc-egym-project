import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/services/api-service/api.service';

@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.scss']
})
export class StudentFormComponent implements OnInit {
  public isNew: boolean = true;
  public formStudent: FormGroup;
  public student: User = new User();

  constructor(private apiService: ApiService, private router: Router, private activatedRoute: ActivatedRoute, private formBuilder: FormBuilder) {
    this.activatedRoute.url.subscribe((result: any) => {
      let urlEdit = result.filter(o => o.path === "edit");
      this.isNew = urlEdit.length === 0;
    });
  }

  ngOnInit(): void {
    this.formStudent = this.formBuilder.group({
      id: [this.student.Id],
      name: [this.student.Name, Validators.required],
      lastName: [this.student.LastName, Validators.required],
      birthday: [this.student.Birthday],
      email: [this.student.Email, Validators.required],
      genre: [this.student.Genre, Validators.required],
      phone: [this.student.Phone, Validators.required],
      registerCode: [this.student.RegisterCode, Validators.required],
      contactPhone: [this.student.ContactPhone],
      addressCode: [this.student.AddressCode, Validators.required],
      addressNumber: [this.student.AddressNumber],
      addressCity: [this.student.AddressCity]
    });
  }

  public save() {
    let entity = this.formStudent.value;
    console.log(entity)
    this.apiService.SendToAPI("User", "InsertNewStudent", entity).subscribe((result: any) => {
      console.log(result);
    });
  }

  public cancel() {
    this.router.navigate(['student']);
  }

  public fieldIsRequired(fieldName: string) {
    let field = this.formStudent.get(fieldName);

    if (field) {
      const validator = field.validator({} as AbstractControl);

      return validator && validator.required
    }

    return false;
  }
  public searchAddress() {
    console.log(this.formStudent.get('contactPhone'));
  }
}

export class User {
  public Id: number = 0;
  public Name: string = '';
  public LastName: string = '';
  public RegisterCode: string = '';
  public Birthday: string = '';
  public Email: string = '';
  public Genre: boolean = false;
  public Phone: string = '';
  public ContactPhone: string = '';
  public AddressCode: string = '';
  public AddressNumber: string = '';
  public AddressCity: string = '';
}