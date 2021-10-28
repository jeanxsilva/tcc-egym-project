import { formatDate } from '@angular/common';
import { EventEmitter, Injectable, Input, Output } from '@angular/core';
import { EventClickArg, DateSelectArg, EventApi, CalendarOptions } from '@fullcalendar/angular';
import ptLocale from '@fullcalendar/core/locales/pt-br';

@Injectable({
  providedIn: 'root'
})
export class FullCalendarService {
  @Output() onClickEvent = new EventEmitter<EventClickArg>();
  @Output() onAddEvent = new EventEmitter<DateSelectArg>();
  @Input() options: any;
  @Input() events: EventApi[] = [];

  public handleDateSelect = new Function();
  public handleDateClick = new Function();

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

  public SetHandleSelectEvent(newFunction: Function) {
    this.handleDateSelect = newFunction;
  }

  public SetHandleDateClick(newFunction: Function) {
    this.handleDateClick = newFunction;
  }

  public ChangeOptions(options: CalendarOptions) {
    this.calendarOptions = options;
  }

  public AddOptions(options: CalendarOptions) {
    this.calendarOptions = Object.assign({}, this.calendarOptions, options);
  }
}
