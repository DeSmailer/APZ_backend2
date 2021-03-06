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
        public Task<Chat> Add(Chat chat);
        public Task<bool> Update(Chat chat);
        public Task<bool> Delete(Chat chat);
        public Task<bool> Post(Message message);
        public Task<IEnumerable<OldChatMessage>> GetAllMessages(int chatId);
        public ChatCodeContainer CreateChatCode(string userId, string institutionId);
        public Task<ChatTokenContainer> GetChatToken(ChatInfo сhatInfo);
        public Task<IEnumerable<ChatWithLastDate>> UserChats(UserInfo userInfo);
        public Task<ChatTokenContainer> GetChatToken(ChatCodeContainer chatCodeContainer, int initiatorId);
        public int GetChatIdFromToken(string token);
        public int GetInitiatorIdFromToken(string token);
        public int GetRecipientIdFromToken(string token);
        public int GetInstitutionIdFromToken(string token);
        public int GetInstitutionIdFromCode(string chatCode);
        public int GetRecipientIdFromCode(string chatCode);
        public string GetUserName(int userId);
    }
}
