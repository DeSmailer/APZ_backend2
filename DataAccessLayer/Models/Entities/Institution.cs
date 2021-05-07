using DataAccessLayer.Models.Entyties.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Entities
{
    public class Institution : BaseTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public IEnumerable<InstitutionEmployee> InstitutionEmployee { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
        public Wallet Wallet { get; set; }
    }
}
