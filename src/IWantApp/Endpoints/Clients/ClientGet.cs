using IWantApp.Domain.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace IWantApp.Endpoints.Clients;

public class ClientGet
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    //[AllowAnonymous]
    public static async Task<IResult> Action(HttpContext http) 
    {
        var user = http.User;
        var result = new
        {
            Id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
            Nome = user.Claims.First(c => c.Type == "Name").Value,
            Cpf = user.Claims.First(c => c.Type == "CPF").Value,
        };

        return Results.Ok(result);
    }
}
