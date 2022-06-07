
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using quntrixAPI.Data;
namespace quntrixAPI
{
   
    public class AppDbContext : IdentityDbContext
    {
        private static IConfigurationRoot _configuration;

        public DbSet<User> users { get; set; }
        

        

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                _configuration = builder.Build();
                var cnstr = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(cnstr);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>()
            .HasIndex(a => a.UserName)
            .IsUnique();

            // Many-to-many: Session <-> Attendee
            modelBuilder.Entity<SessionAttendee>()
                .HasKey(ca => new { ca.SessionId, ca.AttendeeId });

            // Many-to-many: Speaker <-> Session
            modelBuilder.Entity<SessionSpeaker>()
                .HasKey(ss => new { ss.SessionId, ss.SpeakerId });
        }

        public DbSet<Session> Sessions => Set<Session>();

        public DbSet<Track> Tracks => Set<Track>();

        public DbSet<Speaker> Speakers => Set<Speaker>();

        public DbSet<Attendee> Attendees => Set<Attendee>();
    }
}