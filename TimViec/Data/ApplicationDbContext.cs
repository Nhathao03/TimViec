using TimViec.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TimViec.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<application> applications { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Decentralize> Decentralizes { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Type_work> Type_Works { get; set; }

        public DbSet<StatusJob> StatusJobs { get; set; }
    }
}
