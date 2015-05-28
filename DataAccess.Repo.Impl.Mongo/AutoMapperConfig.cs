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
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using DE = DataAccess.Domain;
    using ME = DataAccess.Repo.Impl.Mongo;

    public static class AutoMapperConfig
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This code is required to configure AutoMapper correctly.  It could potentially be broken down into smaller methods later.")]
        public static void SetAutoMapperConfiguration()
        {
            Mapper.CreateMap<ME.Catalog.Category, DE.Catalog.Category>();

            Mapper.CreateMap<ME.Catalog.Subcategory, DE.Catalog.Subcategory>()
                .BeforeMap((src, dest) => dest.Category = new DE.Catalog.Category());

            // we have to create the Subcategory and Category domain entities for the Product domain entity
            // and assign the name and id properties manually since the objects look so different
            Mapper.CreateMap<ME.Catalog.Product, DE.Catalog.Product>()
                .ForMember(dest => dest.Weight, conf => conf.MapFrom(src => src.Weight == null ? (decimal?)null : src.Weight.Value))
                .ForMember(dest => dest.WeightUnitMeasureCode, conf => conf.MapFrom(src => src.Weight == null ? null : src.Weight.UnitOfMeasure))
                .ForMember(dest => dest.Size, conf => conf.MapFrom(src => src.Size == null ? null : src.Size.Value))
                .ForMember(dest => dest.SizeUnitMeasureCode, conf => conf.MapFrom(src => src.Size == null ? null : src.Size.UnitOfMeasure))
                .AfterMap((src, dest) =>
                {
                    dest.Subcategory = new DE.Catalog.Subcategory()
                    {
                        Category = new DE.Catalog.Category()
                        {
                            Id = src.Category.CategoryId,
                            Name = src.Category.Name
                        },
                        Id = src.Category.Subcategory.SubcategoryId,
                        Name = src.Category.Subcategory.Name
                    };
                });

            Mapper.CreateMap<ME.Order.OrderHistory, DE.Order.OrderHistory>()
                .ForMember(dest => dest.HistoryId, src => src.MapFrom(dest => dest.Id))
                .ForMember(dest => dest.TrackingId, src => src.MapFrom(dest => dest.OrderCode));

            Mapper.CreateMap<ME.Order.OrderItem, DE.Order.OrderItem>()
                .AfterMap((src, dest) => dest.Product = new DE.Catalog.Product() { Id = src.ProductId, Name = src.ProductName });

            Mapper.CreateMap<ME.Order.Address, DE.Person.Address>();
            Mapper.CreateMap<ME.Order.CreditCard, DE.Person.CreditCard>();

            Mapper.CreateMap<DE.Order.OrderHistory, ME.Order.OrderHistory>()
                .ForMember(dest => dest.Items, src => src.MapFrom(dest => dest.OrderItems));

            Mapper.CreateMap<DE.Person.Address, ME.Order.Address>();
            Mapper.CreateMap<DE.Person.CreditCard, ME.Order.CreditCard>();

            Mapper.CreateMap<DE.Order.OrderItem, ME.Order.OrderItem>()
                .ForMember(dest => dest.ProductId, src => src.MapFrom(dest => dest.Product.Id))
                .ForMember(dest => dest.ProductName, src => src.MapFrom(dest => dest.Product.Name));
        }
    }
}