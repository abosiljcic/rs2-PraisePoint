using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _cache;

        public BasketRepository(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _cache.GetStringAsync(username);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            //deserialize
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            var basketString = JsonConvert.SerializeObject(basket);
            await _cache.SetStringAsync(basket.Username, basketString);
            return await GetBasket(basket.Username);
        }
        public async Task DeleteBasket(string username)
        {
            await _cache.RemoveAsync(username);
        }

        public async Task<int> RemoveItemFromBasket(string username, string itemId, int quantityToRemove)
        {
            if (quantityToRemove <= 0)
            {
                throw new ArgumentException("Quantity to remove must be greater than zero.", nameof(quantityToRemove));
            }
            // Get the existing basket
            var basket = await GetBasket(username);
            if (basket == null)
            {
                return quantityToRemove; // Return the quantity requested if the basket is not found
            }

            // Find the item to remove
            var itemToRemove = basket.Items.FirstOrDefault(item => item.ProductId == itemId);

            if (itemToRemove == null)
            {
                // Item not found in the basket
                return quantityToRemove;
            }

            // Calculate the quantity that can be removed
            int quantityAvailable = itemToRemove.Quantity;
            int quantityRemoved = Math.Min(quantityToRemove, quantityAvailable);

            // Update the quantity of the item in the basket
            if (quantityAvailable > quantityRemoved)
            {
                itemToRemove.Quantity -= quantityRemoved;
            }
            else
            {
                basket.Items.Remove(itemToRemove);
            }

            // Update the basket in the cache
            await UpdateBasket(basket);

            // Return the number of items that could not be removed
            return quantityToRemove - quantityRemoved;
        }



    }
}

