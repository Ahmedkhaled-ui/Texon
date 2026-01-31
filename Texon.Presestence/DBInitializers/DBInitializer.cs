using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Texon.Domin.Contracts;
using Texon.Domin.Entities.Products;
using Texon.Persistence.Context;

namespace Texon.Persistence.DBInitializers
{
    public class DBInitializer(TexonContext context) : IDBInitializer
    {
        public void Initialize()
        {
            context.Database.Migrate();


            if(!context.categories.Any()) 
            {
              var   categoryJson= File.ReadAllText(@"../Texon.Presestence/DataSeed/category.json");
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<List<Category>>(categoryJson, option);

                if(result != null && result.Any())
                {
                    context.categories.AddRange(result);
                    context.SaveChanges();
                }
            }


        }
    }
}
