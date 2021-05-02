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
    public static class Messages
    {
        [FunctionName("Messages")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "items",
                ConnectionStringSetting = "DBConnectionString",
                SqlQuery = "SELECT TOP 8 * FROM items ORDER BY items._ts"
            )]IEnumerable<dynamic> IDK,
            ILogger log)
        {
            log.LogInformation("Hello World this is Matej!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            log.LogInformation(requestBody);

            return new OkObjectResult(JsonConvert.SerializeObject(IDK));
            //return new OkObjectResult($"Hello {output}");
        }
    }
}
