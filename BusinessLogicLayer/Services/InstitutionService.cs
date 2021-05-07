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
    public class InstitutionService : IInstitutionService
    {
        private readonly IRepository repository;

        public InstitutionService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Add(Institution institution, int ownerId)
        {
            var newInstitution = await this.repository.AddAsync<Institution>(institution);

            WalletService walletService = new WalletService(repository);

            await walletService.Add(new Wallet
            {
                InstitutionId = newInstitution.Id,
                CurrentBalance = 100f,
                CostPerDay = 10f
            });

            InstitutionEmployeeService institutionEmployeeService = new InstitutionEmployeeService(repository);

            await institutionEmployeeService.Add(new InstitutionEmployee
            {
                InstitutionId = newInstitution.Id,
                UserId = ownerId,
                Role = "admin",
                IsWorking = true
            });

            return true;
        }

        public async Task<bool> Delete(Institution institution)
        {
            var currentInstitution = await this.repository.GetAsync<Institution>(true, x => x.Id == institution.Id);
            if (currentInstitution == null)
            {
                throw new Exception("Institution not found.");
            }
            await this.repository.DeleteAsync<Institution>(currentInstitution);
            return true;
        }

        public async Task<IEnumerable<Institution>> Get()
        {
            var institutions = await this.repository.GetRangeAsync<Institution>(true, x => true);
            return institutions.ToArray();
        }

        public async Task<Institution> GetById(int id)
        {
            var institution = await this.repository.GetAsync<Institution>(true, x => x.Id == id);
            if (institution == null)
            {
                throw new Exception("Institution not found");
            }
            return institution;
        }

       

        public async Task<bool> UpdateProfile(Institution institution)
        {
            var currentInstitution = await this.repository.GetAsync<Institution>(true, x => x.Id == institution.Id);
            if (currentInstitution == null)
            {
                throw new Exception("User not found.");
            }
            currentInstitution.Name = institution.Name;
            currentInstitution.Location = institution.Location;
            await this.repository.UpdateAsync<Institution>(currentInstitution);
            return true;
        }
    }
}
