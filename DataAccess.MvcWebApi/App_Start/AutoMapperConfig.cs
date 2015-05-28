//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.MvcWebApi
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using DE = DataAccess.Domain;
    using DTO = DataAccess.MvcWebApi.Models;

    public static class AutomapperConfig
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This code is required to configure AutoMapper correctly.  It could potentially be broken down into smaller methods later."),
        SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This code is required to configure AutoMapper correctly.  It could potentially be broken down into smaller methods later.")]
        public static void SetAutoMapperConfiguration()
        {
            // define the mapping from the Person domain entity to the PersonDetail DTO including address, credit cards, and email address
            Mapper.CreateMap<DE.Person.Person, DTO.PersonDetail>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses))
                .ForMember(dest => dest.CreditCards, opt => opt.MapFrom(src => src.CreditCards))
                .ForMember(dest => dest.EmailAddresses, opt => opt.MapFrom(src => src.EmailAddresses));

            // individual mapping definitions for Address and CreditCard domain entities to DTO's
            Mapper.CreateMap<DE.Person.Address, DTO.AddressInfo>();
            Mapper.CreateMap<DE.Person.CreditCard, DTO.CreditCardInfo>()
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber.Substring(src.CardNumber.Length - 4, 4)));

            // define the mapping from the ShoppingCartItem domain entity to the CartItme DTO
            Mapper.CreateMap<DE.ShoppingCart.ShoppingCartItem, DTO.CartItem>();

            // define the mapping from the CartItem DTO to the domain entity ShoppingCartItem
            Mapper.CreateMap<DTO.CartItem, DE.ShoppingCart.ShoppingCartItem>();

            // define the mapping from the Category domain entity to the Category DTO
            Mapper.CreateMap<DE.Catalog.Category, DTO.Category>();

            // define the mapping from the Order domain entity to the OrderDetail DTO including billing and shipping addresses, credit card and order details
            Mapper.CreateMap<DE.Order.Order, DTO.OrderDetail>()
                .ForMember(dest => dest.BillingAddress, opt => opt.MapFrom(src => src.BillToAddress))
                .ForMember(dest => dest.CreditCard, opt => opt.MapFrom(src => src.CreditCard))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            // define the mapping from the OrderItem domain entity to the OrderItemInfo DTO
            Mapper.CreateMap<DE.Order.OrderItem, DTO.OrderItemInfo>()
                .ForMember(dest => dest.ProductName, src => src.MapFrom(dest => dest.Product == null ? null : dest.Product.Name))
                .ForMember(dest => dest.ProductId, src => src.MapFrom(dest => dest.Product == null ? 0 : dest.Product.Id));

            // define the mapping from the OrderHistory domain entity to the OrderHistoryInfo DTO including the billing and shipping addresses, credit card and order details
            Mapper.CreateMap<DE.Order.OrderHistory, DTO.OrderHistoryInfo>()
                .ForMember(dest => dest.BillingAddress, opt => opt.MapFrom(src => src.BillToAddress))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
                .ForMember(dest => dest.CreditCard, opt => opt.MapFrom(src => src.CreditCard))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            // define the mapping from the Product domain entity to the ProductInfo DTO
            Mapper.CreateMap<DE.Catalog.Product, DTO.ProductInfo>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(dest => dest.Subcategory == null ? 0 : (dest.Subcategory.Category == null ? 0 : dest.Subcategory.Category.Id)))
                .ForMember(dest => dest.CategoryName, src => src.MapFrom(dest => dest.Subcategory == null ? null : (dest.Subcategory.Category == null ? null : dest.Subcategory.Category.Name)))
                .ForMember(dest => dest.SubcategoryName, src => src.MapFrom(dest => dest.Subcategory == null ? null : dest.Subcategory.Name));

            // define the mapping from the Product domain entity to the ProductDetail DTO
            Mapper.CreateMap<DE.Catalog.Product, DTO.ProductDetail>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(dest => dest.Subcategory == null ? 0 : (dest.Subcategory.Category == null ? 0 : dest.Subcategory.Category.Id)))
                .ForMember(dest => dest.CategoryName, src => src.MapFrom(dest => dest.Subcategory == null ? null : (dest.Subcategory.Category == null ? null : dest.Subcategory.Category.Name)))
                .ForMember(dest => dest.SubcategoryName, src => src.MapFrom(dest => dest.Subcategory == null ? null : dest.Subcategory.Name))
                .ForMember(dest => dest.ParentId, src => src.MapFrom(dest => dest.Subcategory == null ? 0 : dest.Subcategory.Id));

            // define the mapping from the RecommendedProduct domain entity to the Recommendation DTO
            Mapper.CreateMap<DE.Catalog.RecommendedProduct, DTO.Recommendation>()
                .ForMember(dest => dest.Id, src => src.MapFrom(dest => dest.ProductId))
                .ForMember(dest => dest.Percentage, src => src.MapFrom(dest => dest.Percentage == null ? 0 : (int)dest.Percentage * 100));

            // define the mapping fromt the Subcategory domain entity to the Subcategory DTO
            Mapper.CreateMap<DE.Catalog.Subcategory, DTO.Subcategory>()
                .ForMember(dest => dest.ParentId, src => src.MapFrom(dest => dest.Category == null ? 0 : dest.Category.Id))
                .ForMember(dest => dest.CategoryName, src => src.MapFrom(dest => dest.Category == null ? null : dest.Category.Name));
        }
    }
}