﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiServer.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ICTDEntities : DbContext
    {
        public ICTDEntities()
            : base("name=ICTDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ComponentCycleLog> ComponentCycleLogs { get; set; }
        public virtual DbSet<ComponentEventLog> ComponentEventLogs { get; set; }
        public virtual DbSet<EnviromentLog> EnviromentLogs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderConfig> OrderConfigs { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<ComponentCycleLogView> ComponentCycleLogViews { get; set; }
        public virtual DbSet<ComponentEventLogView> ComponentEventLogViews { get; set; }
        public virtual DbSet<OrderListView> OrderListViews { get; set; }
    
        public virtual int AddNewCycleLog(Nullable<System.DateTime> timestamp, Nullable<int> cycleTime, Nullable<int> sourceID)
        {
            var timestampParameter = timestamp.HasValue ?
                new ObjectParameter("Timestamp", timestamp) :
                new ObjectParameter("Timestamp", typeof(System.DateTime));
    
            var cycleTimeParameter = cycleTime.HasValue ?
                new ObjectParameter("CycleTime", cycleTime) :
                new ObjectParameter("CycleTime", typeof(int));
    
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddNewCycleLog", timestampParameter, cycleTimeParameter, sourceIDParameter);
        }
    
        public virtual int AddNewEventLog(Nullable<System.DateTime> timestamp, string description, Nullable<int> sourceID)
        {
            var timestampParameter = timestamp.HasValue ?
                new ObjectParameter("Timestamp", timestamp) :
                new ObjectParameter("Timestamp", typeof(System.DateTime));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddNewEventLog", timestampParameter, descriptionParameter, sourceIDParameter);
        }
    
        public virtual int AddNewOrder(Nullable<System.Guid> userID, Nullable<int> black, Nullable<int> blue, Nullable<int> red, Nullable<int> green, Nullable<int> white)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(System.Guid));
    
            var blackParameter = black.HasValue ?
                new ObjectParameter("Black", black) :
                new ObjectParameter("Black", typeof(int));
    
            var blueParameter = blue.HasValue ?
                new ObjectParameter("Blue", blue) :
                new ObjectParameter("Blue", typeof(int));
    
            var redParameter = red.HasValue ?
                new ObjectParameter("Red", red) :
                new ObjectParameter("Red", typeof(int));
    
            var greenParameter = green.HasValue ?
                new ObjectParameter("Green", green) :
                new ObjectParameter("Green", typeof(int));
    
            var whiteParameter = white.HasValue ?
                new ObjectParameter("White", white) :
                new ObjectParameter("White", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddNewOrder", userIDParameter, blackParameter, blueParameter, redParameter, greenParameter, whiteParameter);
        }
    
        public virtual int CancelOrder(Nullable<int> orderID)
        {
            var orderIDParameter = orderID.HasValue ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CancelOrder", orderIDParameter);
        }
    
        public virtual int CompleteOrder(Nullable<int> orderID)
        {
            var orderIDParameter = orderID.HasValue ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CompleteOrder", orderIDParameter);
        }
    
        public virtual ObjectResult<FiltCompCyclLogBySour_Result> FiltCompCyclLogBySour(Nullable<int> sourceID)
        {
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FiltCompCyclLogBySour_Result>("FiltCompCyclLogBySour", sourceIDParameter);
        }
    
        public virtual ObjectResult<FiltCompEvenLogBySour_Result> FiltCompEvenLogBySour(Nullable<int> sourceID)
        {
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FiltCompEvenLogBySour_Result>("FiltCompEvenLogBySour", sourceIDParameter);
        }
    
        public virtual ObjectResult<FiltOrdeList_Result> FiltOrdeList(Nullable<int> statusID)
        {
            var statusIDParameter = statusID.HasValue ?
                new ObjectParameter("StatusID", statusID) :
                new ObjectParameter("StatusID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FiltOrdeList_Result>("FiltOrdeList", statusIDParameter);
        }
    
        public virtual int UpdateOrderStatus(Nullable<int> orderID, string orderStatus)
        {
            var orderIDParameter = orderID.HasValue ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(int));
    
            var orderStatusParameter = orderStatus != null ?
                new ObjectParameter("OrderStatus", orderStatus) :
                new ObjectParameter("OrderStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateOrderStatus", orderIDParameter, orderStatusParameter);
        }
    
        public virtual int UpdateOrderStatusByID(Nullable<int> orderID, Nullable<int> orderStatus)
        {
            var orderIDParameter = orderID.HasValue ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(int));
    
            var orderStatusParameter = orderStatus.HasValue ?
                new ObjectParameter("OrderStatus", orderStatus) :
                new ObjectParameter("OrderStatus", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateOrderStatusByID", orderIDParameter, orderStatusParameter);
        }
    
        public virtual int UpdateProductProduced(Nullable<int> orderID, Nullable<int> productNumber)
        {
            var orderIDParameter = orderID.HasValue ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(int));
    
            var productNumberParameter = productNumber.HasValue ?
                new ObjectParameter("ProductNumber", productNumber) :
                new ObjectParameter("ProductNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateProductProduced", orderIDParameter, productNumberParameter);
        }
    
        public virtual ObjectResult<zFilterComponentCycleLogBySourceID_Result> zFilterComponentCycleLogBySourceID(Nullable<int> sourceID)
        {
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<zFilterComponentCycleLogBySourceID_Result>("zFilterComponentCycleLogBySourceID", sourceIDParameter);
        }
    
        public virtual ObjectResult<zFilterComponentEventLogBySourceID_Result> zFilterComponentEventLogBySourceID(Nullable<int> sourceID)
        {
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<zFilterComponentEventLogBySourceID_Result>("zFilterComponentEventLogBySourceID", sourceIDParameter);
        }
    }
}
