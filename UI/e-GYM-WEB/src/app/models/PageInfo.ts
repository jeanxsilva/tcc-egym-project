export class PageInfo {
    // The number of elements in the page
    private _size: number;
    // The total number of elements
    private _totalElements: number;
    // The skip number of elements
    private _skip: number;
    // The take number of elements
    private _take: number;

    constructor() { }

    get Size(): number {
        return this._size;
    }
    set Size(value: number) {
        this._size = value;
    }

    get TotalElements(): number {
        return this._totalElements;
    }
    set TotalElements(value: number) {
        this._totalElements = value;
    }

    get Skip(): number {
        return this._skip;
    }
    set Skip(value: number) {
        this._skip = value;
    }

    get Take(): number {
        return this._take;
    }
    set Take(value: number) {
        this._take = value;
    }
}