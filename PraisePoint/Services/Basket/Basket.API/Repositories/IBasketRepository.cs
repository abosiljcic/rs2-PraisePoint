using Basket.API.Entities;

namespace Basket.API.Repositories
{
    //key - username
    //value - sadrzaj korpe
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string username);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket (string username);
        Task<int> RemoveItemFromBasket(string username, string itemId, int quantityToRemove);
    }
}
