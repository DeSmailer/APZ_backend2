using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models.Entities;
using DataAccessLayer.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class InstitutionEmployeeService : IInstitutionEmployeeService
    {
        private readonly IRepository repository;

        public InstitutionEmployeeService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Add(InstitutionEmployee institutionEmployee)
        {
            if (AlreadyWorked(institutionEmployee) != null)
            {
                await Reinstate(institutionEmployee);
            }
            else
            {
                institutionEmployee.IsWorking = true;
                await this.repository.AddAsync<InstitutionEmployee>(institutionEmployee);
            }

            return true;
        }
        private InstitutionEmployee AlreadyWorked(InstitutionEmployee institutionEmployee)
        {
            var currentInstitutionEmployee = this.repository.Get<InstitutionEmployee>(true,
                 x => x.InstitutionId == institutionEmployee.InstitutionId &&
                 x.UserId == institutionEmployee.UserId && x.Role == institutionEmployee.Role);

            return currentInstitutionEmployee;
        }

        public async Task<bool> Delete(InstitutionEmployee institutionEmployee)
        {
            var currentInstitutionEmployee = await this.repository.GetAsync<InstitutionEmployee>(true, x => x.Id == institutionEmployee.Id);
            if (currentInstitutionEmployee == null)
            {
                throw new Exception("InstitutionEmployee not found.");
            }
            await this.repository.DeleteAsync<InstitutionEmployee>(currentInstitutionEmployee);
            return true;
        }

        public async Task<bool> Dismiss(InstitutionEmployee institutionEmployee)
        {
            var currentInstitutionEmployee = await this.repository.GetAsync<InstitutionEmployee>(true,
                x => x.InstitutionId == institutionEmployee.InstitutionId &&
                x.UserId == institutionEmployee.UserId && x.Role == institutionEmployee.Role);

            if (currentInstitutionEmployee == null)
            {
                throw new Exception("InstitutionEmployee not found.");
            }

            currentInstitutionEmployee.IsWorking = false;
            await this.repository.UpdateAsync<InstitutionEmployee>(currentInstitutionEmployee);
            return true;
        }

        public async Task<IEnumerable<InstitutionEmployee>> Get()
        {
            var institutionEmployees = await this.repository.GetRangeAsync<InstitutionEmployee>(true, x => true);
            return institutionEmployees.ToArray();
        }

        public async Task<InstitutionEmployee> GetById(int id)
        {
            var institutionEmployee = await this.repository.GetAsync<InstitutionEmployee>(true, x => x.Id == id);
            if (institutionEmployee == null)
            {
                throw new Exception("InstitutionEmployee not found");
            }
            return institutionEmployee;
        }

        public async Task<bool> Reinstate(InstitutionEmployee institutionEmployee)
        {
            var currentInstitutionEmployee = await this.repository.GetAsync<InstitutionEmployee>(true,
                x => x.InstitutionId == institutionEmployee.InstitutionId &&
                x.UserId == institutionEmployee.UserId && x.Role == institutionEmployee.Role);

            if (currentInstitutionEmployee == null)
            {
                throw new Exception("InstitutionEmployee not found.");
            }

            currentInstitutionEmployee.IsWorking = true;
            await this.repository.UpdateAsync<InstitutionEmployee>(currentInstitutionEmployee);
            return true;
        }

        public async Task<bool> Update(InstitutionEmployee institutionEmployee)
        {
            var currentInstitutionEmployee = await this.repository.GetAsync<InstitutionEmployee>(true, x => x.Id == institutionEmployee.Id);

            if (currentInstitutionEmployee == null)
            {
                throw new Exception("InstitutionEmployee not found.");
            }

            await this.repository.UpdateAsync<InstitutionEmployee>(currentInstitutionEmployee);
            return true;
        }

        public async Task<IEnumerable<UserJobs>> GetUserJobs(User user)
        {
            var currentUser = await this.repository.GetAsync<User>(true, x => x.Id == user.Id);

            if (currentUser == null)
            {
                throw new Exception("User not found.");
            }

            var institutionEmployees = await this.repository.GetRangeAsync<InstitutionEmployee>(true, x => x.UserId == currentUser.Id && x.IsWorking == true);
            List<UserJobs> userJobs = new List<UserJobs>();
            foreach (InstitutionEmployee item in institutionEmployees)
            {
                if (item != null)
                {
                    userJobs.Add(new UserJobs
                    {
                        InstitutionId = item.InstitutionId,
                        InstitutionName = this.repository.Get<Institution>(true, x => x.Id == item.InstitutionId).Name,
                        Role = item.Role,
                        IsWorking = item.IsWorking
                    });
                }
            }
            return userJobs.ToArray();
        }

        public async Task<IEnumerable<InstitutionEmployeeInfo>> GetEmployeesOfTheInstitution(int institutionId)
        {
            var currentInstitution = await this.repository.GetAsync<Institution>(true, x => x.Id == institutionId);

            if (currentInstitution == null)
            {
                throw new Exception("Institution not found.");
            }

            var institutionEmployees = await this.repository.GetRangeAsync<InstitutionEmployee>(true, x => x.InstitutionId == currentInstitution.Id);

            List<InstitutionEmployeeInfo> institutionEmployeeInfos = new List<InstitutionEmployeeInfo>();

            foreach (InstitutionEmployee item in institutionEmployees)
            {
                institutionEmployeeInfos.Add(new InstitutionEmployeeInfo
                {
                    Id = item.Id,
                    InstitutionId = item.InstitutionId,
                    InstitutionName = this.repository.Get<Institution>(true, x => x.Id == item.InstitutionId).Name,
                    UserId = item.UserId,
                    UserName = this.repository.Get<User>(true, x => x.Id == item.UserId).Name,
                    UserSurname = this.repository.Get<User>(true, x => x.Id == item.UserId).Surname,
                    Role = item.Role,
                    IsWorking = item.IsWorking
                });
            }

            return institutionEmployeeInfos.ToArray();
        }

        public async Task<IEnumerable<InstitutionEmployee>> GetAllWorkingEmployeesOfTheInstitution(Institution institution)
        {
            var currentInstitution = await this.repository.GetAsync<Institution>(true, x => x.Id == institution.Id);

            if (currentInstitution == null)
            {
                throw new Exception("Institution not found.");
            }

            var institutionEmployees = await this.repository.GetRangeAsync<InstitutionEmployee>(true, 
                x => x.InstitutionId == currentInstitution.Id && x.IsWorking == true);

            return institutionEmployees.ToArray();
        }

        public async Task<IEnumerable<InstitutionEmployee>> GetAllDismissedEmployeesOfTheInstitution(Institution institution)
        {
            var currentInstitution = await this.repository.GetAsync<Institution>(true, x => x.Id == institution.Id);

            if (currentInstitution == null)
            {
                throw new Exception("Institution not found.");
            }

            var institutionEmployees = await this.repository.GetRangeAsync<InstitutionEmployee>(true,
                x => x.InstitutionId == currentInstitution.Id && x.IsWorking == false);

            return institutionEmployees.ToArray();
        }
    }
}
