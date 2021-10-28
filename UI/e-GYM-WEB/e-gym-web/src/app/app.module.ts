import { AuthService } from './services/auth-service.ts/auth-service.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { InMemoryCache } from '@apollo/client/core';
import { APOLLO_OPTIONS } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { StudentListComponent } from './pages/student/student-list/student-list.component';
import { StudentFormComponent } from './pages/student/student-form/student-form.component';
import { RemoveModalComponent } from './components/remove-modal/remove-modal.component';
import { AbilityModule } from '@casl/angular';
import { Ability, PureAbility } from '@casl/ability';
import { UserPermissionService } from './services/user-permissions-service/user-permission.service';
import { ApiService } from './services/api-service/api.service';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ContextMenuModule } from 'primeng/contextmenu';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { FileUploadModule } from 'primeng/fileupload';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressBarModule } from 'primeng/progressbar';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { SliderModule } from 'primeng/slider';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { PaymentListComponent } from './pages/payment/payment-list/payment-list.component';

import localePt from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';
import { InvoiceListComponent } from './pages/invoice/invoice-list/invoice-list.component';
import { InvoiceDetailsComponent } from './pages/invoice/invoice-details/invoice-details.component';
import { PaymentScreenComponent } from './pages/payment/payment-screen/payment-screen.component';
registerLocaleData(localePt);
import { ChipModule } from 'primeng/chip';
import { PhysicalAssesmentListComponent } from './pages/physicalAssesment/physical-assesment-list/physical-assesment-list.component';
import { PhysicalAssesmentScreenComponent } from './pages/physicalAssesment/physical-assesment-screen/physical-assesment-screen.component';
import { SchedulePhysicalAssesmentComponent } from './pages/physicalAssesment/schedule-physical-assesment/schedule-physical-assesment.component';
import { PhysicalAssesmentScheduleListComponent } from './pages/physicalAssesment/physical-assesment-schedule-list/physical-assesment-schedule-list.component';
import { StudentRequestListComponent } from './pages/student-request/student-request-list/student-request-list.component';
import { FullCalendarModule } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid'; // a plugin!
import interactionPlugin from '@fullcalendar/interaction'; // a plugin!
import timeGridPlugin from '@fullcalendar/timegrid';
import { PhysicalAssesmentDetailsComponent } from './pages/physicalAssesment/physical-assesment-details/physical-assesment-details.component';
import { TrainingPlanListComponent } from './pages/training/training-plan-list/training-plan-list.component';
import { TrainingPlanFormComponent } from './pages/training/training-plan-form/training-plan-form.component';
import { EmployeeListComponent } from './pages/employee/employee-list/employee-list.component';
import { EmployeeFormComponent } from './pages/employee/employee-form/employee-form.component';
import { ReversalPaymentListComponent } from './pages/payment/reversal-payment-list/reversal-payment-list.component';
import { ReversalPaymentScreenComponent } from './pages/payment/reversal-payment-screen/reversal-payment-screen.component';
import { StudentRequestScreenComponent } from './pages/student-request/student-request-screen/student-request-screen.component';
import { LastNewsScreenComponent } from './pages/last-news/last-news-screen/last-news-screen.component';
import { ExerciseListComponent } from './pages/exercise/exercise-list/exercise-list.component';
import { ExerciseFormComponent } from './pages/exercise/exercise-form/exercise-form.component';
import { ModalityClassListComponent } from './pages/modality-class/modality-class-list/modality-class-list.component';
import { ModalityClassFormComponent } from './pages/modality-class/modality-class-form/modality-class-form.component';
import { ModalityFormComponent } from './pages/modality/modality-form/modality-form.component';
import { ModalityListComponent } from './pages/modality/modality-list/modality-list.component';
import { ReversalPaymentDetailsComponent } from './pages/payment/reversal-payment-details/reversal-payment-details.component';

FullCalendarModule.registerPlugins([ // register FullCalendar plugins
  dayGridPlugin,
  timeGridPlugin,
  interactionPlugin
]);

@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    NgbModule,
    RouterModule,
    AppRoutingModule,
    ReactiveFormsModule,
    AbilityModule,
    TableModule,
    CalendarModule,
    SliderModule,
    DialogModule,
    MultiSelectModule,
    ContextMenuModule,
    DropdownModule,
    ButtonModule,
    ToastModule,
    InputTextModule,
    ProgressBarModule,
    FileUploadModule,
    ToolbarModule,
    RatingModule,
    RadioButtonModule,
    InputNumberModule,
    ConfirmDialogModule,
    InputTextareaModule,
    AbilityModule,
    FullCalendarModule,
    ChipModule
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    AuthLayoutComponent,
    StudentListComponent,
    StudentFormComponent,
    RemoveModalComponent,
    PaymentListComponent,
    InvoiceListComponent,
    InvoiceDetailsComponent,
    PaymentScreenComponent,
    PhysicalAssesmentListComponent,
    PhysicalAssesmentScreenComponent,
    SchedulePhysicalAssesmentComponent,
    PhysicalAssesmentScheduleListComponent,
    StudentRequestListComponent,
    PhysicalAssesmentDetailsComponent,
    TrainingPlanListComponent,
    TrainingPlanFormComponent,
    EmployeeListComponent,
    EmployeeFormComponent,
    ReversalPaymentListComponent,
    ReversalPaymentScreenComponent,
    StudentRequestScreenComponent,
    LastNewsScreenComponent,
    ExerciseListComponent,
    ExerciseFormComponent,
    ModalityClassListComponent,
    ModalityClassFormComponent,
    ModalityFormComponent,
    ModalityListComponent,
    ReversalPaymentDetailsComponent
  ],
  providers: [
    {
      provide: APOLLO_OPTIONS,
      useFactory: (httpLink: HttpLink) => {
        return {
          cache: new InMemoryCache(),
          link: httpLink.create({
            uri: 'https://localhost:5001/graphql',
          }),
        };
      },
      deps: [HttpLink],
    },
    { provide: Ability, useValue: new Ability() },
    { provide: PureAbility, useExisting: Ability },
    ApiService,
    AuthService,
    UserPermissionService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }