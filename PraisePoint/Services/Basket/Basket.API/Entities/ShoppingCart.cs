

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string Username { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        //vazno za serijalizaciju
        public ShoppingCart()
        {           
        }

        public ShoppingCart(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }

        public int TotalPrice
        { 
            get
            {
                int totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        
        }       
    }
}
