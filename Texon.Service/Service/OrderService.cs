using AutoMapper;
using E_Commerce.Shared;
using Texon.Domin.Contracts;
using Texon.Domin.Entities.DeliveryMethod;
using Texon.Domin.Entities.Order;
using Texon.Service.Abstraction.Common;
using Texon.Service.Abstraction.IService;
using Texon.Service.specfications;
using Texon.Shared.AddressDTo;
using Texon.Shared.OrderDto;
using Texon.Shared.ProductDto;

namespace Texon.Service.Service
{
    public class OrderService(IUnitofWork unitofWork, IMapper mapper
        , IBasketRepository basketRepository 
         ) : IOrderService
    {
        public async Task<Result<OrderDto>> CreateOrderAsync(string Email, int deliveryMethodId, string basketId, AddressDto shippingAddress)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);
            if (basket == null)
                return Error.NotFound("Basket Not Found", "Basket Not Found");


            var DeliveryMethod = await unitofWork.GetRepository<DeliveryMethods, int>().GetByIdAsync(deliveryMethodId);

            if (DeliveryMethod == null)
                return Error.NotFound("DeliveryMethod Not Found", "DeliveryMethod Not Found");


            #region get order items
            var productRepo = unitofWork.GetRepository<Product, int>();
            var orderItems = new List<OrderItem>();
            var Ids = basket.BasketItems.Select(i => i.Id).ToList();

            var products = (await productRepo.GetAllAsync(new GetProductByIdsSpec(Ids)))
                .ToDictionary(x => x.Id);

            var ValidationErrors = new List<Error>();

            foreach (var item in basket.BasketItems)
            {
                if (!products.TryGetValue(item.Id, out Product? product))
                {
                    ValidationErrors.Add(Error.Validation("ProductNotFound", $"Product with Id {item.Id} not found"));
                    continue;
                }

                if (product.StockQuantity < item.Quantity)
                {
                    ValidationErrors.Add(Error.Validation("LowStock", $"الكمية المطلوبة من {product.NameAr} غير متوفرة. المتاح: {product.StockQuantity}"));
                    continue;
                }

                product.StockQuantity -= item.Quantity;
                unitofWork.GetRepository<Product, int>().update(product);
                var orderItem = new OrderItem
                {
                    Price = product.Price, 
                    Quantity = item.Quantity,
                    photo = product.PhotoUrl,
                    ProductName = product.NameAr,
                    ProductId = product.Id,
                };
                orderItems.Add(orderItem);
            }

            if (ValidationErrors.Any())
                return ValidationErrors;
            #endregion

            var subtotal = orderItems.Sum(i => i.Price * i.Quantity);

            var address = mapper.Map<OrderAddress>(shippingAddress);

            #region CreateOrder
            var order = new Order
            {
                
                UserEmail = Email,
                OrderItems = orderItems,
                SubTotal = subtotal,
                Address = address,
                DeliveryMethodID= deliveryMethodId,
                ShippingCost = DeliveryMethod.Cost



            };

            await unitofWork.GetRepository<Order, Guid>().AddAsync(order);

            var result = await unitofWork.SaveChangesAsync();

            if (result <= 0)
                return Error.Failure("OrderCreationFailed", "حدث خطأ أثناء حفظ الأوردر");

            // امسح السلة فقط بعد التأكد من الحفظ
            await basketRepository.DeleteBasketAsync(basketId);

            return mapper.Map<OrderDto>(order);
            #endregion
        }

        public async Task<PagenatedResult<OrderDto>> GetAllOrdersAsync(string lang, OrderQuary productQuary)
        {
            var spec = new OrderWithItemsSpecification(productQuary);

            var products = await unitofWork.GetRepository<Order, Guid>().GetAllAsync();
            if (!products.Any())
                return null;


            var totalCount = await unitofWork.GetRepository<Order, Guid>().CountAsync(new OrderCountSpecfication(productQuary));

            var result = mapper.Map<IEnumerable<OrderDto>>(products, opt => opt.Items["lang"] = lang);

            return new(productQuary.pageIndex, result.Count(), totalCount, result);
        }

        public async Task<IEnumerable<DeliveryMethods>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethod = await unitofWork.GetRepository<DeliveryMethods, int>().GetAllAsync();
                return mapper.Map<IEnumerable<DeliveryMethods>>(DeliveryMethod);


       }

        public async Task<Result<OrderDto>?> GetOrderByIdAsync(Guid id, string UserEmail)
        {
            var order = await unitofWork.GetRepository<Order, Guid>().GetAsync(new OrderWithIdAndEmailSpecfications(id, UserEmail));
            if (order == null)
                return Error.NotFound("Order Not Found", "Order Not Found");

            return Result<OrderDto>.Ok(mapper.Map<OrderDto>(order));

        }

       
    }
}
