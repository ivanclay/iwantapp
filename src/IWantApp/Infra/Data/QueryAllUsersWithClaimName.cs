﻿using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Execute(int page, int rows) 
    {
        var db = new SqlConnection(configuration["Database:SqlServerConnection"]);
        var query = @"select Email, ClaimValue as Name
                from AspNetUsers u inner join
                AspNetUserClaims c 
                on u.id = c.UserId and c.claimType = 'Name' order by name
                OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        return db.Query<EmployeeResponse>(query, new { page, rows });
    }
}
