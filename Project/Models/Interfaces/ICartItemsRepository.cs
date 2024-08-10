using Project.Models.Entities;
using System.Collections.Generic;

namespace Project.Models.Interfaces
{
    public interface ICartItemRepository
    {
        void AddCartItem(CartItem cartItem);
        void RemoveCartItem(int id);
        List<CartItem> GetCartItemsByUserId(int userId);
        CartItem GetCartItemById(int id);
    }
}
