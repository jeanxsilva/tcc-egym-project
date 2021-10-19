import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-base-card',
  templateUrl: './base-card.component.html',
  styleUrls: ['./base-card.component.scss']
})
export class BaseCardComponent implements OnInit {
  @Input() hideFooter: boolean = false;
  @Input() hideHeader: boolean = false;
  @Input() title: string = "";
  @ViewChild('footer') footer: TemplateRef<any>;

  constructor(private location: Location) { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
  }
  
  goBack() {
    this.location.back();
  }
}