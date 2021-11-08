import { ApiService } from 'src/app/services/api-service/api.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { QueryBuilder } from 'src/app/services/query-builder/query-builder';
import { SortEnum } from 'src/app/services/query-builder/enums';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-last-news-list',
  templateUrl: './last-news-list.component.html',
  styleUrls: ['./last-news-list.component.scss']
})
export class LastNewsListComponent implements OnInit {
  public lastNews: any[] = [];

  constructor(private router: Router, private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadLastNews();
  }

  private loadLastNews() {
    let queryBuilder: QueryBuilder = new QueryBuilder("listLastNews");
    queryBuilder.AddSort("registerDateTime", SortEnum.DESC);
    queryBuilder.AddColumn("id").AddColumn("title").AddColumn("content").AddColumn("options").AddColumn("photoUrl").AddColumn("expireDateTime").AddColumn("registerDateTime")
      .AddEntity("publishedByUser").AddColumn("id").AddColumn("name");

    console.log(queryBuilder.GetQuery().ToString());
    this.apiService.GetFromGraphQL(queryBuilder.GetQuery()).subscribe(result => {
      this.lastNews = result.items;
    });
  }

  public edit(id: number) {
    this.router.navigate(['news', id]);
  }

  public remove(entity) {
    Swal.fire({
      title: 'Tem certeza que deseja remover?',
      text: 'Não sera possivel recuperar este registro!',
      icon: 'warning',
      showCancelButton: true,
      customClass:{
        cancelButton: 'bg-default',
        confirmButton: 'bg-danger'
      },
      confirmButtonText: 'Sim, remover!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.value) {
        this.apiService.SendToAPI("LastNews", "Delete", entity).subscribe((result) => {
          console.log(result);
          if (result.HasError === false) {
            Swal.fire(
              'Removido!',
              'O arquivo foi removido com sucesso.',
              'success'
            );

            this.loadLastNews();
          }
        }, (err) => {
          console.error(err);
        });
      }
    });
  }

  public create() {
    this.router.navigate(['news/register']);
  }
}