using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorbitsChallenge.Models
{
    public class HomeModel
    {
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public int? TireCount { get; set; }
        public List<string> LicensePl { get; set; }
        public string Description { get; set; }
        public string Carmodel { get; set; }
        public string Brand { get; set; }
        public string AddCarReturn { get; set; }
        public string DeleteCarReturn { get; set; }
        public string ModifyCarReturn { get; set; }
        public bool LPexists { get; set; }
    }
}
