import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { ApiService } from 'src/app/services/api-service/api.service';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { ListMatchTypeEnum, MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { FilterBuilder } from './../../../services/query-builder/filter/filter-builder';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

let self;
@Component({
  selector: 'app-student-request-list',
  templateUrl: './student-request-list.component.html',
  styleUrls: ['./student-request-list.component.scss']
})
export class StudentRequestListComponent implements OnInit {
  public userLevelId: number;
  public requestCategoriesAllowed: any;

  constructor(private router: Router, private authService: AuthService, private apiService: ApiService) {
    self = this;
    this.getUserLevelRequest();
  }

  ngOnInit(): void {
  }

  public seeDetails(studentRequestId: number) {
    this.router.navigate(['student-request', studentRequestId]);
  }

  public filter(queryFilter: FilterBuilder) {
    self.userLevelId = self.authService.GetUserLogged().UserLevel.Id;
    queryFilter.AddEntity("requestCategory").AddEntityList("requestCategoryLevels", ListMatchTypeEnum.SOME).AddEntity("userLevel").AddCondition("id", MatchTypeEnum.EQUALS, self.userLevelId);
  }

  public getUserLevelRequest() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listRequestCategoryLevel");
    queryBuilder.CreateFilter().AddEntity("userLevel").AddCondition("id", MatchTypeEnum.EQUALS, this.userLevelId);
    queryBuilder.AddColumn("id").AddEntity("userLevel").AddColumn("id")
    queryBuilder.AddEntity("requestCategory").AddColumn("id");

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      console.log(result);
    });
  }
}
