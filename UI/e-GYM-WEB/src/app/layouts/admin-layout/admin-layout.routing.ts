import { TrainingDetailsComponent } from './../../pages/training/training-details/training-details.component';
import { CancelGuard } from './../../cancel.guard';
import { LastNewsScreenComponent } from './../../pages/last-news/last-news-screen/last-news-screen.component';
import { RegistrationModalityFormComponent } from './../../pages/registration-modality/registration-modality-form/registration-modality-form.component';
import { PaymentDetailsComponent } from './../../pages/payment/payment-details/payment-details.component';
import { ExerciseListComponent } from './../../pages/exercise/exercise-list/exercise-list.component';
import { ExerciseFormComponent } from './../../pages/exercise/exercise-form/exercise-form.component';
import { ModalityFormComponent } from './../../pages/modality/modality-form/modality-form.component';
import { ModalityListComponent } from './../../pages/modality/modality-list/modality-list.component';
import { ModalityClassFormComponent } from './../../pages/modality-class/modality-class-form/modality-class-form.component';
import { ModalityClassListComponent } from './../../pages/modality-class/modality-class-list/modality-class-list.component';
import { ReversalPaymentScreenComponent } from './../../pages/payment/reversal-payment-screen/reversal-payment-screen.component';
import { ReversalPaymentDetailsComponent } from './../../pages/payment/reversal-payment-details/reversal-payment-details.component';
import { ReversalPaymentListComponent } from './../../pages/payment/reversal-payment-list/reversal-payment-list.component';
import { EmployeeFormComponent } from './../../pages/employee/employee-form/employee-form.component';
import { EmployeeListComponent } from './../../pages/employee/employee-list/employee-list.component';
import { TrainingPlanListComponent } from './../../pages/training/training-plan-list/training-plan-list.component';
import { TrainingPlanFormComponent } from './../../pages/training/training-plan-form/training-plan-form.component';
import { PhysicalAssesmentDetailsComponent } from './../../pages/physical-assesment/physical-assesment-details/physical-assesment-details.component';
import { SchedulePhysicalAssesmentComponent } from './../../pages/physical-assesment/schedule-physical-assesment/schedule-physical-assesment.component';
import { PhysicalAssesmentScreenComponent } from './../../pages/physical-assesment/physical-assesment-screen/physical-assesment-screen.component';
import { PhysicalAssesmentScheduleListComponent } from './../../pages/physical-assesment/physical-assesment-schedule-list/physical-assesment-schedule-list.component';
import { PaymentScreenComponent } from './../../pages/payment/payment-screen/payment-screen.component';
import { PaymentListComponent } from './../../pages/payment/payment-list/payment-list.component';
import { StudentFormComponent } from './../../pages/student/student-form/student-form.component';
import { StudentListComponent } from './../../pages/student/student-list/student-list.component';
import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { InvoiceListComponent } from 'src/app/pages/invoice/invoice-list/invoice-list.component';
import { InvoiceDetailsComponent } from 'src/app/pages/invoice/invoice-details/invoice-details.component';
import { PhysicalAssesmentListComponent } from 'src/app/pages/physical-assesment/physical-assesment-list/physical-assesment-list.component';
import { LastNewsListComponent } from 'src/app/pages/last-news/last-news-list/last-news-list.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'user-profile', component: UserProfileComponent },

    { path: 'employees', component: EmployeeListComponent },
    { path: 'employee/create', component: EmployeeFormComponent, canDeactivate: [CancelGuard] },
    { path: 'employee/edit/:id', component: EmployeeFormComponent, canDeactivate: [CancelGuard] },

    { path: 'students', component: StudentListComponent },
    { path: 'student/create', component: StudentFormComponent, canDeactivate: [CancelGuard] },
    { path: 'student/edit/:id', component: StudentFormComponent, canDeactivate: [CancelGuard] },

    { path: 'trainings', component: TrainingPlanListComponent },
    { path: 'training/create', component: TrainingPlanFormComponent, canDeactivate: [CancelGuard] },
    { path: 'training/:id', component: TrainingDetailsComponent },

    { path: 'payments', component: PaymentListComponent },
    { path: 'payment/register', component: PaymentScreenComponent, canDeactivate: [CancelGuard] },
    { path: 'payment/invoices', component: InvoiceListComponent },
    { path: 'payment/reversals', component: ReversalPaymentListComponent },
    { path: 'payment/:id', component: PaymentDetailsComponent },
    { path: 'payment/invoice/:id', component: InvoiceDetailsComponent },
    { path: 'payment/reversal/:id', component: ReversalPaymentDetailsComponent },

    { path: 'modalities', component: ModalityListComponent },
    { path: 'modality/create', component: ModalityFormComponent, canDeactivate: [CancelGuard] },
    { path: 'modality/edit/:id', component: ModalityFormComponent, canDeactivate: [CancelGuard] },
    { path: 'modality/classes', component: ModalityClassListComponent },
    { path: 'modality/class/create', component: ModalityClassFormComponent, canDeactivate: [CancelGuard] },
    { path: 'modality/class/edit/:id', component: ModalityClassFormComponent, canDeactivate: [CancelGuard] },

    { path: 'registration', component: RegistrationModalityFormComponent, canDeactivate: [CancelGuard] },

    { path: 'last-news', component: LastNewsListComponent },
    { path: 'news/register', component: LastNewsScreenComponent, canDeactivate: [CancelGuard] },
    { path: 'news/:id', component: LastNewsScreenComponent, canDeactivate: [CancelGuard] },

    { path: 'exercises', component: ExerciseListComponent },
    { path: 'exercise/create', component: ExerciseFormComponent, canDeactivate: [CancelGuard] },
    { path: 'exercise/edit/:id', component: ExerciseFormComponent, canDeactivate: [CancelGuard] },

    { path: 'assessments', component: PhysicalAssesmentListComponent },
    { path: 'assessment/scheduleds', component: PhysicalAssesmentScheduleListComponent },
    { path: 'assessment/schedule/register', component: SchedulePhysicalAssesmentComponent, canDeactivate: [CancelGuard] },
    { path: 'assessment/register/:scheduledId', component: PhysicalAssesmentScreenComponent, canDeactivate: [CancelGuard] },
    { path: 'assessment/:id', component: PhysicalAssesmentDetailsComponent },
];