import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-last-news-card',
  templateUrl: './last-news-card.component.html',
  styleUrls: ['./last-news-card.component.scss']
})
export class LastNewsCardComponent implements OnInit {
  @Input() news: any;
  @Output() onEdit: EventEmitter<any> = new EventEmitter<any>();
  @Output() onRemove: EventEmitter<any> = new EventEmitter<any>();

  constructor() { }

  ngOnInit(): void {
  }

  public edit(){
    this.onEdit.emit(this.news.id);
  }

  public remove(){
    this.onRemove.emit(this.news);
  }
}