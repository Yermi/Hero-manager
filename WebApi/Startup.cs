using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Infrastructure;
using WebApi.Models;
using WebApi.Services;
using WebApi.Validators;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<Context>(c => 
            c.UseSqlServer(Configuration.GetConnectionString("HerosDatabase")));
            //services.AddDbContext<Context>(c => c.UseInMemoryDatabase("HerosDb"));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
            }));

            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                option.Filters.Add(typeof(CustomExceptionFilterAttribute));
                option.Filters.Add(typeof(InterceptionAttribute));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).AddFluentValidation();

            services.AddAuthentication("BasicAuthentication").
                        AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                        ("BasicAuthentication", null);

            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<IHeroService, HeroService>();
            services.AddScoped<ITrainService, TrainService>();
            services.AddTransient<IValidator<Trainer>, TrainerValidator>();
            services.AddTransient<IValidator<Hero>, HeroValidator>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseMvc();
        }
    }
}