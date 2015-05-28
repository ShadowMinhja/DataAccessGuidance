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
    using MongoDB.Driver.Builders;
    using DE = DataAccess.Domain.Catalog;

    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        private const string MongoCollection = "categories";

        public CategoryRepository(string hostNames, string databaseName)
            : base(hostNames, databaseName)
        {
        }

        public ICollection<DE.Category> GetAllCategories()
        {
            try
            {
                // This method is ONLY used for the front page of the reference implementation.  As an optimization, instead of pulling
                // back the whole category document, we are only returning the _id and name of the category with no associated subcategories.
                var categories = GetDatabase().GetCollection<Category>(MongoCollection)
                    .FindAll()
                    .SetFields(Fields.Include("_id", "name"))
                    .ToList();

                if (categories == null)
                {
                    return null;
                }

                var result = new List<DE.Category>();

                Mapper.Map(categories, result);
                return result;
            }
            catch (Exception e)
            {
                throw new RepositoryException(Strings.ErrorRetrievingAllCategories, e);
            }
        }

        public ICollection<DE.Subcategory> GetSubcategories(int categoryId)
        {
            try
            {
                var category = this.GetMongoCategory(categoryId);

                if (category != null && category.Subcategories != null)
                {
                    var subcategories = category.Subcategories.ToList();

                    if (subcategories == null)
                    {
                        return null;
                    }

                    var result = new List<DE.Subcategory>();

                    Mapper.Map(subcategories, result);
                    return result.Select(s =>
                        {
                            // Map the parent category.
                            Mapper.Map(category, s.Category);
                            return s;
                        }).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new RepositoryException(string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingSubcategories, categoryId), e);
            }
        }

        private Category GetMongoCategory(int categoryId)
        {
            return GetDatabase().GetCollection<Category>(MongoCollection).FindOneById(categoryId);
        }
    }
}
