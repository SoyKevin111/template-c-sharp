using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using templatebase.src.User.Infraestructure.Adapters.Out.Entities;
using templatebase.src.User.Infraestructure.configuration;

namespace templatebase
{
    public class AplicationDbContext : IdentityDbContext<UserEntity>
    {

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }

        public DbSet<UserEntity> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}