using System;
using MCCMWebServiceApp.Infrastructure.SqlContext;
using MCCMWebServiceApp.Domain.Entities;
using MCCMWebServiceApp.Infrastructure.Interfaces;

namespace MCCMWebServiceApp.Infrastructure.Repositories;

public class UserTestRepositories : IUserTestRepositories
{
    DapperHelper db = new DapperHelper();

    private readonly DapperHelper _dapperHelper;

    public UserTestRepositories(DapperHelper dapperHelper)
    {
        _dapperHelper = dapperHelper;
    }


    public async Task<UserTest> AuthenticateUser(string username, string password)
    {
        string sql = @"SELECT id, username, email, create_date
                           FROM shop.dbo.UserTest 
                           WHERE username = @userName AND password = @passWord";

        var user = db.QueryFirstOrDefault<UserTest>(sql, new { username, password });
        return user;

        
    }
}

