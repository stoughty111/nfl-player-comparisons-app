using FootballApp.Helpers;
using FootballApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FootballApp
{
    public class PlayerComparer
    {
        private readonly ILogger<PlayerComparer> _logger;

        public PlayerComparer(ILogger<PlayerComparer> logger)
        {
            _logger = logger;
        }

        [Function("PlayerComparer")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var client = new HttpClient();
            object? responseMsg;

            try
            {
                dynamic? data = await RequestHandler.GetBody(req);

                if (data == null)
                {
                    return new BadRequestObjectResult("Must provide two active players' names that play the same position which are either a quarterback, running back, full back, wide receiver or tight end.");
                }

                if (!RequestHandler.ValidInput(data, out string? player1Name, out string? player2Name))
                {
                    return new BadRequestObjectResult("Must provide two active players' names that are either a quarterback, running back, full back, wide receiver or tight end.");

                }

                // Using blob storage:
                // List<Player>? players = await DataCollector.GetPlayersFromBlobAsync(
                //     Environment.GetEnvironmentVariable("AzureWebJobsStorage"),
                //     Environment.GetEnvironmentVariable("PlayerInfoContainer"),
                //     Environment.GetEnvironmentVariable("PlayerProfilesFilename"));

                // Using local file:
                if (Environment.GetEnvironmentVariable("PlayerProfilesFilename") == null)
                {
                    return new BadRequestObjectResult("Player profiles file not found.");
                }
                List<Player>? players = DataCollector.GetPlayersFromFile(
                    Environment.GetEnvironmentVariable("PlayerProfilesFilename"));

                List<Player>? playersStats = await PlayerHandler.GetPlayersStatsAsync(client, players, player1Name, player2Name);

                responseMsg = new List<object>
                {
                    playersStats[0],
                    playersStats[1]
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult(responseMsg);
        }
    }
}