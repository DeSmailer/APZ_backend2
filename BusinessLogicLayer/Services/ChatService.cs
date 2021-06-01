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
        private const int lengthOfTheChatCodePart = 4;
        public ChatService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Chat> Add(Chat chat)
        {
            Chat newChat = await this.repository.AddAsync<Chat>(chat);
            return newChat;
        }

        public ChatCodeContainer CreateChatCode(string userId, string institutionId)
        {
            string code = "";

            while (institutionId.Length < lengthOfTheChatCodePart)
            {
                institutionId = "0" + institutionId;
            }
            while (userId.Length < lengthOfTheChatCodePart)
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

        public async Task<IEnumerable<OldChatMessage>> GetAllMessages(int chatId)
        {
            var messages = this.repository.GetRangeAsync<Message>(true, x => x.ChatId == chatId).Result.TakeLast(20);
            List<OldChatMessage> oldChatMessages = new List<OldChatMessage>();
            foreach (Message message in messages)
            {
                string userName = GetUserName(message.SenderId);
                oldChatMessages.Add(new OldChatMessage
                {
                    UserName = userName,
                    Message = message.Text,
                    Time = message.Time.ToString(),
                });
            }
            return oldChatMessages.ToArray();
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

        public async Task<ChatTokenContainer> GetChatToken(ChatInfo сhatInfo)
        {
            var chat = await this.repository.GetAsync<Chat>(true, x => x.Id == сhatInfo.Id && x.InitiatorId == сhatInfo.InitiatorId &&
                x.RecipientId == сhatInfo.RecipientId && x.InstitutionId == сhatInfo.InstitutionId);
            if (chat == null)
            {
                throw new Exception("Chat not found");
            }
            ChatTokenContainer chatTokenContainer = new ChatTokenContainer();
            chatTokenContainer.ChatToken = CryptographicService.EncryptString(chat.Id + "|" + chat.InitiatorId + "|" +
                chat.RecipientId + "|" + chat.InstitutionId);
            chatTokenContainer.ChatId = chat.Id;
            return chatTokenContainer;
        }

        public async Task<ChatTokenContainer> GetChatToken(ChatCodeContainer chatCodeContainer, int initiatorId)
        {
            ChatInfo сhatInfo = new ChatInfo();

            сhatInfo.InitiatorId = initiatorId;
            сhatInfo.RecipientId = GetRecipientIdFromCode(chatCodeContainer.ChatCode);
            сhatInfo.InstitutionId = GetInstitutionIdFromCode(chatCodeContainer.ChatCode);

            var chat = await this.repository.GetAsync<Chat>(true, x => x.InitiatorId == сhatInfo.InitiatorId &&
                x.RecipientId == сhatInfo.RecipientId && x.InstitutionId == сhatInfo.InstitutionId);
            if (chat == null)
            {
                chat = await Add(new Chat { InitiatorId = сhatInfo.InitiatorId, RecipientId = сhatInfo.RecipientId, InstitutionId = сhatInfo.InstitutionId });
            }
            сhatInfo.Id = chat.Id;
            return await GetChatToken(сhatInfo);
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
                int messageCount = 0;
                foreach (Message message in bufferListMessage)
                {
                    messageCount++;
                }
                if (messageCount <= 0)
                {
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
                        DateTime = DateTime.Now,
                        Time = String.Format("u: {0:u}", DateTime.Now)
                    }) ;
                }
                else
                {
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
                        DateTime = bufferListMessage.Max(x => x.Time),
                        Time = String.Format("u: {0:u}", bufferListMessage.Max(x => x.Time))
                    });
                }
                
            }

            return chatWithLastDates.OrderByDescending(x => x.DateTime).ToArray();
        }

        public int GetChatIdFromToken(string token)
        {
            return Convert.ToInt32(CryptographicService.DecryptString(token).Split("|")[0]);
        }

        public int GetInitiatorIdFromToken(string token)
        {
            return Convert.ToInt32(CryptographicService.DecryptString(token).Split("|")[1]);
        }

        public int GetRecipientIdFromToken(string token)
        {
            return Convert.ToInt32(CryptographicService.DecryptString(token).Split("|")[2]);
        }

        public int GetInstitutionIdFromToken(string token)
        {
            return Convert.ToInt32(CryptographicService.DecryptString(token).Split("|")[3]);
        }

        public int GetInstitutionIdFromCode(string chatCode)
        {
            string InstitutionIdWithZeros = chatCode.Split(".")[0];
            string InstitutionIdWithoutZeros = "";
            foreach (char c in InstitutionIdWithZeros)
            {
                if (c != '0')
                {
                    InstitutionIdWithoutZeros += c;
                }
            }
            return Convert.ToInt32(InstitutionIdWithoutZeros);
        }

        public int GetRecipientIdFromCode(string chatCode)
        {
            string RecipientIdWithZeros = chatCode.Split(".")[1];
            string IRecipientIdWithoutZeros = "";
            foreach (char c in RecipientIdWithZeros)
            {
                if (c != '0')
                {
                    IRecipientIdWithoutZeros += c;
                }
            }
            return Convert.ToInt32(IRecipientIdWithoutZeros);
        }

        public string GetUserName(int userId)
        {
            UserService userService = new UserService(repository);
            return userService.GetUserName(userId);
        }
    }
}