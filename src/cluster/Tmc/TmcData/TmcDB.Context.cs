﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TmcData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
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
    
        public DbSet<ComponentCycleLogView> ComponentCycleLogViews { get; set; }
        public DbSet<ComponentEventLogView> ComponentEventLogViews { get; set; }
        public DbSet<EnvironmentLogView> EnvironmentLogViews { get; set; }
        public DbSet<OrderListView> OrderListViews { get; set; }
    
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
    
        public virtual int AddNewEnvironmentLog(Nullable<System.DateTime> timestamp, Nullable<int> sourceID, Nullable<double> reading, Nullable<int> typeID)
        {
            var timestampParameter = timestamp.HasValue ?
                new ObjectParameter("Timestamp", timestamp) :
                new ObjectParameter("Timestamp", typeof(System.DateTime));
    
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            var readingParameter = reading.HasValue ?
                new ObjectParameter("Reading", reading) :
                new ObjectParameter("Reading", typeof(double));
    
            var typeIDParameter = typeID.HasValue ?
                new ObjectParameter("TypeID", typeID) :
                new ObjectParameter("TypeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddNewEnvironmentLog", timestampParameter, sourceIDParameter, readingParameter, typeIDParameter);
        }
    
        public virtual int AddNewEventLog(Nullable<System.DateTime> timestamp, string description, Nullable<int> sourceID, Nullable<int> logTypeID)
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
    
            var logTypeIDParameter = logTypeID.HasValue ?
                new ObjectParameter("LogTypeID", logTypeID) :
                new ObjectParameter("LogTypeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddNewEventLog", timestampParameter, descriptionParameter, sourceIDParameter, logTypeIDParameter);
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
    
        public virtual int CycleLogBySource(Nullable<int> sourceID)
        {
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CycleLogBySource", sourceIDParameter);
        }
    
        public virtual int EnvironmentLogBySource(Nullable<int> sourceID)
        {
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EnvironmentLogBySource", sourceIDParameter);
        }
    
        public virtual int EventLogBySource(Nullable<int> sourceID)
        {
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EventLogBySource", sourceIDParameter);
        }
    
        public virtual int OrderConfigByOrder(Nullable<int> orderID)
        {
            var orderIDParameter = orderID.HasValue ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("OrderConfigByOrder", orderIDParameter);
        }
    
        public virtual int OrderListByStatus(Nullable<int> statusID)
        {
            var statusIDParameter = statusID.HasValue ?
                new ObjectParameter("StatusID", statusID) :
                new ObjectParameter("StatusID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("OrderListByStatus", statusIDParameter);
        }
    
        public virtual int UpdateOrderList(Nullable<int> orderID, string orderStatus)
        {
            var orderIDParameter = orderID.HasValue ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(int));
    
            var orderStatusParameter = orderStatus != null ?
                new ObjectParameter("OrderStatus", orderStatus) :
                new ObjectParameter("OrderStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateOrderList", orderIDParameter, orderStatusParameter);
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
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int AcknowledgeEvent(Nullable<int> eventID)
        {
            var eventIDParameter = eventID.HasValue ?
                new ObjectParameter("EventID", eventID) :
                new ObjectParameter("EventID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AcknowledgeEvent", eventIDParameter);
        }
    }
}
