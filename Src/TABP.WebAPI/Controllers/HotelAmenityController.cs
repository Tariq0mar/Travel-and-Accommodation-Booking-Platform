using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.HotelAmenity;

namespace TABP.WebAPI.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/hotelamenity")]
public class HotelAmenityController : ControllerBase
{
    private readonly IHotelAmenityService _hotelAmenityService;
    private readonly IMapper _mapper;

    public HotelAmenityController(IHotelAmenityService hotelAmenityService, IMapper mapper)
    {
        _hotelAmenityService = hotelAmenityService ?? throw new ArgumentException(nameof(hotelAmenityService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HotelAmenityResponseDto>> GetById(int id)
    {
        var hotelAmenity = await _hotelAmenityService.GetByIdAsync(id);

        var dto = _mapper.Map<HotelAmenityResponseDto>(hotelAmenity);
        return Ok(dto);
    }

    [HttpGet("hotelamenity-search")]
    public async Task<ActionResult<IEnumerable<HotelAmenityResponseDto>>> GetFiltered([FromQuery] HotelAmenityFilterDto query)
    {
        var newQuery = _mapper.Map<HotelAmenityFilter>(query);

        var filteredHotelAmenities = await _hotelAmenityService.GetAllAsync(newQuery);
        var dtos = filteredHotelAmenities
            .Select(x => _mapper.Map<HotelAmenityResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<HotelAmenityResponseDto>> Create([FromBody] HotelAmenityRequestDto hotelAmenity)
    {
        var newHotelAmenity = _mapper.Map<HotelAmenity>(hotelAmenity);

        var addedHotelAmenity = await _hotelAmenityService.AddAsync(newHotelAmenity);

        var dto = _mapper.Map<HotelAmenityResponseDto>(addedHotelAmenity);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] HotelAmenityRequestDto hotelAmenity)
    {
        var newHotelAmenity = _mapper.Map<HotelAmenity>(hotelAmenity);
        newHotelAmenity.Id = id;

        await _hotelAmenityService.UpdateAsync(newHotelAmenity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _hotelAmenityService.DeleteAsync(id);
        return NoContent();
    }
}
