export class QueryBase {
    pageNumber: number;
    pageSize: number;
    sortBy: string;
    isDescending: boolean ;
    
    constructor(
        pageNumber: number, 
        pageSize: number, 
        sortBy: string, 
        isDescending: boolean
    ) {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
        this.sortBy = sortBy;
        this.isDescending = isDescending
    }
}

export class ProductQuery extends QueryBase {
    minPrice: number | null;
    maxPrice: number | null;
    minRating: number | null;
    maxRating: number | null;
    name: string | null;
    tag: string | null;

    constructor(
        pageNumber: number, 
        pageSize: number, 
        sortBy: string, 
        isDescending: boolean,
        minPrice: number | null,
        maxPrice: number | null,
        minRating: number | null,
        maxRating: number | null,
        name: string | null,
        tag: string | null
    ) {
        super(pageNumber, pageSize, sortBy, isDescending);
        this.minPrice = minPrice;
        this.maxPrice = maxPrice;
        this.minRating = minRating;
        this.maxRating = maxRating;
        this.name = name;
        this.tag = tag;
    }
}