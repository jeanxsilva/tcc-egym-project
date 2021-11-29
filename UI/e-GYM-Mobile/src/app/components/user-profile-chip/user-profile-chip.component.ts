import { ApiService } from './../../services/api-service/api.service';
import { MatchTypeEnum } from './../../services/query-builder/enums';
import { QueryBuilder } from './../../services/query-builder/query-builder';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';
import { MenuController } from '@ionic/angular';

@Component({
  selector: 'app-user-profile-chip',
  templateUrl: './user-profile-chip.component.html',
  styleUrls: ['./user-profile-chip.component.scss'],
})
export class UserProfileChipComponent implements OnInit {
  public user: any;

  constructor(private authService: AuthService, private router: Router, private menuController: MenuController, private apiService: ApiService) {
    this.authService.GetUserLogged().then(userProfile => {
      this.user = userProfile.User;
      console.log(this.user);
      this.loadUser();
    });
  }

  ngOnInit() { }

  public loadUser() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listUser");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.user.Id);
    queryBuilder.AddColumn("name").AddColumn("lastName");
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.user = result.items[0];
    })
  }

  public seeProfile() {
    this.router.navigate(['user-profile']);
    this.menuController.close();
  }
}
