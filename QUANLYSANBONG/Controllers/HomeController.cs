using Microsoft.AspNetCore.Mvc;

namespace QUANLYSANBONG.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Trả về view Trang Chủ (Client) cho mọi người dùng
        return View();
    }
}
