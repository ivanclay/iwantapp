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
    public static async Task<IResult> Action(int? page, int? rows, string? orderby, ApplicationDbContext context) 
    {
        if (page == null) page = 1;
        if (rows == null) rows = 10;
        if (string.IsNullOrEmpty(orderby)) orderby = "name";

        var queryBase = context.Products
            .Include(p => p.Category)
            .Where(p => p.HasStock && p.Category.Active);

        var filterQuery = queryBase;

        if (orderby.Equals("name"))
            filterQuery = filterQuery.OrderBy(p => p.Name);
        else
            filterQuery = filterQuery.OrderBy(p => p.Price);
        
        filterQuery = filterQuery.Skip((page.Value - 1) * rows.Value).Take(rows.Value);

        var products = filterQuery.ToList();

        var results = products
            .Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Price, p.Active));
        return Results.Ok(results);
    }
}
