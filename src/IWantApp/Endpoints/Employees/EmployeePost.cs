﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager) 
    {
        var userAdminId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value; 

        var newUser = new IdentityUser {  UserName = employeeRequest.email, Email = employeeRequest.email };
        var result = await userManager.CreateAsync(newUser, employeeRequest.password);

        if(!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetail());

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.employeeCode),
            new Claim("Name", employeeRequest.name),
            new Claim("CreatedBy", userAdminId)
        };

        var claimResult = await userManager.AddClaimsAsync(newUser, userClaims);

        if(!claimResult.Succeeded)
            return Results.BadRequest(result.Errors.First());

        return Results.Created($"/employees/{newUser.Id}", newUser.Id);
    }
}
