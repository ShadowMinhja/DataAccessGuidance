//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Sql.Order
{
    using System;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using DataAccess.Domain;
    using DataAccess.Domain.Order;
    using DataAccess.Repo.Impl.Sql.Resources;
    using DataAccess.Repository;
    using DE = DataAccess.Domain.Order;

    public class SalesOrderRepository : BaseRepository, ISalesOrderRepository
    {
        public DE.Order SaveOrder(DE.Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            try
            {
                var salesOrderHeader = new SalesOrderHeader();
                Mapper.Map(order, salesOrderHeader);

                using (var context = new SalesOrderContext())
                {
                    using (var transactionScope = this.GetTransactionScope())
                    {
                        context.SalesOrderHeaders.Add(salesOrderHeader);
                        context.SaveChanges();

                        transactionScope.Complete();
                    }
                }

                return order;
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorSavingOrderWithTrackingId, order.TrackingId),
                    e);
            }
        }

        public bool UpdateOrderStatus(Guid trackingId, OrderStatus newStatus)
        {
            try
            {
                using (var context = new SalesOrderContext())
                {
                    using (var transactionScope = this.GetTransactionScope())
                    {
                        var orderEntity = context.SalesOrderHeaders.FirstOrDefault(o => o.TrackingId.Equals(trackingId));

                        if (orderEntity == null)
                        {
                            return false;
                        }

                        orderEntity.Status = (byte)newStatus;
                        var result = context.SaveChanges() > 0;

                        transactionScope.Complete();

                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorUpdatingOrderStatus, trackingId, newStatus),
                    e);
            }
        }

        public bool IsOrderSaved(Guid trackingId)
        {
            try
            {
                bool isOrderSubmitted = false;
                using (var context = new SalesOrderContext())
                {
                    using (var transactionScope = this.GetTransactionScope())
                    {
                        var orderEntity = context.SalesOrderHeaders.FirstOrDefault(o => o.TrackingId.Equals(trackingId));

                        if (orderEntity != null)
                        {
                            isOrderSubmitted = true;
                        }

                        transactionScope.Complete();
                    }
                }

                return isOrderSubmitted;
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorCheckingOrderSubmission, trackingId),
                    e);
            }
        }
    }
}
