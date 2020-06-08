using DaHo.M151.DataFormatValidator.Data;
using DaHo.M151.DataFormatValidator.Models.StorageModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DaHo.M151.DataFormatValidator.Abstractions.Helpers
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                if (!context.Users.Any())
                {
                    context.Users.AddRange
                    (
                        new User { Username = "admin", Password = "admin", Role = Models.ServiceModels.Role.Admin },
                        new User { Username = "user", Password = "user", Role = Models.ServiceModels.Role.User }
                    );
                }

                if (!context.DataSchemas.Any())
                {
                    context.DataSchemas.AddRange
                    (
                        new DataSchema { ForFormat = Models.DataFormat.JSON, SchemaName = "JSON schema 1", Schema = "" },
                        new DataSchema { ForFormat = Models.DataFormat.JSON, SchemaName = "JSON schema 2", Schema = "" },
                        new DataSchema { ForFormat = Models.DataFormat.XML, SchemaName = "XML schema 1", Schema = "" },
                        new DataSchema { ForFormat = Models.DataFormat.XML, SchemaName = "XML schema 2", Schema = "" },
                        new DataSchema { ForFormat = Models.DataFormat.YAML, SchemaName = "YAML schema 1", Schema = "" },
                        new DataSchema { ForFormat = Models.DataFormat.YAML, SchemaName = "YAML schema 2", Schema = "" }
                    );
                }

                context.SaveChanges();
            }

            return app;
        }
    }
}
