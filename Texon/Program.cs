using Texon.Domin.Contracts;
using Texon.Persistence.Attachment;
using Texon.Persistence.DependencyInjection;
using Texon.Persistence.Repository;
using Texon.Persistence.UnitofWork;
using Texon.Service.Abstraction.IService;
using Texon.Service.MappingProfile;
using Texon.Service.Service;

namespace Texon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddScoped<IUnitofWork, UnitofWork>();

            builder.Services.AddScoped<IproductService, ProductService>();
            builder.Services.AddScoped<IcategoryService, categoryService>();

            builder.Services.AddScoped<IAttachmentService, AttachmentService>();


            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new ProductsProfile(builder.Configuration));
                cfg.AddProfile(new CategoryProfile());

            });

           ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            #region Seed
            using var scope = app.Services.CreateScope();
            var Initializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
            Initializer.Initialize();
            #endregion
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
