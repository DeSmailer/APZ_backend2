using DataAccessLayer.Models.Entyties;
using DataAccessLayer.Models.Entyties.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Entities
{
    public class User : BaseTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Feature { get; set; }
        public IEnumerable<InstitutionEmployee> InstitutionEmployee { get; set; }
        public IEnumerable<Chat> InitiatedChats { get; set; }
        public IEnumerable<Chat> AcceptedChats { get; set; }
        public IEnumerable<Message> SendedMessages { get; set; }

    }
}
