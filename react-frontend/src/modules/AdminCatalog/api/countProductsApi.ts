import axios from "axios"
import type { AxiosResponse } from "axios";

export const countProducts = (
    setProductCount: (count: number) => void,
    minPrice: number | null = null,
    maxPrice: number | null = null,
    minRating: number | null = null,
    maxRating: number | null = null,
    name: string | null = null,
    tag: string | null = null,
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