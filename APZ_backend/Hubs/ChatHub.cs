using DataAccessLayer.Models.Entities;
using Microsoft.AspNetCore.SignalR;
using PresentationLayer.Hubs.Clients;
using PresentationLayer.Models;
using System.Threading.Tasks;

namespace PresentationLayer.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public Task JoinGroup(string groupName)
        {
            if (groupName == null)
            {
                groupName = "meow";
            }
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        }
    }
}
