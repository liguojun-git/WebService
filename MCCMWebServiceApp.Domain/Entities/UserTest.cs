using System.ComponentModel.DataAnnotations.Schema;

namespace MCCMWebServiceApp.Domain.Entities;

public class UserTest
{
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; } 
    public string email { get; set; }
    public DateTime createDate { get; set; }
}

