To run the HOVLaneViolation application, open VS Code and select the folder named HOVLaneViolation.
Server
.) Press Ctrl + ~ to open the terminal, then type cd api to switch to the ...\HOVLaneViolation\api folder.
.) Run dotnet build to check for errors, and if everything is fine, run dotnet run to start the API—you’ll see a URL like http://localhost:xxxx, which you can copy. 
.) In addition to running the api, it will also run the migrations and create the necessary tables and master data in the database.


Client
.) Open a new terminal tab, type cd client to switch to the client application ...\HOVLaneViolation\client folder, and run ng serve to launch the Angular UI.
.) Then, open your browser and go to http://localhost:xxxx to see the app live.

Database
.) To open the database, go to the searchbar at the top, and press the ">" key, and the first option should be "Open Database"
.) Click that option, and you should see a "SQLite Explorer" on the right hand side of the screen.
.) Click the dropdown menu, and you she be able to see all the tables in your database.
.) If you want, you can see the data in the tables by hovering to the right of the Table's name, and clicking the arrow button.




