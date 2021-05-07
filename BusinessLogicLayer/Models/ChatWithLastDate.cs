using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Models
{
    public class ChatWithLastDate
    {
        public int Id { get; set; }
        public int InitiatorId { get; set; }
        public string InitiatorName { get; set; }
        public string InitiatorSurname { get; set; }
        public int RecipientId { get; set; }
        public string RecipienName { get; set; }
        public string RecipienSurname { get; set; }
        public int InstitutionId { get; set; }
        public string Time { get; set; }
        public DateTime DateTime { get; set; }
    }
}
