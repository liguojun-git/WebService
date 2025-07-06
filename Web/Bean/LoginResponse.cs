namespace Web.Bean;

public class LoginResponse : BaseResponse
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreateDate { get; set; }
}

