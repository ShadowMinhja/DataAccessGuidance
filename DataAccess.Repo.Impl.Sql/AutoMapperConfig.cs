//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Sql
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using DE = DataAccess.Domain;

    internal static class AutoMapperConfig
    {
        private static bool autoMapperConfigured = false;
        private static object autoMapperLock = new object();

        public static void SetAutoMapperConfiguration()
        {
            if (!autoMapperConfigured)
            {
                lock (autoMapperLock)
                {
                    if (!autoMapperConfigured)
                    {
                        autoMapperConfigured = true;
                        SetAutoMapperConfigurationPrivate();
                    }
                }
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This code is required to configure AutoMapper correctly.  It could potentially be broken down into smaller methods later."),
        SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This code is required to configuring AutoMapper correctly.  It could potentially be broken down into smaller methods later.")]
        private static void SetAutoMapperConfigurationPrivate()
        {
            // define the mapping from the InventoryProduct entity to the InventoryProduct domain entity
            Mapper.CreateMap<Order.InventoryProduct, DE.Order.InventoryProduct>();

            // define the mapping from the Order domain entity to the SalesOrderHeader entity
            Mapper.CreateMap<DE.Order.Order, Order.SalesOrderHeader>()
                .ForMember(dest => dest.OnlineOrderFlag, src => src.MapFrom(val => true)) // static value
                .ForMember(dest => dest.RevisionNumber, src => src.MapFrom(val => 3)) // static value
                .ForMember(dest => dest.SalesOrderDetails, src => src.MapFrom(val => val.OrderItems))
                .ForMember(dest => dest.ShipMethodId, src => src.MapFrom(val => 5)) // static value
                .ForMember(dest => dest.Status, src => src.MapFrom(val => (byte)val.Status))
                .ForMember(dest => dest.TaxAmt, src => src.MapFrom(val => 0)) // static value
                .ForMember(dest => dest.BillToAddressId, src => src.MapFrom(val => val.BillToAddress.Id))
                .ForMember(dest => dest.ShipToAddressId, src => src.MapFrom(val => val.ShippingAddress.Id))
                .ForMember(dest => dest.CreditCardId, src => src.MapFrom(val => val.CreditCard.Id))
                .ForMember(dest => dest.BillToAddress, src => src.Ignore())
                .ForMember(dest => dest.CreditCard, src => src.Ignore());

            // define the mapping from the OrderItem domain entity to the SalesOrderDetail entity
            Mapper.CreateMap<DE.Order.OrderItem, Order.SalesOrderDetail>()
                .ForMember(dest => dest.OrderQty, src => src.MapFrom(val => val.Quantity))
                .ForMember(dest => dest.ProductId, src => src.MapFrom(val => val.Product.Id))
                .ForMember(dest => dest.SpecialOfferId, src => src.MapFrom(val => 1)) // static value
                .ForMember(dest => dest.UnitPriceDiscount, src => src.MapFrom(val => 0)); // static value

            // define the mapping from the Person entity to the Person domain entity
            Mapper.CreateMap<Person.Person, DE.Person.Person>()
                .ForMember(dest => dest.Id, src => src.MapFrom(val => val.BusinessEntityId))
                .ForMember(dest => dest.PasswordHash, src => src.MapFrom(val => val.Password.PasswordHash))
                .ForMember(dest => dest.PasswordSalt, src => src.MapFrom(val => val.Password.PasswordSalt));

            // define the mapping from the Person domain entity to the Person entity including static values
            Mapper.CreateMap<DE.Person.Person, Person.Person>()
                .ForMember(dest => dest.BusinessEntityId, src => src.MapFrom(val => val.Id))
                .ForMember(dest => dest.EmailPromotion, src => src.MapFrom(val => 0)) // static value
                .ForMember(dest => dest.NameStyle, src => src.MapFrom(val => false)) // static value
                .ForMember(dest => dest.PersonType, src => src.MapFrom(val => "EM")) // static value
                // the dest (dest.Password.PasswordHash is not allowed) must resolve to a top-level member, so we do this after map instead
                .ForMember(dest => dest.EmailAddresses, src => src.Ignore())
                .ForMember(dest => dest.Addresses, src => src.Ignore())
                .AfterMap((src, dest) => dest.Password.PasswordHash = src.PasswordHash)
                .AfterMap((src, dest) => dest.Password.PasswordSalt = src.PasswordSalt);

            // define the mapping from the PersonBusinessEntityAddress join entity to the Person domain entity
            Mapper.CreateMap<Person.PersonBusinessEntityAddress, DE.Person.Address>()
                .ForMember(dest => dest.AddressLine1, src => src.MapFrom(dest => dest.Address.AddressLine1))
                .ForMember(dest => dest.AddressLine2, src => src.MapFrom(dest => dest.Address.AddressLine2))
                .ForMember(dest => dest.City, src => src.MapFrom(dest => dest.Address.City))
                .ForMember(dest => dest.Id, src => src.MapFrom(dest => dest.Address.AddressId))
                .ForMember(dest => dest.PostalCode, src => src.MapFrom(dest => dest.Address.PostalCode))
                .ForMember(dest => dest.StateProvinceId, src => src.MapFrom(dest => dest.Address.StateProvinceId));

            // define the mapping from the Address domain entity to the PersonAddress entity
            Mapper.CreateMap<DE.Person.Address, Person.PersonAddress>()
                .ForMember(dest => dest.AddressLine1, src => src.MapFrom(dest => dest.AddressLine1))
                .ForMember(dest => dest.AddressLine2, src => src.MapFrom(dest => dest.AddressLine2))
                .ForMember(dest => dest.City, src => src.MapFrom(dest => dest.City))
                .ForMember(dest => dest.AddressId, src => src.MapFrom(dest => dest.Id))
                .ForMember(dest => dest.PostalCode, src => src.MapFrom(dest => dest.PostalCode))
                .ForMember(dest => dest.StateProvinceId, src => src.MapFrom(dest => dest.StateProvinceId));

            Mapper.CreateMap<Person.PersonAddress, DE.Person.Address>()
                .ForMember(dest => dest.Id, src => src.MapFrom(val => val.AddressId));

            Mapper.CreateMap<Person.PersonCreditCard, DE.Person.CreditCard>()
                .ForMember(dest => dest.Id, src => src.MapFrom(val => val.CreditCardId));

            Mapper.CreateMap<DE.Person.CreditCard, Person.PersonCreditCard>()
                .ForMember(dest => dest.CreditCardId, src => src.MapFrom(val => val.Id));

            Mapper.CreateMap<StateProvince.StateProvince, DE.Person.StateProvince>();
        }
    }
}