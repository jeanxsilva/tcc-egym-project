import { Column } from "./column/column";
import { StringMatchTypeEnum } from "./enums";
import { ComplexFilterField, Filter, FilterField, FilterListField } from "./filter/filter";
import { Operator } from "./filter/filter-builder";

export class Query {
  constructor(queryName: string) {
    this.QueryName = queryName;
    this.Filter = new Filter();
  }

  public QueryName: string;
  public Parameters: QueryParameter[] = [];
  public Pagination: QueryParameter[];
  public Sort: QueryParameter[] = [];
  public Filter: Filter;
  public Columns: Column[] = [];
  public IsCollection: boolean = true;

  /**
   * Return the query string.
   *
   * @returns Return the query string.
   *
  */
  public ToString(): string {
    let parameters = this.assembleParameters();
    parameters = parameters && parameters.length !== 0 ? `(${parameters})` : parameters;

    let query = `
      {
        ${this.QueryName}${parameters}{
          ${this.IsCollection ? `items{${this.assembleColumns(this.Columns)}}, totalCount` : this.assembleColumns(this.Columns)}
        }
      }`;

    return query;
  }

  //#region 'Montagem da query'

  private assembleColumns(columns: Column[]): string {
    let queryColumns: string = "";

    columns.forEach((column, index) => {
      queryColumns += column.Name;

      if (column.Children && column.Children.length > 0) {
        queryColumns += ` {${this.assembleColumns(column.Children)}} `;

      }

      if (index + 1 < columns.length) {
        queryColumns += ", ";
      }
    });

    return queryColumns;
  }

  private assemblePagination(queryParameters?: string): string {
    if (this.Pagination) {
      this.Pagination.forEach((param, index) => {
        if (queryParameters && index == 0) {
          queryParameters += ", ";
        }

        queryParameters += `${param.field}: ${param.value}`;

        if (index < this.Pagination.length - 1) {
          queryParameters += ", ";
        }
      });
    }

    return this.assembleSort(queryParameters);
  }

  private assembleSort(queryParameters?: string): string {
    this.Sort.forEach((param, index) => {
      if (queryParameters) {
        queryParameters += ", ";
      }

      if (index == 0) {
        queryParameters += "order: [{";
      }

      // Funcionalidade até a construção do builder de sort.
      let splitedString = [];

      if (param.field.includes(".")) {
        splitedString = param.field.split(".");

        splitedString.forEach((entity, index) => {

          if (index < splitedString.length - 1) {
            queryParameters += `${entity}: {`;
          } else {
            queryParameters += `${entity}: ${param.value}}`;
          }

        });

        queryParameters += `}`;
      } else {
        queryParameters += `${param.field}: ${param.value}`;
      }

      if (index < this.Sort.length - 1) {
        queryParameters += ", ";
      } else {
        queryParameters += "}]";
      }
    });

    return this.assembleFilterFields(queryParameters);
  }

  private assembleParameters(): string {
    let queryParameters: string = null;

    if (this.Parameters) {
      queryParameters = "";
    }

    this.Parameters.forEach((param, index) => {
      queryParameters += `${param.field}: ${typeof (param.value) == "number" ? param.value : `"${param.value}"`}`;

      if (index < this.Parameters.length - 1) {
        queryParameters += ", ";
      }
    });

    return this.assemblePagination(queryParameters);
  }

  private assembleComplexFields(complexField: ComplexFilterField): string {
    let assembledComplexFields = `${complexField.Name}: {`;

    if (complexField.ComplexChildren.length > 0) {
      complexField.ComplexChildren.forEach(child => {
        assembledComplexFields += this.assembleComplexFields(child);
      });
    }

    if (complexField.Filters.length > 0) {
      if (complexField.ComplexChildren.length > 0) {
        assembledComplexFields += ", ";
      }

      complexField.Filters.forEach((filter, index) => {
        if (filter instanceof FilterField) {
          assembledComplexFields += `${filter.Field}: {${filter.Match}: ${typeof filter.Value === "string" ? `"${filter.Value}"` : filter.Value}}`;
        } else if (filter instanceof FilterListField) {
          assembledComplexFields += `${filter.ListField}: { ${filter.Match}: {${filter.FilterField.Field}: {${filter.FilterField.Match}: ${typeof filter.FilterField.Value === "string" ? `"${filter.FilterField.Value}"` : filter.FilterField.Value}}}}`;
        }

        if (index < complexField.Filters.length - 1) {
          assembledComplexFields += ", ";
        }
      });

    }

    assembledComplexFields += `}`;
    return assembledComplexFields;
  }

