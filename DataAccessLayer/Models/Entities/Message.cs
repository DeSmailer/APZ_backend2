using DataAccessLayer.Models.Entyties.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Entities
{
    public class Message : BaseTable
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
