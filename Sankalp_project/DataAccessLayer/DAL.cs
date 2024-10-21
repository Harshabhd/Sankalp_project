using Microsoft.EntityFrameworkCore;
using Sankalp_project.Models;

namespace Sankalp_project.DataAccessLayer
{
    public class DAL:DbContext
    {
        //public DAL(DbContextOptions<DAL>options):base(options) 
        //{
                
        //}
        public DAL(DbContextOptions<DAL>options):base(options) { }
        public DbSet<Login_Model> Login_Details { get; set; }
        public DbSet<CompanyModel> Company_Details { get; set; }
        public DbSet<EmployeeModel> Employee_Details { get; set; }
    }
}
