using TimViec.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TimViec.ViewModel;

namespace TimViec.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<applications> applications { get; set; }

        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Decentralize> Decentralizes { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Type_work> Type_Works { get; set; }
        public DbSet<StatusJob> StatusJobs { get; set; }
        public DbSet<TimViec.ViewModel.SearchViewModel> SearchViewModel { get; set; } = default!;
        public DbSet<TimViec.ViewModel.Details_CPN> Details_CPN { get; set; } = default!;
    }
}
