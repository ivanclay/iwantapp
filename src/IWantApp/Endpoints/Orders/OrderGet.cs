using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data.Entity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Orders;

public class OrderGet
{
    public static string Template => "/orders/order";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    
    [Authorize(Roles ="Client, Employee")]
    public static async Task<IResult> Action(Guid orderId, ApplicationDbContext context) 
    {
        var orderDetails = context.Orders.Include(o => o.Products).Where(o => o.Id == orderId).ToList();

        return Results.Ok(orderDetails);
    }
}
