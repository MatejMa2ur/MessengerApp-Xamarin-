using System;
using System.Net.Http;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Messaging.Models;
using System.Collections.Generic;

namespace Messaging.Recieve
{
    public static class Register
    {
        [FunctionName("Register")]
        public static bool Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "users",
                CreateIfNotExists = true,
                ConnectionStringSetting = "DBConnectionString"
            )]out dynamic document,
            ILogger log)
        {
            string name = req.Query["name"];
            string pass = req.Query["pass"];
            string url = "https://xamarinfinal.azurewebsites.net/api/appusers";
            HttpClient _client = new HttpClient();
            string content = _client.GetStringAsync(url).Result;
            IEnumerable<User> Users = JsonConvert.DeserializeObject<IEnumerable<User>>(content);
            var c = from s in Users
                    where s.username == name
                    select s;
            if(c.Count() == 0 && !String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(pass))
            {
                document = new {id = Guid.NewGuid(),username = name,password = pass};
                return true;
            }
            document = null;
            return false;
        }
    }
}

