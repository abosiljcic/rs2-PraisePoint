import { ICartItem } from "./cart-item";

export interface ICart {
    username: string | undefined;
    products: ICartItem[];
    total: number;
  }