using BusinessLogicLayer.Models;
using DataAccessLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IInstitutionEmployeeService
    {
        public Task<IEnumerable<InstitutionEmployee>> Get();
        public Task<InstitutionEmployee> GetById(int id);
        public Task<bool> Add(InstitutionEmployee institutionEmployee);
        public Task<bool> Update(InstitutionEmployee institutionEmployee);
        public Task<bool> Delete(InstitutionEmployee institutionEmployee);
        public Task<bool> Dismiss(InstitutionEmployee institutionEmployee);
        public Task<bool> Reinstate(InstitutionEmployee institutionEmployee);
        /// <summary>
        /// де працює людина
        /// </summary>
        /// <param name="user"></param>
        /// <returns>IEnumerable<InstitutionEmployee></returns>
        public Task<IEnumerable<UserJobs>> GetUserJobs(User user);
        /// <summary>
        /// повернути всіх працівників закладу
        /// </summary>
        /// <param name="institution"></param>
        /// <returns>IEnumerable<InstitutionEmployeeInfo></returns>
        public Task<IEnumerable<InstitutionEmployeeInfo>> GetEmployeesOfTheInstitution(int institutionId);
        /// <summary>
        /// повернути всіх працюючих працівників закладу
        /// </summary>
        /// <param name="institution"></param>
        /// <returns>IEnumerable<InstitutionEmployee></returns>
        public Task<IEnumerable<InstitutionEmployee>> GetAllWorkingEmployeesOfTheInstitution(Institution institution);
        /// <summary>
        /// повернути всіх звільнених працівників закладу
        /// </summary>
        /// <param name="institution"></param>
        /// <returns>IEnumerable<InstitutionEmployee></returns>
        public Task<IEnumerable<InstitutionEmployee>> GetAllDismissedEmployeesOfTheInstitution(Institution institution);
    }
}
