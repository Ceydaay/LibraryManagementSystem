using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    [Route("book")]
    public class BookController : Controller
    {
        // This method populates a dropdown with authors' full names and IDs for use in book creation/editing forms.
        public void AuthorIdConnect()
        {
            // Creating a list of SelectListItem objects with author names and IDs.
            List<SelectListItem> values = (from x in AuthorController._authors.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.FullName,  // Display the full name of the author
                                               Value = x.Id.ToString()  // Store the author's ID as the value
                                           }).ToList();

            // Pass the list to the View using ViewBag for rendering in a dropdown list.
            ViewBag.Authors = values;
        }

        // Static list of books to simulate a database, pre-populated with sample books.
        static List<Book> _books = new List<Book>()
        {
            new Book{Id= 1,
                Title = "Madonna in a fur Coat",
                AuthorId = 1,
                Genre = "Love",
                PublishDate = new DateTime(1943,1,1),
                ISBN = "321321",
                CopiesAvailable = 234 },

            new Book{Id= 2,
                Title = "Lord of the Rings",
                AuthorId = 2,
                Genre = "Fantasy Fiction",
                PublishDate = new DateTime(1954,7,26),
                ISBN = "321321",
                CopiesAvailable = 8787 },

            new Book{Id= 3,
                Title = "Harry Potter",
                AuthorId = 3,
                Genre = "Fantasy Fiction",
                PublishDate = new DateTime(1997,1,1),
                ISBN = "321321",
                CopiesAvailable = 67567 },
        };

        // Display the list of books that are not marked as deleted.      
        [Route("liste")]
        public IActionResult List()
        {
            var books = _books.Where(x => x.IsDeleted == false).ToList();
            return View(books);
        }

        // Display detailed information about a book by its ID.
        [Route("{stringURL?}/{id}")]
        //.com/denemece-askjdhasjdas/
        public IActionResult Details(int id)
        {
            // Create a new view model for passing book data to the view.
            BookViewModel model = new BookViewModel();

            // Find the book by its ID.
            var book = _books.Find(x => x.Id == id);

            // Use ViewBag to pass the author's full name to the view.
            ViewBag.authorfname = AuthorController._authors.Where(x => x.Id == book.AuthorId).First().FullName;

            // Populate the view model with the book's details.
            model.Title = book.Title;
            model.Genre = book.Genre;
            model.PublishDate = book.PublishDate;
            model.ISBN = book.ISBN;
            model.CopiesAvailable = book.CopiesAvailable;

            // Return the book details view.
            return View(model);
        }

        // GET: Display the form to create a new book.
        [HttpGet]
        public IActionResult Create()
        {
            AuthorIdConnect();  // Populate the author dropdown.
            return View();
        }

        // POST: Handle form submission for creating a new book.
        [HttpPost]
        public IActionResult Create(BookViewModel formData)
        {
            // If the form data is invalid, re-render the view with the current data and author dropdown.
            if (!ModelState.IsValid)
            {
                AuthorIdConnect();
                return View(formData);
            }

            // Find the maximum current book ID to assign a unique ID to the new book.
            int maxId = _books.Max(x => x.Id);

            // Create a new book object and populate it with the form data.
            var newBook = new Book()
            {
                Id = maxId + 1,
                Title = formData.Title,
                Genre = formData.Genre,
                PublishDate = formData.PublishDate,
                ISBN = formData.ISBN,
                CopiesAvailable = formData.CopiesAvailable,
            };

            // Add the new book to the list.
            _books.Add(newBook);

            // Redirect back to the book list after successful creation.
            return RedirectToAction("List");
        }

        // GET: Display the form to edit an existing book by ID.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AuthorIdConnect();  // Populate the author dropdown.

            // Find the book by its ID.
            var book = _books.Find(x => x.Id == id);

            // Create a view model and populate it with the book's current data.
            var viewModel = new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                PublishDate = book.PublishDate,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable,
            };

            // Render the edit view with the current book data.
            return View(viewModel);
        }

        // POST: Handle form submission for editing an existing book.
        [HttpPost]
        public IActionResult Edit(BookViewModel formData)
        {
            // If the form data is invalid, re-render the view with the current data and author dropdown.
            if (!ModelState.IsValid)
            {
                AuthorIdConnect();
                return View(formData);
            }

            // Find the book by its ID and update its properties with the form data.
            var book = _books.Find(x => x.Id == formData.Id);
            book.Title = formData.Title;
            book.Genre = formData.Genre;
            book.PublishDate = formData.PublishDate;
            book.ISBN = formData.ISBN;
            book.CopiesAvailable = formData.CopiesAvailable;

            // Redirect back to the book list after successful edit.
            return RedirectToAction("List");
        }

        // POST: Handle book deletion by marking it as deleted.
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Find the book by its ID and mark it as deleted.
            var book = _books.Find(x => x.Id == id);
            book.IsDeleted = true;

            // Redirect back to the book list.
            return RedirectToAction("List");
        }
    }
}
