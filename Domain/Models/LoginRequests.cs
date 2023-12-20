using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class LoginRequests
    {
        [Key]
        public int Id { get; set; } // Exempel: antar att det finns ett unikt numeriskt ID för inloggning

        public string Username { get; set; }
        public string Password { get; set; }
    }
}