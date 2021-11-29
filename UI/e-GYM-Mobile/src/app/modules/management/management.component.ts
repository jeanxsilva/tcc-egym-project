import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-management',
  templateUrl: './management.component.html',
  styleUrls: ['./management.component.scss'],
})
export class ManagementComponent implements OnInit {
  public user: any;
  public appPages = [
    { title: 'Inicio', url: '/home', icon: 'home-outline' },
    { title: 'Matriculas', url: '/registrations', icon: 'id-card-outline' },
    { title: 'Pagamentos', url: '/payments', icon: 'wallet-outline' },
    { title: 'Faturas', url: '/invoices', icon: 'document-text-outline' },
    { title: 'Requisições', url: '/requests', icon: 'file-tray-full-outline' },
  ];
  constructor(private authService: AuthService) {
    this.authService.GetUserLogged().then(userProfile => {
      this.user = userProfile.User;
    });
  }

  ngOnInit() { }

  public logout() {
    this.authService.RealizeLogOut();
  }

}