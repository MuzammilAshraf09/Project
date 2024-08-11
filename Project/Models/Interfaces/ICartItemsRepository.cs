using Project.Models.Entities;
using System.Collections.Generic;

namespace Project.Models.Interfaces
{
    public interface ICartItemsRepository
    {
        void AddCartItem(CartItems cartItem);
        void RemoveCartItem(int id);
        List<CartItems> GetCartItemsByUserId(int userId);
        CartItems  GetCartItemById(int id);
    }
}
