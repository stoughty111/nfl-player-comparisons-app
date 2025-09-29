import { Grid2, Typography } from "@mui/material";
import "../index.css";
import nflLogo from "../assets/images/nfl-logo.svg";

export default function Header() {
  return (
    <>
      <div className="nfl-logo-container">
        <img src={nflLogo} className="nfl-logo" />
      </div>
      <Grid2
        container
        justifyContent="center"
        alignContent="center"
      >
        <Grid2 item="true">
          <Typography
            variant="h1"
            sx={{ fontSize: "3.5em", fontWeight: "400", color: "var(--bg-colour)" }}
          >
            NFL Player Comparisons
          </Typography>
        </Grid2>
      </Grid2>
    </>
  );
}
