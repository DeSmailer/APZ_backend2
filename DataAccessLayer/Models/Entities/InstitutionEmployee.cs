using DataAccessLayer.Models.Entyties.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Entities
{
    public class InstitutionEmployee : BaseTable
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Role { get; set; }
        public bool IsWorking { get; set; }
    }
}
