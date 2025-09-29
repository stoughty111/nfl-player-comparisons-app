import SearchBar from "./SearchBar";
import CompareButton from "./CompareButton";
import { CircularProgress, colors, Grid2, Typography } from "@mui/material";
import { useState, useContext, useEffect } from "react";
import { fetchData } from "../ApiFetch.js";
import { PlayerContext } from "../contexts/PlayerDataStore.jsx";
import "../index.css";

export default function ComparePanel() {
  const initialMsg =
    "* Players must play one of the following positions: QB, RB, FB, WR, or TE";
  const serverErrorMsg =
    "* App was unable to connect to NFL data. Try again later.";
  const [player1, setPlayer1] = useState("");
  const [player2, setPlayer2] = useState("");
  const [error1, setError1] = useState(false);
  const [helperText1, setHelperText1] = useState("");
  const [error2, setError2] = useState(false);
  const [helperText2, setHelperText2] = useState("");
  const { data, setData } = useContext(PlayerContext);
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState(initialMsg);

  const handleChange1 = (event) => {
    setPlayer1(event.target.value);
  };

  const handleChange2 = (event) => {
    setPlayer2(event.target.value);
  };

  const handleValidate = () => {
    let isValid = true;
    const words1 = player1.trim().split(/\s+/);
    const words2 = player2.trim().split( /\s+/ );
    let msg = "* ";

    if (player1.trim() === "") {
      setError1(true);
      // setHelperText1( "Input cannot be empty" );
      msg += "Player 1: Input cannot be empty. ";
      isValid = false;
    } else if (words1.length < 2) {
      setError1(true);
      // setHelperText1( "Must provide players full name" );
      msg += "Player 1: Must provide players full name. ";
      isValid = false;
    } else {
      setError1(false);
      setHelperText1("");
    }
    if (player2.trim() === "") {
      setError2(true);
      // setHelperText2( "Input cannot be empty" );
      msg += "Player 2: Input cannot be empty.";
      isValid = false;
    } else if (words2.length < 2) {
      setError2(true);
      // setHelperText2( "Must provide players full name" );
      msg += "Player 2: Must provide players full name.";
      isValid = false;
    } else {
      setError2(false);
      setHelperText2("");
    }
    if ( !isValid )
    {
      setMessage( msg );
      setData(null);
    }
    return isValid;
  };

  const getData = async () => {
    setLoading(true);
    const result = await fetchData( player1.trim(), player2.trim() );
    setData( result );
    setLoading(false);
  };

  useEffect(() => {
    if (data === "error") {
      setMessage(serverErrorMsg);
    } else if (data && data.length === 1 && data[0].error !== null) {
      if ( data[0].error == "" )
      {
        setMessage( serverErrorMsg );
      } else
      {
        setMessage("* " + data[0].error);
      }
      setError1(true);
      setError2(true);
    } else if (data && data.length === 2) {
      setMessage("");
    } else {
      setMessage(initialMsg);
    }
  }, [data]);

  const onClick = async () => {
    if (handleValidate()) {
      await getData();
    }
  };

  return (
    <>
      <Grid2
        container
        spacing={7}
        alignItems="flex-start"
        justifyContent="center"
        sx={{ paddingTop: "var(--top-padding)" }}
      >
        <Grid2 visibility="hidden">
          <CompareButton />
        </Grid2>
        <Grid2>
          <SearchBar
            label="Player 1"
            value={player1}
            onChange={handleChange1}
            error={error1}
          />
        </Grid2>
        <Grid2>
          <SearchBar
            label="Player 2"
            value={player2}
            onChange={handleChange2}
            error={error2}
          />
        </Grid2>
        <Grid2>
          <CompareButton onClick={onClick} />
        </Grid2>
      </Grid2>
      {message !== "" && (
        <Grid2 container justifyContent="center">
          <Typography
            justifyContent="center"
            sx={{
              paddingTop: "10px",
              color:
                message === initialMsg ? "var(--bg-colour)" : "var(--nfl-red)",
            }}
          >
            {message}
          </Typography>
        </Grid2>
      )}
      {loading && (
        <Grid2
          container
          justifyContent="center"
          sx={{ paddingTop: "var(--top-padding)" }}
        >
          <CircularProgress sx={{ color: "var(--nfl-blue)" }} />
        </Grid2>
      )}
    </>
  );
}
