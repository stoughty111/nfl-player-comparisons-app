export const fetchData = async (player1, player2) => {
  console.log(player1, player2);
  let data = null;
  try {
    const response = await fetch(import.meta.env.VITE_API_ENDPOINT, {
      method: "POST",
      body: JSON.stringify({
        player1: player1,
        player2: player2,
      }),
    });
    console.log("Response status:", response.status);
    console.log("Response headers:", response.headers);
    if (!response.ok) {
      var errorMsg = await response.text();
      data = [{ error: errorMsg }];
      console.log(`Error: ${data[0].error}`);
    } else {
      data = await response.json();
      console.log("Fetched data:", data);
    }
    return data;
  } catch (error) {
    console.error("Error fetching data:", error);
    return "error";
  }
};
