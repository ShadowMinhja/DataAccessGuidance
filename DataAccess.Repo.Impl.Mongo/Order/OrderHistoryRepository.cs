//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Mongo.Order
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using DataAccess.Repo.Impl.Mongo.Resources;
    using DataAccess.Repository;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using DE = DataAccess.Domain.Order;

    public class OrderHistoryRepository : BaseRepository, IOrderHistoryRepository
    {
        private const string MongoCollection = "ordershistory";

        public OrderHistoryRepository(string hostNames, string databaseName, bool setWriteConcernToJournal, bool setWriteConcernToWMajority)
            : base(hostNames, databaseName, setWriteConcernToJournal, setWriteConcernToWMajority)
        {
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This method could potentially be refactored to remove some of the coupling.")]
        public DE.Order GetPendingOrderByTrackingId(Guid trackingId)
        {
            if (Guid.Empty.Equals(trackingId))
            {
                throw new ArgumentNullException("trackingId");
            }

            try
            {
                IMongoQuery query = null;
                var collection = GetDatabase().GetCollection<OrderHistory>(MongoCollection);
                query = Query.And(
                    Query<OrderHistory>.EQ(e => e.OrderCode, trackingId),
                    Query<OrderHistory>.EQ(e => e.Status, DE.OrderStatus.Pending));
                var mongoHistory = collection.Find(query).FirstOrDefault();

                if (mongoHistory == null)
                {
                    return null;
                }

                var orderHistory = new DE.OrderHistory();
                var domainOrderItems = new List<DE.OrderItem>();

                Mapper.Map(mongoHistory, orderHistory);
                if (mongoHistory.Items != null)
                {
                    Mapper.Map(mongoHistory.Items, domainOrderItems);
                    domainOrderItems.ForEach(i => orderHistory.AddOrderItem(i));
                }

                if (orderHistory != null)
                {
                    var newOrder = new DE.Order()
                    {
                        BillToAddress = orderHistory.BillToAddress,
                        CreditCard = orderHistory.CreditCard,
                        CustomerId = orderHistory.CustomerId,
                        DueDate = orderHistory.DueDate,
                        Freight = orderHistory.Freight,
                        OrderDate = orderHistory.OrderDate,
                        ShippingAddress = orderHistory.ShippingAddress,
                        Status = orderHistory.Status,
                        TrackingId = orderHistory.TrackingId,
                    };

                    foreach (var shoppingCartItem in orderHistory.OrderItems)
                    {
                        newOrder.AddOrderItem(new DE.OrderItem()
                        {
                            Product = shoppingCartItem.Product,
                            Quantity = (short)shoppingCartItem.Quantity,
                            UnitPrice = shoppingCartItem.UnitPrice
                        });
                    }

                    return newOrder;
                }

                return null;
            }
            catch (Exception e)
            {
                throw new RepositoryException(string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingOrderByTrackingId, trackingId), e);
            }
        }

        public DE.OrderHistory GetOrderHistoryByTrackingId(Guid trackingId)
        {
            if (Guid.Empty.Equals(trackingId))
            {
                throw new ArgumentNullException("trackingId");
            }

            try
            {
                var orderHistory = this.GetOrderHistoriesFromMongo(new Dictionary<string, object> { { "orderCode", trackingId } }).FirstOrDefault();

                if (orderHistory == null)
                {
                    return null;
                }

                var result = new DE.OrderHistory();
                var orderItems = new List<DE.OrderItem>();

                Mapper.Map(orderHistory, result);
                if (orderHistory.Items != null)
                {
                    Mapper.Map(orderHistory.Items, orderItems);
                    orderItems.ForEach(i => result.AddOrderItem(i));
                }

                return result;
            }
            catch (Exception e)
            {              
                throw new RepositoryException(string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingOrderByTrackingId, trackingId), e);
            }
        }

        public bool IsOrderCompleted(Guid trackingId)
        {
            if (Guid.Empty.Equals(trackingId))
            {
                throw new ArgumentNullException("trackingId");
            }

            try
            {
                IMongoQuery query = null;
                var collection = GetDatabase().GetCollection<OrderHistory>(MongoCollection);
                query = Query.And(
                    Query<OrderHistory>.EQ(e => e.OrderCode, trackingId),
                    Query<OrderHistory>.EQ(e => e.Status, DE.OrderStatus.Completed));
                var count = collection.Find(query).Count();
                return count > 0;
            }
            catch (Exception e)
            {
                throw new RepositoryException(string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingOrderByTrackingId, trackingId), e);
            }
        }

        public DE.OrderHistory GetOrderHistoryByHistoryId(Guid historyId)
        {
            if (Guid.Empty.Equals(historyId))
            {
                throw new ArgumentNullException("historyId");
            }

            try
            {
                var orderHistory = this.GetOrderHistoryFromMongo(historyId);
                if (orderHistory == null)
                {
                    return null;
                }

                var result = new DE.OrderHistory();
                var orderItems = new List<DE.OrderItem>();

                Mapper.Map(orderHistory, result);

                if (orderHistory.Items != null)
                {
                    Mapper.Map(orderHistory.Items, orderItems);
                    orderItems.ForEach(i => result.AddOrderItem(i));
                }

                return result;
            }
            catch (Exception e)
            {
                throw new RepositoryException(string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingOrderHistoryByHistoryId, historyId), e);
            }
        }

        public ICollection<DE.OrderHistory> GetOrdersHistories(int customerId)
        {
            try
            {
                var histories = this.GetOrderHistoriesFromMongo(new Dictionary<string, object> { { "customerId", customerId } }).ToList();
                if (histories == null || histories.Count() == 0)
                {
                    return null;
                }

                var result = new List<DE.OrderHistory>();

                foreach (var history in histories)
                {
                    var orderHistory = new DE.OrderHistory();
                    Mapper.Map(history, orderHistory);

                    var orderItems = new List<DE.OrderItem>();
                    Mapper.Map(history.Items, orderItems);
                    orderItems.ForEach(i => orderHistory.AddOrderItem(i));

                    result.Add(orderHistory);
                }

                return result;
            }
            catch (Exception e)
            {
                throw new RepositoryException(string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingOrdersHistoryForCustomerId, customerId), e);
            }
        }

        public void SaveOrderHistory(DE.OrderHistory orderHistory)
        {
            if (orderHistory == null)
            {
                throw new ArgumentNullException("orderHistory");
            }

            try
            {
                var savedOrderHistory = new OrderHistory(orderHistory.HistoryId, orderHistory.TrackingId);
                Mapper.Map(orderHistory, savedOrderHistory);
                this.SaveOrderHistoryToMongo(savedOrderHistory);
            }
            catch (Exception e)
            {
                throw new RepositoryException(string.Format(CultureInfo.CurrentCulture, Strings.ErrorSavingOrderHistory, orderHistory.TrackingId), e);
            }
        }

        private OrderHistory GetOrderHistoryFromMongo(Guid id)
        {
            if (Guid.Empty.Equals(id))
            {
                throw new ArgumentNullException("id");
            }

            return GetDatabase().GetCollection<OrderHistory>(MongoCollection).FindOneById(id);
        }

        private IEnumerable<OrderHistory> GetOrderHistoriesFromMongo(IDictionary<string, object> matchingFilter = null)
        {
            return (matchingFilter != null)
                ? GetDatabase().GetCollection<OrderHistory>(MongoCollection).Find(new QueryDocument(matchingFilter))
                : GetDatabase().GetCollection<OrderHistory>(MongoCollection).FindAll();
        }

        private void SaveOrderHistoryToMongo(OrderHistory orderHistory)
        {
            WriteConcernResult result = null;
            IMongoQuery query = null;
            var collection = GetDatabase().GetCollection<OrderHistory>(MongoCollection);

            if (orderHistory == null)
            {
                throw new ArgumentNullException("orderHistory");
            }

            if (orderHistory.Status == DE.OrderStatus.Pending)
            {
                query = Query<OrderHistory>.EQ(e => e.OrderCode, orderHistory.OrderCode);
                var storedHistory = collection.Find(query).FirstOrDefault();

                if (storedHistory == null)
                {
                    // insert if the pending history does not exist
                    result = collection.Insert(orderHistory);
                }
                else
                {
                    // update if pending history already exists
                    query = Query.And(
                        Query<OrderHistory>.EQ(e => e.OrderCode, orderHistory.OrderCode),
                        Query<OrderHistory>.EQ(e => e.Status, orderHistory.Status));

                    var update = Update<OrderHistory>
                        .Set(e => e.BillToAddress, orderHistory.BillToAddress)
                        .Set(e => e.CreditCard, orderHistory.CreditCard)
                        .Set(e => e.CustomerId, orderHistory.CustomerId)
                        .Set(e => e.DueDate, orderHistory.DueDate)
                        .Set(e => e.Freight, orderHistory.Freight)
                        .Set(e => e.Items, orderHistory.Items)
                        .Set(e => e.ModifiedDate, orderHistory.ModifiedDate)
                        .Set(e => e.ShippingAddress, orderHistory.ShippingAddress);

                    result = collection.Update(query, update);
                }
            }
            else
            {
                // insert if the order status is not pending
                result = collection.Insert(orderHistory);
            }

            if (!result.Ok)
            {
                throw new MongoException(result.LastErrorMessage, new MongoException(result.ErrorMessage));
            }
        }
    }
}
