import { ApiService } from '../../services/api-service/api.service';
import { MatchingTypes, MatchTypeEnum, StringMatchTypeEnum, NumberMatchTypeEnum, SortEnum } from '../../services/query-builder/enums';
import { QueryBuilder } from '../../services/query-builder/query-builder';
import { HttpClient } from '@angular/common/http';
import { Sort, SortInfo } from '../../models/SortInfo';
import { Component, ContentChild, ContentChildren, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { PageInfo } from 'src/app/models/PageInfo';
import { DataColumn } from 'src/app/models/DataColumn';
import { Filter, FilterInfo } from 'src/app/models/FilterInfo';
import { LazyLoadEvent } from 'primeng/api';
import { Query } from 'src/app/services/query-builder/query';

@Component({
  selector: 'app-custom-table',
  templateUrl: './custom-table.component.html',
  styleUrls: ['./custom-table.component.scss']
})
export class CustomTableComponent implements OnInit {

  public paginationInfo: PageInfo = new PageInfo();
  public sortInfo: SortInfo = new SortInfo();
  public filterInfo: FilterInfo = new FilterInfo();
  public isLoading: boolean = false;
  public isLoaded: boolean = false;
  public rows: any[] = [];
  public columns: DataColumn[] = [];

  @Output() editEvent: EventEmitter<any> = new EventEmitter<any>();
  @Output() deleteEvent: EventEmitter<any> = new EventEmitter<any>();
  @Output() insertEvent: EventEmitter<any> = new EventEmitter<any>();
  @Input() isCrud: boolean = true;
  @Input() entity: string;
  @Input() title: string;
  @ContentChildren('column') additionalColumns: TemplateRef<any>;
  @ContentChildren('row') additionalRows: TemplateRef<any>;

  constructor(private apiService: ApiService) {
    this.paginationInfo.Size = 5;
    this.paginationInfo.TotalElements = 0;
    this.columns = [];
  }

  ngOnInit(): void {
    if (this.entity) {
      this.apiService.GetFromAPI(this.entity, "GetDataColumns").subscribe((result: DataColumn[]) => {
        console.log(result);
        if (result.length > 0) {
          this.columns = result;
          this.getData();

          this.isLoaded = true;
        }
      }, err => console.error(err));
    }
  }

  public getData(query?: Query) {
    if (!query) {
      let queryBuilder: QueryBuilder = new QueryBuilder(`list${this.entity}`)
      queryBuilder = this.getColumnsToFetch(queryBuilder);
      queryBuilder.AddPagination(0, this.paginationInfo.Size);

      query = queryBuilder.GetQuery();
    }

    console.log(query.ToString());
    this.apiService.GetFromGraphQL(query).subscribe((result) => {
      this.rows = result.items;

      this.paginationInfo.TotalElements = result.totalCount;
    }, err => console.error(err));
  }

  public getColumnsToFetch(queryBuilder: QueryBuilder): QueryBuilder {
    this.columns.forEach((column) => {
      if (column.PropertyName.includes(".")) {
        let splittedColumn = column.PropertyName.split(".");
        let entityBuilder = queryBuilder.AddEntity(splittedColumn[0]);

        splittedColumn.forEach((element, index) => {
          if (index < splittedColumn.length - 1) {
            if (index != 0) {
              entityBuilder = entityBuilder.AddEntity(element);
            }

            return;
          }

          entityBuilder.AddColumn(element);
        });

        return;
      }

      queryBuilder.AddColumn(column.PropertyName);
    });

    return queryBuilder;
  }

  onLazyLoad(event: LazyLoadEvent) {
    let queryBuilder: QueryBuilder = new QueryBuilder(`list${this.entity}`);
    queryBuilder = this.getColumnsToFetch(queryBuilder);
    queryBuilder.AddPagination(event.first, event.rows);

    if (event.sortField) {
      let order = SortEnum.ASC;

      if (event.sortOrder == -1) {
        order = SortEnum.DESC;
      }

      queryBuilder.AddSort(event.sortField, order);
    }

    if (event.filters) {
      let keys = Object.keys(event.filters);
      let queryFilter = queryBuilder.CreateFilter();

      keys.forEach(key => {
        if (event.filters[key].value != null) {
          queryFilter.AddCondition(key, this.getMatchMode(event.filters[key].matchMode), event.filters[key].value);
        }
      });
    }

    this.getData(queryBuilder.GetQuery());
  }

  insertEntity() {
    this.insertEvent.emit(true);
  }

  editEntity(entity: any) {
    this.editEvent.emit(entity);
  }

  deleteEntity(entity: any) {
    this.deleteEvent.emit(entity);
  }

  getContentOfColumn(column, value): any {
    let result = "-";

    if (value) {
      if (column.PropertyName.includes(".")) {
        let splittedColumn = column.PropertyName.split(".");
        result = value;

        splittedColumn.forEach(element => {
          if (!result[element]) {
            return;
          }

          result = result[element];
        });
      } else {
        result = value[column.PropertyName];
      }
    }

    if (typeof result === 'object') {
      return '';
    }

    return result;
  }

  convertDataType(dataType: number) {
    let convertedDataType = "text";

    switch (dataType) {
      case 0:
        convertedDataType = "numeric"
        break;
      case 1:
        convertedDataType = "text"
        break;
      case 2:
        convertedDataType = "date"
        break;
      case 3:
        convertedDataType = "boolean"
        break;
    }

    return convertedDataType;
  }

  private getMatchMode(matchMode: string): MatchingTypes {
    let convertedMatchMode: MatchingTypes = MatchTypeEnum.EQUALS;

    switch (matchMode) {
      case "equals":
        convertedMatchMode = MatchTypeEnum.EQUALS;
        break;
      case "contains":
        convertedMatchMode = StringMatchTypeEnum.CONTAINS;
        break;
      case "startsWith":
        convertedMatchMode = StringMatchTypeEnum.STARTS_WITH;
        break;
      case "endsWith":
        convertedMatchMode = StringMatchTypeEnum.ENDS_WITH;
        break;
      case "notContains":
        convertedMatchMode = StringMatchTypeEnum.NOT_CONTAINS;
        break;
      case "notEquals":
        convertedMatchMode = MatchTypeEnum.NOT_EQUALS;
        break;
      case "lt":
        convertedMatchMode = NumberMatchTypeEnum.LESS_THAN;
        break;
      case "lte":
        convertedMatchMode = NumberMatchTypeEnum.LESS_THAN_OR_EQUALS;
        break;
      case "gt":
        convertedMatchMode = NumberMatchTypeEnum.GREATER_THAN;
        break;
      case "gte":
        convertedMatchMode = NumberMatchTypeEnum.GREATER_THAN_OR_EQUALS;
        break;
    }

    return convertedMatchMode;
  }
}