namespace QUANLYSANBONG.Filters;

public class AdminAuthorizeAttribute : SessionAuthorizeAttribute
{
    public AdminAuthorizeAttribute() : base("Admin")
    {
    }
}
