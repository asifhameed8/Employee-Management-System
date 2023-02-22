using AutoMapper;
using EMS.Core.Context;
using EMS.Core.Factory;
using EMS.Core.Repositories;
using EMS.Models.Entities;
using EMS.Service.EmplyeeSrv;
using EMS.Service.Uow;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace EMS.Service
{
    public static class ServicesModule
    {
        public static void Register(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            services.AddDbContext<DatabaseContext>();
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddScoped<IContextFactory, ContextFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }
}