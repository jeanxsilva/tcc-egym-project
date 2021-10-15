import { AuthService } from './services/auth-service.ts/auth-service.service';
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