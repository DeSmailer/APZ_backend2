using DataAccessLayer.Models.Entyties.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models.Entities
{
    public class Chat : BaseTable
    {
        public int Id { get; set; }
        public int InitiatorId { get; set; }
        [ForeignKey("InitiatorId")]
        public User Initiator { get; set; }
        public int RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }
        public int InstitutionId { get; set; }
        [ForeignKey("InstitutionId")]
        public Institution Institution { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
