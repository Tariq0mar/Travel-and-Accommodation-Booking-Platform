using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Hotel;

namespace TABP.WebAPI.Controllers.CRUD;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/hotel")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly IMapper _mapper;

    public HotelController(IHotelService hotelService, IMapper mapper)
    {
        _hotelService = hotelService ?? throw new ArgumentException(nameof(hotelService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HotelResponseDto>> GetById(int id)
    {
        var hotel = await _hotelService.GetByIdAsync(id);

        var dto = _mapper.Map<HotelResponseDto>(hotel);
        return Ok(dto);
    }

    [HttpGet("hotel-search")]
    public async Task<ActionResult<IEnumerable<HotelResponseDto>>> GetFiltered([FromQuery] HotelFilterDto query)
    {
        var newQuery = _mapper.Map<HotelFilter>(query);

        var filteredHotels = await _hotelService.GetAllAsync(newQuery);
        var dtos = filteredHotels
            .Select(x => _mapper.Map<HotelResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<HotelResponseDto>> Create([FromBody] HotelRequestDto hotel)
    {
        var newHotel = _mapper.Map<Hotel>(hotel);

        var addedHotel = await _hotelService.AddAsync(newHotel);

        var dto = _mapper.Map<HotelResponseDto>(addedHotel);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] HotelRequestDto hotel)
    {
        var newHotel = _mapper.Map<Hotel>(hotel);
        newHotel.Id = id;

        await _hotelService.UpdateAsync(newHotel);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _hotelService.DeleteAsync(id);
        return NoContent();
    }
}
