import { ColumnBuilder } from "./column/column-builder";
import { SortEnum } from "./enums";
import { FilterBuilder } from "./filter/filter-builder";
import { Query, QueryParameter } from "./query";

export class QueryBuilder {
  private query: Query;

  constructor(queryName: string, isCollection: boolean = true, parameters?: QueryParameter[]) {
    this.query = new Query(queryName);
    this.query.IsCollection = isCollection;

    if (parameters) {
      this.query.Parameters = parameters;
    }
  }

  /**
   * Creates a new column in the query.
   *
   * @param columnName - The column name in the database
   * @returns A new instance of ColumnBuilder
   *
  */
  public AddColumn(columnName: string): QueryBuilder {
    let column = new ColumnBuilder(this.query, columnName);

    return this;
  }

  /**
   * Creates a new column in the query.
   *
   * @param entityName - The column name in the database
   * @returns A new instance of ColumnBuilder
   *
  */
  public AddEntity(entityName: string): ColumnBuilder {
    let column = new ColumnBuilder(this.query, entityName);

    return column;
  }

  /**
   * Creates the pagination parameter in the query.
   *
   * @param skip - The number of results to skip
   * @param take - The number of results to take
   * @returns This instance of QueryBuilder.
   *
  */
  public AddPagination(skip: number, take: number): QueryBuilder {
    this.query.Pagination = [];

    this.query.Pagination.push(new QueryParameter("skip", skip));
    this.query.Pagination.push(new QueryParameter("take", take));

    return this;
  }

  /**
   * Create the sort parameter in the query.
   *
   * @param field - The field to sort by
   * @param value - The SortEnum value
   * @returns This instance of QueryBuilder.
   *
  */
  public AddSort(field: string, value: SortEnum): QueryBuilder {
    this.query.Sort.push(new QueryParameter(field, value));

    return this;
  }

  /**
   * Create a new parameter in the query.
   *
   * @param field - The field
   * @param value - The value
   * @returns This instance of QueryBuilder.
   *
  */
  public AddParameter(field: string, value: any): QueryBuilder {
    let param = new QueryParameter(field, value);
    this.query.Parameters.push(param);

    return this;
  }

  /**
   * Return the instance of FilterBuilder.
   *
   * @returns Return the instance of FilterBuilder.
   *
  */
  public CreateFilter(): FilterBuilder {
    return new FilterBuilder(this.query);
  }

  /**
   * Return the query object.
   *
   * @returns Return the query object.
   *
  */
  public GetQuery(): Query {
    return this.query;
  }
}