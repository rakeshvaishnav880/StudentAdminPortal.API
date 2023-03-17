using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortal.API.DataModels
{
    public class StudentAdminContext : DbContext
    {
        public StudentAdminContext(DbContextOptions<StudentAdminContext> options):base (options) 
        {

        }

        public DbSet<Student> Tbl_SA_Student { get; set; }

        public DbSet<Gender> Tbl_SA_Gender { get; set; }

        public DbSet<Address> Tbl_SA_Address { get; set; }
    }
}
