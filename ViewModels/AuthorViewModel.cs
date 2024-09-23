using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="sdfsdfsfsdf")] 
        public string FirstName { get; set; }
        [Required(ErrorMessage = "sdfsdfsfsdf")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "sdfsdfsfsdf")]
        public DateTime DateOfBirth { get; set; }
        public bool IsDeleted { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
