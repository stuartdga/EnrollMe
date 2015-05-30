# EnrollMe
This solution demonstrates a common pattern for creating .NET Web API's. 

The basic design is a business/data layer with a Web API facade that accesses the business/data layer.

The underlying database for this solution is SQL Server.  The database name is EnrollMe.  Create your own database using SQLExpress.  You can then create all of the necessary database objects by running the EnrollMe.edmx.sql script located in the EnrollMeDB project.

Each project also includes test projects with automated tests.  These tests will access the database.
