//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repository
{
    using System;
    using System.Collections.Generic;
    using DataAccess.Domain.Order;
    
    public interface IOrderHistoryRepository
    {
        Order GetPendingOrderByTrackingId(Guid trackingId);

        OrderHistory GetOrderHistoryByTrackingId(Guid trackingId);

        OrderHistory GetOrderHistoryByHistoryId(Guid historyId);

        ICollection<OrderHistory> GetOrdersHistories(int customerId);

        void SaveOrderHistory(OrderHistory orderHistory);

        bool IsOrderCompleted(Guid trackingId);
    }
}
