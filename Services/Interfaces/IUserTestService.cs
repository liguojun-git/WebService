using MCCMWebServiceApp.Domain.Entities;

namespace MCCMWebServiceApp.Services.Interfaces
{
    public interface IUserTestService
    {
        Task<UserTest> Authenticate(string username, string password);
    }
}