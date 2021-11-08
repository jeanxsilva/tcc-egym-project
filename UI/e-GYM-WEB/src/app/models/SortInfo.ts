export class SortInfo {
    private _sortableColumns: Sort[] = [];

    constructor() { }

    GetSortableColumns() {
        return this._sortableColumns;
    }

    AddSortableColumns(value: Sort) {
        let existentIndex = this._sortableColumns.findIndex(item => item.Property === value.Property);

        console.log(existentIndex);

        if (existentIndex != -1) {
            this._sortableColumns[existentIndex] = value;

            return;
        }

        this._sortableColumns.push(value);
    }
    
    Clear(){
        this._sortableColumns = [];
    }
}

export class Sort {
    public Property: string;
    public Order: string;
}