import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApiService } from 'src/app/services/api-service/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { formatDate } from '@angular/common';
import { MatchTypeEnum } from 'src/app/services/query-builder/enums';
import Swal from 'sweetalert2';
import { IFormBase } from 'src/app/models/CrudBase';

@Component({
  selector: 'app-last-news-screen',
  templateUrl: './last-news-screen.component.html',
  styleUrls: ['./last-news-screen.component.scss']
})
export class LastNewsScreenComponent implements OnInit, IFormBase {
  public newsId: number;
  public news: any;
  public isNew: boolean = true;
  public formEntity: FormGroup;

  constructor(private activatedRoute: ActivatedRoute, private apiService: ApiService, private formBuilder: FormBuilder, private router: Router) {
    this.activatedRoute.params.subscribe(result => {
      if (result && result.id) {
        this.isNew = false;
        this.newsId = parseInt(result.id);
        this.loadNews();
      }
    });

    this.formEntity = this.formBuilder.group({
      id: [0],
      title: ['', Validators.required],
      content: ['', Validators.required],
      photoUrl: [''],
      options: [''],
      expireDateTime: [new Date(), Validators.required],
      registerDateTime: [new Date()],
      publishedByUserId: [0]
    });
  }

  ngOnInit(): void {
  }

  private updateForm() {
    this.formEntity.patchValue({
      id: this.news.id,
      title: this.news.title,
      content: this.news.content,
      photoUrl: this.news.photoUrl,
      options: this.news.options,
      expireDateTime: formatDate(this.news.expireDateTime, 'yyyy-MM-dd', 'en'),
      registerDateTime: this.news.registerDateTime,
      publishedByUserId: this.news.publishedByUser?.id
    });
  }

  private loadNews() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listLastNews");
    queryBuilder.CreateFilter().AddCondition("id", MatchTypeEnum.EQUALS, this.newsId);
    queryBuilder.AddColumn("id").AddColumn("title").AddColumn("content").AddColumn("options").AddColumn("photoUrl").AddColumn("expireDateTime").AddColumn("registerDateTime")
      .AddEntity("publishedByUser").AddColumn("id").AddColumn("name");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.news = result.items[0];
      this.updateForm();
    });
  }

  public save() {
    if (this.formEntity.valid) {
      let entity = this.formEntity.value;
      this.apiService.SendToAPI("LastNews", "Save", entity).subscribe(result => {
        if (result.HasError === false) {
          this.formEntity.markAsPristine();
          this.router.navigate(['last-news']);
        }
      });
    }
  }

  public cancel() {
    this.router.navigate(['last-news']);
  }
}