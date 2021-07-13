using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sale.Startup))]
namespace Sale
{
    public partial class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
            //app.UseHttpsRedirection();
           
        }

        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(x => x
            //.AllowAnyOrigin()
            //.AllowAnyMethod()
            //.AllowAnyHeader());

            //app.UseHttpsRedirection();
            ConfigureAuth(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowOrigin", builder =>
            {
                builder.WithOrigins("http://localhost:44343",
                                    "http://localhost:4200"
                                    )
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            

            //services.AddControllers();
        }

    }
}
