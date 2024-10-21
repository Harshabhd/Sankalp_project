using System.ComponentModel.DataAnnotations;

namespace Sankalp_project.Models
{
    public class Login_Model
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
