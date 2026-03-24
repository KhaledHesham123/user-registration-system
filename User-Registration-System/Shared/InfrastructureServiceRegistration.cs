using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User_Registration_System.Data.DBContexts;
using User_Registration_System.Features.UserFeatures.CQRS.Commands.CreateUser;
using User_Registration_System.Shared.Behaviors;
using User_Registration_System.Shared.Interfaces;
using User_Registration_System.Shared.Repositories;
using User_Registration_System.Shared.UnitOfWork;

namespace User_Registration_System.Shared
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.DBContexts.ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(TransactionBehavior<,>));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitofWork>();
            return services;


        }
    } 
}
