using flashcards.Models;
using flashcards.utils;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class MyContext : DbContext
{
    public DbSet<Stacks> Stacks { get; set; }
    public DbSet<FlashCardDb> FlashCardDbx { get; set; }
    public DbSet<Study> Study { get; set; }

    public DbSet<vw_LeStacks> ConsultaSql {get; set;}

    
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=FlashCardDB.db;");
        optionsBuilder.EnableSensitiveDataLogging(true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stacks>(new StackMap().Configure);
        modelBuilder.Entity<FlashCardDb>(new FlashCardDbMap().Configure);
        modelBuilder.Entity<Study>(new StudyMap().Configure);

        modelBuilder.Entity<Stacks>()
                        .HasMany(f => f.FlashCards)
                        .WithOne(s => s.Stack)
                        .HasForeignKey(f => f.StackId);
         
        modelBuilder.Entity<FlashCardDb>()
                     .HasOne(f => f.Stack)
                     .WithMany(s => s.FlashCards)
                     .HasForeignKey(f => f.StackId);
                     ;

        modelBuilder.Entity<Study>() 
                     .HasOne(f => f.Stack)
                     .WithMany(s => s.Studies)
                     .HasForeignKey(f => f.StackId)
                     ;
        
        modelBuilder.Entity<Study>()
                     .HasOne(f => f.FlashCard)
                     .WithMany(s => s.Studies)
                     .HasForeignKey(f => f.FlashCardId)
                     ;

        modelBuilder.Entity<vw_LeStacks>()
                    .HasNoKey();

    }
    
}
