# DataFormatValidator

The DataFormatValidator is a web application that allows validation and checking schemas of various data formats (JSON, XML, YAML, etc.). Users have the ability to define their own schemas for a specific format. Additionally, users can convert one format to another.
The functionality is exposed via a REST API. Additionally, a simple web frontend can be used for a user friendlier way to interact with the data.

## Technologies

The following technologies were used to create this project:

- [Angular](https://angular.io/) for the frontend
- [ASP.NET Core web API](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1) for the REST-backend
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) for the ORM
- [PostgreSQL](https://www.postgresql.org/) for the database
- [Docker](https://www.docker.com/) and [Docker Compose](https://docs.docker.com/compose/) for containerization

## API specs

The API specs are generated automatically by the backend. To view them, ensure that you started all the Docker containers (take a look at the `How to run` section in the README). Then navigate to <http://localhost:5000> to see the UI for the API specs. If the raw swagger.json file is desired, navigate to <http://localhost:5000/swagger/v1/swagger.json>.

## Look back

### Challenges

I enjoyed carrying out this project from start to finish. Although I already knew most of the parts used for it (ASP.Net Core, Angular, Entity Framework, Migrations, Docker) beforehand, using them all together in a single application was still challenging to me. One completely new thing for me was using a cache, namely redis. I had the problem with forgetting to invalidate or update the cache. This meant, that I could edit or delete a schema through the API, but when I listed all schemas, the updated or deleted one was still there. It took me like half an hour to realize that the problem was neither in the database nor my controller, but in my repository where I manage the cache.
Additionally, because I really don't like designing UIs and working with JavaScript/TypeScript, creating the frontend was difficult to me. I decided that I want to concentrate more on the backend and therefore produce more beautiful code. That's why the frontend didn't turn out so visually appealing :).

### Lessons learned

1) Using a cache **correctly** is not easy
2) I don't want to be a web developer (at least not for the frontend)
3) Automatically generating Swagger specs is a really nice feature that we could even use at work
4) I really like developing in the whole "ASP.Net Core - EntityFramework - Dependency Injection - C#" environment
