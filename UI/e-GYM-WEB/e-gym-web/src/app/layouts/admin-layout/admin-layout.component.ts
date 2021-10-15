import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { UserPermissionService } from 'src/app/services/user-permissions-service/user-permission.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {

  constructor(private userPermissionService: UserPermissionService) { }

  ngOnInit() {
    this.userPermissionService.getUserPermissions();
  }

}
