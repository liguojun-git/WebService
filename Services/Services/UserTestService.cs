using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCCMWebServiceApp.Domain.Entities;
using MCCMWebServiceApp.Infrastructure.Interfaces;
using MCCMWebServiceApp.Infrastructure.Repositories;
using MCCMWebServiceApp.Services.Interfaces;

namespace MCCMWebServiceApp.Services.Services;

public class UserTestService : IUserTestService
{
    private readonly IUserTestRepositories _UserTestRepositories;

    public UserTestService(IUserTestRepositories userTestRepositories)
    {
        _UserTestRepositories = userTestRepositories;
    }
    public async Task<UserTest> Authenticate(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return null;

        var user = await _UserTestRepositories.AuthenticateUser(username, password);

        return user;
    }
}

