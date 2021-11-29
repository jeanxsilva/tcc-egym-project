import { Component, ContentChild, Input, OnInit, TemplateRef } from '@angular/core';
import { MenuController } from '@ionic/angular';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  @Input() title: string = '';
  @Input() redirectToLast: boolean = false;
  @Input() defaultBack: string;
  @Input() withOverflow: boolean = false;
  @ContentChild('additional') additional: TemplateRef<any>;

  constructor() {
  }

  ngOnInit() {
  }

}
