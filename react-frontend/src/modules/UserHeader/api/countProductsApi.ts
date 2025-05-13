import axios from "axios"
import type { AxiosResponse } from "axios";

export const countProducts = (
    setProductCount: (count: number) => void,
    minPrice?: number,
    maxPrice?: number,
    minRating?: number,
    maxRating?: number,
    name?: string,
    tag?: string,
) => {
    let queryString = "/api/products/count?";
    queryString += minPrice != null ? `&MinPrice=${minPrice}` : "";
    queryString += maxPrice != null ? `&MaxPrice=${maxPrice}` : "";
    queryString += minRating != null ? `&MinRating=${minRating}` : "";
    queryString += maxRating != null ? `&MaxRating=${maxRating}` : "";
    queryString += name != null ? `&Name=${name}` : "";
    queryString += tag != null ? `&Tag=${tag}` : "";

    axios.get<number>(queryString).then((res: AxiosResponse<number>) => {
        setProductCount(res.data);
    }).catch(message => console.error(message));
}