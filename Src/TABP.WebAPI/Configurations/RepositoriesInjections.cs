using TABP.Domain.Interfaces.Repositories;
using TABP.Infrastructure.Repositories;

namespace TABP.WebAPI.Configurations;

public static class RepositoriesInjections
{
    public static IServiceCollection AddAppRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAmenityRepository, AmenityRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IGalleryRepository, GalleryRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IHotelAmenityRepository, HotelAmenityRepository>();
        services.AddScoped<IHotelDiscountRepository, HotelDiscountRepository>();
        services.AddScoped<IHotelGalleryRepository, HotelGalleryRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IRoomAmenityRepository, RoomAmenityRepository>();
        services.AddScoped<IRoomCategoryRepository, RoomCategoryRepository>();
        services.AddScoped<IRoomCategoryDiscountRepository, RoomCategoryDiscountRepository>();
        services.AddScoped<IRoomGalleryRepository, RoomGalleryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserDiscountRepository, UserDiscountRepository>();

        return services;
    }
}

