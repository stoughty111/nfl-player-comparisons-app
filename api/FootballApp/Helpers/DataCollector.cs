using Azure.Storage.Blobs;
using FootballApp.Models;
using Newtonsoft.Json;

namespace FootballApp.Helpers
{
    public class DataCollector
    {

        private static BlobClient GetPlayerProfilesBlob(string? connectionString, string? containerName, string? fileName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            return blobContainerClient.GetBlobClient(fileName);
        }

        public static async Task<List<Player>?> GetPlayersFromBlobAsync(string? connectionString, string? containerName, string? fileName)  
        {
            BlobClient blobClient = GetPlayerProfilesBlob(connectionString, containerName, fileName);

            using (MemoryStream stream = new MemoryStream())
            {
                await blobClient.DownloadToAsync(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    List<Player>? result = JsonConvert.DeserializeObject<List<Player>>(json);
                    if (result == null || result.Count == 0)
                    {
                        throw new Exception("Unexpected error: Unable to convert player profiles into a list of Player objects");
                    }
                    return result;
                }
            }
        }

        public static List<Player>? GetPlayersFromFile(string filename) {
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.FullName;
            string filePath = Path.Combine(projectDirectory, "Data", filename);

            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                List<Player>? result = JsonConvert.DeserializeObject<List<Player>>(json);
                if (result == null || result.Count == 0)
                {
                    throw new Exception("Unexpected error: Unable to convert player profiles into a list of Player objects");
                }
                return result;
            }
        }
        
        public static async Task<string> RetrievePlayerStatsFromAPI(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
