﻿using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);

        Order GetOrderById(string username, int id);

        bool SaveAll();


        void AddEntity(object model);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        void AddOrder(Order newOrder);
    }
}