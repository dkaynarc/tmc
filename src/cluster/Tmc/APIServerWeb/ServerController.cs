﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
using APIServerWeb.Parcels;
using APIServerWeb;
using APIServerWeb.EF;
using System.ServiceModel;
using APIServerWeb.Authentication;
using System.Web.Security;
using Tmc.Common;


namespace APIServerWeb
{
    public class ServerController : ApiController
    {
        private const String SCADA_UNAVAILABLE_MESSAGE = "fail :";
        ICTDEntities repository;
        private const string userRoleName = "user";
        private const string adminRoleName = "operator";
        private int passwordLength = 7;
        private int numberOfNonAlphanumericCharacters = 0;
        public string[] Roles { get { string[] vv = { userRoleName, adminRoleName }; return vv; } }



        public SCADAUserManager UserManager
        {
            get { return new SCADAUserManager(); }
        }

        public SCADARoleManager RoleManager
        {
            get { return new SCADARoleManager(); }
        }




        public ServerController()
        {
            repository = new ICTDEntities();
            //SetupRoles(Roles);
        }


        [HttpGet]
        public HttpResponseMessage AddUser(string userName, string password, string roleName)
        {
            SCADAUser user = new SCADAUser();
            user.UserName = userName;
            try
            {
                var result = UserManager.Create(user, password);
                UserManager.AddToRole(user.Id, roleName);
                return Request.CreateResponse(HttpStatusCode.OK, "success");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "fail");
            };

        }




        [HttpPost]
        public String ChangePassword(string var)
        {
            if (String.IsNullOrWhiteSpace(var))
            {
                return "fail: no username provided";
            }
            var user = UserManager.FindByName(var);

            if (user == null)
            {
                return "fail: User with this username doesn't exist";
            }
            else
            {
                string newPassword = Membership.GeneratePassword(passwordLength, numberOfNonAlphanumericCharacters);
                UserManager.RemovePassword(user.Id);
                UserManager.AddPassword(user.Id, newPassword);

                return "success: password reset: " + newPassword;
            }
        }










