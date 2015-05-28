//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Sql.Order
{
    using System;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using DataAccess.Domain;
    using DataAccess.Repo.Impl.Sql.Resources;
    using DataAccess.Repository;
    using DE = DataAccess.Domain.Order;

    public class InventoryProductRepository : BaseRepository, IInventoryProductRepository
    {
        public DE.InventoryProduct GetInventoryProduct(int productId)
        {
            try
            {
                using (var context = new InventoryProductContext())
                {
                    InventoryProduct inventoryProduct = null;

                    using (var transactionScope = this.GetTransactionScope())
                    {
                        inventoryProduct = context.InventoryProducts.SingleOrDefault(p => p.ProductId == productId);
                        transactionScope.Complete();
                    }

                    if (inventoryProduct == null)
                    {
                        return null;
                    }

                    var result = new DE.InventoryProduct();
                    Mapper.Map(inventoryProduct, result);

                    return result;
                }
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingInventoryForProductId, productId),
                    e);
            }
        }
    }
}