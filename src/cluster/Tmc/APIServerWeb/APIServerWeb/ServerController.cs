using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ApiServerWeb.Authentication;
using APIServerWeb.Parcels;
using APIServerWeb;
using APIServerWeb.EF;
using System.ServiceModel;

namespace APIServerWeb
{
    public class ServerController : ApiController
    {
        //private ScadaClient _scadaClient;
        private const String SCADA_UNAVAILABLE_MESSAGE = "Scada is unavailable to make a connection";
        ICTDEntities repository;
        private const string ON = "ON";
        private const string OFF = "OFF";

        public ServerController()
        {
           
            repository = new ICTDEntities();


            /*  SCADAUser user = new SCADAUser();
              user.UserName = "carlo";
              try { var result = UserManager.Create(user,"1");      }
              catch(Exception exc)
              {
           
              };
             */
        }

        public SCADAUserManager UserManager
        {
            get { return new SCADAUserManager(); }
        }






        [HttpGet]
        public AuthParcel Authenticate(string var1, string var2)
        {
            try
            {
                var user = UserManager.Find(var1, var2);
                AuthParcel parcel = new AuthParcel
                {
                    Name = user.UserName
                };

                if (user != null) parcel.Result = "success";

                else parcel.Result = "fail";

                return parcel;
            }
            catch (Exception exc)
            {
                return new AuthParcel { Result = "fail: " + exc.ToString()};
            }
            //return new AuthParcel { Name = "sergei", Result = "success" };
            //return "hello";
        }


        

        [HttpGet]
        [ActionName("PlaceOrder")]
        public string PlaceOrder(string name, int var1, int var2, int var3, int var4, int var5)
        {
            Order order = new Order
            {
                NumberOfProducts = var1 + var2 + var3 + var4 + var5,
                UserID = Guid.Parse((UserManager.FindByName(name)).Id),
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
        public string StartScada()
        {
           try
           {
               ScadaConnectionManager.ScadaClient.Start();
               return "success";
           }
           catch (EndpointNotFoundException)
           {
               return SCADA_UNAVAILABLE_MESSAGE;
           }
         //   return "success";
        }



        [HttpGet]
        public string StopScada()
        {
            /*try
            {
                ScadaConnectionManager.ScadaClient.Stop();
                return "Scada stopped successfully";
            }
            catch (EndpointNotFoundException)
            {
                return SCADA_UNAVAILABLE_MESSAGE;
            }*/
            return "success";
        }

        [HttpGet]
        public string ResumeScada()
        {
            /*try
            {
                ScadaConnectionManager.ScadaClient.Resume();
                return "Scada resumed successfully";
            }
            catch (EndpointNotFoundException)
            {
                return SCADA_UNAVAILABLE_MESSAGE;
            }*/
            return "success";
        }

        [HttpGet]
        public string EmergencyStopScada()
        {
            /* try
             {
                 ScadaConnectionManager.ScadaClient.EmergencyStop();
                 return "success";
             }
             catch (EndpointNotFoundException)
             {
                 return SCADA_UNAVAILABLE_MESSAGE;
             }*/
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
                return new DeleteOrderParcel { orderId = Convert.ToString(orderParam), result = "success" };

            }
            catch (Exception exc)
            {
                return new DeleteOrderParcel { orderId = Convert.ToString(orderParam), result = "fail" };
            }
        }


        // TO DO: get real environment information  either from database or from SCADA
        [HttpGet]
        public EnvironmentParcel GetEnvironment()
        {
            return new EnvironmentParcel
            {
                Dust = "38",
                Humidity = "13",
                Light = "35",
                Sound = "34",
                Temperature = "30",
                Result = "success"
            };
        }




        [HttpGet]
        public HttpResponseMessage GetMachineryStatus()
        {
            List<MachineParcel> machinery = new List<MachineParcel>
           { 
             new MachineParcel{ Name = "SORTER", Status = ON },
             new MachineParcel { Name = "ASSEMBLER", Status = ON },
             new MachineParcel { Name = "LOADER", Status = ON },
             new MachineParcel { Name = "PALLETISER", Status = OFF },
             new MachineParcel { Name = "CONVEYOR #1", Status = OFF },
             new MachineParcel { Name = "CONVEYOR #2", Status = OFF }

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
                return Request.CreateResponse(HttpStatusCode.OK, parcels);
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exc.ToString());
            }
        }




        private LinkedList<OrderParcel> CopyOrders(IEnumerable<Order> orders)
        {
                LinkedList<OrderParcel> parcels = new LinkedList<OrderParcel>();
                List<Order> list = orders.ToList();

                foreach (var order in list)
                {
                    string startDate;
                    string endDate;
                    OrderConfig config = repository.OrderConfigs.Where(p => p.OrderID == order.OrderID).First();
                    try
                    {
                        startDate = order.StartTime.Value.ToShortDateString() + " "
                                                       + order.StartTime.Value.Hour + ":" +
                                                       order.StartTime.Value.Minute + ":" +
                                                       order.StartTime.Value.Second;
                    }
                    catch (Exception)
                    {
                        startDate = "";
                    }
                    try
                    {
                        endDate = order.EndTime.Value.ToShortDateString() + " "
                                                       + order.EndTime.Value.Hour + ":" +
                                                       order.EndTime.Value.Minute + ":" +
                                                       order.EndTime.Value.Second;
                    }
                    catch (Exception)
                    {
                        endDate = "";
                    }


                    SCADAUser user = order.UserID == null ? null : UserManager.FindById(Convert.ToString(order.UserID));
                                  
                    parcels.AddLast(new OrderParcel
                    {
                        startTime = startDate,
                        endTime = endDate,
                        mOrderId = order.OrderID,
                        mOrderOwner = (user == null ? "not_provided" : user.UserName),
                        mOrderStatus = order.Status.Name,
                        black = config.Black,
                        blue = config.Blue,
                        green = config.Green,
                        red = config.Red,
                        white = config.White
                    });
                }
                return parcels;
            
        }






        [HttpGet]
        public String ModifyOrder(string orderName, int orderId, string status, int black, int blue, int green, int red, int white)
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
