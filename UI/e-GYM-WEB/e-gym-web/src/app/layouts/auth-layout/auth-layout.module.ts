import { AuthService } from './../../services/auth-service.ts/auth-service.service';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthLayoutRoutes } from './auth-layout.routing';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from 'src/app/pages/authentication/login/login.component';
import { RegisterComponent } from 'src/app/pages/authentication/register/register.component';
import { UserPermissionService } from 'src/app/services/user-permissions-service/user-permission.service';
import { ApiService } from 'src/app/services/api-service/api.service';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AuthLayoutRoutes),
    FormsModule,
    ReactiveFormsModule
    // NgbModule
  ],
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  providers:[
    ApiService,
    AuthService,
    UserPermissionService
  ]
})
export class AuthLayoutModule { }
