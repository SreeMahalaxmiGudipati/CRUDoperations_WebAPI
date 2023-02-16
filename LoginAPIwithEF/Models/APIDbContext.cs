using Microsoft.EntityFrameworkCore;

//acts as middleware layer between actual entity framework model and physical db
//after migration what should be their inside the physical db
//establishing database connection to sqldb
namespace LoginAPIwithEF.Models
{
    public class APIDbContext:DbContext
    {
        public APIDbContext(DbContextOptions option):base(option)
        {

        }
        //entity mapping is required to create table in db in model class
        public DbSet<Student> Students { get; set; }
    }
}
