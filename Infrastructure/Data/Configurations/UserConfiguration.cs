using Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .HasColumnName("user_id")
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd();
        
        builder.Property(u=>u.UserName)
            .HasColumnName("user_name")
            .HasColumnType("varchar(255)")
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(u => u.FirstName)
            .HasColumnName("first_name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .HasColumnType("varchar(100)")
            .IsRequired();
        
        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(255)")
            .IsRequired();
        
        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .HasColumnType("varchar(255)")
            .IsRequired();
        
        builder.Property(u=>u.BirthDate)
            .HasColumnName("birth_date")
            .HasColumnType("date")
            .IsRequired();
    }
}