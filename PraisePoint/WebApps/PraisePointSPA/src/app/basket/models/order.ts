import { ICartItem } from "./cart-item";

export interface Order {
    buyerId: string;
    buyerUsername: string;
    emailAddress: string;
    street: string;
    country: string;
    city: string;
    state: string;
    zipCode: string;
    orderItems: ICartItem[];
}