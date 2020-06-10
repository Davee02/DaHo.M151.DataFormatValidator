# DataFormatValidator

The DataFormatValidator is a web application that allows validation and checking schemas of various data formats (JSON, XML, YAML, etc.). Users have the ability to define their own schemas for a specific format. Additionally, users can convert one format to another.
The functionality is exposed via a REST API. Additionally, a frontend can be used for a user friendlier way to interact with the data.

## User groups

The application consists of 2 user groups:

- **Regular users**: Have the ability to validate the format of a snippet of a document (i.e. JSON or XML), validate a snippet against an existing schema and convert on format to another (i.e. JSON -> XML)
- **Admin users**: Can do everything a _Regular user_ can do. Furthermore, they have the rights to add, edit and delete schemas

## User Stories

1. As an regular user, I want convert my YAML document to a XML document.
2. As an regular user, I want to ensure that my JSON document has a valid structure.
3. As an regular user, I want to check my XML document against a predefined schema. If validation errors occur, I want to know them.
4. As an regular user, I want to execute the above actions with simple HTTP calls using a REST API and with an easy to use web-frontend.
5. As an admin user, I want to do the same things as an regular user.
6. As an admin user, I want to add, edit and delete schemas for the various formats.

## Technologies

The following technologies are intended to be used:

- [Angular](https://angular.io/) for the frontend
- [ASP.NET Core web API](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1) for the REST-backend
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) for the ORM
- [PostgreSQL](https://www.postgresql.org/) for the database
- [Docker](https://www.docker.com/) and [Docker Compose](https://docs.docker.com/compose/) for containerization
