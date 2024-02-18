using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, HttpContext http, ApplicationDbContext context) 
    {
        var userId = http.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var categorySaved = context.Categories.FirstOrDefault(c => c.Id == id);

        if (categorySaved == null)
            return Results.NotFound();

        categorySaved.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);

        if(!categorySaved.IsValid)
            return Results.ValidationProblem(categorySaved.Notifications.ConvertToProblemDetails());

        context.SaveChanges();
        return Results.Ok();
    }
}
