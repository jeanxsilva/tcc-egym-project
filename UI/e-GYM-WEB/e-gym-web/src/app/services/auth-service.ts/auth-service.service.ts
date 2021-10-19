import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as moment from "moment";
import { map } from 'rxjs';
import { UserPermissionService } from '../user-permissions-service/user-permission.service';

let self;
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = "https://localhost:5001/api";
  private userProfile: any = {};
  private token: string;

  constructor(private http: HttpClient, private userPermissionService: UserPermissionService, private router: Router) {
    self = this;
  }

  public AuthenticateUser(form: any) {
    return this.http.post<any>(this.baseUrl + "/Authentication/Authenticate", form).pipe(map(result => {
      let response = {
        isAuthenticated: false,
        message: ""
      }

      if (result && !result.HasError) {
        console.log(result);
        this.setSession(result);
        this.userProfile = result.UserProfile;
        // self.userPermissionService.setUserPermissions(this.GetUserLogged());

        response.isAuthenticated = true;
      } else if (result) {
        response.message = result.Message;
      }

      return response;
    }));
  }

  public GetToken(): any {
    let token = localStorage.getItem('id_token');
    if (this.IsLoggedIn()) {
      return token;
    }

    this.RealizeLogOut();
    return null;
  }

  private setSession(authResult) {
    const expiresAt = moment().add(authResult.ExpiresIn, 'hour');

    localStorage.setItem('id_token', authResult.Token);
    localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));
    localStorage.setItem("user_profile", JSON.stringify(authResult.UserProfile));

    this.token = authResult.Token;
  }

  public RealizeLogOut() {
    localStorage.removeItem("id_token");
    localStorage.removeItem("expires_at");

    this.router.navigate(['login']);
  }

  public IsLoggedIn() {
    return moment().isBefore(this.GetExpiration());
  }

  public IsLoggedOut() {
    return !this.IsLoggedIn();
  }

  public GetExpiration() {
    const expiration = localStorage.getItem("expires_at");
    const expiresAt = JSON.parse(expiration);

    return moment(expiresAt);
  }

  public GetUserLogged() {
    this.userProfile = JSON.parse(localStorage.getItem("user_profile"));

    return this.userProfile;
  }
}