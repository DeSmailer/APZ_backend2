using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models.Interfaces;

namespace AzureFunction
{
    public class PayDailyCost
    {
        private readonly IRepository repository;

        public PayDailyCost(IRepository repository)
        {
            this.repository = repository;
        }

        [FunctionName("PayDailyCost")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            IWalletService walletService = new WalletService(repository);
            walletService.PayDailyCost();
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
