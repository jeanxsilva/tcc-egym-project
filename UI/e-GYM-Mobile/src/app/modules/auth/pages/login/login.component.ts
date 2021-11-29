import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { ApiService } from 'src/app/services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Storage } from '@ionic/storage';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  public authForm: FormGroup;
  public message: string;

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) {
    this.authForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit() {
  }

  public authenticate() {
    if (this.authForm.valid) {
      let entity = this.authForm.value;
      
      this.authService.AuthenticateStudent(entity).subscribe((response: any) => {
        if (response && response.isAuthenticated) {
          this.router.navigate(['home']);
        } else {
          this.message = response?.message;
        }
      });
    }
  }
}