using Microsoft.AspNetCore.Mvc;
using ProjectCart.Data;  // Import DbContext
using ProjectCart.Models;  // Import Models
using System.Diagnostics;


namespace ProjectCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;  // Thêm dòng này

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)  // Inject DbContext
        {
            _logger = logger;
            _context = context;  // Gán _context để sử dụng
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList(); // Lấy dữ liệu từ database
            return View(products); // Truyền danh sách sản phẩm vào View
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
