import { URL_BASE_API } from './../../../configs/config';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { QueryBuilder } from '../query-builder/query-builder';
import { Storage } from '@ionic/storage-angular';

let self;
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = URL_BASE_API;
  private userProfile: any = {};
  public _storage?: Storage;
  private token: string;

  constructor(private http: HttpClient, private router: Router, private storage: Storage) {
    self = this;
    this.init();
  }

  public async init() {
    this._storage = await this.storage.create();
  }

  public AuthenticateStudent(form: any) {
    return this.http.post<any>(this.baseUrl + "/Authentication/AuthenticateStudent", form).pipe(map(result => {
      let response = {
        isAuthenticated: false,
        message: null
      }

      if (result && !result.HasError) {
        this.setSession(result);

        response.isAuthenticated = true;
      } else if (result) {
        response.message = result.Message;
      }

      return response;
    }),
      catchError((err) => of(console.error(err))));
  }

  public GetToken(){
    return this.storage.get('id_token').then((token) => {
      if (this.IsLoggedIn()) {
        return token;
      }
      this.RealizeLogOut();
      return null;
    });
  }

  private async setSession(authResult) {
    const expiresAt = moment().add(authResult.ExpiresIn, 'hour');
    this.userProfile = authResult.UserProfile;

    this.storage.set('id_token', authResult.Token);
    this.storage.set("expires_at", JSON.stringify(expiresAt.valueOf()));
    this.storage.set("user_profile", JSON.stringify(authResult.UserProfile));

    this.token = authResult.Token;
  }

  public RealizeLogOut() {
    this.storage.remove("id_token");
    this.storage.remove("expires_at");

    this.router.navigate(['authentication/login']);
  }

  public async IsLoggedIn() {
    let expiration = await this.GetExpiration();
    return moment().isBefore(expiration);
  }

  public IsLoggedOut() {
    return !this.IsLoggedIn();
  }

  public async GetExpiration() {
    let expiration = await this.storage.get("expires_at");
    let expiresAt = JSON.parse(expiration);

    return moment(expiresAt);
  }

  public async GetUserLogged() {
    let userProfile = await this.storage.get("user_profile");
    this.userProfile = JSON.parse(userProfile);

    return this.userProfile;
  }
}