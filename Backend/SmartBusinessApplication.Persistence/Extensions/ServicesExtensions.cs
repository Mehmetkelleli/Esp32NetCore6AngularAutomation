using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartBusinessApplication.Application.Abstract;
using SmartBusinessApplication.Application.AutoMapperProfile;
using SmartBusinessApplication.Persistence.Concrete;
using SmartBusinessApplication.Persistence.Data.Context;

namespace SmartBusinessApplication.Persistence.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection Services)
        {
            //Databse Added
            Services.AddDbContext<DataContext>(options => options.UseSqlServer("workstation id=Databasextr.mssql.somee.com;packet size=4096;user id=Pckopatxtr_SQLLogin_1;pwd=u9wycnnr27;data source=Databasextr.mssql.somee.com;persist security info=False;initial catalog=Databasextr"));
            //Add Dependency Class
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IClientRepository, ClientRepository>();
            Services.AddAutoMapper(typeof(ClientProfile));
            
        }
    }
}
