using System.ComponentModel.DataAnnotations;

namespace SimpleApiProject.Dto
{
    public record DepositDto([Required] string AccNum, [Required] decimal Amount);
}
