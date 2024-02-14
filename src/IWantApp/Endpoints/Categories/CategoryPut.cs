using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, ApplicationDbContext context) 
    {
        var categorySaved = context.Categories.FirstOrDefault(c => c.Id == id);
        if (categorySaved != null)
        {
            categorySaved.Name = categoryRequest.Name;
            categorySaved.Active = categoryRequest.Active;
            categorySaved.EditedOn = DateTime.UtcNow;
            categorySaved.EditedBy = "Test_edit";
        }

        context.SaveChanges();

        return Results.Ok();
    }
}
