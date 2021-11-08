import { ApiService } from '../../services/api-service/api.service';
import { MatchingTypes, MatchTypeEnum, StringMatchTypeEnum, NumberMatchTypeEnum, SortEnum } from '../../services/query-builder/enums';
import { QueryBuilder } from '../../services/query-builder/query-builder';
import { HttpClient } from '@angular/common/http';
import { Sort, SortInfo } from '../../models/SortInfo';
import { Component, ContentChild, ContentChildren, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { PageInfo } from 'src/app/models/PageInfo';
import { DataColumn, DataTypes } from 'src/app/models/DataColumn';
import { Filter, FilterInfo } from 'src/app/models/FilterInfo';
import { LazyLoadEvent } from 'primeng/api';
import { Query } from 'src/app/services/query-builder/query';
import { CurrencyPipe, DatePipe, DecimalPipe } from '@angular/common';
import { Observable, Subject } from 'rxjs';

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
  @Input() refresh: Subject<boolean> = new Subject<boolean>();
  @ContentChildren('column') additionalColumns: TemplateRef<any>;
  @ContentChildren('row') additionalRows: TemplateRef<any>;
  @ContentChild('headerButton') additionalButton: TemplateRef<any>;

  constructor(private apiService: ApiService) {
    this.paginationInfo.Size = 5;
    this.paginationInfo.TotalElements = 0;
    this.columns = [];
  }

  ngOnInit(): void {
    if (this.entity) {
      this.apiService.GetFromAPI(this.entity, "GetDataColumns").subscribe((result: DataColumn[]) => {
        if (result.length > 0) {
          this.columns = result;
          this.getData();

          this.isLoaded = true;
        }
      }, err => console.error(err));
    }
  }

  //#region -- Events
  onInsert() {
    this.insertEvent.emit(true);
  }

  onEdit(entity: any) {
    this.editEvent.emit(entity);
  }

  onDelete(entity: any) {
    this.deleteEvent.emit(entity);
  }
  //#endregion

  //#region -- PrimeNG Lazy loading
  onLazyLoad(event: LazyLoadEvent) {
    let queryBuilder: QueryBuilder = new QueryBuilder(`list${this.entity}`, true, true);
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
          
          if (typeof event.filters[key].value === 'boolean') {
            event.filters[key].matchMode = "equals";
          }

          queryFilter.AddCondition(key, this.getMatchMode(event.filters[key].matchMode), event.filters[key].value);
        }
      });
    }

    this.getData(queryBuilder.GetQuery());
  }
  //#endregion

  //#region -- Mount pipe
  public getPipeType(dateType: any) {
    switch (dateType) {
      case DataTypes.Date:
        return DatePipe;
      case DataTypes.Currency:
        return CurrencyPipe;
      case DataTypes.Double:
        return DecimalPipe;
      case DataTypes.Time:
        return "time";
    }
  }

  public getPipeArguments(dateType: any) {
    switch (dateType) {
      case DataTypes.Date:
        return 'shortDate';
      case DataTypes.Currency:
        return 'BRL';
      case DataTypes.Double:
        return '1.3-3';
    }
  }
  //#endregion

  //#region -- Mount data
  getColumnsToFetch(queryBuilder: QueryBuilder): QueryBuilder {
    let hasIdentifier: boolean = false;
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

          if (element == "id") {
            hasIdentifier = true;
          }

          entityBuilder.AddColumn(element);
        });

        return;
      }

      if (column.PropertyName == "id") {
        hasIdentifier = true;
      }

      queryBuilder.AddColumn(column.PropertyName);
    });

    if (!hasIdentifier) {
      queryBuilder.AddColumn("id");
    }

    return queryBuilder;
  }

  getData(query?: Query) {
    if (!query) {
      let queryBuilder: QueryBuilder = new QueryBuilder(`list${this.entity}`, true, true);
      queryBuilder = this.getColumnsToFetch(queryBuilder);
      queryBuilder.AddPagination(0, this.paginationInfo.Size);

      query = queryBuilder.GetQuery();
    }

    console.log(query.ToString())
    this.apiService.GetFromGraphQL(query).subscribe((result) => {
      if (result && result.items.length > 0) {
        this.rows = result.items;

        this.paginationInfo.TotalElements = result.totalCount;
      } else {
        this.paginationInfo.TotalElements = 0;
      }
    }, err => console.error(err));
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

    if (typeof result === 'boolean') {
      return result ? "Sim" : "Não";
    }
    
    return result;
  }
  //#endregion

  //#region -- HELPER'S
  convertDataType(dataType: number) {
    let convertedDataType = "text";

    if (dataType != DataTypes.String) {
      switch (dataType) {
        case DataTypes.Int:
          convertedDataType = "numeric"
          break;
        case DataTypes.Date:
          convertedDataType = "date"
          break;
        case DataTypes.Boolean:
          convertedDataType = "boolean"
          break;
        case DataTypes.Double:
          convertedDataType = "numeric"
          break;
        case DataTypes.Currency:
          convertedDataType = "numeric"
          break;
      }
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
  //#endregion
}