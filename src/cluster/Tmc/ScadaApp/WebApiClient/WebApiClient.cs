using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.App
{
    public class WebApiClient
    {
        private RestClient _client;

        public WebApiClient(string url)
        {
            _client = new RestClient(url);
        }

        public bool AddUser(string username, string password, string roleName)
        {
            var request = new RestRequest("api/server/adduser/{userName}/{password}/{rolename}");
            request.AddUrlSegment("userName", username);
            request.AddUrlSegment("password", password);
            request.AddUrlSegment("rolename", roleName);

            return _client.Execute(request).Content == "success";   
        }

        public UserInfo Authenticate(string username, string password)
        {
            var request = new RestRequest("api/server/authenticate/{username}/{password}");
            request.AddUrlSegment("username", username);
            request.AddUrlSegment("password", password);

            var reponse = _client.Execute<UserInfo>(request);
            return reponse.Data;
        }


        public class UserInfo
        {
            public string Result { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
        }
    }
}
