using TABP.Application.Services;
using TABP.Domain.Interfaces.Services;

namespace TABP.WebAPI.Configurations;

public static class ServicesInjections
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IAmenityService, AmenityService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IGalleryService, GalleryService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IHotelAmenityService, HotelAmenityService>();
        services.AddScoped<IHotelDiscountService, HotelDiscountService>();
        services.AddScoped<IHotelGalleryService, HotelGalleryService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IRoomAmenityService, RoomAmenityService>();
        services.AddScoped<IRoomCategoryService, RoomCategoryService>();
        services.AddScoped<IRoomCategoryDiscountService, RoomCategoryDiscountService>();
        services.AddScoped<IRoomGalleryService, RoomGalleryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserDiscountService, UserDiscountService>();

        return services;
    }
}

