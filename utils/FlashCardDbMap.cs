using flashcards.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace flashcards.utils
{
    public class FlashCardDbMap : IEntityTypeConfiguration<FlashCardDb>
    {
        public void Configure(EntityTypeBuilder<FlashCardDb> builder)
        {
            builder.ToTable("FLASHCARDS");
            builder.HasKey(m=> m.Id);
            builder.HasIndex(m => m.Expression);
            builder.Property(m => m.Expression)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(m => m.OppositeExpression)
                    .HasMaxLength(50);                   

            
            builder.Property(m => m.StackId) 
                   .IsRequired();  

        
                 
            
        }
    }
}