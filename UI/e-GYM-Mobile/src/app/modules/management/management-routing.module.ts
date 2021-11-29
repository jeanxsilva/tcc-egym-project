import { RequestDetailsComponent } from './pages/request/request-details/request-details.component';
import { RequestComponent } from './pages/request/request.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { InvoiceDetailsComponent } from './pages/payment/invoice-details/invoice-details.component';
import { RegistrationFormComponent } from './pages/registration/registration-form/registration-form.component';
import { InvoiceComponent } from './pages/payment/invoice/invoice.component';
import { TrainingDetailsComponent } from './pages/training/training-details/training-details.component';
import { TrainingListComponent } from './pages/training/training-list/training-list.component';
import { PaymentComponent } from './pages/payment/payment.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { AssesmentDetailsComponent } from './pages/assesment/assesment-details/assesment-details.component';
import { AssesmentComponent } from './pages/assesment/assesment.component';
import { TrainingComponent } from './pages/training/training.component';
import { HomeComponent } from './pages/home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'training',
    component: TrainingComponent
  },
  {
    path: 'training/historic',
    component: TrainingListComponent
  },
  {
    path: 'training/details/:id',
    component: TrainingDetailsComponent
  },
  {
    path: 'assesment',
    component: AssesmentComponent
  },
  {
    path: 'assesment/:id',
    component: AssesmentDetailsComponent
  },
  {
    path: 'requests',
    component: RequestComponent
  },
  {
    path: 'request/:id',
    component: RequestDetailsComponent
  },
  {
    path: 'registrations',
    component: RegistrationComponent
  },
  {
    path: 'registration/insert',
    component: RegistrationFormComponent
  },
  {
    path: 'payments',
    component: PaymentComponent
  },
  {
    path: 'invoices',
    component: InvoiceComponent
  },
  {
    path: 'invoice/:id',
    component: InvoiceDetailsComponent
  },
  {
    path: 'user-profile',
    component: UserProfileComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagementRoutingModule { }