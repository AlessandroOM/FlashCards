using flashcards.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace flashcards.utils
{
    public class StackMap : IEntityTypeConfiguration<Stacks>
    {
        public void Configure(EntityTypeBuilder<Stacks> builder)
        {
            builder.ToTable("STACKS");
            builder.HasKey(m=> m.Id);
            builder.HasIndex(m => m.Name);
            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}