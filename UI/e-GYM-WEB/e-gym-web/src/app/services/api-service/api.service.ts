import { HttpClient } from '@angular/common/http';
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

  public GetFromApi(controller: string, action: string, params?: string) {
    let url = this.baseUrl + "/" + controller + "/" + action;

    if (params) {
      url += params;
    }

    return this.httpClient.get(url);
  }

  public GetFromGraphQL(query: Query | string){
    if(query instanceof Query){
      return this.apollo.watchQuery({ query: gql`${query.ToString()}` })
      .valueChanges.pipe(map(result => result.data[query.QueryName]));
    }
  }
}
