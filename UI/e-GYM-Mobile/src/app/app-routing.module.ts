import { ManagementComponent } from './modules/management/management.component';
import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './modules/auth/auth.component';
import { LoggedGuard } from './guards/logged-guard/logged-guard.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'authentication/login',
    pathMatch: 'full'
  },
  {
    path: 'authentication',
    component: AuthComponent,
    loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: '',
    component: ManagementComponent,
    loadChildren: () => import('./modules/management/management.module').then(m => m.ManagementModule),
    canActivate: [LoggedGuard]
  },
  {
    path: '**',
    redirectTo: 'authentication/login'
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
