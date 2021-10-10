import { StudentFormComponent } from './../../pages/student/student-form/student-form.component';
import { StudentListComponent } from './../../pages/student/student-list/student-list.component';
import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'user-profile', component: UserProfileComponent },
    { path: 'student', component: StudentListComponent },
    { path: 'student/create', component: StudentFormComponent },
    { path: 'student/edit/:id', component: StudentFormComponent }
];