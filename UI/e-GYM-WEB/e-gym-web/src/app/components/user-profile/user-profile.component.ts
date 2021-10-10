import { AuthService } from './../../services/auth-service.ts/auth-service.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
  public userProfile: any;
  constructor(public authService: AuthService) { }

  ngOnInit(): void {
    this.userProfile = this.authService.GetUserLogged();
  }

}