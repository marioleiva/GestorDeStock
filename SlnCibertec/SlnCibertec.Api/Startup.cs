using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SlnCibertec.Infra.Data;
using SlnCibertec.Core.Interfaces;
using SlnCibertec.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using SlnCibertec.Api.Hubs;

namespace SlnCibertec.Api
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
            // configurar el contexto de EF
            services.AddDbContext<ICibertecContext, CibertecContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CibertecConnection")));

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProductoService, ProductoService>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IProveedorService, ProveedorService>();
            services.AddTransient<INuevaVentaService, NuevaVentaService>();

            // configurar la autenticación con JWT
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerConfig =>
            {
                // solo para pruebas (en prod debería ser true)
                bearerConfig.RequireHttpsMetadata = false;
                bearerConfig.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("cibertec12345678")),
                    ValidIssuer = "Cibertec",
                    ValidAudience = "app-react",
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(10) // en producción debe ser un valor mayor (por defecto es 5 minutos)
                };
            });

            // configurar el policy para habilitar la autenticación a nivel global
            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder(new[] { JwtBearerDefaults.AuthenticationScheme }).RequireAuthenticatedUser().Build();
            });
            // agregar el servicio de CORS
            services.AddCors();

            // configurar el servicio de SignalR
            services.AddSignalR();

            services.AddMvc(config => config.Filters.Add(new AuthorizeFilter())).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // configurar CORS
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(host => true));

            // habilitar el servicio de autenticación en el app
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chathub", options => { options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets; });
                routes.MapHub<ProductHub>("/producthub", options => { options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets; });
            });
            app.UseMvc();
        }
    }
}
