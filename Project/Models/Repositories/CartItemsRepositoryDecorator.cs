using System;
using System.Collections.Generic;
using Project.Models.Entities;
using Project.Models.Interfaces;

public class CartItemsRepositoryDecorator : ICartItemsRepository
{
    private readonly ICartItemsRepository _cartItemsRepository;

    public CartItemsRepositoryDecorator(ICartItemsRepository cartItemsRepository)
    {
        _cartItemsRepository = cartItemsRepository;
    }

    public void Add(CartItems cartItem)
    {
        Console.WriteLine($"Adding CartItem with Id: {cartItem.Id}");
        _cartItemsRepository.Add(cartItem);
    }

    public void RemoveCartItem(int id)
    {
        Console.WriteLine($"Removing CartItem with Id: {id}");
        _cartItemsRepository.RemoveCartItem(id);
    }

    public List<CartItems> GetCartItemsByUserId(int userId)
    {
        Console.WriteLine($"Getting CartItems for UserId: {userId}");
        return _cartItemsRepository.GetCartItemsByUserId(userId);
    }

    public CartItems GetById(int id)
    {
        Console.WriteLine($"Getting CartItem by Id: {id}");
        return _cartItemsRepository.GetById(id);
    }
    public void ClearCartForUser(int userId)
    {
        _cartItemsRepository.ClearCartForUser(userId);
    }
}
