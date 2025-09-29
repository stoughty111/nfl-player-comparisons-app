import * as React from "react";
import { TextField, Input } from "@mui/material";
import "../index.css"

function SearchBar({ label, value, onChange, error}) {
  return (
    <Input placeholder={label}      
    error={error}
    value={value}
    onChange={onChange}
    sx={{
      "&.MuiInput-root": {
        borderRadius: "10px",
        background: "var(--bg-colour)",
        padding: "4px 5px 4px",
        border: "3px solid var(--nfl-blue)",
        transition: 'border-color 0.15s linear',
        "&:hover": {
          borderColor: "var(--hover-blue)"
        },
        "&.Mui-focused": {
          borderColor: "var(--hover-blue)"
        },
        "&.Mui-error": {
          borderColor: "var(--nfl-red)"
        },
        "&.Mui-error:hover": {
          borderColor: "var(--hover-red)"
        },
        "&.Mui-error.Mui-focused": {
          borderColor: "var(--hover-red)"
        },
        input: {
          color: "black",
          opacity: "1"
        },
      },
        '&.MuiInput-underline:before': {
      borderBottom: 'none',
    },
    '&.MuiInput-underline:hover:not(.Mui-disabled):before': {
      borderBottom: 'none',
    },
    '&.MuiInput-underline:after': {
      borderBottom: 'none',
    },
    '&.Mui-error.MuiInput-underline:before': {
      borderBottom: 'none',
    },
    '&.Mui-error.MuiInput-underline:after': {
      borderBottom: 'none',
    },
      }
    }/>
  );
}

export default SearchBar;
