using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Review;

namespace TABP.WebAPI.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/review")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IMapper _mapper;

    public ReviewController(IReviewService reviewService, IMapper mapper)
    {
        _reviewService = reviewService ?? throw new ArgumentException(nameof(reviewService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("my-reviews")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ReviewResponseDto>>> GetMyReviews()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized("User ID not found in token");

        var userId = int.Parse(userIdClaim);
        var userReviews = await _reviewService.GetByUserIdAsync(userId);
        var dtos = userReviews.Select(x => _mapper.Map<ReviewResponseDto>(x));

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ReviewResponseDto>> GetById(int id)
    {
        var review = await _reviewService.GetByIdAsync(id);

        var dto = _mapper.Map<ReviewResponseDto>(review);
        return Ok(dto);
    }

    [HttpGet("review-search")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<ReviewResponseDto>>> GetFiltered([FromQuery] ReviewFilterDto query)
    {
        var newQuery = _mapper.Map<ReviewFilter>(query);

        var filteredReviews = await _reviewService.GetAllAsync(newQuery);
        var dtos = filteredReviews
            .Select(x => _mapper.Map<ReviewResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ReviewResponseDto>> Create([FromBody] ReviewRequestDto review)
    {
        var newReview = _mapper.Map<Review>(review);

        var addedReview = await _reviewService.AddAsync(newReview);

        var dto = _mapper.Map<ReviewResponseDto>(addedReview);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Update(int id, [FromBody] ReviewRequestDto review)
    {
        var newReview = _mapper.Map<Review>(review);
        newReview.Id = id;

        await _reviewService.UpdateAsync(newReview);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _reviewService.DeleteAsync(id);
        return NoContent();
    }
}
