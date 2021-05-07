using DataAccessLayer.Models.Entities;
using Microsoft.AspNetCore.SignalR;
using PresentationLayer.Hubs.Clients;
using System.Threading.Tasks;

namespace PresentationLayer.Hubs
{
    public class ChatHub : Hub<IChatClient>
    { }
}
