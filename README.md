Employee-Directory
==================

* Employee directory built on Angular JS, Bootstrap, and Kendo UI
* Token Based Authentication using ASP.NET Web API 2, Owin and Identity 2
* Entity Framework 6 at database level

How to Run It
-------------

In order to run this project, make sure you have VS2013 installed and MSSQL 2012. Project is already configured to run with your local IIS -- make sure is intalled too --. Database will be created automatically using Entity Framework (EF) Model-First -- once you do at last one request --.

How to Generate 30K Records
---------------------------

* A SQL file was provided into the `Database` folder. 
* This file was generated using Mockarro Realistic Test Data Generator `http://www.mockaroo.com/92231220`.
* After running the application at least once and database was successfully created, run the SQL script file using MSSQL Management Studio or any other tool in order to properly generate all employees with their respective access.

Requirements For Build 1
------------------------

* Search over 30K employees
* HR (Human Resource) and Employee Roles
* At least to include the following information: Name, Job Title, Location, Phone, Number, Email
* Use email for authentication
