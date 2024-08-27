import { ICartItem } from "./cart-item";

export interface ICart {
    products: ICartItem[];
    total: number;
  }