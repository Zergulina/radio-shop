import type { Product } from "./api";

export interface CartItem extends Product {
  quantity: number;
}

export interface CartState {
  items: CartItem[];
  total: number;
}