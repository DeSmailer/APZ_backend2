using BusinessLogicLayer.Models;
using DataAccessLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IChatService
    {
        public Task<IEnumerable<Chat>> Get();
        public Task<Chat> GetById(int id);
        public Task<bool> Add(Chat chat);
        public Task<bool> Update(Chat chat);
        public Task<bool> Delete(Chat chat);
        public Task<bool> Post(Message message);
        public Task<IEnumerable<Message>> GetAllMessages(int chatId);
        public ChatCodeContainer CreateChatCode(string userId, string institutionId);
        public ChatCodeContainer GetChatToken(ChatInfo сhatInfo);
        public Task<IEnumerable<ChatWithLastDate>> UserChats(UserInfo userInfo);
    }
}
