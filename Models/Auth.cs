using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Auth
    {
        public Auth()
        {
            JoinDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }

        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
    }
}
