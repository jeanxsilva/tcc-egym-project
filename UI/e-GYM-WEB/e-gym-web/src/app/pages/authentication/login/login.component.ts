import { AuthService } from './../../../services/auth-service.ts/auth-service.service';
import { ApiService } from './../../../services/api-service/api.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public formLogin: FormGroup;
  public message: string;

  constructor(private router: Router, private formBuilder: FormBuilder, private apiService: ApiService, private authService: AuthService) { }

  ngOnInit() {
    this.formLogin = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', Validators.required]
    });
  }

  authenticate() {
    this.message = null;
    console.log(this.formLogin);

    this.authService.AuthenticateUser(this.formLogin.value).subscribe((response: any) => {
      if (response.isAuthenticated) {
        this.router.navigate(['dashboard']);
      } else {
        this.message = response.message;
      }
    });
  }
}