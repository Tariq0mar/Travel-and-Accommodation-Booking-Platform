using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Booking;

namespace TABP.WebAPI.Controllers.CRUD;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/booking")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public BookingController(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService ?? throw new ArgumentException(nameof(bookingService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingResponseDto>> GetById(int id)
    {
        var booking = await _bookingService.GetByIdAsync(id);

        var dto = _mapper.Map<BookingResponseDto>(booking);
        return Ok(dto);
    }

    [HttpGet("booking-search")]
    public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetFiltered([FromQuery] BookingFilterDto query)
    {
        var newQuery = _mapper.Map<BookingFilter>(query);
        
        var filteredBookings = await _bookingService.GetAllAsync(newQuery);
        var dtos = filteredBookings
            .Select(x => _mapper.Map<BookingResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<BookingResponseDto>> Create([FromBody] BookingRequestDto booking)
    {
        var newBooking = _mapper.Map<Booking>(booking);

        var addedBooking = await _bookingService.AddAsync(newBooking);

        var dto = _mapper.Map<BookingResponseDto>(addedBooking);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] BookingRequestDto booking)
    {
        var newBooking = _mapper.Map<Booking>(booking);
        newBooking.Id = id;

        await _bookingService.UpdateAsync(newBooking);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _bookingService.DeleteAsync(id);
        return NoContent();
    }
}
