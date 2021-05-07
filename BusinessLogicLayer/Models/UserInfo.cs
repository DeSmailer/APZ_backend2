using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Models
{
    public class UserInfo
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string InstitutionId { get; set; }
    }
}
