using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models.Interfaces;
using DataAccessLayer.Models.Repositories;

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
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer)
        {
            IWalletService walletService = new WalletService(repository);
            walletService.PayDailyCost();
        }
    }
}
