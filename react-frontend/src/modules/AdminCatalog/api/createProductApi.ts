import axios from "axios";
import type { CreateProduct, Product } from "../../../types/api";

export const createProduct = (createProductCallback: (products: Product) => void, product: CreateProduct) => {
    axios.post<Product>("/api/products", product, { headers: { 'Content-Type': 'multipart/form-data', }, }).then(res => createProductCallback(res.data));
}