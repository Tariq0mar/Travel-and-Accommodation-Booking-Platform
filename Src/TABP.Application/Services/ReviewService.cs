using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IValidator<Review> _reviewValidator;

    public ReviewService(
        IReviewRepository reviewRepository,
        IValidator<Review> reviewValidator)
    {
        _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        _reviewValidator = reviewValidator ?? throw new ArgumentNullException(nameof(reviewValidator));
    }

    public async Task<Review> GetByIdAsync(int id)
    {
        var review = await _reviewRepository.GetByIdAsync(id);

        if (review is null)
        {
            throw new NotFoundException($"Review with Id = {id} not found.");
        }

        return review;
    }

    public async Task<IEnumerable<Review>> GetAllAsync(ReviewFilter queryFilter)
    {
        return await _reviewRepository.GetAllAsync(queryFilter);
    }

    public async Task<Review> AddAsync(Review review)
    {
        var validation = await _reviewValidator.ValidateAsync(review);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid review: {errors}");
        }

        var addedReview = await _reviewRepository.AddAsync(review);

        if (addedReview is null)
        {
            throw new CreationException($"Review could not be created.");
        }

        await _reviewRepository.SaveChangesAsync();

        return addedReview;
    }

    public async Task UpdateAsync(Review review)
    {
        var validation = await _reviewValidator.ValidateAsync(review);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid review update: {errors}");
        }

        var success = await _reviewRepository.UpdateAsync(review);
        if (!success)
        {
            throw new NotFoundException($"Review with Id = {review.Id} not found.");
        }

        await _reviewRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _reviewRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"Review with Id = {id} not found.");
        }

        await _reviewRepository.SaveChangesAsync();
    }
}
