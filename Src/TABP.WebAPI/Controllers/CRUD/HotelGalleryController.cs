using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.HotelGallery;

namespace TABP.WebAPI.Controllers.CRUD;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/hotelgallery")]
public class HotelGalleryController : ControllerBase
{
    private readonly IHotelGalleryService _hotelGalleryService;
    private readonly IMapper _mapper;

    public HotelGalleryController(IHotelGalleryService hotelGalleryService, IMapper mapper)
    {
        _hotelGalleryService = hotelGalleryService ?? throw new ArgumentException(nameof(hotelGalleryService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HotelGalleryResponseDto>> GetById(int id)
    {
        var hotelGallery = await _hotelGalleryService.GetByIdAsync(id);

        var dto = _mapper.Map<HotelGalleryResponseDto>(hotelGallery);
        return Ok(dto);
    }

    [HttpGet("hotelgallery-search")]
    public async Task<ActionResult<IEnumerable<HotelGalleryResponseDto>>> GetFiltered([FromQuery] HotelGalleryFilterDto query)
    {
        var newQuery = _mapper.Map<HotelGalleryFilter>(query);

        var filteredHotelGalleries = await _hotelGalleryService.GetAllAsync(newQuery);
        var dtos = filteredHotelGalleries
            .Select(x => _mapper.Map<HotelGalleryResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<HotelGalleryResponseDto>> Create([FromBody] HotelGalleryRequestDto hotelGallery)
    {
        var newHotelGallery = _mapper.Map<HotelGallery>(hotelGallery);

        var addedHotelGallery = await _hotelGalleryService.AddAsync(newHotelGallery);

        var dto = _mapper.Map<HotelGalleryResponseDto>(addedHotelGallery);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] HotelGalleryRequestDto hotelGallery)
    {
        var newHotelGallery = _mapper.Map<HotelGallery>(hotelGallery);
        newHotelGallery.Id = id;

        await _hotelGalleryService.UpdateAsync(newHotelGallery);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _hotelGalleryService.DeleteAsync(id);
        return NoContent();
    }
}
