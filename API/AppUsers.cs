using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Messaging.Listening
{
    public static class AppUsers
    {
        [FunctionName("AppUsers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "users",
                ConnectionStringSetting = "DBConnectionString",
                SqlQuery = "SELECT * FROM users"
            )]IEnumerable<dynamic> IDK,
            ILogger log)
        {
            return new OkObjectResult(JsonConvert.SerializeObject(IDK));
        }
    }
}
