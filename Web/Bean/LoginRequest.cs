using MCCMWebServiceApp.Web.Bean;

namespace Web.Bean;

public class LoginRequest: BaseRequest
{
    public string username { get; set; }
    public string password { get; set; }
}

