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
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess.Domain.Catalog;
    using DataAccess.Repository;

    public class CategoryRepository : ICategoryRepository
    {
        private static readonly Dictionary<Category, List<Subcategory>> Categories = new Dictionary<Category, List<Subcategory>>();

        static CategoryRepository()
        {
            Category category = null;
            category = new Category() { Id = 1, Name = "Bikes" };
            Categories.Add(category, new List<Subcategory>() { new Subcategory() { Id = 1, Name = "Mountain Bikes", Category = category }, new Subcategory() { Id = 2, Name = "Road Bikes", Category = category }, new Subcategory() { Id = 3, Name = "Touring Bikes", Category = category } });
            category = new Category() { Id = 2, Name = "Components" };
            Categories.Add(category, new List<Subcategory>() { new Subcategory() { Id = 4, Name = "Handlebars", Category = category }, new Subcategory() { Id = 5, Name = "Bottom Brackets", Category = category }, new Subcategory() { Id = 6, Name = "Brakes", Category = category }, new Subcategory() { Id = 7, Name = "Chains", Category = category }, new Subcategory() { Id = 8, Name = "Cranksets", Category = category }, new Subcategory() { Id = 9, Name = "Derailleurs", Category = category }, new Subcategory() { Id = 10, Name = "Forks", Category = category }, new Subcategory() { Id = 11, Name = "Headsets", Category = category }, new Subcategory() { Id = 12, Name = "Mountain Frames", Category = category }, new Subcategory() { Id = 13, Name = "Pedals", Category = category }, new Subcategory() { Id = 14, Name = "Road Frames", Category = category }, new Subcategory() { Id = 15, Name = "Saddles", Category = category }, new Subcategory() { Id = 16, Name = "Touring Frames", Category = category }, new Subcategory() { Id = 17, Name = "Wheels", Category = category } });
            category = new Category() { Id = 3, Name = "Clothing" };
            Categories.Add(category, new List<Subcategory>() { new Subcategory() { Id = 18, Name = "Bib-Shorts", Category = category }, new Subcategory() { Id = 19, Name = "Caps", Category = category }, new Subcategory() { Id = 20, Name = "Gloves", Category = category }, new Subcategory() { Id = 21, Name = "Jerseys", Category = category }, new Subcategory() { Id = 22, Name = "Shorts", Category = category }, new Subcategory() { Id = 23, Name = "Socks", Category = category }, new Subcategory() { Id = 24, Name = "Tights", Category = category }, new Subcategory() { Id = 25, Name = "Vests", Category = category } });
            category = new Category() { Id = 4, Name = "Accessories" };
            Categories.Add(category, new List<Subcategory>() { new Subcategory() { Id = 26, Name = "Bike Racks", Category = category }, new Subcategory() { Id = 27, Name = "Bike Stands", Category = category }, new Subcategory() { Id = 28, Name = "Bottles and Cages", Category = category }, new Subcategory() { Id = 29, Name = "Cleaners", Category = category }, new Subcategory() { Id = 30, Name = "Fenders", Category = category }, new Subcategory() { Id = 31, Name = "Helmets", Category = category }, new Subcategory() { Id = 32, Name = "Hydration Packs", Category = category }, new Subcategory() { Id = 33, Name = "Lights", Category = category }, new Subcategory() { Id = 34, Name = "Locks", Category = category }, new Subcategory() { Id = 35, Name = "Panniers", Category = category }, new Subcategory() { Id = 36, Name = "Pumps", Category = category }, new Subcategory() { Id = 37, Name = "Tires and Tubes", Category = category } });
        }

        public ICollection<Category> GetAllCategories()
        {
            return CategoryRepository.Categories.Keys;
        }

        public ICollection<Subcategory> GetSubcategories(int categoryId)
        {
            return CategoryRepository.Categories
                .Where(kvp => kvp.Key.Id == categoryId)
                .Select(kvp => kvp.Value)
                .DefaultIfEmpty(new List<Subcategory>())
                .Single();
        }
    }
}
