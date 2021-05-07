using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using NUnit.Framework;

namespace BusinessLogicLayer.Test
{
    public class AuthenticationServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DecryptString_1employee1_Token()
        {
            string userId = "1";
            string userRole = "employee";
            string institutionId = "1";

            string result = AuthenticationService.GetUserInfo(userId, userRole, institutionId).Token;

            string expected = "IfENfUqVoxDl1Ug+7oLv+g==";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetUserId_token_1()
        {
            string token = "IfENfUqVoxDl1Ug+7oLv+g==";

            int result = AuthenticationService.GetUserId(token);

            int expected = 1;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetUserRole_token_employee()
        {
            string token = "IfENfUqVoxDl1Ug+7oLv+g==";

            string result = AuthenticationService.GetUserRole(token);

            string expected = "employee";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetInstitutionId_token_1()
        {
            string token = "IfENfUqVoxDl1Ug+7oLv+g==";

            int result = AuthenticationService.GetInstitutionId(token);

            int expected = 1;

            Assert.AreEqual(expected, result);
        }
    }
}