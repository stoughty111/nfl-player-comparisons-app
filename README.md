# NFL Player Comparisons
A small full-stack app that compares 2024 NFL player season statistics. The backend is an Azure Function (C#) that loads player profiles, fetches season stats from [SportsDataIO](https://sportsdata.io/), and returns two player objects. The frontend is a React app (Vite + MUI) that posts player names and renders side‑by‑side stats.

## Demo
https://github.com/user-attachments/assets/3980f736-3df1-4598-8251-e1d420a9fb04

## Run Locally
### 1. api (FootballApp):
**In Visual Studio Code:**
1. Install .NET 8 (check installation via `dotnet --version`)
2. Install [Azure Function Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=macos%2Cisolated-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-csharp)
3. Install the Azure Functions VS Code extension
4. In the FootballApp folder:
    - Create a `local.settings.json` file and copy the contents from the sample file [local.settings.json.example](https://github.com/stoughty111/nfl-player-comparisons-app/blob/main/api/FootballApp/local.settings.json.example)
    - Run the following commands in the terminal:
        ```{}
        dotnet clean
        dotnet build
        func start
        ```
**In Visual Studio:**
1. Install .NET 8 (check installation via `dotnet --version`)
2. Install [Azure Function Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=macos%2Cisolated-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-csharp)
3. Open the function app via the `FootballApp.sln` file
4. In the FootballApp folder create a `local.settings.json` file and copy the contents from the sample file [local.settings.json.example](https://github.com/stoughty111/nfl-player-comparisons-app/blob/main/api/FootballApp/local.settings.json.example)
5. Select the run button at the top of Visual Studio

### Sample Endpoint Request
Send a **POST** request with a JSON body in the following format:
```json
{
  "player1": "Justin Jefferson",
  "player2": "Tyreek Hill"
}
```

### 2. react-app
1. Ensure Node.js is installed (check via `node -v`)
2. Set `VITE_API_ENDPOINT` in `.env` to the api URL.
3. In the react-app folder run the following commands in the terminal:
  ```{}
  npm install
  npm run dev
  ```

## Motivation
- Deepen my knowledge of C#
- Gain hands-on experience building Azure Function Apps
- Practice building modern UIs with React
- Combine learning with passion as a Minnesota Vikings fan! 🏈
