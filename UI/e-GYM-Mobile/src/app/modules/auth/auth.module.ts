import { AuthService } from './../../services/auth-service.ts/auth-service.service';
import { IonicModule } from '@ionic/angular';
import { AuthComponent } from './auth.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './pages/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AuthComponent, LoginComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AuthRoutingModule,
    HttpClientModule,
    IonicModule.forRoot()
  ],
  exports: [AuthComponent, LoginComponent],
  providers: [AuthService]
})
export class AuthModule { }
