using Application.Dtos;
using Core.Enities;
using Mapster;

namespace Application.Mapping;

public class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Event, EventUpdateDto>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.MaximumParticipants, src => src.MaximumParticipants)
            .Map(dest => dest.Date, src => src.Date)
            .Map(dest => dest.Place, src => new PlaceDto { Name = src.Place.Name, Address = src.Place.Address })
            .Map(dest => dest.Category, src => new CategoryDto { Name = src.Category.Name });
        
        TypeAdapterConfig<EventUpdateDto, Event>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.MaximumParticipants, src => src.MaximumParticipants)
            .Map(dest => dest.Date, src => src.Date.ToUniversalTime())
            .Map(dest => dest.Place, src => new PlaceDto { Name = src.Place.Name, Address = src.Place.Address })
            .Map(dest => dest.Category, src => new CategoryDto { Name = src.Category.Name });

        TypeAdapterConfig<Event, EventPreviewDto>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.DateOnly, src => DateOnly.FromDateTime(src.Date))
            .Map(dest => dest.Place, src => src.Place.Name + ", " + src.Place.Address );

        TypeAdapterConfig<Event, EventFullDto>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Date, src => src.Date.ToString("yyyy-MM-dd HH:mm"))
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.Place, src => src.Place.Name + ", " + src.Place.Address )
            .Map(dest=>dest.MaximumParticipants, src => src.MaximumParticipants)
            .Map(dest => dest.Category, src => src.Category.Name);

        TypeAdapterConfig<EventCreationDto, Event>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.MaximumParticipants, src => src.MaximumParticipants)
            .Map(dest => dest.Date, src => src.Date.ToUniversalTime())
            .Map(dest => dest.Place, src => new PlaceDto { Name = src.Place.Name, Address = src.Place.Address })
            .Map(dest => dest.Category, src => new CategoryDto { Name = src.Category.Name });

        TypeAdapterConfig<User, UserProfileDto>.NewConfig()
            .Map(dest => dest.FullName, src => src.FirstName + " " + src.LastName)
            .Map(dest => dest.Email, src => src.Email);
        
        TypeAdapterConfig<UserRegistrationDto, User>.NewConfig()
            .Map(dest => dest.UserName, src=>src.UserName)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PasswordHash, src => src.Password)
            .Map(dest => dest.BirthDate, src => src.BirthDate);
        
        TypeAdapterConfig<UserUpdateDto, User>.NewConfig()
            .Map(dest => dest.Email, src => src.Email);
    }
}