  private assembleFilterFields(queryParameters?: string) {
    queryParameters += queryParameters != "" ? ", " : "";

    if (this.Filter.Fields.length > 0 || this.Filter.Operators.length > 0) {
      queryParameters += "where: {";

      if (this.Filter.Fields.length > 0) {
        this.Filter.Fields.forEach((item, index) => {

          if (item instanceof FilterField) {
            queryParameters += `${item.Field}: { ${item.Match}: ${typeof item.Value === "string" ? `"${item.Value}"` : item.Value}}`;
          } else if (item instanceof FilterListField) {
            queryParameters += `${item.ListField}: { ${item.Match}: {${item.FilterField.Field}: {${item.FilterField.Match}: ${typeof item.FilterField.Value === "string" ? `"${item.FilterField.Value}"` : item.FilterField.Value}}}}`;
          } else if (item instanceof ComplexFilterField) {
            if (item) {
              queryParameters += this.assembleComplexFields(item);
            }
          }

          if (index < this.Filter.Fields.length - 1) {
            queryParameters += ", ";
          }
        });
      }

      if (this.Filter.Operators.length > 0) {
        queryParameters += this.Filter.Fields.length > 0 ? ", " : "";
        queryParameters += this.assembleOperator();
      }

      queryParameters += "}";
    }

    return queryParameters;
  }

  private assembleChild(children) {
    let assembled = "";
    children.forEach((child: Operator | Filter, index) => {
      if (child instanceof Operator) {
        assembled += `{${child.type}: [${this.assembleChild(child.children)}`;
        assembled += this.assembleFilterMember(child.filters, true);
        assembled += "]}";
      }
      else if (child instanceof Filter) {
        assembled += this.assembleFilterMember(child.Fields, true);
      }

      if (index < children.length - 1) {
        assembled += ", ";
      }
    });
    return assembled;
  }

  private assembleFilterMember(items, withBrackets = false) {
    let filters = "";

    items.forEach((element, index) => {
      filters += withBrackets ? "{" : "";
      
      let splitedString = [];

      if (element.Field.includes(".")) {
        splitedString = element.Field.split(".");

        splitedString.forEach((entity, index) => {
          if (index < splitedString.length - 1) {
            filters += `${entity}: { `;
          } else {
            filters += `${entity}: {${element.Match}: ${typeof element.Value === "string" ? `"${element.Value}"` : element.Value}}`;
            filters += ` }`;
          }
        });
        
      } else {
        filters += `${element.Field}: {${element.Match}: ${typeof element.Value === "string" ? `"${element.Value}"` : element.Value}}`;
      }
      
      filters += withBrackets ? "}" : "";

      if (index < items.length - 1) {
        filters += ", ";
      }
    });

    return filters;
  }

  private assembleOperator() {
    let assembled = "";

    this.Filter.Operators.forEach((item, index) => {

      assembled += `${item.type}: [${this.assembleChild(item.children)}${item.children.length > 0 && item.filters.length > 0 ? ", " : ""}${this.assembleFilterMember(item.filters, true)}]`;

      if (index < this.Filter.Operators.length - 1) {
        assembled += ", ";
      }
    });

    return assembled;
  }

  //#endregion
}

export class QueryParameter {
  constructor(field: string, value: any) {
    this.field = field;
    this.value = value;
  }

  public field: string;
  public value: any;
}

