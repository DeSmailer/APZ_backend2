using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InstitutionEmployeeController : Controller
    {
        private readonly IInstitutionEmployeeService institutionEmployeeService;

        public InstitutionEmployeeController(IInstitutionEmployeeService institutionEmployeeService)
        {
            this.institutionEmployeeService = institutionEmployeeService;
        }

        [HttpGet]
        public async Task<IEnumerable<InstitutionEmployee>> Get()
        {
            return await institutionEmployeeService.Get();
        }

        [HttpGet("{id}")]
        public async Task<InstitutionEmployee> GetById(int id)
        {
            return await institutionEmployeeService.GetById(id);
        }

        [HttpPost]
        public async Task<bool> Add([FromBody] InstitutionEmployee institutionEmployee)
        {
            string token = this.TokenFromHeader(Request);
            institutionEmployee.InstitutionId = AuthenticationService.GetInstitutionId(token);
            return await institutionEmployeeService.Add(institutionEmployee);
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] InstitutionEmployee institutionEmployee)
        {
            return await institutionEmployeeService.Update(institutionEmployee);
        }

        [HttpDelete]
        public async Task<bool> Delete([FromBody] InstitutionEmployee institutionEmployee)
        {
            return await institutionEmployeeService.Delete(institutionEmployee);
        }

        [HttpPost]
        public async Task<bool> Dismiss([FromBody] InstitutionEmployee institutionEmployee)
        {
            return await institutionEmployeeService.Dismiss(institutionEmployee);
        }

        [HttpPost]
        public async Task<bool> Reinstate([FromBody] InstitutionEmployee institutionEmployee)
        {
            return await institutionEmployeeService.Reinstate(institutionEmployee);
        }

        [HttpPost]
        public async Task<IEnumerable<UserJobs>> GetUserJobs([FromBody] TokenContainer tokenContainer)
        {
            int id = AuthenticationService.GetUserId(tokenContainer.Token);
            return await institutionEmployeeService.GetUserJobs(new User { Id = id });
        }

        [HttpPost]
        public async Task<IEnumerable<InstitutionEmployeeInfo>> GetEmployeesOfTheInstitution([FromBody] TokenContainer tokenContainer)
        {
            int institutionId = AuthenticationService.GetInstitutionId(tokenContainer.Token);
            return await institutionEmployeeService.GetEmployeesOfTheInstitution(institutionId);
        }

        public string TokenFromHeader(HttpRequest request)
        {
            var re = Request;
            var headers = re.Headers;
            string token = "";
            if (headers.ContainsKey("token"))
            {
                token = headers["token"];
            }
            return token;
        }
    }
}
