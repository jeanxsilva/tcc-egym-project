import { formatDate } from '@angular/common';
import { Component, OnInit, EventEmitter, Output, Input, OnChanges, SimpleChanges, ElementRef, ViewChild } from '@angular/core';
import { CalendarOptions, DateSelectArg, EventApi, EventClickArg } from '@fullcalendar/angular';
import ptLocale from '@fullcalendar/core/locales/pt-br';

@Component({
  selector: 'app-fullcalendar',
  templateUrl: './fullcalendar.component.html',
  styleUrls: ['./fullcalendar.component.scss']
})
export class FullcalendarComponent implements OnInit {
  @Output() onClickEvent = new EventEmitter<EventClickArg>();
  @Output() onAddEvent = new EventEmitter<DateSelectArg>();
  @Input() options: any;
  @Input() events: EventApi[] = [];

  public dateNow = formatDate(new Date(), 'yyyy-MM-dd hh:mm', 'pt-br').toString();
  public dateEnd = formatDate(new Date().setHours(new Date().getHours() + 168), 'yyyy-MM-dd', 'pt-br').toString();
  public locales = [ptLocale];
  public calendarOptions: CalendarOptions = {
    select: this.handleDateSelect.bind(this),
    eventClick: this.handleDateClick.bind(this),
    locales: this.locales,
    initialView: 'timeGridWeek',
    weekends: true,
    allDaySlot: false,
    editable: true,
    selectable: true,
    selectMirror: true,
    dayMaxEvents: true,
    hiddenDays: [0],
    showNonCurrentDates: false,
    defaultTimedEventDuration: 30,
    validRange: {
      start: this.dateNow,
      end: this.dateEnd
    },
    eventConstraint: "businessHours",
    headerToolbar: {
      left: 'prev,next',
      center: 'title',
      right: 'timeGridWeek,timeGridDay'
    },
    businessHours: [{
      daysOfWeek: [1, 2, 3, 4, 5],
      startTime: '06:00',
      endTime: '23:00'
    }, {
      daysOfWeek: [6],
      startTime: '06:00',
      endTime: '12:00'
    }],
  };

  constructor() { }

  ngDoCheck(): void {
    this.calendarOptions = { ...this.calendarOptions, ...this.options };
  }

  ngOnInit(): void {
  }

  handleDateSelect(selectInfo: DateSelectArg) {
    this.onAddEvent.emit(selectInfo);
  }

  handleDateClick(clickInfo: EventClickArg) {
    this.onClickEvent.emit(clickInfo);
  }
}

export class CalendarEvent {
  public id: string;
  public title: string;
  public start: string;
  public end: string;
  public eventBackgroundColor?: string;

}