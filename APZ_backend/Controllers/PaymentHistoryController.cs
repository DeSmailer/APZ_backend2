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
    public class PaymentHistoryController : Controller
    {
        private readonly IPaymentHistoryService paymentHistoryService;

        public PaymentHistoryController(IPaymentHistoryService paymentHistoryService)
        {
            this.paymentHistoryService = paymentHistoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentHistory>> Get()
        {
            return await paymentHistoryService.Get();
        }

        [HttpGet("{id}")]
        public async Task<PaymentHistory> GetById(int id)
        {
            return await paymentHistoryService.GetById(id);
        }

        [HttpPost]
        public async Task<IEnumerable<PaymentHistory>> GetByInstitutionId([FromBody] TokenContainer tokenContainer)
        {
            int institutionId = AuthenticationService.GetInstitutionId(tokenContainer.Token);
            return await paymentHistoryService.GetByInstitutionId(institutionId);
        }

        [HttpPost]
        public async Task<bool> Add([FromBody] PaymentHistory paymentHistory)
        {
            return await paymentHistoryService.Add(paymentHistory);
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] PaymentHistory paymentHistory)
        {
            return await paymentHistoryService.Update(paymentHistory);
        }

        [HttpDelete]
        public async Task<bool> Delete([FromBody] PaymentHistory paymentHistory)
        {
            return await paymentHistoryService.Delete(paymentHistory);
        }
    }
}
