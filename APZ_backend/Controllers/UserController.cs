using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await userService.Get();
        }

        [HttpPost]
        public async Task<User> GetByToken([FromBody] TokenContainer tokenContainer)
        {
            int userId = AuthenticationService.GetUserId(tokenContainer.Token);
            return await userService.GetById(userId);
        }

        [HttpPost]
        public async Task<bool> Register([FromBody] User user)
        {
            return await userService.Register(user);
        }

        [HttpPut]
        public async Task<bool> UpdateProfile([FromBody] User user)
        {
            string token = this.TokenFromHeader(Request);
            int id = AuthenticationService.GetUserId(token);
            if (user.Id == id)
            {
                return await userService.UpdateProfile(user);
            }
            throw new Exception("Wrong user");
        }

        [HttpDelete]
        public async Task<bool> Delete([FromBody] User user)
        {
            return await userService.Delete(user);
        }

        [HttpPost]
        public async Task<UserInfo> LoginLikeClient([FromBody] User user)
        {
            return await userService.LoginLikeClient(user);
        }

        [HttpPost]
        public async Task<UserInfo> LoginLikeEmployee([FromBody] UserInfo userInfo)
        {
            return await userService.LoginLikeEmployee(userInfo);
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
