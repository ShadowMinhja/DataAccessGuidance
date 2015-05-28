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
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using DataAccess.Domain.Catalog;
    using DataAccess.Repository;

    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> Products = new List<Product>();

        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = "This class is only to facilitate running the reference implementation without setting up all of the required databases.")]
        static ProductRepository()
        {
            ProductRepository.Products.Add(new Product()
            {
                Id = 680,
                Name = "HL Road Frame - Black, 58",
                ProductNumber = "FR-R92B-58",
                Color = "Black",
                ListPrice = 1431.5000m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 2.24m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 706,
                Name = "HL Road Frame - Red, 58",
                ProductNumber = "FR-R92R-58",
                Color = "Red",
                ListPrice = 1431.5000m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 2.24m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 707,
                Name = "Sport-100 Helmet, Red",
                ProductNumber = "HL-U509-R",
                Color = "Red",
                ListPrice = 34.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 31,
                    Name = "Helmets",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 708,
                Name = "Sport-100 Helmet, Black",
                ProductNumber = "HL-U509",
                Color = "Black",
                ListPrice = 34.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 31,
                    Name = "Helmets",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 709,
                Name = "Mountain Bike Socks, M",
                ProductNumber = "SO-B909-M",
                Color = "White",
                ListPrice = 9.5000m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 23,
                    Name = "Socks",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 710,
                Name = "Mountain Bike Socks, L",
                ProductNumber = "SO-B909-L",
                Color = "White",
                ListPrice = 9.5000m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 23,
                    Name = "Socks",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 711,
                Name = "Sport-100 Helmet, Blue",
                ProductNumber = "HL-U509-B",
                Color = "Blue",
                ListPrice = 34.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 31,
                    Name = "Helmets",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 712,
                Name = "AWC Logo Cap",
                ProductNumber = "CA-1098",
                Color = "Multi",
                ListPrice = 8.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 19,
                    Name = "Caps",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 713,
                Name = "Long-Sleeve Logo Jersey, S",
                ProductNumber = "LJ-0192-S",
                Color = "Multi",
                ListPrice = 49.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 21,
                    Name = "Jerseys",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 714,
                Name = "Long-Sleeve Logo Jersey, M",
                ProductNumber = "LJ-0192-M",
                Color = "Multi",
                ListPrice = 49.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 21,
                    Name = "Jerseys",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 715,
                Name = "Long-Sleeve Logo Jersey, L",
                ProductNumber = "LJ-0192-L",
                Color = "Multi",
                ListPrice = 49.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 21,
                    Name = "Jerseys",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 716,
                Name = "Long-Sleeve Logo Jersey, XL",
                ProductNumber = "LJ-0192-X",
                Color = "Multi",
                ListPrice = 49.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 21,
                    Name = "Jerseys",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 717,
                Name = "HL Road Frame - Red, 62",
                ProductNumber = "FR-R92R-62",
                Color = "Red",
                ListPrice = 1431.5000m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 2.30m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 718,
                Name = "HL Road Frame - Red, 44",
                ProductNumber = "FR-R92R-44",
                Color = "Red",
                ListPrice = 1431.5000m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.12m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 719,
                Name = "HL Road Frame - Red, 48",
                ProductNumber = "FR-R92R-48",
                Color = "Red",
                ListPrice = 1431.5000m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 2.16m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 720,
                Name = "HL Road Frame - Red, 52",
                ProductNumber = "FR-R92R-52",
                Color = "Red",
                ListPrice = 1431.5000m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 2.20m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 721,
                Name = "HL Road Frame - Red, 56",
                ProductNumber = "FR-R92R-56",
                Color = "Red",
                ListPrice = 1431.5000m,
                Size = "56",
                SizeUnitMeasureCode = "CM",
                Weight = 2.24m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 722,
                Name = "LL Road Frame - Black, 58",
                ProductNumber = "FR-R38B-58",
                Color = "Black",
                ListPrice = 337.2200m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 2.46m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 723,
                Name = "LL Road Frame - Black, 60",
                ProductNumber = "FR-R38B-60",
                Color = "Black",
                ListPrice = 337.2200m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 2.48m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 724,
                Name = "LL Road Frame - Black, 62",
                ProductNumber = "FR-R38B-62",
                Color = "Black",
                ListPrice = 337.2200m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 2.50m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 725,
                Name = "LL Road Frame - Red, 44",
                ProductNumber = "FR-R38R-44",
                Color = "Red",
                ListPrice = 337.2200m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.32m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 726,
                Name = "LL Road Frame - Red, 48",
                ProductNumber = "FR-R38R-48",
                Color = "Red",
                ListPrice = 337.2200m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 2.36m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 727,
                Name = "LL Road Frame - Red, 52",
                ProductNumber = "FR-R38R-52",
                Color = "Red",
                ListPrice = 337.2200m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 2.40m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 728,
                Name = "LL Road Frame - Red, 58",
                ProductNumber = "FR-R38R-58",
                Color = "Red",
                ListPrice = 337.2200m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 2.46m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 729,
                Name = "LL Road Frame - Red, 60",
                ProductNumber = "FR-R38R-60",
                Color = "Red",
                ListPrice = 337.2200m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 2.48m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 730,
                Name = "LL Road Frame - Red, 62",
                ProductNumber = "FR-R38R-62",
                Color = "Red",
                ListPrice = 337.2200m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 2.50m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 731,
                Name = "ML Road Frame - Red, 44",
                ProductNumber = "FR-R72R-44",
                Color = "Red",
                ListPrice = 594.8300m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.22m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 732,
                Name = "ML Road Frame - Red, 48",
                ProductNumber = "FR-R72R-48",
                Color = "Red",
                ListPrice = 594.8300m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 2.26m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 733,
                Name = "ML Road Frame - Red, 52",
                ProductNumber = "FR-R72R-52",
                Color = "Red",
                ListPrice = 594.8300m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 2.30m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 734,
                Name = "ML Road Frame - Red, 58",
                ProductNumber = "FR-R72R-58",
                Color = "Red",
                ListPrice = 594.8300m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 2.36m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 735,
                Name = "ML Road Frame - Red, 60",
                ProductNumber = "FR-R72R-60",
                Color = "Red",
                ListPrice = 594.8300m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 2.38m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 736,
                Name = "LL Road Frame - Black, 44",
                ProductNumber = "FR-R38B-44",
                Color = "Black",
                ListPrice = 337.2200m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.32m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 737,
                Name = "LL Road Frame - Black, 48",
                ProductNumber = "FR-R38B-48",
                Color = "Black",
                ListPrice = 337.2200m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 2.36m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 738,
                Name = "LL Road Frame - Black, 52",
                ProductNumber = "FR-R38B-52",
                Color = "Black",
                ListPrice = 337.2200m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 2.40m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 739,
                Name = "HL Mountain Frame - Silver, 42",
                ProductNumber = "FR-M94S-42",
                Color = "Silver",
                ListPrice = 1364.5000m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 2.72m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 740,
                Name = "HL Mountain Frame - Silver, 44",
                ProductNumber = "FR-M94S-44",
                Color = "Silver",
                ListPrice = 1364.5000m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.76m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 741,
                Name = "HL Mountain Frame - Silver, 48",
                ProductNumber = "FR-M94S-52",
                Color = "Silver",
                ListPrice = 1364.5000m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 2.80m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 742,
                Name = "HL Mountain Frame - Silver, 46",
                ProductNumber = "FR-M94S-46",
                Color = "Silver",
                ListPrice = 1364.5000m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 2.84m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 743,
                Name = "HL Mountain Frame - Black, 42",
                ProductNumber = "FR-M94B-42",
                Color = "Black",
                ListPrice = 1349.6000m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 2.72m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 744,
                Name = "HL Mountain Frame - Black, 44",
                ProductNumber = "FR-M94B-44",
                Color = "Black",
                ListPrice = 1349.6000m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.76m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 745,
                Name = "HL Mountain Frame - Black, 48",
                ProductNumber = "FR-M94B-48",
                Color = "Black",
                ListPrice = 1349.6000m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 2.80m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 746,
                Name = "HL Mountain Frame - Black, 46",
                ProductNumber = "FR-M94B-46",
                Color = "Black",
                ListPrice = 1349.6000m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 2.84m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 747,
                Name = "HL Mountain Frame - Black, 38",
                ProductNumber = "FR-M94B-38",
                Color = "Black",
                ListPrice = 1349.6000m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 2.68m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 748,
                Name = "HL Mountain Frame - Silver, 38",
                ProductNumber = "FR-M94S-38",
                Color = "Silver",
                ListPrice = 1364.5000m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 2.68m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 749,
                Name = "Road-150 Red, 62",
                ProductNumber = "BK-R93R-62",
                Color = "Red",
                ListPrice = 3578.2700m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 15.00m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 750,
                Name = "Road-150 Red, 44",
                ProductNumber = "BK-R93R-44",
                Color = "Red",
                ListPrice = 3578.2700m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 13.77m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 751,
                Name = "Road-150 Red, 48",
                ProductNumber = "BK-R93R-48",
                Color = "Red",
                ListPrice = 3578.2700m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 14.13m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 752,
                Name = "Road-150 Red, 52",
                ProductNumber = "BK-R93R-52",
                Color = "Red",
                ListPrice = 3578.2700m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 14.42m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 753,
                Name = "Road-150 Red, 56",
                ProductNumber = "BK-R93R-56",
                Color = "Red",
                ListPrice = 3578.2700m,
                Size = "56",
                SizeUnitMeasureCode = "CM",
                Weight = 14.68m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 754,
                Name = "Road-450 Red, 58",
                ProductNumber = "BK-R68R-58",
                Color = "Red",
                ListPrice = 1457.9900m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 17.79m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 755,
                Name = "Road-450 Red, 60",
                ProductNumber = "BK-R68R-60",
                Color = "Red",
                ListPrice = 1457.9900m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 17.90m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 756,
                Name = "Road-450 Red, 44",
                ProductNumber = "BK-R68R-44",
                Color = "Red",
                ListPrice = 1457.9900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 16.77m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 757,
                Name = "Road-450 Red, 48",
                ProductNumber = "BK-R68R-48",
                Color = "Red",
                ListPrice = 1457.9900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 17.13m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 758,
                Name = "Road-450 Red, 52",
                ProductNumber = "BK-R68R-52",
                Color = "Red",
                ListPrice = 1457.9900m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 17.42m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 759,
                Name = "Road-650 Red, 58",
                ProductNumber = "BK-R50R-58",
                Color = "Red",
                ListPrice = 782.9900m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 19.79m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 760,
                Name = "Road-650 Red, 60",
                ProductNumber = "BK-R50R-60",
                Color = "Red",
                ListPrice = 782.9900m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 19.90m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 761,
                Name = "Road-650 Red, 62",
                ProductNumber = "BK-R50R-62",
                Color = "Red",
                ListPrice = 782.9900m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 20.00m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 762,
                Name = "Road-650 Red, 44",
                ProductNumber = "BK-R50R-44",
                Color = "Red",
                ListPrice = 782.9900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 18.77m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 763,
                Name = "Road-650 Red, 48",
                ProductNumber = "BK-R50R-48",
                Color = "Red",
                ListPrice = 782.9900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 19.13m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 764,
                Name = "Road-650 Red, 52",
                ProductNumber = "BK-R50R-52",
                Color = "Red",
                ListPrice = 782.9900m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 19.42m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 765,
                Name = "Road-650 Black, 58",
                ProductNumber = "BK-R50B-58",
                Color = "Black",
                ListPrice = 782.9900m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 19.79m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 766,
                Name = "Road-650 Black, 60",
                ProductNumber = "BK-R50B-60",
                Color = "Black",
                ListPrice = 782.9900m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 19.90m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 767,
                Name = "Road-650 Black, 62",
                ProductNumber = "BK-R50B-62",
                Color = "Black",
                ListPrice = 782.9900m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 20.00m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 768,
                Name = "Road-650 Black, 44",
                ProductNumber = "BK-R50B-44",
                Color = "Black",
                ListPrice = 782.9900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 18.77m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 769,
                Name = "Road-650 Black, 48",
                ProductNumber = "BK-R50B-48",
                Color = "Black",
                ListPrice = 782.9900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 19.13m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 770,
                Name = "Road-650 Black, 52",
                ProductNumber = "BK-R50B-52",
                Color = "Black",
                ListPrice = 782.9900m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 19.42m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 771,
                Name = "Mountain-100 Silver, 38",
                ProductNumber = "BK-M82S-38",
                Color = "Silver",
                ListPrice = 3399.9900m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 20.35m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 772,
                Name = "Mountain-100 Silver, 42",
                ProductNumber = "BK-M82S-42",
                Color = "Silver",
                ListPrice = 3399.9900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 20.77m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 773,
                Name = "Mountain-100 Silver, 44",
                ProductNumber = "BK-M82S-44",
                Color = "Silver",
                ListPrice = 3399.9900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 21.13m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 774,
                Name = "Mountain-100 Silver, 48",
                ProductNumber = "BK-M82S-48",
                Color = "Silver",
                ListPrice = 3399.9900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 21.42m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 775,
                Name = "Mountain-100 Black, 38",
                ProductNumber = "BK-M82B-38",
                Color = "Black",
                ListPrice = 3374.9900m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 20.35m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 776,
                Name = "Mountain-100 Black, 42",
                ProductNumber = "BK-M82B-42",
                Color = "Black",
                ListPrice = 3374.9900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 20.77m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 777,
                Name = "Mountain-100 Black, 44",
                ProductNumber = "BK-M82B-44",
                Color = "Black",
                ListPrice = 3374.9900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 21.13m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 778,
                Name = "Mountain-100 Black, 48",
                ProductNumber = "BK-M82B-48",
                Color = "Black",
                ListPrice = 3374.9900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 21.42m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 779,
                Name = "Mountain-200 Silver, 38",
                ProductNumber = "BK-M68S-38",
                Color = "Silver",
                ListPrice = 2319.9900m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 23.35m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 780,
                Name = "Mountain-200 Silver, 42",
                ProductNumber = "BK-M68S-42",
                Color = "Silver",
                ListPrice = 2319.9900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 23.77m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 781,
                Name = "Mountain-200 Silver, 46",
                ProductNumber = "BK-M68S-46",
                Color = "Silver",
                ListPrice = 2319.9900m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 24.13m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 782,
                Name = "Mountain-200 Black, 38",
                ProductNumber = "BK-M68B-38",
                Color = "Black",
                ListPrice = 2294.9900m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 23.35m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 783,
                Name = "Mountain-200 Black, 42",
                ProductNumber = "BK-M68B-42",
                Color = "Black",
                ListPrice = 2294.9900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 23.77m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 784,
                Name = "Mountain-200 Black, 46",
                ProductNumber = "BK-M68B-46",
                Color = "Black",
                ListPrice = 2294.9900m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 24.13m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 785,
                Name = "Mountain-300 Black, 38",
                ProductNumber = "BK-M47B-38",
                Color = "Black",
                ListPrice = 1079.9900m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 25.35m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 786,
                Name = "Mountain-300 Black, 40",
                ProductNumber = "BK-M47B-40",
                Color = "Black",
                ListPrice = 1079.9900m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 25.77m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 787,
                Name = "Mountain-300 Black, 44",
                ProductNumber = "BK-M47B-44",
                Color = "Black",
                ListPrice = 1079.9900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 26.13m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 788,
                Name = "Mountain-300 Black, 48",
                ProductNumber = "BK-M47B-48",
                Color = "Black",
                ListPrice = 1079.9900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 26.42m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 789,
                Name = "Road-250 Red, 44",
                ProductNumber = "BK-R89R-44",
                Color = "Red",
                ListPrice = 2443.3500m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 14.77m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 790,
                Name = "Road-250 Red, 48",
                ProductNumber = "BK-R89R-48",
                Color = "Red",
                ListPrice = 2443.3500m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 15.13m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 791,
                Name = "Road-250 Red, 52",
                ProductNumber = "BK-R89R-52",
                Color = "Red",
                ListPrice = 2443.3500m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 15.42m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 792,
                Name = "Road-250 Red, 58",
                ProductNumber = "BK-R89R-58",
                Color = "Red",
                ListPrice = 2443.3500m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 15.79m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 793,
                Name = "Road-250 Black, 44",
                ProductNumber = "BK-R89B-44",
                Color = "Black",
                ListPrice = 2443.3500m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 14.77m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 794,
                Name = "Road-250 Black, 48",
                ProductNumber = "BK-R89B-48",
                Color = "Black",
                ListPrice = 2443.3500m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 15.13m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 795,
                Name = "Road-250 Black, 52",
                ProductNumber = "BK-R89B-52",
                Color = "Black",
                ListPrice = 2443.3500m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 15.42m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 796,
                Name = "Road-250 Black, 58",
                ProductNumber = "BK-R89B-58",
                Color = "Black",
                ListPrice = 2443.3500m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 15.68m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 797,
                Name = "Road-550-W Yellow, 38",
                ProductNumber = "BK-R64Y-38",
                Color = "Yellow",
                ListPrice = 1120.4900m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 17.35m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 798,
                Name = "Road-550-W Yellow, 40",
                ProductNumber = "BK-R64Y-40",
                Color = "Yellow",
                ListPrice = 1120.4900m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 17.77m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 799,
                Name = "Road-550-W Yellow, 42",
                ProductNumber = "BK-R64Y-42",
                Color = "Yellow",
                ListPrice = 1120.4900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 18.13m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 800,
                Name = "Road-550-W Yellow, 44",
                ProductNumber = "BK-R64Y-44",
                Color = "Yellow",
                ListPrice = 1120.4900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 18.42m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 801,
                Name = "Road-550-W Yellow, 48",
                ProductNumber = "BK-R64Y-48",
                Color = "Yellow",
                ListPrice = 1120.4900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 18.68m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 802,
                Name = "LL Fork",
                ProductNumber = "FK-1639",
                ListPrice = 148.2200m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 10,
                    Name = "Forks",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 803,
                Name = "ML Fork",
                ProductNumber = "FK-5136",
                ListPrice = 175.4900m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 10,
                    Name = "Forks",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 804,
                Name = "HL Fork",
                ProductNumber = "FK-9939",
                ListPrice = 229.4900m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 10,
                    Name = "Forks",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 805,
                Name = "LL Headset",
                ProductNumber = "HS-0296",
                ListPrice = 34.2000m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 11,
                    Name = "Headsets",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 806,
                Name = "ML Headset",
                ProductNumber = "HS-2451",
                ListPrice = 102.2900m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 11,
                    Name = "Headsets",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 807,
                Name = "HL Headset",
                ProductNumber = "HS-3479",
                ListPrice = 124.7300m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 11,
                    Name = "Headsets",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 808,
                Name = "LL Mountain Handlebars",
                ProductNumber = "HB-M243",
                ListPrice = 44.5400m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 4,
                    Name = "Handlebars",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 809,
                Name = "ML Mountain Handlebars",
                ProductNumber = "HB-M763",
                ListPrice = 61.9200m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 4,
                    Name = "Handlebars",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 810,
                Name = "HL Mountain Handlebars",
                ProductNumber = "HB-M918",
                ListPrice = 120.2700m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 4,
                    Name = "Handlebars",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 811,
                Name = "LL Road Handlebars",
                ProductNumber = "HB-R504",
                ListPrice = 44.5400m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 4,
                    Name = "Handlebars",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 812,
                Name = "ML Road Handlebars",
                ProductNumber = "HB-R720",
                ListPrice = 61.9200m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 4,
                    Name = "Handlebars",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 813,
                Name = "HL Road Handlebars",
                ProductNumber = "HB-R956",
                ListPrice = 120.2700m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 4,
                    Name = "Handlebars",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 814,
                Name = "ML Mountain Frame - Black, 38",
                ProductNumber = "FR-M63B-38",
                Color = "Black",
                ListPrice = 348.7600m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 2.73m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 815,
                Name = "LL Mountain Front Wheel",
                ProductNumber = "FW-M423",
                Color = "Black",
                ListPrice = 60.7450m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 816,
                Name = "ML Mountain Front Wheel",
                ProductNumber = "FW-M762",
                Color = "Black",
                ListPrice = 209.0250m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 817,
                Name = "HL Mountain Front Wheel",
                ProductNumber = "FW-M928",
                Color = "Black",
                ListPrice = 300.2150m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 818,
                Name = "LL Road Front Wheel",
                ProductNumber = "FW-R623",
                Color = "Black",
                ListPrice = 85.5650m,
                Weight = 900.00m,
                WeightUnitMeasureCode = "G",
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 819,
                Name = "ML Road Front Wheel",
                ProductNumber = "FW-R762",
                Color = "Black",
                ListPrice = 248.3850m,
                Weight = 850.00m,
                WeightUnitMeasureCode = "G",
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 820,
                Name = "HL Road Front Wheel",
                ProductNumber = "FW-R820",
                Color = "Black",
                ListPrice = 330.0600m,
                Weight = 650.00m,
                WeightUnitMeasureCode = "G",
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 821,
                Name = "Touring Front Wheel",
                ProductNumber = "FW-T905",
                Color = "Black",
                ListPrice = 218.0100m,
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 822,
                Name = "ML Road Frame-W - Yellow, 38",
                ProductNumber = "FR-R72Y-38",
                Color = "Yellow",
                ListPrice = 594.8300m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 2.18m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 823,
                Name = "LL Mountain Rear Wheel",
                ProductNumber = "RW-M423",
                Color = "Black",
                ListPrice = 87.7450m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 824,
                Name = "ML Mountain Rear Wheel",
                ProductNumber = "RW-M762",
                Color = "Black",
                ListPrice = 236.0250m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 825,
                Name = "HL Mountain Rear Wheel",
                ProductNumber = "RW-M928",
                Color = "Black",
                ListPrice = 327.2150m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 826,
                Name = "LL Road Rear Wheel",
                ProductNumber = "RW-R623",
                Color = "Black",
                ListPrice = 112.5650m,
                Weight = 1050.00m,
                WeightUnitMeasureCode = "G",
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 827,
                Name = "ML Road Rear Wheel",
                ProductNumber = "RW-R762",
                Color = "Black",
                ListPrice = 275.3850m,
                Weight = 1000.00m,
                WeightUnitMeasureCode = "G",
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 828,
                Name = "HL Road Rear Wheel",
                ProductNumber = "RW-R820",
                Color = "Black",
                ListPrice = 357.0600m,
                Weight = 890.00m,
                WeightUnitMeasureCode = "G",
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 829,
                Name = "Touring Rear Wheel",
                ProductNumber = "RW-T905",
                Color = "Black",
                ListPrice = 245.0100m,
                Subcategory = new Subcategory()
                {
                    Id = 17,
                    Name = "Wheels",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 830,
                Name = "ML Mountain Frame - Black, 40",
                ProductNumber = "FR-M63B-40",
                Color = "Black",
                ListPrice = 348.7600m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 2.77m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 831,
                Name = "ML Mountain Frame - Black, 44",
                ProductNumber = "FR-M63B-44",
                Color = "Black",
                ListPrice = 348.7600m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.81m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 832,
                Name = "ML Mountain Frame - Black, 48",
                ProductNumber = "FR-M63B-48",
                Color = "Black",
                ListPrice = 348.7600m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 2.85m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 833,
                Name = "ML Road Frame-W - Yellow, 40",
                ProductNumber = "FR-R72Y-40",
                Color = "Yellow",
                ListPrice = 594.8300m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 2.22m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 834,
                Name = "ML Road Frame-W - Yellow, 42",
                ProductNumber = "FR-R72Y-42",
                Color = "Yellow",
                ListPrice = 594.8300m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 2.26m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 835,
                Name = "ML Road Frame-W - Yellow, 44",
                ProductNumber = "FR-R72Y-44",
                Color = "Yellow",
                ListPrice = 594.8300m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.30m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 836,
                Name = "ML Road Frame-W - Yellow, 48",
                ProductNumber = "FR-R72Y-48",
                Color = "Yellow",
                ListPrice = 594.8300m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 2.34m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 837,
                Name = "HL Road Frame - Black, 62",
                ProductNumber = "FR-R92B-62",
                Color = "Black",
                ListPrice = 1431.5000m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 2.30m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 838,
                Name = "HL Road Frame - Black, 44",
                ProductNumber = "FR-R92B-44",
                Color = "Black",
                ListPrice = 1431.5000m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.12m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 839,
                Name = "HL Road Frame - Black, 48",
                ProductNumber = "FR-R92B-48",
                Color = "Black",
                ListPrice = 1431.5000m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 2.16m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 840,
                Name = "HL Road Frame - Black, 52",
                ProductNumber = "FR-R92B-52",
                Color = "Black",
                ListPrice = 1431.5000m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 2.20m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 14,
                    Name = "Road Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 841,
                Name = "Men's Sports Shorts, S",
                ProductNumber = "SH-M897-S",
                Color = "Black",
                ListPrice = 59.9900m,
                Style = "M",
                Subcategory = new Subcategory()
                {
                    Id = 22,
                    Name = "Shorts",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 842,
                Name = "Touring-Panniers, Large",
                ProductNumber = "PA-T100",
                Color = "Grey",
                ListPrice = 125.0000m,
                Subcategory = new Subcategory()
                {
                    Id = 35,
                    Name = "Panniers",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 843,
                Name = "Cable Lock",
                ProductNumber = "LO-C100",
                ListPrice = 25.0000m,
                Subcategory = new Subcategory()
                {
                    Id = 34,
                    Name = "Locks",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 844,
                Name = "Minipump",
                ProductNumber = "PU-0452",
                ListPrice = 19.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 36,
                    Name = "Pumps",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 845,
                Name = "Mountain Pump",
                ProductNumber = "PU-M044",
                ListPrice = 24.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 36,
                    Name = "Pumps",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 846,
                Name = "Taillights - Battery-Powered",
                ProductNumber = "LT-T990",
                ListPrice = 13.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 33,
                    Name = "Lights",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 847,
                Name = "Headlights - Dual-Beam",
                ProductNumber = "LT-H902",
                ListPrice = 34.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 33,
                    Name = "Lights",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 848,
                Name = "Headlights - Weatherproof",
                ProductNumber = "LT-H903",
                ListPrice = 44.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 33,
                    Name = "Lights",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 849,
                Name = "Men's Sports Shorts, M",
                ProductNumber = "SH-M897-M",
                Color = "Black",
                ListPrice = 59.9900m,
                Style = "M",
                Subcategory = new Subcategory()
                {
                    Id = 22,
                    Name = "Shorts",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 850,
                Name = "Men's Sports Shorts, L",
                ProductNumber = "SH-M897-L",
                Color = "Black",
                ListPrice = 59.9900m,
                Style = "M",
                Subcategory = new Subcategory()
                {
                    Id = 22,
                    Name = "Shorts",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 851,
                Name = "Men's Sports Shorts, XL",
                ProductNumber = "SH-M897-X",
                Color = "Black",
                ListPrice = 59.9900m,
                Style = "M",
                Subcategory = new Subcategory()
                {
                    Id = 22,
                    Name = "Shorts",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 852,
                Name = "Women's Tights, S",
                ProductNumber = "TG-W091-S",
                Color = "Black",
                ListPrice = 74.9900m,
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 24,
                    Name = "Tights",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 853,
                Name = "Women's Tights, M",
                ProductNumber = "TG-W091-M",
                Color = "Black",
                ListPrice = 74.9900m,
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 24,
                    Name = "Tights",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 854,
                Name = "Women's Tights, L",
                ProductNumber = "TG-W091-L",
                Color = "Black",
                ListPrice = 74.9900m,
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 24,
                    Name = "Tights",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 855,
                Name = "Men's Bib-Shorts, S",
                ProductNumber = "SB-M891-S",
                Color = "Multi",
                ListPrice = 89.9900m,
                Style = "M",
                Subcategory = new Subcategory()
                {
                    Id = 18,
                    Name = "Bib-Shorts",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 856,
                Name = "Men's Bib-Shorts, M",
                ProductNumber = "SB-M891-M",
                Color = "Multi",
                ListPrice = 89.9900m,
                Style = "M",
                Subcategory = new Subcategory()
                {
                    Id = 18,
                    Name = "Bib-Shorts",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 857,
                Name = "Men's Bib-Shorts, L",
                ProductNumber = "SB-M891-L",
                Color = "Multi",
                ListPrice = 89.9900m,
                Style = "M",
                Subcategory = new Subcategory()
                {
                    Id = 18,
                    Name = "Bib-Shorts",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 858,
                Name = "Half-Finger Gloves, S",
                ProductNumber = "GL-H102-S",
                Color = "Black",
                ListPrice = 24.4900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 20,
                    Name = "Gloves",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 859,
                Name = "Half-Finger Gloves, M",
                ProductNumber = "GL-H102-M",
                Color = "Black",
                ListPrice = 24.4900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 20,
                    Name = "Gloves",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 860,
                Name = "Half-Finger Gloves, L",
                ProductNumber = "GL-H102-L",
                Color = "Black",
                ListPrice = 24.4900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 20,
                    Name = "Gloves",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 861,
                Name = "Full-Finger Gloves, S",
                ProductNumber = "GL-F110-S",
                Color = "Black",
                ListPrice = 37.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 20,
                    Name = "Gloves",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 862,
                Name = "Full-Finger Gloves, M",
                ProductNumber = "GL-F110-M",
                Color = "Black",
                ListPrice = 37.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 20,
                    Name = "Gloves",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 863,
                Name = "Full-Finger Gloves, L",
                ProductNumber = "GL-F110-L",
                Color = "Black",
                ListPrice = 37.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 20,
                    Name = "Gloves",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 864,
                Name = "Classic Vest, S",
                ProductNumber = "VE-C304-S",
                Color = "Blue",
                ListPrice = 63.5000m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 25,
                    Name = "Vests",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 865,
                Name = "Classic Vest, M",
                ProductNumber = "VE-C304-M",
                Color = "Blue",
                ListPrice = 63.5000m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 25,
                    Name = "Vests",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 866,
                Name = "Classic Vest, L",
                ProductNumber = "VE-C304-L",
                Color = "Blue",
                ListPrice = 63.5000m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 25,
                    Name = "Vests",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 867,
                Name = "Women's Mountain Shorts, S",
                ProductNumber = "SH-W890-S",
                Color = "Black",
                ListPrice = 69.9900m,
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 22,
                    Name = "Shorts",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 868,
                Name = "Women's Mountain Shorts, M",
                ProductNumber = "SH-W890-M",
                Color = "Black",
                ListPrice = 69.9900m,
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 22,
                    Name = "Shorts",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 869,
                Name = "Women's Mountain Shorts, L",
                ProductNumber = "SH-W890-L",
                Color = "Black",
                ListPrice = 69.9900m,
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 22,
                    Name = "Shorts",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 870,
                Name = "Water Bottle - 30 oz.",
                ProductNumber = "WB-H098",
                ListPrice = 4.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 28,
                    Name = "Bottles and Cages",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 871,
                Name = "Mountain Bottle Cage",
                ProductNumber = "BC-M005",
                ListPrice = 9.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 28,
                    Name = "Bottles and Cages",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 872,
                Name = "Road Bottle Cage",
                ProductNumber = "BC-R205",
                ListPrice = 8.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 28,
                    Name = "Bottles and Cages",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 873,
                Name = "Patch Kit/8 Patches",
                ProductNumber = "PK-7098",
                ListPrice = 2.2900m,
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 874,
                Name = "Racing Socks, M",
                ProductNumber = "SO-R809-M",
                Color = "White",
                ListPrice = 8.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 23,
                    Name = "Socks",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 875,
                Name = "Racing Socks, L",
                ProductNumber = "SO-R809-L",
                Color = "White",
                ListPrice = 8.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 23,
                    Name = "Socks",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 876,
                Name = "Hitch Rack - 4-Bike",
                ProductNumber = "RA-H123",
                ListPrice = 120.0000m,
                Subcategory = new Subcategory()
                {
                    Id = 26,
                    Name = "Bike Racks",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 877,
                Name = "Bike Wash - Dissolver",
                ProductNumber = "CL-9009",
                ListPrice = 7.9500m,
                Subcategory = new Subcategory()
                {
                    Id = 29,
                    Name = "Cleaners",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 878,
                Name = "Fender Set - Mountain",
                ProductNumber = "FE-6654",
                ListPrice = 21.9800m,
                Subcategory = new Subcategory()
                {
                    Id = 30,
                    Name = "Fenders",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 879,
                Name = "All-Purpose Bike Stand",
                ProductNumber = "ST-1401",
                ListPrice = 159.0000m,
                Subcategory = new Subcategory()
                {
                    Id = 27,
                    Name = "Bike Stands",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 880,
                Name = "Hydration Pack - 70 oz.",
                ProductNumber = "HY-1023-70",
                Color = "Silver",
                ListPrice = 54.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 32,
                    Name = "Hydration Packs",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 881,
                Name = "Short-Sleeve Classic Jersey, S",
                ProductNumber = "SJ-0194-S",
                Color = "Yellow",
                ListPrice = 53.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 21,
                    Name = "Jerseys",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 882,
                Name = "Short-Sleeve Classic Jersey, M",
                ProductNumber = "SJ-0194-M",
                Color = "Yellow",
                ListPrice = 53.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 21,
                    Name = "Jerseys",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 883,
                Name = "Short-Sleeve Classic Jersey, L",
                ProductNumber = "SJ-0194-L",
                Color = "Yellow",
                ListPrice = 53.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 21,
                    Name = "Jerseys",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 884,
                Name = "Short-Sleeve Classic Jersey, XL",
                ProductNumber = "SJ-0194-X",
                Color = "Yellow",
                ListPrice = 53.9900m,
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 21,
                    Name = "Jerseys",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Clothing"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 885,
                Name = "HL Touring Frame - Yellow, 60",
                ProductNumber = "FR-T98Y-60",
                Color = "Yellow",
                ListPrice = 1003.9100m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 3.08m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 886,
                Name = "LL Touring Frame - Yellow, 62",
                ProductNumber = "FR-T67Y-62",
                Color = "Yellow",
                ListPrice = 333.4200m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 3.20m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 887,
                Name = "HL Touring Frame - Yellow, 46",
                ProductNumber = "FR-T98Y-46",
                Color = "Yellow",
                ListPrice = 1003.9100m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 2.96m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 888,
                Name = "HL Touring Frame - Yellow, 50",
                ProductNumber = "FR-T98Y-50",
                Color = "Yellow",
                ListPrice = 1003.9100m,
                Size = "50",
                SizeUnitMeasureCode = "CM",
                Weight = 3.00m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 889,
                Name = "HL Touring Frame - Yellow, 54",
                ProductNumber = "FR-T98Y-54",
                Color = "Yellow",
                ListPrice = 1003.9100m,
                Size = "54",
                SizeUnitMeasureCode = "CM",
                Weight = 3.04m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 890,
                Name = "HL Touring Frame - Blue, 46",
                ProductNumber = "FR-T98U-46",
                Color = "Blue",
                ListPrice = 1003.9100m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 2.96m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 891,
                Name = "HL Touring Frame - Blue, 50",
                ProductNumber = "FR-T98U-50",
                Color = "Blue",
                ListPrice = 1003.9100m,
                Size = "50",
                SizeUnitMeasureCode = "CM",
                Weight = 3.00m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 892,
                Name = "HL Touring Frame - Blue, 54",
                ProductNumber = "FR-T98U-54",
                Color = "Blue",
                ListPrice = 1003.9100m,
                Size = "54",
                SizeUnitMeasureCode = "CM",
                Weight = 3.04m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 893,
                Name = "HL Touring Frame - Blue, 60",
                ProductNumber = "FR-T98U-60",
                Color = "Blue",
                ListPrice = 1003.9100m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 3.08m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 894,
                Name = "Rear Derailleur",
                ProductNumber = "RD-2308",
                Color = "Silver",
                ListPrice = 121.4600m,
                Weight = 215.00m,
                WeightUnitMeasureCode = "G",
                Subcategory = new Subcategory()
                {
                    Id = 9,
                    Name = "Derailleurs",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 895,
                Name = "LL Touring Frame - Blue, 50",
                ProductNumber = "FR-T67U-50",
                Color = "Blue",
                ListPrice = 333.4200m,
                Size = "50",
                SizeUnitMeasureCode = "CM",
                Weight = 3.10m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 896,
                Name = "LL Touring Frame - Blue, 54",
                ProductNumber = "FR-T67U-54",
                Color = "Blue",
                ListPrice = 333.4200m,
                Size = "54",
                SizeUnitMeasureCode = "CM",
                Weight = 3.14m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 897,
                Name = "LL Touring Frame - Blue, 58",
                ProductNumber = "FR-T67U-58",
                Color = "Blue",
                ListPrice = 333.4200m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 3.16m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 898,
                Name = "LL Touring Frame - Blue, 62",
                ProductNumber = "FR-T67U-62",
                Color = "Blue",
                ListPrice = 333.4200m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 3.20m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 899,
                Name = "LL Touring Frame - Yellow, 44",
                ProductNumber = "FR-T67Y-44",
                Color = "Yellow",
                ListPrice = 333.4200m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 3.02m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 900,
                Name = "LL Touring Frame - Yellow, 50",
                ProductNumber = "FR-T67Y-50",
                Color = "Yellow",
                ListPrice = 333.4200m,
                Size = "50",
                SizeUnitMeasureCode = "CM",
                Weight = 3.10m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 901,
                Name = "LL Touring Frame - Yellow, 54",
                ProductNumber = "FR-T67Y-54",
                Color = "Yellow",
                ListPrice = 333.4200m,
                Size = "54",
                SizeUnitMeasureCode = "CM",
                Weight = 3.14m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 902,
                Name = "LL Touring Frame - Yellow, 58",
                ProductNumber = "FR-T67Y-58",
                Color = "Yellow",
                ListPrice = 333.4200m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 3.16m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 903,
                Name = "LL Touring Frame - Blue, 44",
                ProductNumber = "FR-T67U-44",
                Color = "Blue",
                ListPrice = 333.4200m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 3.02m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 16,
                    Name = "Touring Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 904,
                Name = "ML Mountain Frame-W - Silver, 40",
                ProductNumber = "FR-M63S-40",
                Color = "Silver",
                ListPrice = 364.0900m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 2.77m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 905,
                Name = "ML Mountain Frame-W - Silver, 42",
                ProductNumber = "FR-M63S-42",
                Color = "Silver",
                ListPrice = 364.0900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 2.81m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 906,
                Name = "ML Mountain Frame-W - Silver, 46",
                ProductNumber = "FR-M63S-46",
                Color = "Silver",
                ListPrice = 364.0900m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 2.85m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 907,
                Name = "Rear Brakes",
                ProductNumber = "RB-9231",
                Color = "Silver",
                ListPrice = 106.5000m,
                Weight = 317.00m,
                WeightUnitMeasureCode = "G",
                Subcategory = new Subcategory()
                {
                    Id = 6,
                    Name = "Brakes",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 908,
                Name = "LL Mountain Seat/Saddle",
                ProductNumber = "SE-M236",
                ListPrice = 27.1200m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 15,
                    Name = "Saddles",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 909,
                Name = "ML Mountain Seat/Saddle",
                ProductNumber = "SE-M798",
                ListPrice = 39.1400m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 15,
                    Name = "Saddles",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 910,
                Name = "HL Mountain Seat/Saddle",
                ProductNumber = "SE-M940",
                ListPrice = 52.6400m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 15,
                    Name = "Saddles",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 911,
                Name = "LL Road Seat/Saddle",
                ProductNumber = "SE-R581",
                ListPrice = 27.1200m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 15,
                    Name = "Saddles",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 912,
                Name = "ML Road Seat/Saddle",
                ProductNumber = "SE-R908",
                ListPrice = 39.1400m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 15,
                    Name = "Saddles",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 913,
                Name = "HL Road Seat/Saddle",
                ProductNumber = "SE-R995",
                ListPrice = 52.6400m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 15,
                    Name = "Saddles",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 914,
                Name = "LL Touring Seat/Saddle",
                ProductNumber = "SE-T312",
                ListPrice = 27.1200m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 15,
                    Name = "Saddles",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 915,
                Name = "ML Touring Seat/Saddle",
                ProductNumber = "SE-T762",
                ListPrice = 39.1400m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 15,
                    Name = "Saddles",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 916,
                Name = "HL Touring Seat/Saddle",
                ProductNumber = "SE-T924",
                ListPrice = 52.6400m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 15,
                    Name = "Saddles",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 917,
                Name = "LL Mountain Frame - Silver, 42",
                ProductNumber = "FR-M21S-42",
                Color = "Silver",
                ListPrice = 264.0500m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 2.92m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 918,
                Name = "LL Mountain Frame - Silver, 44",
                ProductNumber = "FR-M21S-44",
                Color = "Silver",
                ListPrice = 264.0500m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.96m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 919,
                Name = "LL Mountain Frame - Silver, 48",
                ProductNumber = "FR-M21S-48",
                Color = "Silver",
                ListPrice = 264.0500m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 3.00m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 920,
                Name = "LL Mountain Frame - Silver, 52",
                ProductNumber = "FR-M21S-52",
                Color = "Silver",
                ListPrice = 264.0500m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 3.04m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 921,
                Name = "Mountain Tire Tube",
                ProductNumber = "TT-M928",
                ListPrice = 4.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 922,
                Name = "Road Tire Tube",
                ProductNumber = "TT-R982",
                ListPrice = 3.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 923,
                Name = "Touring Tire Tube",
                ProductNumber = "TT-T092",
                ListPrice = 4.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 924,
                Name = "LL Mountain Frame - Black, 42",
                ProductNumber = "FR-M21B-42",
                Color = "Black",
                ListPrice = 249.7900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 2.92m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 925,
                Name = "LL Mountain Frame - Black, 44",
                ProductNumber = "FR-M21B-44",
                Color = "Black",
                ListPrice = 249.7900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 2.96m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 926,
                Name = "LL Mountain Frame - Black, 48",
                ProductNumber = "FR-M21B-48",
                Color = "Black",
                ListPrice = 249.7900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 3.00m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 927,
                Name = "LL Mountain Frame - Black, 52",
                ProductNumber = "FR-M21B-52",
                Color = "Black",
                ListPrice = 249.7900m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 3.04m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 928,
                Name = "LL Mountain Tire",
                ProductNumber = "TI-M267",
                ListPrice = 24.9900m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 929,
                Name = "ML Mountain Tire",
                ProductNumber = "TI-M602",
                ListPrice = 29.9900m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 930,
                Name = "HL Mountain Tire",
                ProductNumber = "TI-M823",
                ListPrice = 35.0000m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 931,
                Name = "LL Road Tire",
                ProductNumber = "TI-R092",
                ListPrice = 21.4900m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 932,
                Name = "ML Road Tire",
                ProductNumber = "TI-R628",
                ListPrice = 24.9900m,
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 933,
                Name = "HL Road Tire",
                ProductNumber = "TI-R982",
                ListPrice = 32.6000m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 934,
                Name = "Touring Tire",
                ProductNumber = "TI-T723",
                ListPrice = 28.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 37,
                    Name = "Tires and Tubes",
                    Category = new Category()
                    {
                        Id = 4,
                        Name = "Accessories"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 935,
                Name = "LL Mountain Pedal",
                ProductNumber = "PD-M282",
                Color = "Silver/Black",
                ListPrice = 40.4900m,
                Weight = 218.00m,
                WeightUnitMeasureCode = "G",
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 13,
                    Name = "Pedals",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 936,
                Name = "ML Mountain Pedal",
                ProductNumber = "PD-M340",
                Color = "Silver/Black",
                ListPrice = 62.0900m,
                Weight = 215.00m,
                WeightUnitMeasureCode = "G",
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 13,
                    Name = "Pedals",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 937,
                Name = "HL Mountain Pedal",
                ProductNumber = "PD-M562",
                Color = "Silver/Black",
                ListPrice = 80.9900m,
                Weight = 185.00m,
                WeightUnitMeasureCode = "G",
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 13,
                    Name = "Pedals",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 938,
                Name = "LL Road Pedal",
                ProductNumber = "PD-R347",
                Color = "Silver/Black",
                ListPrice = 40.4900m,
                Weight = 189.00m,
                WeightUnitMeasureCode = "G",
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 13,
                    Name = "Pedals",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 939,
                Name = "ML Road Pedal",
                ProductNumber = "PD-R563",
                Color = "Silver/Black",
                ListPrice = 62.0900m,
                Weight = 168.00m,
                WeightUnitMeasureCode = "G",
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 13,
                    Name = "Pedals",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 940,
                Name = "HL Road Pedal",
                ProductNumber = "PD-R853",
                Color = "Silver/Black",
                ListPrice = 80.9900m,
                Weight = 149.00m,
                WeightUnitMeasureCode = "G",
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 13,
                    Name = "Pedals",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 941,
                Name = "Touring Pedal",
                ProductNumber = "PD-T852",
                Color = "Silver/Black",
                ListPrice = 80.9900m,
                Subcategory = new Subcategory()
                {
                    Id = 13,
                    Name = "Pedals",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 942,
                Name = "ML Mountain Frame-W - Silver, 38",
                ProductNumber = "FR-M63S-38",
                Color = "Silver",
                ListPrice = 364.0900m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 2.73m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 943,
                Name = "LL Mountain Frame - Black, 40",
                ProductNumber = "FR-M21B-40",
                Color = "Black",
                ListPrice = 249.7900m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 2.88m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 944,
                Name = "LL Mountain Frame - Silver, 40",
                ProductNumber = "FR-M21S-40",
                Color = "Silver",
                ListPrice = 264.0500m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 2.88m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 12,
                    Name = "Mountain Frames",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 945,
                Name = "Front Derailleur",
                ProductNumber = "FD-2342",
                Color = "Silver",
                ListPrice = 91.4900m,
                Weight = 88.00m,
                WeightUnitMeasureCode = "G",
                Subcategory = new Subcategory()
                {
                    Id = 9,
                    Name = "Derailleurs",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 946,
                Name = "LL Touring Handlebars",
                ProductNumber = "HB-T721",
                ListPrice = 46.0900m,
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 4,
                    Name = "Handlebars",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 947,
                Name = "HL Touring Handlebars",
                ProductNumber = "HB-T928",
                ListPrice = 91.5700m,
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 4,
                    Name = "Handlebars",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 948,
                Name = "Front Brakes",
                ProductNumber = "FB-9873",
                Color = "Silver",
                ListPrice = 106.5000m,
                Weight = 317.00m,
                WeightUnitMeasureCode = "G",
                Subcategory = new Subcategory()
                {
                    Id = 6,
                    Name = "Brakes",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 949,
                Name = "LL Crankset",
                ProductNumber = "CS-4759",
                Color = "Black",
                ListPrice = 175.4900m,
                Weight = 600.00m,
                WeightUnitMeasureCode = "G",
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 8,
                    Name = "Cranksets",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 950,
                Name = "ML Crankset",
                ProductNumber = "CS-6583",
                Color = "Black",
                ListPrice = 256.4900m,
                Weight = 635.00m,
                WeightUnitMeasureCode = "G",
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 8,
                    Name = "Cranksets",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 951,
                Name = "HL Crankset",
                ProductNumber = "CS-9183",
                Color = "Black",
                ListPrice = 404.9900m,
                Weight = 575.00m,
                WeightUnitMeasureCode = "G",
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 8,
                    Name = "Cranksets",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 952,
                Name = "Chain",
                ProductNumber = "CH-0234",
                Color = "Silver",
                ListPrice = 20.2400m,
                Subcategory = new Subcategory()
                {
                    Id = 7,
                    Name = "Chains",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 953,
                Name = "Touring-2000 Blue, 60",
                ProductNumber = "BK-T44U-60",
                Color = "Blue",
                ListPrice = 1214.8500m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 27.90m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 954,
                Name = "Touring-1000 Yellow, 46",
                ProductNumber = "BK-T79Y-46",
                Color = "Yellow",
                ListPrice = 2384.0700m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 25.13m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 955,
                Name = "Touring-1000 Yellow, 50",
                ProductNumber = "BK-T79Y-50",
                Color = "Yellow",
                ListPrice = 2384.0700m,
                Size = "50",
                SizeUnitMeasureCode = "CM",
                Weight = 25.42m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 956,
                Name = "Touring-1000 Yellow, 54",
                ProductNumber = "BK-T79Y-54",
                Color = "Yellow",
                ListPrice = 2384.0700m,
                Size = "54",
                SizeUnitMeasureCode = "CM",
                Weight = 25.68m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 957,
                Name = "Touring-1000 Yellow, 60",
                ProductNumber = "BK-T79Y-60",
                Color = "Yellow",
                ListPrice = 2384.0700m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 25.90m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 958,
                Name = "Touring-3000 Blue, 54",
                ProductNumber = "BK-T18U-54",
                Color = "Blue",
                ListPrice = 742.3500m,
                Size = "54",
                SizeUnitMeasureCode = "CM",
                Weight = 29.68m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 959,
                Name = "Touring-3000 Blue, 58",
                ProductNumber = "BK-T18U-58",
                Color = "Blue",
                ListPrice = 742.3500m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 29.90m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 960,
                Name = "Touring-3000 Blue, 62",
                ProductNumber = "BK-T18U-62",
                Color = "Blue",
                ListPrice = 742.3500m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 30.00m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 961,
                Name = "Touring-3000 Yellow, 44",
                ProductNumber = "BK-T18Y-44",
                Color = "Yellow",
                ListPrice = 742.3500m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 28.77m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 962,
                Name = "Touring-3000 Yellow, 50",
                ProductNumber = "BK-T18Y-50",
                Color = "Yellow",
                ListPrice = 742.3500m,
                Size = "50",
                SizeUnitMeasureCode = "CM",
                Weight = 29.13m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 963,
                Name = "Touring-3000 Yellow, 54",
                ProductNumber = "BK-T18Y-54",
                Color = "Yellow",
                ListPrice = 742.3500m,
                Size = "54",
                SizeUnitMeasureCode = "CM",
                Weight = 29.42m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 964,
                Name = "Touring-3000 Yellow, 58",
                ProductNumber = "BK-T18Y-58",
                Color = "Yellow",
                ListPrice = 742.3500m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 29.79m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 965,
                Name = "Touring-3000 Yellow, 62",
                ProductNumber = "BK-T18Y-62",
                Color = "Yellow",
                ListPrice = 742.3500m,
                Size = "62",
                SizeUnitMeasureCode = "CM",
                Weight = 30.00m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 966,
                Name = "Touring-1000 Blue, 46",
                ProductNumber = "BK-T79U-46",
                Color = "Blue",
                ListPrice = 2384.0700m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 25.13m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 967,
                Name = "Touring-1000 Blue, 50",
                ProductNumber = "BK-T79U-50",
                Color = "Blue",
                ListPrice = 2384.0700m,
                Size = "50",
                SizeUnitMeasureCode = "CM",
                Weight = 25.42m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 968,
                Name = "Touring-1000 Blue, 54",
                ProductNumber = "BK-T79U-54",
                Color = "Blue",
                ListPrice = 2384.0700m,
                Size = "54",
                SizeUnitMeasureCode = "CM",
                Weight = 25.68m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 969,
                Name = "Touring-1000 Blue, 60",
                ProductNumber = "BK-T79U-60",
                Color = "Blue",
                ListPrice = 2384.0700m,
                Size = "60",
                SizeUnitMeasureCode = "CM",
                Weight = 25.90m,
                WeightUnitMeasureCode = "LB",
                Class = "H",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 970,
                Name = "Touring-2000 Blue, 46",
                ProductNumber = "BK-T44U-46",
                Color = "Blue",
                ListPrice = 1214.8500m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 27.13m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 971,
                Name = "Touring-2000 Blue, 50",
                ProductNumber = "BK-T44U-50",
                Color = "Blue",
                ListPrice = 1214.8500m,
                Size = "50",
                SizeUnitMeasureCode = "CM",
                Weight = 27.42m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 972,
                Name = "Touring-2000 Blue, 54",
                ProductNumber = "BK-T44U-54",
                Color = "Blue",
                ListPrice = 1214.8500m,
                Size = "54",
                SizeUnitMeasureCode = "CM",
                Weight = 27.68m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 973,
                Name = "Road-350-W Yellow, 40",
                ProductNumber = "BK-R79Y-40",
                Color = "Yellow",
                ListPrice = 1700.9900m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 15.35m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 974,
                Name = "Road-350-W Yellow, 42",
                ProductNumber = "BK-R79Y-42",
                Color = "Yellow",
                ListPrice = 1700.9900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 15.77m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 975,
                Name = "Road-350-W Yellow, 44",
                ProductNumber = "BK-R79Y-44",
                Color = "Yellow",
                ListPrice = 1700.9900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 16.13m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 976,
                Name = "Road-350-W Yellow, 48",
                ProductNumber = "BK-R79Y-48",
                Color = "Yellow",
                ListPrice = 1700.9900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 16.42m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 977,
                Name = "Road-750 Black, 58",
                ProductNumber = "BK-R19B-58",
                Color = "Black",
                ListPrice = 539.9900m,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                Weight = 20.79m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 978,
                Name = "Touring-3000 Blue, 44",
                ProductNumber = "BK-T18U-44",
                Color = "Blue",
                ListPrice = 742.3500m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 28.77m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 979,
                Name = "Touring-3000 Blue, 50",
                ProductNumber = "BK-T18U-50",
                Color = "Blue",
                ListPrice = 742.3500m,
                Size = "50",
                SizeUnitMeasureCode = "CM",
                Weight = 29.13m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 3,
                    Name = "Touring Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 980,
                Name = "Mountain-400-W Silver, 38",
                ProductNumber = "BK-M38S-38",
                Color = "Silver",
                ListPrice = 769.4900m,
                Size = "38",
                SizeUnitMeasureCode = "CM",
                Weight = 26.35m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 981,
                Name = "Mountain-400-W Silver, 40",
                ProductNumber = "BK-M38S-40",
                Color = "Silver",
                ListPrice = 769.4900m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 26.77m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 982,
                Name = "Mountain-400-W Silver, 42",
                ProductNumber = "BK-M38S-42",
                Color = "Silver",
                ListPrice = 769.4900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 27.13m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 983,
                Name = "Mountain-400-W Silver, 46",
                ProductNumber = "BK-M38S-46",
                Color = "Silver",
                ListPrice = 769.4900m,
                Size = "46",
                SizeUnitMeasureCode = "CM",
                Weight = 27.42m,
                WeightUnitMeasureCode = "LB",
                Class = "M",
                Style = "W",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 984,
                Name = "Mountain-500 Silver, 40",
                ProductNumber = "BK-M18S-40",
                Color = "Silver",
                ListPrice = 564.9900m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 27.35m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 985,
                Name = "Mountain-500 Silver, 42",
                ProductNumber = "BK-M18S-42",
                Color = "Silver",
                ListPrice = 564.9900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 27.77m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 986,
                Name = "Mountain-500 Silver, 44",
                ProductNumber = "BK-M18S-44",
                Color = "Silver",
                ListPrice = 564.9900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 28.13m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 987,
                Name = "Mountain-500 Silver, 48",
                ProductNumber = "BK-M18S-48",
                Color = "Silver",
                ListPrice = 564.9900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 28.42m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 988,
                Name = "Mountain-500 Silver, 52",
                ProductNumber = "BK-M18S-52",
                Color = "Silver",
                ListPrice = 564.9900m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 28.68m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 989,
                Name = "Mountain-500 Black, 40",
                ProductNumber = "BK-M18B-40",
                Color = "Black",
                ListPrice = 539.9900m,
                Size = "40",
                SizeUnitMeasureCode = "CM",
                Weight = 27.35m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 990,
                Name = "Mountain-500 Black, 42",
                ProductNumber = "BK-M18B-42",
                Color = "Black",
                ListPrice = 539.9900m,
                Size = "42",
                SizeUnitMeasureCode = "CM",
                Weight = 27.77m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 991,
                Name = "Mountain-500 Black, 44",
                ProductNumber = "BK-M18B-44",
                Color = "Black",
                ListPrice = 539.9900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 28.13m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 992,
                Name = "Mountain-500 Black, 48",
                ProductNumber = "BK-M18B-48",
                Color = "Black",
                ListPrice = 539.9900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 28.42m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 993,
                Name = "Mountain-500 Black, 52",
                ProductNumber = "BK-M18B-52",
                Color = "Black",
                ListPrice = 539.9900m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 28.68m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 1,
                    Name = "Mountain Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 994,
                Name = "LL Bottom Bracket",
                ProductNumber = "BB-7421",
                ListPrice = 53.9900m,
                Weight = 223.00m,
                WeightUnitMeasureCode = "G",
                Class = "L",
                Subcategory = new Subcategory()
                {
                    Id = 5,
                    Name = "Bottom Brackets",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 995,
                Name = "ML Bottom Bracket",
                ProductNumber = "BB-8107",
                ListPrice = 101.2400m,
                Weight = 168.00m,
                WeightUnitMeasureCode = "G",
                Class = "M",
                Subcategory = new Subcategory()
                {
                    Id = 5,
                    Name = "Bottom Brackets",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 996,
                Name = "HL Bottom Bracket",
                ProductNumber = "BB-9108",
                ListPrice = 121.4900m,
                Weight = 170.00m,
                WeightUnitMeasureCode = "G",
                Class = "H",
                Subcategory = new Subcategory()
                {
                    Id = 5,
                    Name = "Bottom Brackets",
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Components"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 997,
                Name = "Road-750 Black, 44",
                ProductNumber = "BK-R19B-44",
                Color = "Black",
                ListPrice = 539.9900m,
                Size = "44",
                SizeUnitMeasureCode = "CM",
                Weight = 19.77m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 998,
                Name = "Road-750 Black, 48",
                ProductNumber = "BK-R19B-48",
                Color = "Black",
                ListPrice = 539.9900m,
                Size = "48",
                SizeUnitMeasureCode = "CM",
                Weight = 20.13m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
            ProductRepository.Products.Add(new Product()
            {
                Id = 999,
                Name = "Road-750 Black, 52",
                ProductNumber = "BK-R19B-52",
                Color = "Black",
                ListPrice = 539.9900m,
                Size = "52",
                SizeUnitMeasureCode = "CM",
                Weight = 20.42m,
                WeightUnitMeasureCode = "LB",
                Class = "L",
                Style = "U",
                Subcategory = new Subcategory()
                {
                    Id = 2,
                    Name = "Road Bikes",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Bikes"
                    }
                }
            });
        }

        public ICollection<Product> GetProducts(int subcategoryId)
        {
            return ProductRepository.Products
                .Where(p => p.Subcategory.Id == subcategoryId)
                .ToList();
        }

        public Product GetProduct(int productId)
        {
            return ProductRepository.Products
                .Where(p => p.Id == productId)
                .SingleOrDefault();
        }

        public bool ProductExists(int productId)
        {
            return ProductRepository.Products.Any(p => p.Id == productId);
        }
    }
}
