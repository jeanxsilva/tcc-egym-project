import { ApiService } from 'src/app/services/api-service/api.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';

@Component({
  selector: 'app-assesment-details',
  templateUrl: './assesment-details.component.html',
  styleUrls: ['./assesment-details.component.scss'],
})
export class AssesmentDetailsComponent implements OnInit {
  public assesmentId: number;
  public assesment: any;

  constructor(private activatedRoute: ActivatedRoute, private apiService: ApiService) {
    this.activatedRoute.params.subscribe(param => {
      this.assesmentId = parseInt(param.id);

      this.loadAssesment();
    });
  }

  ngOnInit() { }

  public loadAssesment() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPhysicalAssesment");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.assesmentId);

    queryBuilder.AddColumn("id").AddColumn("studentGoal").AddEntity("registeredByEmployee").AddEntity("user").AddColumn("id").AddColumn("name");
    queryBuilder.AddEntity("student").AddColumn("id").AddColumn("code").AddEntity("user").AddColumn("id").AddColumn("name").AddColumn("lastName");
    queryBuilder.AddEntity("studentCaracteristics")
      .AddColumn("id")
      .AddColumn("weight")
      .AddColumn("height")
      .AddColumn("triceps")
      .AddColumn("chest")
      .AddColumn("subaxillary")
      .AddColumn("subscapular")
      .AddColumn("abdominal")
      .AddColumn("suprailiac")
      .AddColumn("thigh")
      .AddColumn("leanMass")
      .AddColumn("fatMass")
      .AddColumn("fatPercentage")
      .AddColumn("bodyMassIndex")
      .AddColumn("ageAtMoment")
      .AddColumn("basalMetabolicRate")
      .AddColumn("bodyDensity");

    console.log(queryBuilder.GetQuery().ToString())
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.assesment = result.items[0];
    }, err => {
      console.error(err);
    });
  }
}