using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureLab4
{
    public static class CheckTemperature
    {
        [FunctionName("CheckTemperature")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            int data = int.MinValue;

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic dataBody = JsonConvert.DeserializeObject(requestBody);
            data = dataBody?.data;

            if (data != int.MinValue)
            {
                return new OkObjectResult(GetTemperature(data));
            }
            else
            {
                return new OkObjectResult("Fail");
            }
        }

        private static string GetTemperature(int data)
        {
            if (data <= -15)
            {
                return "+95⸰С";
            }
            else if (data <= -10)
            {
                return "+83⸰С";
            }
            else if (data <= -5)
            {
                return "+70⸰С";
            }
            else if (data <= 0)
            {
                return "+57⸰С";
            }
            else if (data <= 5)
            {
                return "+44⸰С";
            }
            else if (data <= 10)
            {
                return "+30⸰С";
            }
            else
            {
                return "+" + data.ToString() + "⸰С";
            }
        }
    }
}