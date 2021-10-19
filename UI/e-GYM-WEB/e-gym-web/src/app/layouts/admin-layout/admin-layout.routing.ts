import { SchedulePhysicalAssesmentComponent } from './../../pages/physicalAssesment/schedule-physical-assesment/schedule-physical-assesment.component';
import { PhysicalAssesmentScreenComponent } from './../../pages/physicalAssesment/physical-assesment-screen/physical-assesment-screen.component';
import { PhysicalAssesmentScheduleListComponent } from './../../pages/physicalAssesment/physical-assesment-schedule-list/physical-assesment-schedule-list.component';
import { PaymentScreenComponent } from './../../pages/payment/payment-screen/payment-screen.component';
import { PaymentListComponent } from './../../pages/payment/payment-list/payment-list.component';
import { StudentFormComponent } from './../../pages/student/student-form/student-form.component';
import { StudentListComponent } from './../../pages/student/student-list/student-list.component';
import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { InvoiceListComponent } from 'src/app/pages/invoice/invoice-list/invoice-list.component';
import { InvoiceDetailsComponent } from 'src/app/pages/invoice/invoice-details/invoice-details.component';
import { PhysicalAssesmentListComponent } from 'src/app/pages/physicalAssesment/physical-assesment-list/physical-assesment-list.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'user-profile', component: UserProfileComponent },
    { path: 'student', component: StudentListComponent },
    { path: 'student/create', component: StudentFormComponent },
    { path: 'student/edit/:id', component: StudentFormComponent },
    { path: 'payment', component: PaymentListComponent },
    { path: 'payment/register', component: PaymentScreenComponent},
    { path: 'payment/invoices', component: InvoiceListComponent },
    { path: 'payment/invoice/:id', component: InvoiceDetailsComponent },
    { path: 'assessments', component: PhysicalAssesmentListComponent},
    { path: 'assessment/register', component: PhysicalAssesmentScreenComponent},
    { path: 'assessment/scheduled', component: PhysicalAssesmentScheduleListComponent},
    { path: 'assessment/schedule/register', component: SchedulePhysicalAssesmentComponent}
];