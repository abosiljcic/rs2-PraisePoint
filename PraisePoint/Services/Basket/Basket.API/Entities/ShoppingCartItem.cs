﻿namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string ProductId { get; set; }  
        public string ProductName { get; set; }
    }
}
