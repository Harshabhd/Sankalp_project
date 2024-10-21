using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Sankalp_project.Models
{
    public class CompanyModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Company Address")]
        public string CompanyAddress { get; set;}
        [Required]
        [Display(Name ="Company Number")]
        
        public string CompanyNumber { get; set;}
        [Required]
        public string Email { get; set;}
        [Required]
        public string Password { get; set;}
        [Compare("Password")]
        [Required]
        public string ComfirmPassword { get; set;}

    }
}
