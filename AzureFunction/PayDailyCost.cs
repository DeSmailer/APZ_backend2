using Microsoft.Azure.WebJobs;
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
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer)
        {
            IWalletService walletService = new WalletService(repository);
            walletService.PayDailyCost();
        }
    }
}
