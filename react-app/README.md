# Introduction
This is a static web application that allows users to compare NFL player statistics for the 2024 season

**WARNING**: This is only the static web app! To achieve app functionality, you must download and set up the API locally as well (information on how to do this below).

Website with API functionality:

https://github.com/user-attachments/assets/226b1942-72cf-49c3-b36b-331a0954f6ce


# Run Locally
## Software Dependencies
- [Node.js](https://nodejs.org/en/): Run `node -v` in your terminal to check if it's been installed properly

## Steps to Run Locally
1. Ensure all software dependencies are installed
2. Clone [this repository](https://dev.azure.com/valuepartnersinvestments/Co-Op%20Projects/_git/Fantasy_App?path=%2F&version=GBmain&_a=contents) and follow the README instructions for setting up the API locally
3. Locate the .env file and set VITE_API_ENDPOINT to the locally running APIs URI (e.g., http://localhost:7272/api/PlayerComparer)
4. Open a terminal in the football-app-frontend directory and run the following:
-  **npm install** to download all the packages
- **npm run dev** to start the react app
5. Select the local link (e.g., http://localhost:5173/) in the terminal to open the app in your browser
6. Start comparing players


## Troubleshoot
If you encounter a package dependency problem, run the following command before resuming at Step 4:

```bash
rm -rf node_modules package-lock.json pnpm-lock.yaml yarn.lock
```
