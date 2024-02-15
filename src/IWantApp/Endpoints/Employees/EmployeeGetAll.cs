using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, IConfiguration configuration) 
    {
        var db = new SqlConnection(configuration["Database:SqlServerConnection"]);
        var query = @"select Email, ClaimValue as Name
                from AspNetUsers u inner join
                AspNetUserClaims c 
                on u.id = c.UserId and c.claimType = 'Name' order by name
                OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
        var employees = db.Query<EmployeeResponse>(query, new { page, rows });

        //var users = userManager.Users.Skip((page -1) * rows).Take(rows).ToList();
        //var employees = new List<EmployeeResponse>();
        //foreach (var item in users)
        //{
        //    var claims = userManager.GetClaimsAsync(item).Result;
        //    var claimName = claims.FirstOrDefault(c => c.Type == "Name");
        //    var userName = claimName != null ? claimName.Value : string.Empty;
        //    employees.Add(new EmployeeResponse(item.Email, userName));
        //}
        
        return Results.Ok(employees);
    }
}
