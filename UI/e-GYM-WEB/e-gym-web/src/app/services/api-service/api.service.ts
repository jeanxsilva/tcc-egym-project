import { AuthService } from './../auth-service.ts/auth-service.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { map } from 'rxjs';
import { Query } from '../query-builder/query';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public baseUrl = "https://localhost:5001/api";

  constructor(private apollo: Apollo, private httpClient: HttpClient, private authService: AuthService) { }

  public GetFromAPI(controller: string, action: string, params?: string) {
    let url = this.baseUrl + "/" + controller + "/" + action;
    let token = this.authService.GetToken();
    let headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Authorization":
        `bearer ${token}`
    });
    
    if (params) {
      url += params;
    }
    
    return this.httpClient.get(url, { headers });
  }

  public SendToAPI(controller: string, action: string, data: any, params?: string) {
    let url = this.baseUrl + "/" + controller + "/" + action;
    let token = this.authService.GetToken();
    let headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Authorization":
        `bearer ${token}`
    });

    if (params) {
      url += params;
    }
    
    return this.httpClient.post(url, data, { headers });
  }

  public GetFromGraphQL(query: Query | string) {
    let token = this.authService.GetToken();
    let headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Authorization":
        `bearer ${token}`
    });

    if (query instanceof Query) {
      return this.apollo.watchQuery({ query: gql`${query.ToString()}` })
        .valueChanges.pipe(map(result => result.data[query.QueryName]));
    }
  }
}
