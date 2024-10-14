using Microsoft.AspNetCore.Mvc;
using Talabalt.APIS.Errors;
using Talabalt.APIS.Helpers;
using Talabat.Core.RepositoryInterfaces;
using Talabat.Repository.Data;

namespace Talabalt.APIS.Extensions
{
    public static class AppExtensions
    {

        public static IServiceCollection applicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(Profiles));
            services.Configure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = (actionContext) =>
            {
                var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0).SelectMany(p => p.Value.Errors).Select(p => p.ErrorMessage).ToList();

                var response = new ApiValidationResponse
                {
                    Error = errors
                };
                return new BadRequestObjectResult(response);
            });

            return services;

        }

        public static WebApplication addSwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }


    }
}
