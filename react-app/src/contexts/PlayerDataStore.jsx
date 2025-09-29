import React, { createContext, useState } from "react";

export const PlayerContext = createContext();

export const PlayerProvider = ({ children }) => {
  const [data, setData] = useState(null);

  return (
    <PlayerContext.Provider value={{ data, setData }}>
      {children}
    </PlayerContext.Provider>
  );
};
