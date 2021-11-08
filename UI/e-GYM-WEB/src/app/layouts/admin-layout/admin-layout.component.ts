import { LoaderService } from './../../services/loader-service/loader-service.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { UserPermissionService } from 'src/app/services/user-permissions-service/user-permission.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {
  public isLoading: Subject<boolean> = this.loaderService.isLoading;

  constructor(private userPermissionService: UserPermissionService, private loaderService: LoaderService) {
  }

  ngOnInit() {
    this.userPermissionService.getUserPermissions();
  }

}
