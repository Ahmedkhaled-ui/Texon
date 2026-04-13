using Microsoft.AspNetCore.Identity;
using Texon.Domin.Contracts;
using Texon.Domin.Entities.Auth;
using Texon.Domin.Entities.Order;
using Texon.Service.Abstraction.IService;
using Texon.Shared.AnaliyticsVeiwModel;

namespace Texon.Service.Service
{
    public class Analiytics(IUnitofWork unitofWork , UserManager<ApplicationUser> userManager ) : IAnaliytics
    {
        public async Task<AnaliyticsVeiwModel> GetAnaliyticsData()
        {
            var spec = new CountSpecification<Product>();
            var count = await unitofWork.GetRepository<Product, int>().CountAsync(spec);

            var specOrder = new CountSpecification<Order>();
            var countOrder = await unitofWork.GetRepository<Order, Guid>().CountAsync(specOrder);
            return new AnaliyticsVeiwModel
            {
              TotalProduct = count,
              TotalOrders= countOrder,
              User = userManager.Users.Count(),

            };  
            
          }
    }
}
