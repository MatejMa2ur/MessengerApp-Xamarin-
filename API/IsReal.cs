using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Messaging.Models;

namespace Messaging.Listening
{
    public static class IsReal
    {
        [FunctionName("IsReal")]
        public static bool Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "users",
                ConnectionStringSetting = "DBConnectionString",
                SqlQuery = "SELECT * FROM users"
            )]IEnumerable<User> Users,
            ILogger log)
        {
            string name = req.Query["name"];
            string pass = req.Query["pass"];
            var c = from s in Users
                    where s.username == name
                    select s;
            if(c.Count()==0)
                return false;
            else
                return true;
        }
    }
}
