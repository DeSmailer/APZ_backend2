using DataAccessLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IWalletService
    {
        public Task<IEnumerable<Wallet>> Get();
        public Task<Wallet> GetById(int id);
        public Task<Wallet> GetByInstitutionId(int institutionId);
        public Task<bool> Add(Wallet wallet);
        public Task<bool> Update(Wallet wallet);
        public Task<bool> Delete(Wallet wallet);
        public Task<bool> ChangeBalance(int institutionId, float amount);
        public Task<bool> PayDailyCost();
    }
}
