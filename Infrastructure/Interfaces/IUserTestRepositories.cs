using MCCMWebServiceApp.Domain.Entities;

namespace MCCMWebServiceApp.Infrastructure.Interfaces;

public interface IUserTestRepositories
{
    Task<UserTest> AuthenticateUser(string username, string password);
}
