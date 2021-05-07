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
    public class MessageService : IMessageService
    {
        private readonly IRepository repository;

        public MessageService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Add(Message message)
        {
            await this.repository.AddAsync<Message>(message);
            return true;
        }

        public async Task<bool> Delete(Message message)
        {
            var currentMessage = await this.repository.GetAsync<Message>(true, x => x.Id == message.Id);
            if (currentMessage == null)
            {
                throw new Exception("Message not found.");
            }
            await this.repository.DeleteAsync<Message>(currentMessage);
            return true;
        }

        public async Task<IEnumerable<Message>> Get()
        {
            var messages = await this.repository.GetRangeAsync<Message>(true, x => true);
            return messages.ToArray();
        }

        public async Task<Message> GetById(int id)
        {
            var message = await this.repository.GetAsync<Message>(true, x => x.Id == id);
            if (message == null)
            {
                throw new Exception("Message not found");
            }
            return message;
        }

        public async Task<bool> Update(Message message)
        {
            var currentMessage = await this.repository.GetAsync<Message>(true, x => x.Id == message.Id);
            if (currentMessage == null)
            {
                throw new Exception("Message not found.");
            }
            await this.repository.UpdateAsync<Message>(currentMessage);
            return true;
        }
    }
}
