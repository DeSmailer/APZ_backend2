using DataAccessLayer.Models.Entyties.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models.Entities
{
    public class Wallet : BaseTable
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        [ForeignKey("InstitutionId")]
        public Institution Institution { get; set; }
        public float CurrentBalance { get; set; }
        public float CostPerDay { get; set; }
        public IEnumerable<PaymentHistory> PaymentHistorys { get; set; }
    }
}
