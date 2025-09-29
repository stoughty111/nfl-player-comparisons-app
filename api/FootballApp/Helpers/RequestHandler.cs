using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FootballApp.Helpers
{
    public class RequestHandler
    {
        public static async Task<dynamic?> GetBody(HttpRequest req)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrEmpty(body))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject(body);
            }
        }

        public static bool ValidInput(dynamic data, out string? player1Name, out string? player2Name)
        {
            player1Name = data?.player1;
            player2Name = data?.player2;

            if (string.IsNullOrEmpty(player1Name) || string.IsNullOrEmpty(player2Name))
            {
                return false;
            }
            return true;
        }
    }
}
