using tripath.Models;
using tripath.Repositories;
using tripath.Repositories.ExportJob;
using tripath.Services;
using Tripath_Logistics_BE.Repositories.ExportJob;

namespace tripath.Utils
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection AddRepositoryRegistration(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            //Registering Repositories and services
            services.AddSingleton<Data.DbContextClass>();
            services.AddScoped<ICustomerMasterRepository, CustomerMasterRepository>();
            services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<ICustomerDetailAddressRepository, CustomerDetailAddressRepository>();
            services.AddScoped<ICustomerContactRepository, CustomerContactRepository>();
            services.AddScoped<ICustomerServiceRepository, CustomerServiceRepository>();
            services.AddScoped<ICustomerShipperRepository, CustomerShipperRepository>();
            services.AddScoped<ICustomerConsigneeRepository, CustomerConsigneeRepository>();
            services.AddScoped<ICustomerCarrierRepository, CustomerCarrierRepository>();
            services.AddScoped<ICustomerIntegrationRepository, CustomerIntegrationRepository>();
            services.AddScoped<ICustomerRegRepository, CustomerRegRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserLogRepository, UserLogRepository>();
            services.AddScoped<IUserDataDetailsRepository, UserDataDetailsRepository>();
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExportJobRepository, ExportJobRepository>();
            //Confi
            services.AddSingleton<IFieldAliasService, FieldAliasService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplicationLogRepository, ApplicationLogRepository>();
           services.AddScoped<IExportEntityRepository, ExportEntityRepository>();

            services.AddScoped<IExporterRepository, ExporterRepository>();
            services.AddScoped<IExporterMasterRepository, ExporterMasterRepository>();


            //Email Service
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            services.AddScoped<ISmtpEmailService, SmtpEmailService>();


            return services;
        }
    }
}
