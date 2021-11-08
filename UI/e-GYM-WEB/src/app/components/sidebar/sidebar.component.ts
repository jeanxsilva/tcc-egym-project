import { QueryBuilder } from './../../services/query-builder/query-builder';
import { ApiService } from './../../services/api-service/api.service';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth-service.ts/auth-service.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  @Input() disable: boolean = false;
  public menuItems: any[];
  public isCollapsed = true;

  constructor(private router: Router, private apiService: ApiService, private authService: AuthService) { }

  ngOnInit() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listUserLevelAccess");
    queryBuilder.AddPagination(0, 1000);
    queryBuilder.AddColumn("id").AddColumn("hasChild").AddColumn("parentId")
      .AddColumn("description").AddColumn("path").AddColumn("iconKey");

    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe((result: any) => {
      this.menuItems = this.mountSidebar(result.items);
    });
  }

  mountSidebar(data: any[]) {
    let menu: MenuItem[] = [];

    data.forEach((item) => {
      let menuItem: MenuItem = {
        Description: item.description,
        Path: item.path,
        Icon: item.iconKey,
        Children: [],
        Collapsed: false
      };

      if (item.hasChild) {
        let children = data.filter((o) => o.parentId === item.id);
        menuItem.Children = this.mountSidebar(children);

        children.forEach((child) => {
          data.forEach((itemData, index) => {

            if (itemData.id === child.id) {
              data.splice(index, 1);
            }
          })
        })
      }

      menu.push(menuItem);
    });

    return menu;
  }

  logout() {
    this.authService.RealizeLogOut();
  }
}

export class MenuItem {
  constructor() {
  }

  public Description: string;
  public Icon: string;
  public Path: string;
  public Children: MenuItem[] = [];
  public Collapsed: boolean = false;
}