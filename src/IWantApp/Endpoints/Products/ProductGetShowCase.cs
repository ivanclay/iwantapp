using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace IWantApp.Endpoints.Products;

public class ProductGetShowCase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ApplicationDbContext context, int page = 1, int rows = 10, string orderby = "name") 
    {
        if (rows > 10)
            return Results.Problem(title: "Row with max 10", statusCode: 400);

        var queryBase = context.Products
            .Include(p => p.Category)
            .Where(p => p.HasStock && p.Category.Active);

        var filterQuery = queryBase;

        if (orderby.Equals("name"))
            filterQuery = filterQuery.OrderBy(p => p.Name);
        else if (orderby.Equals("price"))
            filterQuery = filterQuery.OrderBy(p => p.Price);
        else
            return Results.Problem(title: "Order only by name or price", statusCode: 400);

        filterQuery = filterQuery.Skip((page - 1) * rows).Take(rows);

        var products = filterQuery.ToList();

        var results = products
            .Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Price, p.Active));
        return Results.Ok(results);
    }
}
