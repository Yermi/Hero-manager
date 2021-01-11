# Hero-manager

Instructions for running home tsak


Pre requirements for running the project:

.Net Core 3.1 - for runing server

Node,Js, Angular cli - for runing client.

SqlServer

======

Server is written in .Net

Run server:
After cloning the project - just navigate to "**webApi**" folder and run the command ```dotnet run```,
or open it in vs code and run the project (Ctr+F5).
the web api is available in address http://localhost:5000.


Client is written with Angular 8.

Run client:
Navigate to "**MyHeroCompany**" folder
1. fetch all depndencies by running ```npm install```.
2. run the command ```ng serve``` and the app is available in address http://localhost:4200.


Db - Sql Server + Entity Framework Core as data access layer:
Make sure to change Connection string in ```appsettings.json``` file
1. run the folowing command ```dotnet ef migrations add InitialCreate```
2. run the command ```dotnet ef database update```
