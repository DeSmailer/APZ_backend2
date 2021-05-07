using DataAccessLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IInstitutionService
    {
        public Task<IEnumerable<Institution>> Get();
        public Task<Institution> GetById(int id);
        public Task<bool> Add(Institution institution, int ownerId);
        public Task<bool> UpdateProfile(Institution institution);
        public Task<bool> Delete(Institution institution);
    }
}
