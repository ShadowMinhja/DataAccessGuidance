﻿//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repository
{
    using System;
    using DataAccess.Domain.ShoppingCart;

    public interface IShoppingCartRepository
    {
        ShoppingCart GetShoppingCart(string shoppingCartId);

        ShoppingCart SaveShoppingCart(ShoppingCart shoppingCart);

        void DeleteShoppingCart(string shoppingCartId);
    }
}