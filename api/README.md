example local.settings.json:

```
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "Functions:Worker:HostEndpoint": "http://localhost:7272",
        "ApiKey": "aee24d5bf1dd4170b7ae4c6070197ee2",
        "PlayerSeasonStats": "https://api.sportsdata.io/v3/nfl/stats/json/PlayerSeasonStatsByPlayerID/2024REG",
        "PlayerProfiles": "https://api.sportsdata.io/v3/nfl/scores/json/PlayersByAvailable",
        "PlayerInfoContainer": "nfl-player-profiles",
        "PlayerProfilesFilename": "ActivePlayerProfiles.json",
        "APPLICATIONINSIGHTS_CONNECTION_STRING": "InstrumentationKey=b97e0f5f-0d86-4e5d-ada7-9d8bb3be250b;IngestionEndpoint=https://canadacentral-1.in.applicationinsights.azure.com/;LiveEndpoint=https://canadacentral.livediagnostics.monitor.azure.com/;ApplicationId=4f88fc03-e474-46fb-bd43-6129f1b41f77"
    },
    "Host": {
        "CORS": "*"
    }
}
```



1. download azure functions extension
2. download azure function core tools: https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=macos%2Cisolated-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-csharp
3. install .net 8
4. run: (inside FootballApp)
5. dotnet clean
6. dotnet build
7. func start



## Steps
Follow these steps (short) to run the function locally.

Prereqs

Install .NET 8 and confirm with dotnet --version.
Install Azure Functions Core Tools (v4+ that supports .NET 8).
Install the Azure Functions VS Code extension.
If the project uses the storage emulator setting (UseDevelopmentStorage=true) run Azurite (or the Azurite VS Code extension).
Create local.settings.json

Put a local.settings.json in the FootballApp folder with the environment variables used by the app (example below from README.md).
1 vulnerability
hardcoded-credentials Embedding credentials in source code risks unauthorized access
Run locally (terminal)

Open a terminal, cd to the FootballApp folder:
Or run the VS Code build+start task from tasks.json or press F5 with the Azure Functions extension (launch uses port 7272 from FootballApp/Properties/launchSettings.json).
What the function expects

The HTTP trigger is implemented in FootballApp.PlayerComparer.Run. It reads player profiles by filename via FootballApp.Helpers.DataCollector.GetPlayersFromFile which expects the file named by env var PlayerProfilesFilename to be in ActivePlayerProfiles.json.
Player stats are fetched by FootballApp.Helpers.PlayerHandler.GetPlayersStatsAsync using the PlayerSeasonStats + ApiKey env vars.
Troubleshooting

If build fails, confirm dotnet --version is .NET 8 and the project targets net8.0 in FootballApp.csproj.
If you see storage errors, start Azurite (or change AzureWebJobsStorage to a real storage connection string).
If the function returns "Player profiles file not found.", check PlayerProfilesFilename is set in local.settings.json and file exists at ActivePlayerProfiles.json.