HOVLaneViolation

To run the HOVLaneViolation application, open VS Code and select the folder named HOVLaneViolation.

---
Downloading the Project
1. Go to the GitHub repository page.  
2. Click Code → Download ZIP, then extract it, or run:
   git clone https://github.com/<your-username>/HOVLaneViolation.git
3. Open the extracted (or cloned) HOVLaneViolation folder in VS Code.
---
Server
1. Press Ctrl + ~ to open the terminal, then type:
   cd api
2. Run:
   dotnet build
   dotnet run
3. If everything builds successfully, you’ll see a URL like http://localhost:xxxx.  
   This starts the API and automatically runs migrations to create the necessary tables and master data in the database.
---
 Client
1. Open a new terminal tab, then type:
   cd client
2. Run:
   npm install
   ng serve
3. Open your browser and go to the URL shown in the terminal (usually http://localhost:4200) to see the app live.
---
Database
1. In VS Code, open the search bar at the top and press the ">" key.  
2. Select “Open Database”.  
3. On the right, you’ll see a SQLite Explorer tab.  
4. Click the dropdown menu to view all tables in the database.  
5. To view data in a table, hover to the right of its name and click the arrow button.
---
That’s it — the backend, frontend, and database should now be up and running!
