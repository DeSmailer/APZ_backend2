using DataAccessLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPaymentHistoryService
    {
        public Task<IEnumerable<PaymentHistory>> Get();
        public Task<PaymentHistory> GetById(int id);
        public Task<bool> Add(PaymentHistory paymentHistory);
        public Task<bool> Update(PaymentHistory paymentHistory);
        public Task<bool> Delete(PaymentHistory paymentHistory);
        public Task<IEnumerable<PaymentHistory>> GetByInstitutionId(int institutionId);
    }
}
