using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        // Simulated list of users for authentication purposes.
        static List<Auth> _auths = new List<Auth>()
        {
            new Auth{Id = 1, Email=".", Password = "."}
        };

        // Dependency Injection is used here to inject services into the controller.
        // IDataProtector is injected to handle encryption/decryption of sensitive data like passwords.
        private readonly IDataProtector _dataProtector;

        // Constructor where IDataProtectionProvider is provided to create a protector instance for security purposes.
        public AuthController(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }

        // GET: SignUp method to return the view for user sign-up.
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: SignUp method handles form submissions for new user registration.
        [HttpPost]
        public IActionResult SignUp(Auth formData)
        {
            // Check if the form data is valid based on the model's validation attributes.
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            // Check if the user already exists by matching email.
            var user = _auths.FirstOrDefault(x => x.Email.ToLower() == formData.Email.ToLower());
            if (user is not null)
            {
                // If user exists, display an error message and return the form.
                ViewBag.Error = "User is available";
                return View(formData);
            }

            // If the user doesn't exist, create a new user with encrypted (protected) password.
            var newUser = new Auth()
            {
                Id = _auths.Max(x => x.Id) + 1, // Assign the next available ID.
                Email = formData.Email.ToLower(), // Store email in lowercase for uniformity.
                Password = _dataProtector.Protect(formData.Password) // Encrypt the password.
            };

            _auths.Add(newUser); // Add the new user to the list.

            // Redirect to the SignIn page after successful registration.
            return RedirectToAction("SignIn", "Auth");
        }

        // GET: SignIn method to return the sign-in view.
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        // POST: SignIn method handles login requests with email and password.
        [HttpPost]
        public async Task<IActionResult> SignIn(Auth formData)
        {
            // Check if the user exists in the list by matching email.
            var user = _auths.FirstOrDefault(x => x.Email.ToLower() == formData.Email.ToLower());

            if (user is null)
            {
                // If no user is found, display an error message.
                ViewBag.Error = "Username or password is incorrect";
                return View(formData);
            }

            // Decrypt (unprotect) the stored password to compare with the entered one.
            var rawPassword = _dataProtector.Unprotect(user.Password);

            // Check if the decrypted password matches the entered password.
            if (rawPassword == formData.Password)
            {
                // If password matches, create claims for the user session.
                var claims = new List<Claim>();

                claims.Add(new Claim("email", user.Email)); // Add email claim.
                claims.Add(new Claim("id", user.Id.ToString())); // Add ID claim.

                // Create a ClaimsIdentity using the claims for the session.
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Define authentication properties like session expiration.
                var autProperties = new AuthenticationProperties
                {
                    AllowRefresh = true, // Allow session refresh.
                    ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48)) // Session lasts for 48 hours.
                };

                // Sign in the user with the claims and session properties using cookie authentication.
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);

                // The 'await' keyword is used here because this is an asynchronous method, meaning it runs in the background and waits for the operation to complete without blocking the main thread.
            }
            else
            {
                // If password doesn't match, display an error message.
                ViewBag.Error = "Username or password is incorrect";
                return View(formData);
            }

            // Redirect to the book list page after a successful login.
            return RedirectToAction("List", "Book");
        }

        // Method to handle user sign-out.
        public async Task<IActionResult> SignOut()
        {
            // Sign out the user from the current session.
            await HttpContext.SignOutAsync();

            // Redirect to the book list page after sign-out.
            return RedirectToAction("List", "Book");
        }
    }
}
