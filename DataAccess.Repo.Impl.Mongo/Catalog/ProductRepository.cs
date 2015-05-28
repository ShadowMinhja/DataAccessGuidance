//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Mongo.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using DataAccess.Repo.Impl.Mongo.Resources;
    using DataAccess.Repository;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using DE = DataAccess.Domain.Catalog;

    public class ProductRepository : BaseRepository, IProductRepository
    {
        private const string MongoCollection = "products";

        public ProductRepository(string hostNames, string databaseName)
            : base(hostNames, databaseName, false)
        {
        }

        public bool ProductExists(int productId)
        {
            try
            {
                var mongoProductId = GetDatabase().GetCollection(MongoCollection)
                    .Find(Query.EQ("_id", productId))
                    .SetLimit(1)
                    .SetFields(Fields.Include("_id"))
                    .FirstOrDefault();
                return mongoProductId != null;
            }
            catch (Exception e)
            {
                throw new RepositoryException(string.Format(CultureInfo.CurrentCulture, Strings.ErrorCheckingProductExistence, productId), e);
            }
        }

        public ICollection<DE.Product> GetProducts(int subcategoryId)
        {
            try
            {
                var products = this.GetMongoProducts(new Dictionary<string, object> { { "category.subcategory.subcategoryId", subcategoryId } }).ToList();
                if (products == null || products.Count == 0)
                {
                    return null;
                }

                var result = new List<DE.Product>();

                Mapper.Map(products, result);
                return result;
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingProductsForSubcategoryId, subcategoryId),
                    e);
            }
        }

        public DE.Product GetProduct(int productId)
        {
            try
            {
                var product = this.GetMongoProduct(productId);
                if (product == null)
                {
                    return null;
                }

                var result = new DE.Product();

                Mapper.Map(product, result);
                return result;
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingProductByProductId, productId),
                    e);
            }
        }

        private IEnumerable<Product> GetMongoProducts(IDictionary<string, object> matchingFilter = null)
        {
            return (matchingFilter != null)
                 ? GetDatabase().GetCollection<Product>(MongoCollection).Find(new QueryDocument(matchingFilter))
                 : GetDatabase().GetCollection<Product>(MongoCollection).FindAll();
        }

        private Product GetMongoProduct(int productId)
        {
            return GetDatabase().GetCollection<Product>(MongoCollection).FindOneById(productId);
        }
    }
}
