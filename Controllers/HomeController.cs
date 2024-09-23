using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        // This action method handles the root URL ("/") of the application.
        public IActionResult Index()
        {
            // Redirect the user to the SignUp page of the AuthController.
            // When the root page is accessed, it will redirect to the SignUp action in the AuthController.
            return RedirectToAction("SignUp", "Auth");
        }

        // This action method renders the About page.
        public IActionResult About()
        {
            // Simply return the About view when this action is called.
            return View();
        }
    }
}
