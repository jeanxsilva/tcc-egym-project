import { FullCalendarService } from './../../../services/full-calendar.service';
import { Router } from '@angular/router';
import { QueryBuilder } from './../../../services/query-builder/query-builder';
import { ApiService } from 'src/app/services/api-service/api.service';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { CalendarEvent } from 'src/app/components/calendar/fullcalendar/fullcalendar.component';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { formatDate } from '@angular/common';
@Component({
  selector: 'app-physical-assesment-schedule-list',
  templateUrl: './physical-assesment-schedule-list.component.html',
  styleUrls: ['./physical-assesment-schedule-list.component.scss']
})
export class PhysicalAssesmentScheduleListComponent implements OnInit {
  public isLoading: boolean = false;
  public physicalAssesmentScheduledList: any[] = [];
  public events: CalendarEvent[] = new Array<CalendarEvent>();
  public options: any = { initialView: 'timeGridDay'};
  @ViewChild('confirmModal') confirmModal: TemplateRef<any>;
  @ViewChild('dialogModal') dialogModal: TemplateRef<any>;

  constructor(private apiService: ApiService, private ngbModal: NgbModal, private router: Router) {
    console.log(this.options)
    this.loadAssesmentScheduled();
  }

  ngOnInit(): void {
  }

  private loadAssesmentScheduled() {
    this.isLoading = true;
    let queryBuilder: QueryBuilder = new QueryBuilder("listPhysicalAssesmentScheduled");
    queryBuilder.CreateFilter().AddCondition("wasCanceled", MatchTypeEnum.EQUALS, false).AddCondition("wasAnswered", MatchTypeEnum.EQUALS, false)
    queryBuilder.AddColumn("id").AddColumn("wasCanceled").AddColumn("wasAnswered").AddColumn("scheduledToDate")
      .AddEntity("studentRegistration").AddColumn("id").AddColumn("code").AddEntity("user").AddColumn("id").AddColumn("name");
    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.physicalAssesmentScheduledList = result.items;
      this.mountEvents();
    });
  }

  private mountEvents() {
    if (this.physicalAssesmentScheduledList.length > 0) {
      this.physicalAssesmentScheduledList.forEach(physicalAssesmentScheduled => {
        let event: CalendarEvent = new CalendarEvent();
        event.id = physicalAssesmentScheduled.id;
        event.start = formatDate(new Date(physicalAssesmentScheduled.scheduledToDate), "yyyy-MM-dd HH:mm", 'pt-br');
        event.end = formatDate(new Date(physicalAssesmentScheduled.scheduledToDate).setMinutes(30), "yyyy-MM-dd HH:mm", 'pt-br');
        event.title = physicalAssesmentScheduled.studentRegistration?.user?.name;

        this.events.push(event);
        this.isLoading = false;
      });

      this.options.events = this.events;
    }
  }

  public handleEvent(clickInfo) {
    let modal = this.ngbModal.open(this.confirmModal, { centered: true, size: 'sm' });
    modal.result.then(doAssessment => {
      if (doAssessment) {
        this.doAssessment(parseInt(clickInfo.event.id));
      } else {
        this.throwDialogModal(parseInt(clickInfo.event.id));
      }
    }, err => {
      console.error(err)
    });
  }

  private throwDialogModal(scheduledId: number) {
    let modal = this.ngbModal.open(this.dialogModal, { centered: true, size: 'sm' });
    modal.result.then(confirm => {
      if (confirm) {
        this.cancelAssessment(scheduledId);
      }

      // clickInfo.event.remove();
    }, err => {
      console.error(err)
    });
  }

  private doAssessment(scheduledId: number) {
    console.log("Redirect")
    this.router.navigate([`assessment/register/${scheduledId}`]);
  }

  private cancelAssessment(scheduledId: number) {
    let physicalAssessmentScheduled = {
      id: scheduledId,
      wasCanceled: true
    }
    this.apiService.SendToAPI("PhysicalAssesmentScheduled", "UpdateScheduleStatus", physicalAssessmentScheduled).subscribe((result: any) => {
      if (result.HasError == false) {
        this.loadAssesmentScheduled();
      }
    });
  }
}