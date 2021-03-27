using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Messaging.Recieve
{
    public static class SendMessage
    {
        [FunctionName("SendMessage")]
        public static void Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "items",
                CreateIfNotExists = true,
                ConnectionStringSetting = "DBConnectionString"
            )]out dynamic document,
            ILogger log)
        {
            string name = req.Query["name"];
            string message = req.Query["message"];
            document = new {id = Guid.NewGuid(),name = name,message = message};
        }
    }
}

