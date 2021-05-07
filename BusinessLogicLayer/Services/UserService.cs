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
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Register(User user)
        {
            var registeredUser = await this.repository.GetAsync<User>(true, x => x.Email == user.Email);

            if (registeredUser != null)
            {
                throw new Exception("Email is already registered.");
            }

            user.Password = AuthenticationService.GetHashString(user.Password);
            await this.repository.AddAsync<User>(user);
            return true;
        }

        public async Task<bool> Delete(User user)
        {
            var currentUser = await this.repository.GetAsync<User>(true, x => x.Id == user.Id);
            if (currentUser == null) { 
                throw new Exception("User not found.");
            }
            await this.repository.DeleteAsync<User>(currentUser);
            return true;
        }

        public async Task<IEnumerable<User>> Get()
        {
            var users = await this.repository.GetRangeAsync<User>(true, x => true);
            return users.ToArray();
        }

        public async Task<User> GetById(int id)
        {
            var user = await this.repository.GetAsync<User>(true, x => x.Id == id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task<bool> UpdateProfile(User user)
        {
            var currentUser = await this.repository.GetAsync<User>(true, x => x.Id == user.Id);
            if (currentUser == null)
            {
                throw new Exception("User not found.");
            }
            currentUser.Name = user.Name;
            currentUser.Surname = user.Surname;
            currentUser.Feature = user.Feature;
            await this.repository.UpdateAsync<User>(currentUser);
            return true;
        }

        public async Task<UserInfo> LoginLikeClient(User user)
        {
            var client = await this.repository.GetAsync<User>(true, x => x.Email == user.Email && x.Password == AuthenticationService.GetHashString(user.Password));
            if (client == null)
            {
                throw new Exception("User not found");
            }
            UserInfo userInfo = new UserInfo();
            userInfo.Token = AuthenticationService.EncryptString(client.Id + "|" + "user" + "|" + "-1");
            userInfo.Role = "user";
            userInfo.InstitutionId = "-1";
            return userInfo;
        }
        public async Task<UserInfo> LoginLikeEmployee(UserInfo userInfo)
        {
            int clientId = AuthenticationService.GetUserId(userInfo.Token);

            var client = await this.repository.GetAsync<User>(true, x => x.Id == clientId);
            if (client == null)
            {
                throw new Exception("User not found");
            }
            string role = userInfo.Role;
            string InstitutionId = userInfo.InstitutionId.ToString();

            UserInfo newUserInfo = new UserInfo();
            newUserInfo.Token = AuthenticationService.EncryptString(clientId + "|" + role + "|" + InstitutionId);
            newUserInfo.Role = role;
            newUserInfo.InstitutionId = InstitutionId;
            return newUserInfo;
        }
    }
}
