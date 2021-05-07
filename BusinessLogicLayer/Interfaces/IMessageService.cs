using DataAccessLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMessageService
    {
        public Task<IEnumerable<Message>> Get();
        public Task<Message> GetById(int id);
        public Task<bool> Add(Message message);
        public Task<bool> Update(Message message);
        public Task<bool> Delete(Message message);
    }
}
