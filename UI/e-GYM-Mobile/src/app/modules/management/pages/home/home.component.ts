import { ApiService } from 'src/app/services/api-service/api.service';
import { MatchTypeEnum, NumberMatchTypeEnum, SortEnum } from './../../../../services/query-builder/enums';
import { QueryBuilder } from './../../../../services/query-builder/query-builder';
import { Component, OnInit, ViewChild } from '@angular/core';
import { IonInfiniteScroll } from '@ionic/angular';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  @ViewChild(IonInfiniteScroll) infiniteScroll: IonInfiniteScroll;
  public news: any[];
  public user: any;

  constructor(private apiService: ApiService, private authService: AuthService) {
    this.authService.GetUserLogged().then(userProfile => {
      this.user = userProfile.User;
    });

    this.loadNews();
  }

  ngOnInit() { }

  private loadNews() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listLastNews");
    queryBuilder.AddSort("id", SortEnum.DESC);
    queryBuilder.AddColumn("id")
      .AddColumn("title")
      .AddColumn("content")
      .AddColumn("registerDateTime")
      .AddColumn("expireDateTime")
      .AddEntity("publishedByUser")
      .AddColumn("id")
      .AddColumn("name");
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.news = result.items;
    });
  }

  loadData(event) {
    setTimeout(() => {
      console.log('Done');
      event.target.complete();

      // App logic to determine if all data is loaded
      // and disable the infinite scroll
      if (event.length == 1000) {
        event.target.disabled = true;
      }
    }, 500);
  }
}
