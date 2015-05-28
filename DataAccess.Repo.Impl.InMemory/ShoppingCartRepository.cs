//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.InMemory
{
    using System;
    using System.Collections.Concurrent;
    using DataAccess.Domain.ShoppingCart;
    using DataAccess.Repository;

    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private static ConcurrentDictionary<string, ShoppingCart> shoppingCarts =
            new ConcurrentDictionary<string, ShoppingCart>();

        public ShoppingCart GetShoppingCart(string shoppingCartId)
        {
            return ShoppingCartRepository.shoppingCarts.GetOrAdd(
                shoppingCartId, new ShoppingCart(shoppingCartId));
        }

        public ShoppingCart SaveShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException("shoppingCart");
            }

            return ShoppingCartRepository.shoppingCarts.AddOrUpdate(
                shoppingCart.UserCartId,
                shoppingCart,
                (key, existingValue) =>
                {
                    return shoppingCart;
                });
        }

        public void DeleteShoppingCart(string shoppingCartId)
        {
            ShoppingCart shoppingCart = null;
            ShoppingCartRepository.shoppingCarts.TryRemove(shoppingCartId, out shoppingCart);
        }
    }
}
