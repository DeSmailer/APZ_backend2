using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models.Entities;
using DataAccessLayer.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PresentationLayer.Hubs;
using PresentationLayer.Hubs.Clients;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : Controller
    {

        private readonly IChatService chatService;
        private readonly IHubContext<ChatHub, IChatClient> chatHub;

        public ChatController(IHubContext<ChatHub, IChatClient> chatHub, IChatService chatService)
        {
            this.chatHub = chatHub;
            this.chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Message message)
        {
            message.Time = DateTime.Now;

            await chatService.Post(message);

            await chatHub.Clients.All.ReceiveMessage(new ChatMessage
            {
                User = message.SenderId.ToString(),
                Message = message.Text
            });

            return Ok();
        }


        [HttpGet]
        public async Task<IEnumerable<Chat>> Get()
        {
            return await chatService.Get();
        }

        [HttpGet("{chatId}")]
        public async Task<IEnumerable<Message>> GetAllMessages(int chatId)
        {
            return await chatService.GetAllMessages(chatId);
        }

        [HttpGet("{id}")]
        public async Task<Chat> GetById(int id)
        {
            return await chatService.GetById(id);
        }

        [HttpPost]
        public async Task<bool> Add([FromBody] Chat chat)
        {
            if (await chatService.Add(chat)!=null)
                return true;
            else
                return false;
        }

        [HttpPut]
        public async Task<bool> Update([FromBody] Chat chat)
        {
            return await chatService.Update(chat);
        }

        [HttpDelete]
        public async Task<bool> Delete([FromBody] Chat chat)
        {
            return await chatService.Delete(chat);
        }

        [HttpPost]
        public ChatCodeContainer CreateChatCode([FromBody] TokenContainer tokenContainer)
        {
            string institutionId = AuthenticationService.GetInstitutionId(tokenContainer.Token).ToString();
            string userId = AuthenticationService.GetUserId(tokenContainer.Token).ToString();

            return chatService.CreateChatCode(userId, institutionId);
        }

        [HttpPost]
        public async Task<IEnumerable<ChatWithLastDate>> UserChats(UserInfo userInfo)
        {
            return await chatService.UserChats(userInfo);
        }

        [HttpPost]
        public async Task<ChatTokenContainer> GetChatToken([FromBody] ChatInfo сhatInfo)
        {
            return await chatService.GetChatToken(сhatInfo);
        }

        [HttpPost]
        public async Task<ChatTokenContainer> GetChatTokenByChatCode([FromBody] ChatCodeContainer chatCodeContainer)
        {
            string token = this.TokenFromHeader(Request);
            int initiatorId = AuthenticationService.GetUserId(token);

            return await chatService.GetChatToken(chatCodeContainer, initiatorId);
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
