using IWantApp.Domain.Products;

namespace IWantApp.Endpoints.Orders;

public record OrderResponse(List<Product> Products, DateTime CreatedOn, Guid OrderId);
