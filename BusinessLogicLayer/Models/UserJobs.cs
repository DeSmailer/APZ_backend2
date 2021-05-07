using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Models
{
    public class UserJobs
    {
        public int InstitutionId { get; set; }
        public string InstitutionName { get; set; }
        public string Role { get; set; }
        public bool IsWorking { get; set; }
    }
}
