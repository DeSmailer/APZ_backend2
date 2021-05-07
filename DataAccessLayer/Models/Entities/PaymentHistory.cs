using DataAccessLayer.Models.Entyties.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Entities
{
    public class PaymentHistory : BaseTable
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public DateTime Date { get; set; }
        public float MoneyTransaction { get; set; }
        public float Remainder { get; set; }
    }
}
