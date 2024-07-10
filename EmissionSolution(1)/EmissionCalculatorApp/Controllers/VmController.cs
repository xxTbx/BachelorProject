using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using EmissionCalculatorApp.Models;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using Regions = EmissionCalculatorApp.Models.Regions;

namespace EmissionCalculatorApp.Controllers
{
    public class VmController : Controller
    {
        public static CosmosClient cosmosClient;
        public static Database database;
        public static Container instances;
        public static Container results;
        public static Container regions;


        private readonly ILogger<VmController> _logger;

        public VmController(ILogger<VmController> logger)
        {
            _logger = logger;
        }

       
        private async Task InitializeInstancesContainer()
        {
            var options = new CosmosClientOptions() { ConnectionMode = ConnectionMode.Gateway };
            cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, options);
            database = cosmosClient.GetDatabase("serverDatabase");
            instances = database.GetContainer("instances");
        }

        private async Task InitializeResultsContainer()
        {
            var options = new CosmosClientOptions() { ConnectionMode = ConnectionMode.Gateway };
            cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, options);
            database = cosmosClient.GetDatabase("serverDatabase");
            results = database.GetContainer("results");
        }

        private async Task InitializeRegionsContainer()
        {
            var options = new CosmosClientOptions() { ConnectionMode = ConnectionMode.Gateway };
            cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, options);
            database = cosmosClient.GetDatabase("serverDatabase");
            regions = database.GetContainer("regions");
        }
        private static async Task<List<Instances>> QueryInstances()
        {
            var sqlQueryText = "SELECT * FROM instances";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Instances> queryResultSetIterator = instances.GetItemQueryIterator<Instances>(queryDefinition);
            List<Instances> instanceList = new List<Instances>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Instances> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Instances instance in currentResultSet)
                {
                    instanceList.Add(instance);
                }
            }

            return instanceList;
        }

        private static async Task<List<Regions>> QueryRegions ()
        {
            var sqlQueryText = "SELECT * FROM regions";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Regions> queryResultSetIterator = regions.GetItemQueryIterator<Regions>(queryDefinition);
            List<Regions> regionList = new List<Regions>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Regions> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Regions region in currentResultSet)
                {
                    regionList.Add(region);
                }
            }

            return regionList;
        }

        public async Task<IActionResult> Vm()
        {
            await InitializeInstancesContainer();
            await InitializeRegionsContainer();
            var instances = await QueryInstances();
            var regions = await QueryRegions();
            ViewBag.instanceItems = instances;
            ViewBag.regionItems = regions;
            return View("Vm");
        }


  
        public async Task <IActionResult> VmInput()
        {
            await InitializeRegionsContainer();
            var regions = await QueryRegions();
            ViewBag.regionItems = regions;
            return View("VmInput");
        }

        [HttpPost]
        public async Task<IActionResult> SaveResult([FromBody] ResultItem resultItem)
        {
            await InitializeResultsContainer();
            try
            {
                // // Lagre resultatet i Cosmos DB
                database = cosmosClient.GetDatabase("serverDatabase");
                results = database.GetContainer("results");
                var response = await results.CreateItemAsync(resultItem);
                return StatusCode(500, "verti");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
