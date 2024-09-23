# Library Management System

## Project Description
This project is developed as an ASP.NET Core MVC application called Library Management System. This system provides a comprehensive platform for managing books and authors in a library. The project includes multiple models and pages, designed following Object-Oriented Programming (OOP) principles.

## Requirements

### Model Creation

#### Book Model
- **Id**: Unique identifier (int)
- **Title**: Book title (string)
- **AuthorId**: Author identifier (int, reference to Author model)
- **Genre**: Book genre (string)
- **PublishDate**: Publication date (DateTime)
- **ISBN**: ISBN number (string)
- **CopiesAvailable**: Number of copies available (int)

#### Author Model
- **Id**: Unique identifier (int)
- **FirstName**: Author's first name (string)
- **LastName**: Author's last name (string)
- **DateOfBirth**: Date of birth (DateTime)

#### User Model
- **Id**: Unique identifier (int)
- **FullName**: Member's full name (string)
- **Email**: Member's email address (string)
- **Password**: Member's login password (string)
- **PhoneNumber**: Member's phone number (string)
- **JoinDate**: Member's registration date (DateTime)

### ViewModel Creation
- A ViewModel to display book details and related information.
- A ViewModel for author details and for displaying authors on their respective pages.
- A ViewModel for registration and login pages.

### Controller Creation

#### BookController
- **List**: Displays a list of books.
- **Details**: Shows details of a specific book.
- **Create**: Provides a form to add a new book.
- **Edit**: Provides a form to edit an existing book.
- **Delete**: Provides a confirmation page to delete a book.

#### AuthorController
- **List**: Displays a list of authors.
- **Details**: Shows details of a specific author.
- **Create**: Provides a form to add a new author.
- **Edit**: Provides a form to edit an existing author.
- **Delete**: Provides a confirmation page to delete an author.

#### AuthController
- **SignUp**: Manages the registration process.
- **Login**: Manages the login process.

### View Creation

#### Book Views
- **List**: A view displaying a list of books, including a delete button.
- **Details**: A view displaying book details.
- **Create**: A view containing a form to add a new book.
- **Edit**: A view containing a form to edit existing books.

#### Author Views
- **List**: A view displaying a list of authors, including a delete button.
- **Details**: A view displaying author details.
- **Create**: A view containing a form to add a new author.
- **Edit**: A view containing a form to edit existing authors.

#### User Views
- **Sign Up**: A view for user registration that includes a password confirmation field and displays a warning if the passwords do not match.
- **Login**: A view for logging in using email and password. If the login fails, an error is displayed on the same page, or the user is redirected to the homepage.

## Program.cs Configuration
- **Adding MVC Services**: Set up MVC configurations.
- **Using wwwroot**: Ensure the use of static files in the wwwroot folder.
- **Routing Configuration**: Ensure requests are directed to the correct controller and action methods.
- **Default Routing**: Add a default routing configuration for the homepage.

## Installation
1. Clone or download this repository.
2. Open the project in Visual Studio or your preferred IDE.
3. Install the required NuGet packages.
4. Configure the database connection settings.
5. Start the application.

## Contributors
- Ceyda Yılmaz - Project Developer


