using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models.Entities;
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
    public class WalletController : Controller
    {
        private readonly IWalletService walletService;

        public WalletController(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        [HttpGet]
        public async Task<IEnumerable<Wallet>> Get()
        {
            return await walletService.Get();
        }

        [HttpPost]
        public async Task<Wallet> GetByInstitutionId([FromBody] TokenContainer tokenContainer)
        {
            int institutionId = AuthenticationService.GetInstitutionId(tokenContainer.Token);
            return await walletService.GetByInstitutionId(institutionId);
        }

        [HttpPost]
        public async Task<bool> Add([FromBody] Wallet wallet)
        {
            return await walletService.Add(wallet);
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] Wallet wallet)
        {
            return await walletService.Update(wallet);
        }

        [HttpDelete]
        public async Task<bool> Delete([FromBody] Wallet wallet)
        {
            return await walletService.Delete(wallet);
        }

        [HttpPost]
        public async Task<bool> ChangeBalance([FromBody] InstitytionAmount institytionAmount)
        {
            int institutionId = AuthenticationService.GetInstitutionId(institytionAmount.Token);
            return await walletService.ChangeBalance(institutionId, institytionAmount.Amount);
        }
    }
}
