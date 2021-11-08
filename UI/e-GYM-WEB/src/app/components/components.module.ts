import { NgxLoadingModule } from 'ngx-loading';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './sidebar/sidebar.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CustomTableComponent } from './custom-table/custom-table.component';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { CalendarModule } from 'primeng/calendar';
import { SliderModule } from 'primeng/slider';
import { MultiSelectModule } from 'primeng/multiselect';
import { ContextMenuModule } from 'primeng/contextmenu';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { ProgressBarModule } from 'primeng/progressbar';
import { InputTextModule } from 'primeng/inputtext';
import { FileUploadModule } from 'primeng/fileupload';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/api';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { AbilityModule } from '@casl/angular';
import { ApiService } from '../services/api-service/api.service';
import { AuthService } from '../services/auth-service.ts/auth-service.service';
import { UserPermissionService } from '../services/user-permissions-service/user-permission.service';
import { BaseCardComponent } from './base-card/base-card/base-card.component';
import { FullcalendarComponent } from './calendar/fullcalendar/fullcalendar.component';
import { FullCalendarModule } from '@fullcalendar/angular';
import { LastNewsCardComponent } from './last-news-card/last-news-card.component';
import { DynamicPipe } from '../pipes/dynamic.pipe';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
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
    FullCalendarModule,
    AbilityModule
  ],
  declarations: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    UserProfileComponent,
    BaseCardComponent,
    CustomTableComponent,
    FullcalendarComponent,
    LastNewsCardComponent,
    DynamicPipe
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    UserProfileComponent,
    BaseCardComponent,
    CustomTableComponent,
    FullcalendarComponent,
    LastNewsCardComponent
  ],
  providers: [
    MessageService,
    ConfirmationService,
    ApiService,
    AuthService,
    UserPermissionService]
})
export class ComponentsModule { }
