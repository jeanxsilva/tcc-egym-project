import { QueryBuilder } from './../../../services/query-builder/query-builder';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api-service/api.service';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import Swal from 'sweetalert2';
import { IFormBase } from 'src/app/models/CrudBase';

@Component({
  selector: 'app-modality-form',
  templateUrl: './modality-form.component.html',
  styleUrls: ['./modality-form.component.scss']
})
export class ModalityFormComponent implements OnInit, IFormBase {
  public isNew: boolean = true;
  public formEntity: FormGroup;
  public modalityId: number;
  public modality: Modality = new Modality();

  constructor(private formBuilder: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute, private apiService: ApiService) {
    this.activatedRoute.params.subscribe(result => {
      if (result && result.id) {
        this.modalityId = parseInt(result.id);
        this.isNew = false;

        this.loadModality();
      }
    });

    this.formEntity = this.formBuilder.group({
      id: [0],
      description: ['', Validators.required],
      price: [0, Validators.required],
      daysInWeek: [0, Validators.required]
    });
  }

  ngOnInit(): void {
  }

  public updateForm() {
    this.formEntity.patchValue({
      id: this.modality.id,
      description: this.modality.description,
      price: this.modality.price,
      daysInWeek: this.modality.daysInWeek
    });
  }

  public loadModality() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listModality");
    queryBuilder.AddColumn("id").AddColumn("description").AddColumn("price").AddColumn("daysInWeek");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.modalityId);

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      console.log(result);
      this.modality = result.items[0];
      this.updateForm();
    }, err => {
      console.error(err);
    });
  }

  public save() {
    if (this.formEntity.valid) {
      let entity = this.formEntity.value;
      this.apiService.SendToAPI("Modality", "Save", entity).subscribe((result: any) => {
        console.log(result);
        if (result.HasError == false) {
          this.formEntity.markAsPristine();
          this.router.navigate(['modalities']);
        }
      }, err => {
        console.error(err);
      });
    }
  }

  public cancel() {
    this.router.navigate(['modalities']);
  }
}

export class Modality {
  public id: number = 0;
  public description: string;
  public price: number;
  public daysInWeek: number;
}