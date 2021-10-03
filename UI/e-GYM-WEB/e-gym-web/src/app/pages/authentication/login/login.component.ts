import { ApiService } from './../../../services/api-service/api.service';
import { Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  public formLogin: FormGroup;
  public message: string;

  constructor(private router: Router, private formBuilder: FormBuilder, private apiService: ApiService) { }

  ngOnInit() {
    this.formLogin = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', Validators.required]
    });
  }

  ngOnDestroy() {
  }

  authenticate() {
    this.message = null;
    console.log(this.formLogin);

    this.apiService.SendToAPI("Authentication","Authenticate", this.formLogin.value).subscribe((result: any)=>{
      console.log(result);
      
      if(result.HasError){
        this.message = result.Message;
      }else{
        //Salvar userprofile;
        this.router.navigate(['dashboard']);
      }
    });
  }

}