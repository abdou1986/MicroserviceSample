using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Infrastucture.Mail;
using Ordering.Infrastucture.Persistence;
using Ordering.Infrastucture.Repositories;


namespace Ordering.Infrastucture
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

            //Using typeof it is for MediatR
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();


            //here we use AddTransient because we need a one instance when we lanch the application
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
