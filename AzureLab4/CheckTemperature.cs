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
             [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            int temperature = int.MinValue;

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic dataBody = JsonConvert.DeserializeObject(requestBody);
            temperature = dataBody?.temperature;

            if (temperature != int.MinValue)
            {
                return new OkObjectResult(GetTemperature(temperature));
            }
            else
            {
                return new OkObjectResult("Fail");
            }
        }

        private static string GetTemperature(int temperature)
        {
            if (temperature <= -15)
            {
                return "+95⸰С";
            }
            else if (temperature <= -10)
            {
                return "+83⸰С";
            }
            else if (temperature <= -5)
            {
                return "+70⸰С";
            }
            else if (temperature <= 0)
            {
                return "+57⸰С";
            }
            else if (temperature <= 5)
            {
                return "+44⸰С";
            }
            else if (temperature <= 10)
            {
                return "+30⸰С";
            }
            else
            {
                return "+" + temperature.ToString() + "⸰С";
            }
        }
    }
}