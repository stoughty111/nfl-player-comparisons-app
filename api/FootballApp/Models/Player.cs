using Newtonsoft.Json;

namespace FootballApp.Models
{
    public class Player
    {
        public int PlayerID { get; set; } = -1;
        public string? Name { get; set; }
        public string? Team { get; set; }
        public string? Position { get; set; }
        public int? HeightFeet { get; set; } = -1;
        public int? HeightInches { get; set; } = -1;
        public int? Weight { get; set; } = -1;
        public int? Age { get; set; } = -1;
        public int Activated { get; set; } = -1;
        public int Played { get; set; } = -1;
        public int Started { get; set; } = -1;
        public double Touchdowns { get; set; } = -1;
        public double Fumbles { get; set; } = -1;
        public double FumblesLost { get; set; } = -1;


        public bool InfoFilled()
        {
            return PlayerID != -1 && Name != null && Team != null && Position != null && HeightFeet != -1 && HeightInches != -1 && Age != -1;
        }

        private static async Task<string> RetrievePlayerStatsFromAPI(HttpClient client, string endpointURL)
        {
            var response = await client.GetAsync(endpointURL);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Player?> GetPlayerStats(HttpClient client, string endpointURL)
        {
            string jsonStats = await RetrievePlayerStatsFromAPI(client, endpointURL);

            Player? playerStats = null;

            if (Position == "QB")
            {
                var qbStatsList = JsonConvert.DeserializeObject<List<QB>>(jsonStats);
                if (qbStatsList == null || qbStatsList[0] == null) {
                    throw new Exception("Unable to deserialize player stats into QB object list");
                }
                playerStats = qbStatsList[0];

            } else if (Position == "FB" || Position == "RB")
            {
                var runnerStatsList = JsonConvert.DeserializeObject<List<Runner>>(jsonStats);
                if (runnerStatsList == null || runnerStatsList[0] == null)
                {
                    throw new Exception("Unable to deserialize player stats into Runner object list");
                }
                playerStats = runnerStatsList[0];
            } else if (Position == "WR" || Position == "TE")
            {
                var recieverStatsList = JsonConvert.DeserializeObject<List<Receiver>>(jsonStats);
                if (recieverStatsList == null || recieverStatsList[0] == null)
                {
                    throw new Exception("Unable to deserialize player stats into Receiver object list");
                }
                playerStats = recieverStatsList[0];
            } else
            {
                throw new Exception("Invalid player position");
            }

            MergeAttributes(playerStats);
            return playerStats;
        }

        private void MergeAttributes(Player newStatsPlayer)
        {
            newStatsPlayer.Name = this.Name;
            newStatsPlayer.Team = this.Team;
            newStatsPlayer.Position = this.Position;
            newStatsPlayer.HeightFeet = this.HeightFeet;
            newStatsPlayer.HeightInches = this.HeightInches;
            newStatsPlayer.Weight = this.Weight;
            newStatsPlayer.Age = this.Age;
        }

    }
}