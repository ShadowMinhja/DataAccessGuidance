//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Repo.Interface.Fakes;
using DataAccess.Entities;

namespace DataAccess.Report.Impl.Sql.Test
{
    [TestClass]
    public class CategoryRepositoryTest
    {
        [TestMethod]
        public void GetAllCategories_ReturnCategories()
        {
            // Arrange
            var productCategories = new[]
                {
                    new ProductCategory{ Name = "TestCategory1", ProductCategoryId = 1, ProductSubcategories = null },
                    new ProductCategory{ Name = "TestCategory2", ProductCategoryId = 2, ProductSubcategories = null }
                };

            var repository = new StubICategoryRepository
            {
                GetAllCategories = () => { return productCategories; }
            };

            // Act
            var result = repository.GetAllCategories();

            // Assert
            Assert.AreEqual(result, productCategories);
        }

        [TestMethod]
        public void GetCategory_ReturnCategory()
        {
            // Arrange
            var productCategory = new ProductCategory()
            {
                Name = "TestCategory",
                ProductCategoryId = 1
            };

            var repository = new StubICategoryRepository
            {
                GetCategoryInt32 = categoryId => { return productCategory; }
            };

            // Act
            var result = repository.GetCategoryInt32(1);

            // Assert
            Assert.AreEqual(result, productCategory); 
        }
    }
}
