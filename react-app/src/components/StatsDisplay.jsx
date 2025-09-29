import { Grid2, ThemeProvider, createTheme } from "@mui/material";
import PlayerStatsList from "./PlayerStatsList";
import { Typography } from "@mui/material";
import { useContext} from "react";
import { PlayerContext } from "../contexts/PlayerDataStore";
import "../index.css";

const playerListWidth = "200px";

function camelToTitleCase(camelCaseString) {
  if (typeof camelCaseString == "string"){
    const words = camelCaseString.replace(/([A-Z])/g, ' $1').split(' ');
    const titleCasedWords = words.map(word => word.charAt(0).toUpperCase() + word.slice(1).toLowerCase());
    return titleCasedWords.join(' ')
  }
  return null;
}

function reorderDisplayElements(playerStats) {
  const last12Elements = playerStats.slice(-12);
  const remainingElements = playerStats.slice(0,-12).sort();
  return last12Elements.concat(remainingElements);
}

const theme = createTheme({
  typography: {
    h6: {
      fontSize: "1.5rem", // Adjust the font size as needed
      width: "100%",
      textAlign: "center",
      color: "var(--bg-colour)"
    },
  },
});

export default function StatsDisplay({}) {
  const { data } = useContext(PlayerContext);

  var statsList = ["No data"];
  var player1Stats = ["No data"];
  var player2Stats = ["No data"];
  var player1Name = "No data";
  var player2Name = "No data";

  if (
    data &&
    data.length === 2 &&
    Object.keys(data[0]).length !== 0 &&
    Object.keys(data[1]).length !== 0 &&
    data[0].name !== null &&
    data[1].name !== null
  ) {
    player1Name = data[0].name;
    player2Name = data[1].name;

    const dataCopy = JSON.parse(JSON.stringify(data));
    const removeAttributes = (player, attributes) => {
      attributes.forEach((attr) => delete player[attr]);
      return player;
    };
    const attributesToRemove = ["name", "playerID"];
    const updatedPlayers = dataCopy.map((player) =>
      removeAttributes(player, attributesToRemove)
    );

    statsList = Object.keys(updatedPlayers[0]);
    statsList = statsList.map(camelToTitleCase);
    statsList = reorderDisplayElements(statsList);
    player1Stats = Object.values(updatedPlayers[0]);
    player1Stats = reorderDisplayElements(player1Stats);
    player2Stats = Object.values(updatedPlayers[1]);
    player2Stats = reorderDisplayElements(player2Stats);
  }

  return (
    <>
      <Grid2
        id="stats-container"
        container
        spacing={7}
        alignItems="center"
        justifyContent="center"
        sx={{ gap: "10px", paddingTop: "var(--top-padding)" }}
        visibility={data ? "visible" : "hidden"}
      >
        <Grid2 item="true" xs={3}>
          <ThemeProvider theme={theme}>
            <Typography variant="h6">{player1Name}</Typography>
          </ThemeProvider>
          <PlayerStatsList
            items={player1Stats}
            listWidth={playerListWidth}
          ></PlayerStatsList>
        </Grid2>
        <Grid2 item="true">
        <ThemeProvider theme={theme}>
            <Typography variant="h6" sx={{visibility: "hidden"}}>{player1Name}</Typography>
          </ThemeProvider>
          <PlayerStatsList
            id="statsList"
            items={statsList}
            listWidth="100%"
          ></PlayerStatsList>
        </Grid2>
        <Grid2 item="true" xs={3}>
        <ThemeProvider theme={theme}>
            <Typography variant="h6">{player2Name}</Typography>
          </ThemeProvider>
          <PlayerStatsList
            items={player2Stats}
            listWidth={playerListWidth}
          ></PlayerStatsList>
        </Grid2>
      </Grid2>
    </>
  );
}
