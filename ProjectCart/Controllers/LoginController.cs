using Microsoft.AspNetCore.Mvc;
using ProjectCart.Models;


namespace ProjectCart.Controllers
{
    public class LoginController : Controller
    {
        private static List<User> users = new List<User>();

        public IActionResult Register() => View("~/Views/Home/Register.cshtml");
        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Password = Models.User.HashPassword(user.Password);
            users.Add(user);
            return RedirectToAction("Login");
        }

        public IActionResult Login() => View("~/Views/Home/Login.cshtml");
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            string hashedPassword = Models.User.HashPassword(password);
            var user = users.FirstOrDefault(u => u.Email == email && u.Password == hashedPassword);
            if (user == null) return View();
            HttpContext.Session.SetString("User", user.Email);
            return RedirectToAction("Index", "Home");
        }
    }
}
