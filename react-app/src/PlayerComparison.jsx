import ComparePanel from "./components/ComparePanel.jsx";
import StatsDisplay from "./components/StatsDisplay.jsx";
import Header from "./components/Header.jsx";
import { PlayerProvider } from "./contexts/PlayerDataStore.jsx";
import { Container } from "@mui/material";

function PlayerComparison() {
  return (
    <PlayerProvider>
      <div>
        <Container
          className="display-container"
          sx={{
            width: "80%",
          }}
        >
          <Header></Header>
          <ComparePanel></ComparePanel>
          <StatsDisplay></StatsDisplay>
        </Container>
      </div>
    </PlayerProvider>
  );
}

export default PlayerComparison;
