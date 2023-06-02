using flashcards.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace flashcards.utils
{
    public class StudyMap : IEntityTypeConfiguration<Study>
    {

        public void Configure(EntityTypeBuilder<Study> builder)
        {
            builder.ToTable("STUDIES");
            builder.HasKey(m=> m.Id);
            builder.HasIndex(m => m.StackId);
            builder.Property(m => m.StackId)
                   .IsRequired();
            
            builder.Property(m => m.FlashCardId) 
                   .IsRequired();
       
            builder.Property(m => m.date) 
                   .IsRequired(); 

            builder.Property(m => m.HaveSucess) 
                   .IsRequired();       

            builder.Property(m => m.Response)
                    .HasMaxLength(50);                   
            
        }
    }
}