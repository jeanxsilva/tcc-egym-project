import { ApiService } from './../../services/api-service/api.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  public menuItems: any[];
  public isCollapsed = true;

  constructor(private router: Router, private apiService: ApiService) { }

  ngOnInit() {
    this.apiService.GetFromAPI("UserLevelAccess", "GetLevelAccess").subscribe((result: any[]) => {
      this.menuItems = this.mountSidebar(result);
    });
  }

  mountSidebar(data: any[]) {
    let menu: MenuItem[] = [];

    data.forEach((item) => {
      let menuItem: MenuItem = {
        Description: item.Description,
        Path: item.Path,
        Icon: item.IconKey,
        Children: [],
        Collapsed: false
      };

      if (item.HasChild) {
        let children = data.filter((o) => o.ParentId === item.Id);
        menuItem.Children = this.mountSidebar(children);

        children.forEach((child) => {
          data.forEach((itemData, index) => {

            if (itemData.Id === child.Id) {
              data.splice(index, 1);
            }
          })
        })
      }

      menu.push(menuItem);
    });

    return menu;
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