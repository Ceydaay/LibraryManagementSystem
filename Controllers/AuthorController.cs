using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorController : Controller
    {
        // Simulated in-memory list of authors, initialized with some sample data.
        public static List<Author> _authors = new List<Author>()
        {
            new Author{
                Id= 1,
                FirstName = "Sabahattin",
                LastName = "Ali",
                DateOfBirth = new DateTime(1907,02,25)
            },
            new Author{
                Id= 2,
                FirstName = "Joanne Kathleen",
                LastName = "Rowling",
                DateOfBirth = new DateTime(1907,02,25)
            },
            new Author{
                Id= 3,
                FirstName = "John Ronald Reuel",
                LastName = "Tolkien",
                DateOfBirth = new DateTime(1892,01,03)
            }
        };

        // Displays the list of authors who are not marked as deleted.
        public IActionResult List()
        {
            var authors = _authors.Where(x => x.IsDeleted == false).ToList();
            return View(authors);
        }

        // Displays details for a specific author by ID.
        public IActionResult Details(int id)
        {
            var author = _authors.Find(x => x.Id == id);
            return View(author);
        }

        // GET: Displays the form to create a new author.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Handles the form submission to create a new author.
        [HttpPost]
        public IActionResult Create(AuthorViewModel formData)
        {
            // If the form data is invalid, return to the view with validation errors.
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Find the maximum current ID to assign a unique ID to the new author.
            var maxId = _authors.Max(x => x.Id);

            // Create a new author object and populate it with form data.
            var newAuthor = new Author()
            {
                Id = maxId + 1,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                DateOfBirth = formData.DateOfBirth
            };

            // Add the new author to the list.
            _authors.Add(newAuthor);

            // Redirect back to the list of authors after successful creation.
            return RedirectToAction("List");
        }

        // GET: Displays the form to edit an existing author by ID.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Find the author to be edited.
            var author = _authors.Find(x => x.Id == id);

            // Prepare a view model with the author's current data.
            var viewModel = new AuthorViewModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth
            };

            // Display the edit view with the author's data.
            return View(viewModel);
        }

        // POST: Handles the form submission to edit an existing author.

        [HttpPost]
        public IActionResult Edit(AuthorViewModel formData)
        {
            // If the form data is invalid, return to the view with validation errors.
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            // Find the author by ID to update their data.
            var author = _authors.Find(x => x.Id == formData.Id);

            // Update the author's properties with the new data from the form.
            if(author != null)
            {
                author.FirstName = formData.FirstName;
                author.LastName = formData.LastName;
                author.DateOfBirth = formData.DateOfBirth;
            }
           

            // Redirect back to the list of authors after successful edit.
            return RedirectToAction("List");
        }

        // POST: Handles the deletion of an author by marking them as deleted.
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Find the author to be deleted.
            var author = _authors.Find(x => x.Id == id);

            // Mark the author as deleted without removing them from the list.
            author.IsDeleted = true;

            // Redirect back to the list of authors.
            return RedirectToAction("List");
        }
    }
}
