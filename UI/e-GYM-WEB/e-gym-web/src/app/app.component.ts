import { UserPermissionService } from './services/user-permissions-service/user-permission.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'E-GYM';

  constructor(){
  }

  ngOnInit() {
  }
}