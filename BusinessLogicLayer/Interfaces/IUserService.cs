using BusinessLogicLayer.Models;
using DataAccessLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> Get();
        public Task<User> GetById(int id);
        public Task<bool> Register(User user);
        public Task<bool> UpdateProfile(User user);
        public Task<bool> Delete(User user);
        public Task<UserInfo> LoginLikeClient(User user);
        public Task<UserInfo> LoginLikeEmployee(UserInfo userInfo);
        public string GetUserName(int userId);
    }
}
