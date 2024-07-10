using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmissionCalculatorApp.Models
{
public class ResultItem
{
    public required string id { get; set; }
    public double Result { get; set; }
    public string Instance { get; set; }
    public string Region{ get; set; }
    public int ComputingHours { get; set; }
}

         

}
