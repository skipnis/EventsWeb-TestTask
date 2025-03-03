using Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");
        
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("event_id")
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd();
        
        builder.Property(e => e.Title)
            .HasColumnName("event_title")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("event_title")
            .HasColumnType("text");

        builder.Property(e => e.ImageUrl)
            .HasColumnName("event_image_url")
            .HasColumnType("varchar(255)")
            .HasMaxLength(255);
        
        builder.Property(e=>e.MaximumParticipants)
            .HasColumnName("event_max_participants")
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(e=>e.Date)
            .HasColumnType("timestampz")
            .HasColumnName("event_date")
            .IsRequired();
        
        builder.OwnsMany(e => e.Categories, category =>
        {
            category.ToTable("EventCategories");
    
            category.WithOwner().HasForeignKey("event_id");

            category.Property(p => p.Name)
                .HasColumnName("category_name")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            category.HasKey("event_id", "category_name");
        });
        
        builder.OwnsMany(e => e.Places, place =>
        {
            place.ToTable("EventPlaces");
    
            place.WithOwner().HasForeignKey("event_id");

            place.Property(p => p.Name)
                .HasColumnName("place_name")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            place.Property(p => p.Address)
                .HasColumnName("place_address")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            place.HasKey("event_id", "place_name");
        });
    }
}