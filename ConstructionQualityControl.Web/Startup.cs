using AutoMapper;
using ConstructionQualityControl.Data;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Web.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace ConstructionQualityControl.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QualityControlContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration.GetConnectionString("ConstructionQualityConnection"));
            });

            services.AddCors();

            services.AddAuthentication(opt => opt.DefaultAuthenticateScheme = "JwtBearer").AddJwtBearer("JwtBearer", jwtOpt =>
            {
                jwtOpt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTAuthenticationManager.Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = Environment.IsProduction()
                };
            });

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<ICryptographer, Cryptographer>(s => new Cryptographer(Configuration["Key"]));
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

            JWTAuthenticationManager.Initialize(Environment);

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers()); 
        }
    }
}
