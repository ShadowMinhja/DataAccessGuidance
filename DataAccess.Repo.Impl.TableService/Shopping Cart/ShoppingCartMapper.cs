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
    using System.Collections.Generic;
    using System.Web.Script.Serialization;
    using DataAccess.Domain.ShoppingCart;
    
    internal static class ShoppingCartMapper
    {
        public static ShoppingCart Map(ShoppingCartTableEntity shoppingCart)
        {
            if (shoppingCart == null)
            {
                return null;
            }

            var result = new ShoppingCart(shoppingCart.RowKey)
            {
                TrackingId = shoppingCart.TrackingId
            };
            var shoppingCartItems = new JavaScriptSerializer().Deserialize<ICollection<ShoppingCartItemTableEntity>>(shoppingCart.ShoppingCartItemsJson);

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                result.AddItem(new ShoppingCartItem()
                {
                    ProductId = shoppingCartItem.ProductId,
                    ProductName = shoppingCartItem.ProductName,
                    ProductPrice = shoppingCartItem.ProductPrice,
                    Quantity = shoppingCartItem.Quantity,
                    CheckoutErrorMessage = shoppingCartItem.CheckoutErrorMessage
                });
            }

            return result;
        }

        public static ShoppingCartTableEntity Map(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                return null;
            }

            var result = new ShoppingCartTableEntity(shoppingCart.UserCartId)
            {
                TrackingId = shoppingCart.TrackingId
            };
            var shoppingCartItems = new List<ShoppingCartItemTableEntity>();

            foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
            {
                shoppingCartItems.Add(new ShoppingCartItemTableEntity()
                {
                    ProductId = shoppingCartItem.ProductId,
                    ProductName = shoppingCartItem.ProductName,
                    ProductPrice = shoppingCartItem.ProductPrice,
                    Quantity = shoppingCartItem.Quantity,
                    CheckoutErrorMessage = shoppingCartItem.CheckoutErrorMessage
                });
            }

            result.ShoppingCartItemsJson = new JavaScriptSerializer().Serialize(shoppingCartItems);

            return result;
        }
    }
}
