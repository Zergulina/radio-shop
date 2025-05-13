import axios from "axios"
import type { Product } from "../../../types/api"

export const deleteProduct = (
    id: number | string | null
) : Promise<any> => {
    return axios.delete<Product>(`/api/products/${id}`);
}