using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmissionCalculatorApp.Models
{
    public class Instances
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Prosessor { get; set; }
        public int Cpus { get; set; }
        public int CpuTdp { get; set; }
        public int Gpus { get; set; }
        public int GpuTdp { get; set; }
        
        [JsonProperty("Memory-Gib")]
        public double MemoryGib { get; set;}
        
        [JsonProperty("Temp-storage-(SSD)-Gib")]
        public int TempStorageGiB { get; set;}
        [JsonProperty("Max-data-disks")]
        public int MaxDataDisks { get; set;}
        
    }
         

}
