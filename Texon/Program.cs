using E_Commerce.infrastructure.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.Tasks;
using Texon.Domin.Contracts;
using Texon.infrastructure.Service;
using Texon.Persistence.DependencyInjection;
using Texon.Persistence.UnitofWork;
using Texon.Service.Abstraction.IService;
using Texon.Service.MappingProfile;
using Texon.Service.Service;

namespace Texon
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddInfrastrucctureService(builder.Configuration);
            builder.Services.AddScoped<IUnitofWork, UnitofWork>();
            builder.Services.AddScoped<IAnaliytics, Analiytics>();


            builder.Services.AddScoped<IproductService, ProductService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IcategoryService, categoryService>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<IDeliveryMethodService, DeliveryMethodService>();



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()   // مسموح لأي موقع يكلم الـ API
                          .AllowAnyMethod()   // مسموح بكل العمليات (GET, POST, etc.)
                          .AllowAnyHeader();  // مسموح بكل الهيدرز
                                              // ❌ شيلنا AllowCredentials عشان AnyOrigin تشتغل
                });
            });

            #region AutoMapper
            builder.Services.AddAutoMapper(cfg =>
           {
               cfg.AddProfile(new ProductsProfile(builder.Configuration));
               cfg.AddProfile(new CategoryProfile());
               cfg.AddProfile(new BaketProfile());
               cfg.AddProfile(new DeliveryMethodProfile());
               cfg.AddProfile(new OrderProfile(builder.Configuration));



           }); 
            #endregion

            ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("JWT",
                    new OpenApiSecurityScheme
                    {
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "JWT"
                }
            },
            new string[] { }
        }
    });
            });
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                var jwt = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true ,
                    ValidateAudience = true ,
                    ValidateIssuer = true ,
                    ValidateIssuerSigningKey = true ,
                    

                    ValidIssuer = jwt.issure,
                    ValidAudience = jwt.audience,
                    IssuerSigningKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                };
            });

            var app = builder.Build();

            #region Seed
            using var scope = app.Services.CreateScope();
            var Initializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
           await Initializer.Initialize();
            #endregion
    
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                    options.EnableFilter();
                    options.EnablePersistAuthorization();
                    options.DisplayRequestDuration();
                });
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization();

            app.MapControllers();

            app.Run();
        }
    }
}
