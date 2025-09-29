using FootballApp.Models;

namespace FootballApp.Helpers
{
    public class PlayerHandler
    {
        private static readonly string[] VALID_POSITIONS = { "QB", "RB", "FB", "WR", "TE" };
        private static Player? RetrievePlayer(List<Player> players, string playerName)
        {
            return players.FirstOrDefault(p => p.Name != null && p.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase)); 
        }

        private static bool PlayersExist(Player? player1, Player? player2)
        {
            return player1 != null && player1.InfoFilled() && player2 != null && player2.InfoFilled();
        }

        private static bool SamePosition(Player player1, Player player2)
        {
            return player1.Position != null ? player1.Position.Equals(player2.Position) : false;
        }

        private static bool ValidPosition(string? position, string[] validPositions)
        {
            return validPositions.Contains(position);
        }

        private static bool ValidPlayers(Player? player1, Player? player2, string[] validPos) 
        {
            return PlayersExist(player1, player2) && SamePosition(player1, player2) && ValidPosition(player1.Position, validPos);
        }

        public static async Task<List<Player>> GetPlayersStatsAsync(HttpClient client, List<Player> players, string player1Name, string player2Name)
        {
            Player? player1 = RetrievePlayer(players, player1Name); 
            Player? player2 = RetrievePlayer(players, player2Name);

            if (!ValidPlayers(player1, player2, VALID_POSITIONS))
            {
                throw new Exception($"The players provided either do not exist, are not active players, do not play the same position, or do not play one of the following positions: {string.Join(", ", VALID_POSITIONS)}");
            }

            string? apiKey = Environment.GetEnvironmentVariable("ApiKey");
            string player1StatsEndpoint = $"{Environment.GetEnvironmentVariable("PlayerSeasonStats")}/{player1.PlayerID}?key={apiKey}";
            string player2StatsEndpoint = $"{Environment.GetEnvironmentVariable("PlayerSeasonStats")}/{player2.PlayerID}?key={apiKey}";

            player1 = await player1.GetPlayerStats(client, player1StatsEndpoint);
            player2 = await player2.GetPlayerStats(client, player2StatsEndpoint);

            if (player1 == null || player2 == null)
            {
                throw new Exception("Unexpected error: Unable to collect player stats");
            }

            return new List<Player> { player1, player2 };
        }

        // Fancy output
        //private static string DisplayComparison(Player p1, Player p2, Stats p1s, Stats p2s)
        //{
        //    StringBuilder output = new StringBuilder();
        //    int padding = 40;

        //    output.AppendLine(p1.Name.PadRight(padding + padding) + p2.Name.PadRight(padding));
        //    output.AppendLine();
        //    output.AppendLine(p1.Team.PadRight(padding) + "Team".PadRight(padding) + p2.Team.PadRight(padding));
        //    output.AppendLine(p1.Position.PadRight(padding) + "Position".PadRight(padding) + p2.Position.PadRight(padding));
        //    output.AppendLine((p1.HeightFeet + "'" + p1.HeightInches + "\"").PadRight(padding) + "Height".PadRight(padding) + (p2.HeightFeet + "'" + p2.HeightInches + "\"").PadRight(padding));
        //    output.AppendLine((p1.Weight + "lbs").PadRight(padding) + "Weight".PadRight(padding) + (p2.Weight + "lbs").PadRight(padding));
        //    output.AppendLine(p1.Age.ToString().PadRight(padding) + "Age".PadRight(padding) + p2.Age.ToString().PadRight(padding));
        //    output.AppendLine();
        //    output.AppendLine("".PadRight(padding) + "Player Stats");
        //    output.AppendLine();

        //    AppendStats(output, p1s, p2s, padding);

        //    return output.ToString();
        //}

        //private static void AppendStats(StringBuilder output, Stats p1s, Stats p2s, int padding)
        //{
        //    var p1Properties = p1s.GetType().GetProperties();
        //    var p2Properties = p2s.GetType().GetProperties();

        //    foreach (var prop in p1Properties)
        //    {
        //        var p2Prop = Array.Find(p2Properties, p => p.Name == prop.Name);
        //        if (p2Prop != null)
        //        {
        //            var p1Value = prop.GetValue(p1s)?.ToString() ?? "";
        //            var p2Value = p2Prop.GetValue(p2s)?.ToString() ?? "";
        //            output.AppendLine(p1Value.PadRight(padding) + prop.Name.PadRight(padding) + p2Value.PadRight(padding));
        //        }
        //    }
        //}
    }
}