        [HttpGet]
        public HttpResponseMessage DeleteUser(string var)
        {
            SCADAUser user = new SCADAUser();

            try
            {
                user = UserManager.FindByName(var);
                var result = UserManager.Delete(user);
                return Request.CreateResponse(HttpStatusCode.OK, "success");
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "fail: " + exc.ToString());
            };

        }



        public void SetupRoles(string[] roles)
        {
            try
            {
                foreach (string role in roles)
                {
                    if (!RoleManager.RoleExists(role))
                        RoleManager.Create(new SCADARole(role));
                }
            }
            catch (Exception)
            {

            }
        }


        [HttpGet]
        public string Test(string var1, string var2)
        {
            return "success";
        }



        [HttpGet]
        public AuthParcel Authenticate(string var1, string var2)
        {
            try
            {
                var user = UserManager.Find(var1, var2);
                AuthParcel parcel = new AuthParcel
                {
                    Name = user.UserName,
                };

                if (user != null)
                {
                    parcel.Result = "success";
                    if (UserManager.IsInRole(user.Id, adminRoleName)) parcel.Role = adminRoleName;
                    else if (UserManager.IsInRole(user.Id, userRoleName)) parcel.Role = userRoleName;
                    else parcel.Result = "fail"; // fail if user doesn't belong to any roles
                }
                else parcel.Result = "fail";

                return parcel;

            }
            catch (Exception exc)
            {
                return new AuthParcel { Result = "fail: " + exc.ToString() };
            }
        }










        [HttpGet]
        public String PlaceOrder(string var1, int var2, int var3, int var4, int var5, int var6)
        {
            Order order = new Order
            {
                NumberOfProducts = var2 + var3 + var4 + var5 + var6,
                UserID = Guid.Parse((UserManager.FindByName(var1)).Id),
                StartTime = System.DateTime.Now,
                StatusID = 1
            };


            try
            {
                repository.Orders.Add(order);
                repository.SaveChanges();

                OrderConfig config = new OrderConfig
                {
                    Black = var2,
                    Blue = var3,
                    Green = var4,
                    Red = var5,
                    White = var6,
                    OrderID = order.OrderID
                };

                repository.OrderConfigs.Add(config);
                repository.SaveChanges();

                return "success";
            }

            catch (DbEntityValidationException exc)
            {
                //foreach (var err in exc.EntityValidationErrors)
                //{
                //    foreach (var errrr in err.ValidationErrors)
                //    {
                //        string st = errrr.ErrorMessage;
                //    }
                //}
                return "fail: " + exc.ToString();
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
            catch (EndpointNotFoundException exc)
            {
                return SCADA_UNAVAILABLE_MESSAGE + exc.ToString();
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
            catch (EndpointNotFoundException exc)
            {
                return SCADA_UNAVAILABLE_MESSAGE + exc.ToString();
            }
        }



        [HttpGet]
        public string ResumeScada()
        {
            try
            {
                ScadaConnectionManager.ScadaClient.Resume();
                return "Scada resumed successfully";
            }
            catch (EndpointNotFoundException exc)
            {
                return SCADA_UNAVAILABLE_MESSAGE + exc.ToString();
            }
        }



        [HttpGet]
        public string EmergencyStopScada()
        {
            try
            {
                ScadaConnectionManager.ScadaClient.EmergencyStop();
                return "success";
            }
            catch (EndpointNotFoundException exc)
            {
                return SCADA_UNAVAILABLE_MESSAGE + exc.ToString();
            }
        }


        [HttpGet]
        public DeleteOrderParcel DeleteOrder(int var)
        {
            try
            {
                repository.Orders.Remove(repository.Orders.Where(p => p.OrderID == var).First());
                repository.OrderConfigs.Remove(repository.OrderConfigs.Where(p => p.OrderID == var).First());

                repository.SaveChanges();
                return new DeleteOrderParcel { orderId = Convert.ToString(var), result = "success" };

            }
            catch (Exception exc)
            {
                return new DeleteOrderParcel { orderId = Convert.ToString(var), result = "fail: " + exc.ToString() };
            }
        }


        // TO DO: get latest data
        [HttpGet]
        public EnvironmentParcel GetEnvironment()
        {
            try
            {
                IEnumerable<EnvironmentLog> temp = repository.EnvironmentLogs.Where(p => p.SourceID == 7).OrderByDescending(p => p.Timestamp).ToList();
                IEnumerable<EnvironmentLog> humidity = repository.EnvironmentLogs.Where(p => p.SourceID == 8).OrderByDescending(p => p.Timestamp).ToList();
                IEnumerable<EnvironmentLog> light = repository.EnvironmentLogs.Where(p => p.SourceID == 9).OrderByDescending(p => p.Timestamp).ToList();
                IEnumerable<EnvironmentLog> sound = repository.EnvironmentLogs.Where(p => p.SourceID == 10).OrderByDescending(p => p.Timestamp).ToList();
                IEnumerable<EnvironmentLog> dust = repository.EnvironmentLogs.Where(p => p.SourceID == 11).OrderByDescending(p => p.Timestamp).ToList();



                EnvironmentParcel parcel = new EnvironmentParcel
                {
                    Temperature = Convert.ToString(temp.First().Reading),//"30",
                    Dust = Convert.ToString(dust.First().Reading),//"38",
                    Humidity = Convert.ToString(humidity.First().Reading),//"13",
                    Light = Convert.ToString(light.First().Reading),//"35",
                    Sound = Convert.ToString(sound.First().Reading),//"34",

                    Result = "success"
                };


                return parcel;
            }
            catch (Exception exc)
            {
                return new EnvironmentParcel { Result = "fail: " + exc.ToString() };
            }
        }






        [HttpGet]
        public HttpResponseMessage GetMachineryStatus()
        {
            List<MachineParcel> machinery = new List<MachineParcel>();
            try
            {
                IDictionary<string, HardwareStatus> statuses = ScadaConnectionManager.ScadaClient.GetLastHardwareStatuses();
                var machNames = ScadaConnectionManager.ScadaClient.GetAllHardwareNames();


                foreach (string name in machNames)
                {
                    machinery.Add(new MachineParcel { Name = name, Status = Convert.ToString(statuses[name]) });
                }
                return Request.CreateResponse(HttpStatusCode.OK, machinery);
            }


            catch (Exception exc)
            {
                if (exc.GetType() == typeof(EndpointNotFoundException))
                {
                    machinery = new List<MachineParcel>
                   { 
                     new MachineParcel{ Name = "SorterRobot", Status = Convert.ToString(HardwareStatus.Offline) },
                     new MachineParcel { Name = "AssemblerRobot", Status = Convert.ToString(HardwareStatus.Offline)},
                     new MachineParcel { Name = "LoaderRobot", Status = Convert.ToString(HardwareStatus.Offline) },
                     new MachineParcel { Name = "SorterCamera", Status = Convert.ToString(HardwareStatus.Offline) },
                     new MachineParcel { Name = "SorterConveyor", Status = Convert.ToString(HardwareStatus.Offline) },
                     new MachineParcel { Name = "AssemblyConveyor", Status = Convert.ToString(HardwareStatus.Offline) },
                     new MachineParcel { Name = "TrayVerifierCamera", Status = Convert.ToString(HardwareStatus.Offline) }
                   };

                    return Request.CreateResponse(HttpStatusCode.OK, machinery);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, exc.ToString());
                }
            }
        }




        [HttpGet]
        public HttpResponseMessage GetCompleteOrders(string var1, string var2, string var3,
                                                              string var4, string var5, string var6)
        {

            DateTime start = new DateTime(Convert.ToInt32(var3), Convert.ToInt32(var2), Convert.ToInt32(var1));
            DateTime end = new DateTime(Convert.ToInt32(var6), Convert.ToInt32(var5), Convert.ToInt32(var4));

            IEnumerable<OrderParcel> parcels = new LinkedList<OrderParcel>();
            IEnumerable<Order> orders = new LinkedList<Order>();
            IEnumerable<Order> sortedOrders = new LinkedList<Order>();

            try
            {
                orders = repository.Orders.Where(p => p != null).Where(p => p.StatusID == 3);
                sortedOrders = orders.Where(p => p.EndTime >= start.Date && p.EndTime <= end.Date);
                parcels = CopyOrders(sortedOrders, true);
                return Request.CreateResponse(HttpStatusCode.OK, parcels);
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exc.ToString());
            }
        }




        [HttpGet]
        public HttpResponseMessage GetIncompleteOrders()
        {


            IEnumerable<Order> orders = repository.Orders.Where(p => p.StatusID != 3); // status id 3 - the order is complete

            try
            {
                LinkedList<OrderParcel> parcels = CopyOrders(orders, false);
                return Request.CreateResponse(HttpStatusCode.OK, parcels);
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exc.ToString());
            }
        }





        private LinkedList<OrderParcel> CopyOrders(IEnumerable<Order> orders, bool complete)
        {
            LinkedList<OrderParcel> parcels = new LinkedList<OrderParcel>();
            List<Order> list = orders.ToList();
            List<OrderConfig> configs = repository.OrderConfigs.ToList();


            foreach (var order in list)
            {
                OrderConfig config = configs.Where(p => p.OrderID == order.OrderID).First();
                string startDate = "";
                string endDate = "";

                try
                {
                    startDate = order.StartTime.Value.ToShortDateString() + " "
                                                   + order.StartTime.Value.Hour + ":" +
                                                   order.StartTime.Value.Minute + ":" +
                                                   order.StartTime.Value.Second;
                }
                catch (Exception)
                {

                }


                if (complete)
                {
                    try
                    {
                        endDate = order.EndTime.Value.ToShortDateString() + " "
                                                       + order.EndTime.Value.Hour + ":" +
                                                       order.EndTime.Value.Minute + ":" +
                                                       order.EndTime.Value.Second;
                    }
                    catch (Exception)
                    {

                    }
                }


                String userName;
                try
                {
                    userName = order.UserID == null ? null : UserManager.FindById(Convert.ToString(order.UserID)).UserName;
                }
                catch (Exception)
                {
                    userName = "not_provided";
                }

                parcels.AddLast(new OrderParcel
                {
                    startTime = startDate,
                    endTime = endDate,
                    mOrderId = order.OrderID,
                    mOrderOwner = userName,
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

                Order order = repository.Orders.Where(p => p.OrderID == orderId).First();
                order.NumberOfProducts = Convert.ToInt16(black) +
                                              Convert.ToInt16(blue) +
                                              Convert.ToInt16(green) +
                                              Convert.ToInt16(red) +
                                              Convert.ToInt16(white);
                repository.SaveChanges();

                return "success";
            }

            catch (DbEntityValidationException exc)
            {
                return "fail " + exc.ToString();
            }
        }





        [HttpGet]
        public HttpResponseMessage GetAlarms()
        {
            IEnumerable<ComponentEventLog> events = repository.ComponentEventLogs.Select(p => p);
            LinkedList<AlarmParcel> alParcels = new LinkedList<AlarmParcel>();

            foreach (ComponentEventLog alarm in events)
            {
                try
                {
                    alParcels.AddLast(
                        new AlarmParcel
                        {
                            Description = alarm.Description,
                            Id = alarm.ID,
                            Type = alarm.LogType.Name,
                            Time = alarm.Timestamp.Value.ToShortDateString() + " " + alarm.Timestamp.Value.ToShortTimeString()
                        });
                }
                catch (Exception)
                { }// continue retrieving alarm events
            }
            return Request.CreateResponse(HttpStatusCode.OK, alParcels);
        }


    }
}
