using BusinessLogic;
using BusinessLogic.Logics;
using BusinessLogicInterface;
using DataAccess.Context;
using DataAccess.Repositories;
using DataAccessInterface.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SessionInterface;

namespace Factory
{
    public class ServiceFactory
    {
        private readonly IServiceCollection services;
        public ServiceFactory(IServiceCollection services)
        {
            this.services = services;
        }
        public void AddCustomServices()
        {
            services.AddScoped<RepositoryMaster>();
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<ICheckRepository, CheckRepository>();
            services.AddScoped<IElementRepository, ElementRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionUserRepository, SessionUserRepository>();
            services.AddScoped<IApartmentLogic, ApartmentLogic>();
            services.AddScoped<ICheckLogic, CheckLogic>();
            services.AddScoped<IElementLogic, ElementLogic>();
            services.AddScoped<IPhotoLogic, PhotoLogic>();
            services.AddScoped<IRentalLogic, RentalLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<ISessionLogic, SessionLogic>();

        }
        public void AddDbContextService()
        {
            services.AddDbContext<DbContext, SgiContext>();
        }
    }
}