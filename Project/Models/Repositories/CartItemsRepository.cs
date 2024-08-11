﻿using Project.Models.Entities;
using Project.Models.Interfaces;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Project.Models.Repositories
{
    public class CartItemsRepository : ICartItemsRepository
    {
        private readonly string _connectionString;

        public CartItemsRepository()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ClothDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        public void AddCartItem(CartItems cartItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO CartItems (ProductId, Quantity, UserId) VALUES (@ProductId, @Quantity, @UserId)", connection);
                command.Parameters.AddWithValue("@ProductId", cartItem.ProductId);
                command.Parameters.AddWithValue("@Quantity", cartItem.Quantity);
                command.Parameters.AddWithValue("@UserId", cartItem.UserId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void RemoveCartItem(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM CartItems WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public List<CartItems> GetCartItemsByUserId(int userId)
        {
            var cartItems = new List<CartItems>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM CartItems WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cartItem = new CartItems
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                        };
                        cartItems.Add(cartItem);
                    }
                }
            }
            return cartItems;
        }

        public CartItems GetCartItemById(int id)
        {
            CartItems cartItem = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM CartItems WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cartItem = new CartItems
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                        };
                    }
                }
            }
            return cartItem;
        }
    }
}
