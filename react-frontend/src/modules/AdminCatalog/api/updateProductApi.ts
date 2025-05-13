import axios from "axios"
import type { Product, UpdateProduct } from "../../../types/api"

export const updateProduct = (updateCallback: (product:Product) => void, id: number, product: UpdateProduct) => {
    axios.put(`/api/products/${id}`, product).then(res => updateCallback(res.data));
}