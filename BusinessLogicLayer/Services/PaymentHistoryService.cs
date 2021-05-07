using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models.Entities;
using DataAccessLayer.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PaymentHistoryService : IPaymentHistoryService
    {
        private readonly IRepository repository;

        public PaymentHistoryService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Add(PaymentHistory paymentHistory)
        {
            await this.repository.AddAsync<PaymentHistory>(paymentHistory);
            return true;
        }

        public async Task<bool> Delete(PaymentHistory paymentHistory)
        {
            var currentPaymentHistory = await this.repository.GetAsync<PaymentHistory>(true, x => x.Id == paymentHistory.Id);
            if (currentPaymentHistory == null)
            {
                throw new Exception("PaymentHistory not found.");
            }
            await this.repository.DeleteAsync<PaymentHistory>(currentPaymentHistory);
            return true;
        }

        public async Task<IEnumerable<PaymentHistory>> Get()
        {
            var paymentsHistorys = await this.repository.GetRangeAsync<PaymentHistory>(true, x => true);
            return paymentsHistorys.ToArray();
        }

        public async Task<PaymentHistory> GetById(int id)
        {
            var paymentHistory = await this.repository.GetAsync<PaymentHistory>(true, x => x.Id == id);
            if (paymentHistory == null)
            {
                throw new Exception("PaymentHistory not found");
            }
            return paymentHistory;
        }
        public async Task<IEnumerable<PaymentHistory>> GetByInstitutionId(int institutionId)
        {
            Wallet wallet = await this.repository.GetAsync<Wallet>(true, x => x.InstitutionId == institutionId);
            var paymentHistory = await this.repository.GetRangeAsync<PaymentHistory>(true, x => x.WalletId == wallet.Id);
            if (paymentHistory == null)
            {
                throw new Exception("PaymentHistory not found");
            }
            foreach(PaymentHistory item in paymentHistory)
            {
                item.Wallet = null;
            }
            return paymentHistory.ToArray();
        }
        public async Task<bool> Update(PaymentHistory paymentHistory)
        {
            var currentPaymentHistory = await this.repository.GetAsync<PaymentHistory>(true, x => x.Id == paymentHistory.Id);
            if (currentPaymentHistory == null)
            {
                throw new Exception("PaymentHistory not found.");
            }
            await this.repository.UpdateAsync<PaymentHistory>(currentPaymentHistory);
            return true;
        }
    }
}
