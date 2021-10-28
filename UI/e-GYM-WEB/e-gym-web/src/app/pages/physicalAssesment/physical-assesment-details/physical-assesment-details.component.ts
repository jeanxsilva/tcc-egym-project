import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/services/api-service/api.service';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';

@Component({
  selector: 'app-physical-assesment-details',
  templateUrl: './physical-assesment-details.component.html',
  styleUrls: ['./physical-assesment-details.component.scss']
})
export class PhysicalAssesmentDetailsComponent implements OnInit {
  public assessmentId: number;
  public assessment: any;

  constructor(private apiService: ApiService, private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router) {
    this.activatedRoute.params.subscribe(result => {
      if (result && result.id) {
        this.assessmentId = parseInt(result.id);
        this.loadAssessment();
      }
    });
  }

  ngOnInit(): void {
  }

  private loadAssessment() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listPhysicalAssesment");
    let queryFilter = queryBuilder.CreateFilter();
    queryFilter.AddCondition("id", MatchTypeEnum.EQUALS, this.assessmentId);

    // public virtual StudentCaracteristic StudentCaracteristics { get; set; }
    queryBuilder.AddColumn("id").AddEntity("registeredByEmployee").AddEntity("user").AddColumn("id").AddColumn("name");
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
      this.assessment = result.items[0];
      console.log(this.assessment)
    }, err => {
      console.error(err);
    });
  }

}
