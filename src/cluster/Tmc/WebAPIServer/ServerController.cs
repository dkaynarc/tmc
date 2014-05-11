using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiServer.Authentication;
using Tmc.Scada.Core;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Configuration;
using WebApiServer.Parcels;
using System.Data.Entity.Validation;
using System.Net.Http;
using System.Net;

namespace WebApiServer
{
    public class ServerController : ApiController
    {
        private const String SCADA_UNAVAILABLE_MESSAGE = "Scada is unavailable to make a connection";
        ICTDEntities repository;
        private const string ON = "on";
        private const string OFF = "off";

       public SCADAUserManager UserManager 
       {
           get { return new SCADAUserManager(); }
       }

        public ServerController()
        {
            repository = new ICTDEntities();
        }




     [HttpGet]
       [ActionName("Authenticate")]
       public AuthParcel Authenticate(string var1, string var2)
       {       
           var user = UserManager.Find(var1, var2);
           AuthParcel parcel = new AuthParcel
           {
               Name = user.UserName
           };

           if (user != null) parcel.Result = "success";
          
           else parcel.Result = "fail";
          
           return parcel;
         // return new AuthParcel { Name = "vas", Result = "success" };
       }


       [HttpGet]
       public string StartScada()
       {
          try
           {
               ScadaConnectionManager.ScadaClient.Start();
               return "Scada started successfully";
           }
           catch (EndpointNotFoundException)
           {
               return SCADA_UNAVAILABLE_MESSAGE;
           }
           return "success";
       }



 


  

       private LinkedList<OrderParcel> CopyOrders(IEnumerable<Order> orders)
       {
           try
           {
               LinkedList<OrderParcel> parcels = new LinkedList<OrderParcel>();
               

               foreach (var order in orders)
               {
                   OrderConfig config = repository.OrderConfigs.Where(p => p.OrderID == order.OrderID).First();
                   parcels.AddLast(new OrderParcel
                   {
                       mOrderId = order.OrderID,
                       mOrderOwner = 
                           (order.UserID == null ? "not_provided" : (UserManager.FindById(Convert.ToString(order.UserID))).UserName),
                       mOrderStatus = order.Status.Name,
                       black  = config.Black,
                       blue = config.Blue,
                       green = config.Green,
                       red = config.Red,
                       white = config.White
                   });
               }
               return parcels;
           }
           catch (Exception exc)
           { return null; }
           
       }


       [HttpGet]
       [ActionName("PlaceOrder")]
       public string PlaceOrder(string name, int var1, int var2, int var3, int var4, int var5)
       {
           Order order = new Order
            {
                NumberOfProducts = var1 + var2 + var3 + var4 + var5, 
                UserID =  Guid.Parse((UserManager.FindByName(name)).Id),
                StartTime = System.DateTime.Now,
                StatusID = 1         
            };


          try
          {
              repository.Orders.Add(order);
              repository.SaveChanges();
              
              OrderConfig config = new OrderConfig
              {
                  Black = var1,
                  Blue = var2,
                  Green = var3,
                  Red = var4,
                  White = var5,
                  OrderID = order.OrderID
              };

              repository.OrderConfigs.Add(config);
              repository.SaveChanges();
              
              return "success";
          }

          catch (DbEntityValidationException exc) 
          {
              foreach (var err in exc.EntityValidationErrors)
              {
                  foreach (var errrr in err.ValidationErrors) 
                  {
                      string st = errrr.ErrorMessage;
                  
                  }
              }
                  return "fail";
          }
          
       }







       [HttpGet]
       public string StopScada()
       {
           try
           {
               ScadaConnectionManager.ScadaClient.Stop();
               return "Scada stopped successfully";
           }
           catch (EndpointNotFoundException)
           {
               return SCADA_UNAVAILABLE_MESSAGE;
           }
           //return "success";
       }

       [HttpGet]
       public string ResumeScada()
       {
           try
           {
               ScadaConnectionManager.ScadaClient.Resume();
               return "Scada resumed successfully";
           }
           catch (EndpointNotFoundException)
           {
               return SCADA_UNAVAILABLE_MESSAGE;
           }
           //return "success";
       }

       [HttpGet]
       public string EmergencyStopScada()
       {
          try
           {
               ScadaConnectionManager.ScadaClient.EmergencyStop();
               return "success";
           }
           catch (EndpointNotFoundException)
           {
               return SCADA_UNAVAILABLE_MESSAGE;
           }
           return "success";
       }


       [HttpGet]
       [ActionName("DeleteOrder")]
       public DeleteOrderParcel DeleteOrder(int orderParam)
       {
           try
           {
              repository.Orders.Remove(repository.Orders.Where(p => p.OrderID == orderParam).First());
              repository.OrderConfigs.Remove(repository.OrderConfigs.Where(p => p.OrderID == orderParam).First());

              repository.SaveChanges();
              return new DeleteOrderParcel{ orderId = Convert.ToString(orderParam), result = "success"};
                 
           }
           catch (Exception exc) 
           {
              return new DeleteOrderParcel { orderId = Convert.ToString(orderParam), result = "fail" };
           }
       }



       [HttpGet]
       public HttpResponseMessage GetMachineryStatus()
       {
           List<MachineParcel> machinery = new List<MachineParcel>
           { 
             new MachineParcel{ Name = "sorter", Status = ON },
             new MachineParcel { Name = "assembler", Status = ON },
             new MachineParcel { Name = "loader", Status = ON },
             new MachineParcel { Name = "palletiser", Status = ON },
             new MachineParcel { Name = "conveyor #1", Status = ON },
             new MachineParcel { Name = "conveyor#2", Status = ON }

           };
           return Request.CreateResponse(HttpStatusCode.OK, machinery);
       }



     
       [HttpGet]
       public HttpResponseMessage GetOrders(String orderParam)
       {

           IEnumerable<Order> orders = new LinkedList<Order>();

           if (orderParam.Equals("complete"))
           {
              orders = repository.Orders.Where(p => p != null).Where(p => p.StatusID == 3); // status id 3 - the order is complete
           }
           else 
           {
              orders = repository.Orders.Where(p => p != null).Where(p => p.StatusID != 3); // status id 3 - the order is complete
         
           }
           try
           { 
               LinkedList<OrderParcel> parcels = CopyOrders(orders);
               return Request.CreateResponse(HttpStatusCode.OK, parcels );
           }
           catch (Exception exc) 
           {
               return null;
           }
       }



       




       [HttpGet]
       public String ModifyOrder(string orderName, int orderId, string status, int black, int blue,int green, int red, int white) 
       {
           try 
           {
              OrderConfig conf = repository.OrderConfigs.Where(p => p.OrderID == orderId).First();
              conf.Green = green;
              conf.Black = black;
              conf.Blue = blue;
              conf.Red = red;
              conf.White = white;

              repository.SaveChanges();
 
              return "success";
           }
           
           catch (DbEntityValidationException) 
           {
               return "fail";
           }
       
       
       }


    }
}





