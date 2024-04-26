using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SimpleApiProject.Dto
{
    public record CreateAccDto([Required] string FirstName, [Required] string LastName, [Required] string address, [Required] string Email, 
        [Required] string phoneNumber);
    
}
