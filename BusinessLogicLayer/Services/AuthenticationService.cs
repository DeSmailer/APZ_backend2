using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Services
{
    public static class AuthenticationService
    {
        public static UserInfo GetUserInfo(string userId, string userRole, string institutionId)
        {
            UserInfo result = new UserInfo { Role = userRole, InstitutionId = institutionId };
            result.Token = CryptographicService.EncryptString(userId + "|" + userRole + "|" + institutionId);
            return result;
        }

        public static int GetUserId(string token)
        {
            return Convert.ToInt32(CryptographicService.DecryptString(token).Split("|")[0]);
        }
        public static string GetUserRole(string token)
        {
            return CryptographicService.DecryptString(token).Split("|")[1];
        }
        public static int GetInstitutionId(string token)
        {
            return Convert.ToInt32(CryptographicService.DecryptString(token).Split("|")[2]);
        }

    }
}
