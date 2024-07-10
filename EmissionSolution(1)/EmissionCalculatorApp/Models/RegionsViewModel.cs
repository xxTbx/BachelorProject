using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmissionCalculatorApp.Models
{
    public class Regions {
        public required string City { get; set; }
        public required string Region { get; set; }
        public required int ECI { get; set; }

    }
}