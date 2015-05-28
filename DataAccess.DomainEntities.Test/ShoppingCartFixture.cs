//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.DomainEntities.Test
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataAccess.Domain.ShoppingCart;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class ShoppingCartFixture
    {
        [TestMethod]
        public void ShouldAddAnItemToShoppingCart()
        {
            // Arrange
            var shoppingCart = new ShoppingCart("testCart");
            var shoppingCartItem = new ShoppingCartItem()
            {
                ProductId = 1,
                ProductName = "Test Product",
                ProductPrice = 9.99m,
                Quantity = 1
            };

            // Act
            shoppingCart.AddItem(shoppingCartItem);
            
            // Assert
            Assert.AreEqual(shoppingCartItem, shoppingCart.ShoppingCartItems.FirstOrDefault(c => c.ProductId == shoppingCartItem.ProductId));
        }

        [TestMethod]
        public void ShouldAddAnExistingItemToShoppingCart()
        {
            // Arrange
            var shoppingCart = new ShoppingCart("testCart");
            var shoppingCartItem = new ShoppingCartItem()
            {
                ProductId = 1,
                ProductName = "Test Product",
                ProductPrice = 9.99m,
                Quantity = 1
            };

            shoppingCart.AddItem(shoppingCartItem);

            // Act
            shoppingCart.AddItem(shoppingCartItem);

            // Assert
            Assert.AreEqual(2, shoppingCart.ShoppingCartItems.FirstOrDefault(c => c.ProductId == shoppingCartItem.ProductId).Quantity);
        }

        [TestMethod]
        public void ShouldRemoveProductFromShoppingCart()
        {
            // Arrange
            var shoppingCart = new ShoppingCart("testCart");

            var shoppingCartItem1 = new ShoppingCartItem()
            {
                ProductId = 1,
                ProductName = "Test Product 1",
                ProductPrice = 9.99m,
                Quantity = 18
            };

            var shoppingCartItem2 = new ShoppingCartItem()
            {
                ProductId = 1,
                ProductName = "Test Product 1",
                ProductPrice = 9.99m,
                Quantity = 3
            };

            var shoppingCartItem3 = new ShoppingCartItem()
            {
                ProductId = 2,
                ProductName = "Test Product 2",
                ProductPrice = 109.99m,
                Quantity = 1
            };

            shoppingCart.AddItem(shoppingCartItem1);
            shoppingCart.AddItem(shoppingCartItem2);
            shoppingCart.AddItem(shoppingCartItem3);

            // Act
            shoppingCart.RemoveProduct(1);

            // Assert
            Assert.AreEqual(1, shoppingCart.ShoppingCartItems.Count());
            Assert.AreEqual(shoppingCartItem3, shoppingCart.ShoppingCartItems.FirstOrDefault(c => c.ProductId == shoppingCartItem3.ProductId));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ShouldThrowValidationExceptionWhenAddingItemToCart()
        {
            // Arrange
            var shoppingCart = new ShoppingCart("testCart");
            var shoppingCartItem = new ShoppingCartItem()
            {
                ProductId = 1,
                ProductName = string.Empty,
                ProductPrice = 9.99m,
                Quantity = 100
            };

            // Act
            shoppingCart.AddItem(shoppingCartItem);
        }
    }
}
