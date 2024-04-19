using Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    // Here in our ApplicationDbContext class we used to make it inherit from the DbContext class which is
    //   comes with the Microsoft.EntityFrameworkCore package, but as we are going to use the Authentication
    //   then we have to inherit the IdentityDbContext class which comes with the
    //   Microsoft.AspNetCore.Identity.EntityFrameworkCore package.
    // Also we have to know that we are going to use the Generic version of the IdentityDbcontext class which
    //   take the class that will be act as a user which will be Authenticated, So in our example here we used
    //   the employee class as a user. We also have to know that the user class(Employee here) must inherit
    //   from IdentityUser class.
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Team> Teams { get; set; }

    }
}
