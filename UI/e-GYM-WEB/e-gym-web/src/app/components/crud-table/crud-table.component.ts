import { ApiService } from './../../services/api-service/api.service';
import { MatchingTypes, MatchTypeEnum, StringMatchTypeEnum, NumberMatchTypeEnum, SortEnum } from './../../services/query-builder/enums';
import { QueryBuilder } from './../../services/query-builder/query-builder';
import { HttpClient } from '@angular/common/http';
import { Sort, SortInfo } from './../../models/SortInfo';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { PageInfo } from 'src/app/models/PageInfo';
import { DataColumn } from 'src/app/models/DataColumn';
import { Filter, FilterInfo } from 'src/app/models/FilterInfo';
import { LazyLoadEvent } from 'primeng/api';
import { Query } from 'src/app/services/query-builder/query';

@Component({
  selector: 'app-crud-table',
  templateUrl: './crud-table.component.html',
  styleUrls: ['./crud-table.component.scss']
})
export class CrudTableComponent implements OnInit {

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
  @Input() entity: string = "Shift";

  constructor(private apiService: ApiService) {
    this.paginationInfo.Size = 5;
    this.columns = [];

    this.apiService.GetFromApi(this.entity, "GetDataColumns").subscribe((result: DataColumn[]) => {
      this.columns = result;
      this.getData();

      this.isLoaded = true;
    });
  }

  ngOnInit(): void {
  }

  public getData(query?: Query) {
    console.log(query);
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

      console.log(this.rows);
    });
  }

  public getColumnsToFetch(queryBuilder: QueryBuilder): QueryBuilder{
    this.columns.forEach((column) => {
      if (column.PropertyName.includes(".")) {
        let splittedColumn = column.PropertyName.split(".");

        splittedColumn.forEach((element, index) => {

          if (index < splittedColumn.length) {
            queryBuilder.AddEntity(element);

            return;
          }

          queryBuilder.AddColumn(element);
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

  openNew() {
    console.log("Abriu");
    this.insertEvent.emit(true);
  }

  editProduct(object: any) {
    console.log(`Editar ${object}?`);
    this.editEvent.emit(object);
  }

  deleteProduct(object: any) {
    console.log(`Deletar ${object}?`);
    this.deleteEvent.emit(object);
  }

  saveProduct() {
    console.log(`Salvo com sucesso`);
    this.insertEvent.emit(true);
  }

  public onLoadTest(event) {
    if (event.sortField) {
      this.sortInfo.Clear();

      let sort = new Sort();

      sort.Property = event.sortField;
      sort.Order = event.sortOrder;

      this.sortInfo.AddSortableColumns(sort);
    }

    this.paginationInfo.Skip = event.first;
    this.paginationInfo.Take = event.rows;

    if (event.filters) {
      let keys = Object.keys(event.filters);

      this.filterInfo.Clear();

      keys.forEach(key => {
        if (event.filters[key].value != null) {
          let filter = new Filter();

          filter.Property = key;
          filter.Value = event.filters[key].value;

          this.filterInfo.AddFilter(filter);
        }
      });
    }
  }

  getContentOfColumn(column, value): any {
    let result = "-";

    if (value) {
      if (column.PropertyName.includes(".")) {
        let splittedColumn = column.PropertyName.split(".");
        result = value;

        splittedColumn.forEach(element => {
          result = result[element];
        });
      } else {
        result = value[column.PropertyName];
      }
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