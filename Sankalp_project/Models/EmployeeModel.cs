using System.ComponentModel.DataAnnotations;

namespace Sankalp_project.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "Employee Name")]
       
        public string Name { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
    }
}
