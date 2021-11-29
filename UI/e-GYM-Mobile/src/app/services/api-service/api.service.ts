import { LoaderService } from './../loader-service/loader-service.service';
import { AuthService } from './../auth-service.ts/auth-service.service';
import { HttpClient, HttpEvent, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Query } from '../query-builder/query';
import { Apollo, gql } from 'apollo-angular';
import { map, mergeMap } from 'rxjs/operators';
import { URL_BASE_API } from 'src/configs/config';
import { defer, from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public baseUrl = URL_BASE_API;

  constructor(private apollo: Apollo, private httpClient: HttpClient, private authService: AuthService, private loaderService: LoaderService) { }

  public GetFromAPI(controller: string, action: string, params?: string) {
    return from(this.authService.GetToken()).pipe(mergeMap(token => {
      let url = this.baseUrl + "/" + controller + "/" + action;
      let headers = new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization":
          `bearer ${token}`
      });

      this.loaderService.show();

      if (params) {
        url += params;
      }

      return this.httpClient.get(url, { headers }).pipe(map((result: any) => {
        this.loaderService.hide();
        return result;
      }, (err) => {
        this.loaderService.hide();
      }));
    }));
  }

  public SendToAPI(controller: string, action: string, data: any, params?: string): Observable<any> {
    return from(this.authService.GetToken()).pipe(mergeMap(token => {
      let url = this.baseUrl + "/" + controller + "/" + action;
      let headers = new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization":
          `bearer ${token}`
      });
      console.log(headers)
      this.loaderService.show().then();

      if (params) {
        url += params;
      }
      console.log(url, data)

      return this.httpClient.post(url, data, { headers }).pipe(
        map((result: any) => {
          console.log(result);

          if (result && result.HasError == false) {
            if (result.Result === true) {
              // this.toast.success('Operação realizada com sucesso!', 'Sucesso!');
            }
          } else if (result.HasError == true) {
            // this.toast.error(result.Message, 'Erro!');
          }

          this.loaderService.hide();

          return result;
        }, (err) => {
          this.loaderService.hide();
        }));
    }));
  }

  public GetFromGraphQL(query: Query | string): Observable<any> {
    return from(this.authService.GetToken()).pipe(mergeMap(token => {

      let headers = new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization":
          `bearer ${token}`
      });

      this.loaderService.show().then();

      if (query instanceof Query) {
        return this.apollo.watchQuery({ query: gql`${query.ToString()}`, fetchPolicy: 'no-cache' })
          .valueChanges.pipe(map((result: any) => {

            this.loaderService.hide();

            return result.data[query.QueryName]
          }, (err) => {
            this.loaderService.hide();
          }));
      }
    }));
  }
}
