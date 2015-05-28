//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Mongo
{
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess.Repo.Impl.Mongo.Entities;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    public sealed class CategoryContext : BaseContext
    {
        private const string CategoriesCollection = "categories";

        public CategoryContext(string hostNames, string databaseName)
            : base(hostNames, databaseName)
        {
        }

        public IEnumerable<Category> GetCategories(IDictionary<string, object> matchingFilter = null)
        {
            return (matchingFilter != null)
                ? this.Database.GetCollection<Category>(CategoriesCollection).Find(new QueryDocument(matchingFilter))
                : this.Database.GetCollection<Category>(CategoriesCollection).FindAll();
        }

        public Category GetCategory(int categoryId)
        {
            return this.Database.GetCollection<Category>(CategoriesCollection).FindOneById(categoryId);
        }
    }
}