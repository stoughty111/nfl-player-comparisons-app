import React from "react";
import { List, ListItem, ListItemText, Divider } from "@mui/material";

const PlayerStatsList = ({ items, listWidth }) => {
  return (
    <>
      <List sx={{ width: `${listWidth}`, bgcolor: "var(--bg-colour)", borderRadius: "10px", border: "3px solid var(--nfl-blue)"}}>
        {items.map((item, index) => (
          <React.Fragment key={index}>
            <ListItem>
              <ListItemText primary={item} align="center" />
            </ListItem>
            {index < items.length - 1 && (
              <Divider variant="middle" component="li" sx={{border: "1px solid var(--nfl-blue)", margin: 0}}/>
            )}
          </React.Fragment>
        ))}
      </List>
    </>
  );
};

export default PlayerStatsList;
