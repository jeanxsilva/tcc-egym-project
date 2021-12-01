using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eGYM.Models;
using eGYM.Core;
using System.Reflection;
using eGYM.GraphQL;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace eGYM
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
            var serverVersion = new MariaDbServerVersion(new Version(10, 4, 18));
            services.AddDbContext<EGymDbContext>(options => options.UseLazyLoadingProxies().UseMySql(Configuration.GetConnectionString("DefaultConnection"), serverVersion)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .ConfigureWarnings(warning =>
                    warning.Default(WarningBehavior.Ignore)
                        .Ignore(CoreEventId.LazyLoadOnDisposedContextWarning)
                        .Throw(RelationalEventId.BoolWithDefaultWarning)));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ClaimResolver>();

            services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectAPI", Version = "v1" });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                });

            var graphql = services.AddGraphQLServer()
                .AddType<Query>()
                .AddAuthorization()
                .AddFiltering()
                .AddSorting().AddProjections()
                .AddType<Company>();

            services.AddHostedService<ManageInvoiceRoutine>();

            Type[] nestedTypes = Assembly.GetAssembly(typeof(Query)).GetTypes();
            List<Type> extensionTypes = nestedTypes.Where(r =>
            {
                if (r.CustomAttributes != null)
                {
                    return r.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name.Equals("ExtendObjectTypeAttribute")) != null;
                }

                return false;
            }).ToList();

            foreach (Type type in extensionTypes)
            {
                graphql.AddTypeExtension(type);
            }

            foreach (Type type in nestedTypes)
            {
                foreach (Type interfaceType in type.GetInterfaces())
                {
                    if (interfaceType.Name.Equals("IEntityBase"))
                    {
                        graphql.AddType(type);
                    }
                }
            }

            Type[] types = Assembly.GetAssembly(typeof(Startup)).GetTypes();
            foreach (Type type in types)
            {
                if (type.Name.EndsWith("Repository") || type.Name.EndsWith("Service"))
                {

                    services.AddScoped(type);
                }
            }

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromSeconds(1),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Settings.SecretByte),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policyBuilder => policyBuilder
                    .WithOrigins("http://localhost:4200", "http://localhost:5000", "http://localhost:8100", "http://192.168.1.10:8100", "https://tcc-egym-project.vercel.app/")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapControllers();
            });
        }
    }
}