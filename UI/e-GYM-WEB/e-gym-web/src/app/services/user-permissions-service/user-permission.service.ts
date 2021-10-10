import { QueryBuilder } from './../query-builder/query-builder';
import { ApiService } from 'src/app/services/api-service/api.service';
import { Injectable } from '@angular/core';
import { Ability, AbilityBuilder } from '@casl/ability';
import { MatchTypeEnum } from '../query-builder/enums';

@Injectable({
  providedIn: 'root'
})
export class UserPermissionService {

  constructor(private ability: Ability, private apiService: ApiService) { }

  private updateAbility(userProfile: any) {
    const { can, rules } = new AbilityBuilder(Ability);

    if (userProfile) {
      let levelId = userProfile.UserLevel.Id;
      let userRoles = [];

      let queryBuilder: QueryBuilder = new QueryBuilder("listUserLevelRole");
      let queryFilter = queryBuilder.CreateFilter();
      queryFilter.AddEntity("userLevel").AddCondition("id", MatchTypeEnum.EQUALS, levelId);
      queryBuilder.AddColumn("id").AddColumn("role");
      console.log(queryBuilder.GetQuery().ToString());

      this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result) => {
        userRoles = result.items;

        if (userProfile.UserLevel.RoleCode === "admin") {
          can("manage", "all");
          this.ability.update(rules);

          return;
        }

        userRoles.forEach((role) => {
          let splittedRole = role.role.split(".");

          // can(action, subject)
          // action: what the user can do
          // subject: entity which you want to check user action

          can(this.getRoleType(splittedRole[1].toLowerCase()), splittedRole[0]);
        });
        
        this.ability.update(rules);
      });
    }
  }

  private getRoleType(type: string) {
    let describedType = "";

    switch (type) {
      case "c":
        describedType = "create"
        break;
      case "r":
        describedType = "read"
        break;
      case "u":
        describedType = "update"
        break;
      case "d":
        describedType = "delete"
        break;
    }

    return describedType;
  }

  setUserPermissions(userProfile: any) {
    this.updateAbility(userProfile);
  }

  clearUserPermissions() {
    this.ability.update([]);
  }
}