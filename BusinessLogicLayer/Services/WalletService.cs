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
    public class WalletService : IWalletService
    {
        private readonly IRepository repository;

        public WalletService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Add(Wallet wallet)
        {
            await this.repository.AddAsync<Wallet>(wallet);
            return true;
        }

        public async Task<bool> ChangeBalance(int institutionId, float amount)
        {
            var wallet = await this.repository.GetAsync<Wallet>(true, x => x.InstitutionId == institutionId);

            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }
            wallet.CurrentBalance = wallet.CurrentBalance + amount;

            PaymentHistoryService paymentHistory = new PaymentHistoryService(repository);
            await paymentHistory.Add(new PaymentHistory { WalletId = wallet.Id, Date = DateTime.Now, MoneyTransaction = amount, Remainder = wallet.CurrentBalance });

            await this.repository.UpdateAsync<Wallet>(wallet);

            return true;
        }

        public async Task<bool> Delete(Wallet wallet)
        {
            var currentWallet = await this.repository.GetAsync<Wallet>(true, x => x.Id == wallet.Id);
            if (currentWallet == null)
            {
                throw new Exception("Wallet not found.");
            }
            await this.repository.DeleteAsync<Wallet>(currentWallet);
            return true;
        }

        public async Task<IEnumerable<Wallet>> Get()
        {
            var wallets = await this.repository.GetRangeAsync<Wallet>(true, x => true);
            return wallets.ToArray();
        }

        public async Task<Wallet> GetById(int id)
        {
            var wallet = await this.repository.GetAsync<Wallet>(true, x => x.Id == id);
            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }
            return wallet;
        }

        public async Task<Wallet> GetByInstitutionId(int institutionId)
        {
            var wallet = await this.repository.GetAsync<Wallet>(true, x => x.InstitutionId == institutionId);
            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }
            return wallet;
        }

        public async Task<bool> PayDailyCost()
        {
            var institutions = await this.repository.GetRangeAsync<Institution>(true, x => true);

            foreach(Institution institution in institutions)
            {
                await ChangeBalance(institution.Id, 0.5f);
            }
            return true;
        }

        public async Task<bool> Update(Wallet wallet)
        {
            var currentWallet = await this.repository.GetAsync<Wallet>(true, x => x.Id == wallet.Id);
            if (currentWallet == null)
            {
                throw new Exception("Wallet not found.");
            }
            await this.repository.UpdateAsync<Wallet>(currentWallet);
            return true;
        }
    }
}
