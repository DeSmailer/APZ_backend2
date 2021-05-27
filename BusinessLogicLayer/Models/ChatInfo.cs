using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Models
{
    public class ChatInfo
    {
        public int Id { get; set; }
        public int InitiatorId { get; set; }
        public int RecipientId { get; set; }
        public int InstitutionId { get; set; }
    }
}
