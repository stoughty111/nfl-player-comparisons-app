import { Button } from "@mui/material";
import { blue } from "@mui/material/colors";

function CompareButton({ onClick }) {
  return (
    <Button
      variant="contained"
      onClick={onClick}
      sx={{
        backgroundColor: "var(--nfl-blue)",
        borderRadius: "7px",
        "&:hover": {
          backgroundColor: "var(--hover-blue)"
        }
      }}
    >
      Compare
    </Button>
  );
}
export default CompareButton;
