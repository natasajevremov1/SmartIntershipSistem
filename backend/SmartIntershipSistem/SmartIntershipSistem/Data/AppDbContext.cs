using Microsoft.EntityFrameworkCore;
using SmartIntershipSistem.Models;

namespace SmartIntershipSistem.Data
{
    public class AppDbContext:DbContext
    {
        //konstruktor sa DbContextOptions-omogucava da se konfiguracija
        //ubaci spolja(dependency injection)
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //kroz dbsetove u kodu radi sve operacije(add,read,..) sa bazom
        public DbSet<User>Users { get; set; }
        public DbSet<Application>Applications { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateSkill> CandidatesSkills{ get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job>Jobs { get; set; }
        public DbSet<JobSkill> JobSkills{ get; set; }
        public DbSet<Skill> Skills { get; set; }

        //join tabele nemaju svoj id i njih id treba da bude kobinacija id zajedno
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateSkill>()
                .HasKey(cs => new {cs.CandidateId,cs.SkillId});

            modelBuilder.Entity<JobSkill>()
                .HasKey(cs=> new {cs.JobId,cs.SkillId});

            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Candidate>(c => c.UserId);

            modelBuilder.Entity<Company>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Company>(c => c.UserId);
        }

    }
}
