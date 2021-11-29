import { RequestDetailsComponent } from './pages/request/request-details/request-details.component';
import { RequestComponent } from './pages/request/request.component';
import { LoaderService } from './../../services/loader-service/loader-service.service';
import { ApiService } from 'src/app/services/api-service/api.service';
import { UserProfileChipComponent } from './../../components/user-profile-chip/user-profile-chip.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { InvoiceDetailsComponent } from './pages/payment/invoice-details/invoice-details.component';
import { RegistrationFormComponent } from './pages/registration/registration-form/registration-form.component';
import { InvoiceComponent } from './pages/payment/invoice/invoice.component';
import { TrainingRequestComponent } from './pages/training/training-request/training-request.component';
import { AssesmentRequestComponent } from './pages/assesment/assesment-request/assesment-request.component';
import { TrainingDetailsComponent } from './pages/training/training-details/training-details.component';
import { TrainingListComponent } from './pages/training/training-list/training-list.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { AssesmentDetailsComponent } from './pages/assesment/assesment-details/assesment-details.component';
import { AssesmentComponent } from './pages/assesment/assesment.component';
import { PaymentComponent } from './pages/payment/payment.component';
import { TrainingComponent } from './pages/training/training.component';
import { HeaderComponent } from './../../components/header/header.component';
import { NewsCardComponent } from './../../components/news-card/news-card.component';
import { HomeComponent } from './pages/home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ManagementComponent } from './management.component';
import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { ManagementRoutingModule } from './management-routing.module';

@NgModule({
  declarations: [
    ManagementComponent,
    HomeComponent,
    NewsCardComponent,
    HeaderComponent,
    TrainingComponent,
    PaymentComponent,
    AssesmentComponent,
    AssesmentDetailsComponent,
    RegistrationComponent,
    RegistrationFormComponent,
    TrainingListComponent,
    TrainingDetailsComponent,
    AssesmentRequestComponent,
    TrainingRequestComponent,
    InvoiceComponent,
    InvoiceDetailsComponent,
    UserProfileComponent,
    UserProfileChipComponent,
    RequestComponent,
    RequestDetailsComponent
  ],
  imports: [
    CommonModule,
    IonicModule.forRoot(),
    FormsModule,
    ManagementRoutingModule,
    ReactiveFormsModule
  ],
  exports: [
    HomeComponent
  ],
  providers: [
    DatePipe,
    ApiService,
    LoaderService
  ]
})
export class ManagementModule { }