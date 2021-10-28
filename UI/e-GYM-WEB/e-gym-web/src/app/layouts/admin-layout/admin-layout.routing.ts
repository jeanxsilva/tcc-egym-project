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
import { PhysicalAssesmentDetailsComponent } from './../../pages/physicalAssesment/physical-assesment-details/physical-assesment-details.component';
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

    { path: 'employees', component: EmployeeListComponent },
    { path: 'employee/create', component: EmployeeFormComponent },
    { path: 'employee/edit/:id', component: EmployeeFormComponent },

    { path: 'students', component: StudentListComponent },
    { path: 'student/create', component: StudentFormComponent },
    { path: 'student/edit/:id', component: StudentFormComponent },

    { path: 'trainings', component: TrainingPlanListComponent },
    { path: 'training/create', component: TrainingPlanFormComponent },
    { path: 'training/edit/:id', component: TrainingPlanFormComponent },

    { path: 'payments', component: PaymentListComponent },
    { path: 'payment/register', component: PaymentScreenComponent },
    { path: 'payment/invoices', component: InvoiceListComponent },
    { path: 'payment/reversals', component: ReversalPaymentListComponent },
    { path: 'payment/reversal/register', component: ReversalPaymentScreenComponent },
    { path: 'payment/invoice/:id', component: InvoiceDetailsComponent },
    { path: 'payment/reversal/:id', component: ReversalPaymentDetailsComponent },

    { path: 'modalities', component: ModalityListComponent },
    { path: 'modality/create', component: ModalityFormComponent },
    { path: 'modality/edit/:id', component: ModalityFormComponent },
    { path: 'modality/classes', component: ModalityClassListComponent },
    { path: 'modality/class/create', component: ModalityClassFormComponent },
    { path: 'modality/class/edit/:id', component: ModalityClassFormComponent },

    { path: 'exercises', component: ExerciseListComponent },
    { path: 'exercise/create', component: ExerciseFormComponent },
    { path: 'exercise/edit/:id', component: ExerciseFormComponent },

    { path: 'assessments', component: PhysicalAssesmentListComponent },
    { path: 'assessment/scheduleds', component: PhysicalAssesmentScheduleListComponent },
    { path: 'assessment/schedule/register', component: SchedulePhysicalAssesmentComponent },
    { path: 'assessment/register/:scheduledId', component: PhysicalAssesmentScreenComponent },
    { path: 'assessment/:id', component: PhysicalAssesmentDetailsComponent },
];