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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess.Domain.Order;
    using DataAccess.Repository;

    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private static ConcurrentDictionary<Guid, OrderHistory> ordersHistory =
            new ConcurrentDictionary<Guid, OrderHistory>();

        public Order GetPendingOrderByTrackingId(Guid trackingId)
        {
            if (Guid.Empty.Equals(trackingId))
            {
                throw new ArgumentNullException("trackingId");
            }

            var orderHistory = OrderHistoryRepository.ordersHistory.Values.FirstOrDefault(oh => (oh.TrackingId.Equals(trackingId) && oh.Status == OrderStatus.Pending));
            if (orderHistory != null)
            {
                var newOrder = new Order()
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
                    newOrder.AddOrderItem(new OrderItem()
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

        public OrderHistory GetOrderHistoryByTrackingId(Guid trackingId)
        {
            if (Guid.Empty.Equals(trackingId))
            {
                throw new ArgumentNullException("trackingId");
            }

            return OrderHistoryRepository.ordersHistory.Values.FirstOrDefault(oh => oh.TrackingId.Equals(trackingId));
        }

        public bool IsOrderCompleted(Guid trackingId)
        {
            if (Guid.Empty.Equals(trackingId))
            {
                throw new ArgumentNullException("trackingId");
            }

            var count = OrderHistoryRepository.ordersHistory.Values
                .Where(oh => (oh.TrackingId.Equals(trackingId) && oh.Status == OrderStatus.Completed))
                .Count();

            return count > 0;
        }

        public OrderHistory GetOrderHistoryByHistoryId(Guid historyId)
        {
            if (Guid.Empty.Equals(historyId))
            {
                throw new ArgumentNullException("historyId");
            }
            
            var orderHistory = default(OrderHistory);
            OrderHistoryRepository.ordersHistory.TryGetValue(historyId, out orderHistory);
            return orderHistory;
        }

        public ICollection<OrderHistory> GetOrdersHistories(int customerId)
        {
            return OrderHistoryRepository.ordersHistory.Values
                .Where(oh => oh.CustomerId.Equals(customerId))
                .OrderBy(oh => oh.ModifiedDate)
                .ToList();
        }

        public void SaveOrderHistory(OrderHistory orderHistory)
        {
            if (orderHistory == null)
            {
                throw new ArgumentNullException("orderHistory");
            }

            OrderHistoryRepository.ordersHistory.AddOrUpdate(
                    orderHistory.HistoryId,
                    orderHistory,
                    (key, existingValue) =>
                    {
                        return orderHistory;
                    });
        }
    }
}
