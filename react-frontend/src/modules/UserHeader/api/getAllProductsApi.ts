import axios from "axios"
import type { AxiosResponse  } from "axios";
import type { Product } from "../../../types/api";

export const getAllProducts = (
    setProducts: (products : Product[]) => void,
    pageNumber: number,
    pageSize: number,
    isDescending: string,
    minPrice: number | null = null,
    maxPrice: number | null = null,
    minRating: number | null = null,
    maxRating: number | null = null,
    name: string | null = null,
    tag: string | null = null,
    sortBy: string | null = null
) => {
    let queryString = `/api/products?PageNumber=${pageNumber}&PageSize=${pageSize}`;
    queryString += minPrice != null ? `&MinPrice=${minPrice}` : "";
    queryString += maxPrice != null ? `&MaxPrice=${maxPrice}` : "";
    queryString += minRating != null ? `&MinRating=${minRating}` : "";
    queryString += maxRating != null ? `&MaxRating=${maxRating}` : "";
    queryString += name != null ? `&Name=${name}` : "";
    queryString += tag != null ? `&Tag=${tag}` : "";
    queryString += `&IsDescending=${isDescending}`;
    queryString += sortBy != null ? `&SortBy=${sortBy}` : "";

    axios.get<Product[]>(queryString).then((res: AxiosResponse<Product[]>) => {
        setProducts(res.data);
    }).catch(message => console.error(message));
}