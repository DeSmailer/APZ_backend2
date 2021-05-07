using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> Get()
        {
            return await messageService.Get();
        }

        [HttpGet("{id}")]
        public async Task<Message> GetById(int id)
        {
            return await messageService.GetById(id);
        }

        [HttpPost]
        public async Task<bool> Add([FromBody] Message message)
        {
            return await messageService.Add(message);
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] Message message)
        {
            return await messageService.Update(message);
        }

        [HttpDelete]
        public async Task<bool> Delete([FromBody] Message message)
        {
            return await messageService.Delete(message);
        }
    }
}
