export enum DataTypes {
    Int = 0,
    String = 1,
    Date = 2,
    Boolean = 3,
    Double = 4,
    Currency = 5
}

export class DataColumn {
    public PropertyName;
    public LabelDescription;
    public DataType;
}