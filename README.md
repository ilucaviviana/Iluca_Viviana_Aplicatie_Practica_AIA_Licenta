# Ilucă_Viviana_Aplicatie_Practica_AIA_Licenta

A payment application for the expenses of a building create with Angular, .NET Core and SQL.

Installation Guid: Clone the repository on your own computer.

For back-end:
   1. Downloade SSMS and import the file PayBlockDataBase
   2. Open the file API.sln with Visual Studio and restore the NuGet Dependencies and the connection with data base from appsettings.json
      "ConnectionStrings": {
       "UsersAppCon": "Data Source=your sorce;Initial Catalog=your database; Integrated Security=true"
      },
   4. Run the application

For front-end:
   1. Open the folder PayBlock using Visual Studio Code
   2. Restore the dependencies using the comand npm install in a new terminal
   3. Run the application with the command npm start or ng serve --open
