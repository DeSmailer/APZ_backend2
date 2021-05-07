using BusinessLogicLayer.Interfaces;
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
    public class InstitutionController : Controller
    {
        private readonly IInstitutionService institutionService;

        public InstitutionController(IInstitutionService institutionService)
        {
            this.institutionService = institutionService;
        }

        [HttpGet]
        public async Task<IEnumerable<Institution>> Get()
        {
            return await institutionService.Get();
        }

        [HttpPost]
        public async Task<Institution> GetById([FromBody] TokenContainer tokenContainer)
        {
            int institutionId = AuthenticationService.GetInstitutionId(tokenContainer.Token);
            return await institutionService.GetById(institutionId);
        }

        [HttpPost]
        public async Task<bool> Add([FromBody] InstitutionAndOwner institutionAndOwner)
        {
            int ownerId = AuthenticationService.GetUserId(institutionAndOwner.OwnerToken);
            return await institutionService.Add(new Institution { Name = institutionAndOwner.Name, Location = institutionAndOwner.Location }, ownerId);
        }

        [HttpPut]
        public async Task<bool> UpdateProfile([FromBody] Institution institution)
        {
            return await institutionService.UpdateProfile(institution);
        }

        [HttpDelete]
        public async Task<bool> Delete([FromBody] Institution institution)
        {
            return await institutionService.Delete(institution);
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
