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

                if (context.Users.Count() > 0)
                {
                    return app;
                }

                context.Users.AddRange
                (
                    new User { Id = 1, Username = "admin", Password = "admin", Role = Models.ServiceModels.Role.Admin },
                    new User { Id = 2, Username = "user", Password = "user", Role = Models.ServiceModels.Role.User }
                );

                context.SaveChanges();
            }

            return app;
        }
    }
}
