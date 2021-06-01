using System;
using System.Collections.Generic;
using System.Text;
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models.Interfaces;
using DataAccessLayer.Models.Repositories;
namespace AzureFunction
{
    public interface IPayDailyCost
    {
        public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log);
    }
}
