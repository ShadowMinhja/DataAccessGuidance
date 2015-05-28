//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.TableService.ShoppingCart
{
    using System;
    using System.Globalization;
    using DataAccess.Domain;
    using DataAccess.Domain.ShoppingCart;
    using DataAccess.Repo.Impl.TableService.Resources;
    using DataAccess.Repository;

    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public ShoppingCart GetShoppingCart(string shoppingCartId)
        {
            try
            {
                var storedCart = new ShoppingCartContext().Get(shoppingCartId);
                return storedCart != null ? ShoppingCartMapper.Map(storedCart) : new ShoppingCart(shoppingCartId);
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingShoppingCartById, shoppingCartId),
                    e);
            }
        }

        public ShoppingCart SaveShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException("shoppingCart");
            }

            try
            {
                new ShoppingCartContext().Save(ShoppingCartMapper.Map(shoppingCart));
                return shoppingCart;
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorSavingShoppingCart, shoppingCart.UserCartId, shoppingCart.TrackingId),
                    e);
            }
        }

        public void DeleteShoppingCart(string shoppingCartId)
        {
            try
            {
                var context = new ShoppingCartContext();
                var shoppingCart = context.Get(shoppingCartId);
                if (shoppingCart != null)
                {
                    context.Delete(shoppingCart);
                }
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorDeletingShoppingCart, shoppingCartId),
                    e);
            }
        }
    }
}