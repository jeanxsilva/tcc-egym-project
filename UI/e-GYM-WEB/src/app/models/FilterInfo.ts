export class FilterInfo {
    public _filters: Filter[];

    constructor() { }

    GetFilters(){
        return this._filters;
    }

    AddFilter(value: Filter){
        let existentIndex = this._filters.findIndex(item => item.Property === value.Property);

        console.log(existentIndex);

        if (existentIndex != -1) {
            this._filters[existentIndex] = value;

            return;
        }

        this._filters.push(value);
    }

    Clear(){
        this._filters = [];
    }
}

export class Filter{
    public Property: string;
    public MatchType: string = "eq";
    public Value: any;

    constructor() { }
}