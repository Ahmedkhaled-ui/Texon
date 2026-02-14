using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Texon.Domin.Contracts;
using Texon.Domin.Entities.Auth;
using Texon.Domin.Entities.Products;
using Texon.Persistence.Context;

namespace Texon.Persistence.DBInitializers
{
    public class DBInitializer(TexonContext context , RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ILogger<DBInitializer> logger
        ) : IDBInitializer
    {
        public async Task Initialize()
        {
            await context.Database.MigrateAsync();


            if (!context.categories.Any())
            {
                var categoryJson = await File.ReadAllTextAsync(@"../Texon.Presestence/DataSeed/category.json");
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<List<Category>>(categoryJson, option);

                if (result != null && result.Any())
                {
                    await context.categories.AddRangeAsync(result);
                    await context.SaveChangesAsync();
                }
            }

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    
                    UserName = "admin",
                    Email = "admin$00@gmail.com"
                };

                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
