using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Venda
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationUrl = "https://localhost:6000";

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authenticationUrl;
                    options.RequireHttpsMetadata = true;
                    options.ApiName = "vendaapi";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }

    //public class Startup
    //{
    //    // This method gets called by the runtime. Use this method to add services to the container.
    //    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        services.AddMvcCore()
    //            .AddAuthorization()
    //            .AddJsonFormatters();

    //        services.AddAuthentication("Bearer")
    //            .AddJwtBearer("Bearer", options =>
    //            {
    //                options.Authority = "http://localhost:6000";
    //                options.RequireHttpsMetadata = false;

    //                options.Audience = "api1";
    //            });
    //    }

    //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //    {
    //        app.UseAuthentication();
    //        app.UseMvc();
    //    }
    //}
}
