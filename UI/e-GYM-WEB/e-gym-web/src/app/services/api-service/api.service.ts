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

  constructor(private apollo: Apollo, private httpClient: HttpClient) { }

  public GetFromAPI(controller: string, action: string, params?: string) {
    let url = this.baseUrl + "/" + controller + "/" + action;

    if (params) {
      url += params;
    }

    return this.httpClient.get(url);
  }

  public SendToAPI(controller: string, action: string, data: any, params?: string) {
    let url = this.baseUrl + "/" + controller + "/" + action;

    if (params) {
      url += params;
    }

    let headers = new HttpHeaders({
      "Content-Type": "application/json"
    })

    return this.httpClient.post(url, data, { headers });
  }

  public GetFromGraphQL(query: Query | string) {
    if (query instanceof Query) {
      return this.apollo.watchQuery({ query: gql`${query.ToString()}` })
        .valueChanges.pipe(map(result => result.data[query.QueryName]));
    }
  }
}
