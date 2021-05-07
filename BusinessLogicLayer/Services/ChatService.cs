using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models.Entities;
using DataAccessLayer.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ChatService : IChatService
    {
        private readonly IRepository repository;

        public ChatService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Add(Chat chat)
        {
            await this.repository.AddAsync<Chat>(chat);
            return true;
        }

        public ChatCodeContainer CreateChatCode(string userId, string institutionId)
        {
            string code = "";

            while (institutionId.Length < 4)
            {
                institutionId = "0" + institutionId;
            }
            while (userId.Length < 4)
            {
                userId = "0" + userId;
            }
            code = institutionId + "." + userId;
            return new ChatCodeContainer { ChatCode = code };
        }

        public async Task<bool> Delete(Chat chat)
        {
            var currentChat = await this.repository.GetAsync<Chat>(true, x => x.Id == chat.Id);
            if (currentChat == null)
            {
                throw new Exception("Chat not found.");
            }
            await this.repository.DeleteAsync<Chat>(currentChat);
            return true;
        }

        public async Task<IEnumerable<Chat>> Get()
        {
            var chats = await this.repository.GetRangeAsync<Chat>(true, x => true);
            return chats.ToArray();
        }

        public async Task<IEnumerable<Message>> GetAllMessages(int chatId)
        {
            var messages = await this.repository.GetRangeAsync<Message>(true, x => x.ChatId == chatId);
            return messages.ToArray();
        }

        public async Task<Chat> GetById(int id)
        {
            var chat = await this.repository.GetAsync<Chat>(true, x => x.Id == id);
            if (chat == null)
            {
                throw new Exception("Chat not found");
            }
            return chat;
        }

        public async Task<bool> Post(Message message)
        {
            await this.repository.AddAsync<Message>(message);
            return true;
        }

        public async Task<bool> Update(Chat chat)
        {
            var currentChat = await this.repository.GetAsync<Chat>(true, x => x.Id == chat.Id);
            if (currentChat == null)
            {
                throw new Exception("Chat not found.");
            }
            await this.repository.UpdateAsync<Chat>(currentChat);
            return true;
        }

        public async Task<IEnumerable<ChatWithLastDate>> UserChats(UserInfo userInfo)
        {
            int institutionId = Convert.ToInt32(userInfo.InstitutionId);
            int userId = Convert.ToInt32(AuthenticationService.GetUserId(userInfo.Token));

            List<ChatWithLastDate> chatWithLastDates = new List<ChatWithLastDate>();


            var chats = await this.repository.GetRangeAsync<Chat>(true, x => x.InstitutionId == institutionId &&
                (x.RecipientId == userId || x.InstitutionId == userId));

            foreach (Chat chat in chats)
            {
                var bufferListMessage = await this.repository.GetRangeAsync<Message>(true, x => x.ChatId == chat.Id);
                User initiator = await this.repository.GetAsync<User>(true, x => x.Id == chat.InitiatorId);
                User recipient = await this.repository.GetAsync<User>(true, x => x.Id == chat.RecipientId);
                chatWithLastDates.Add(new ChatWithLastDate
                {
                    Id = chat.Id,
                    InitiatorId = chat.InitiatorId,
                    InitiatorName = initiator.Name,
                    InitiatorSurname = initiator.Surname,
                    RecipientId = chat.RecipientId,
                    InstitutionId = chat.InstitutionId,
                    RecipienName = recipient.Name,
                    RecipienSurname = recipient.Surname,
                    DateTime =bufferListMessage.Max(x => x.Time),
                    Time = String.Format("u: {0:u}", bufferListMessage.Max(x => x.Time))
                }) ;
            }

            return chatWithLastDates.OrderByDescending(x => x.DateTime).ToArray();
        }
    }
}