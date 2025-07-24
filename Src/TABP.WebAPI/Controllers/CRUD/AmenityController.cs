using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Amenity;

namespace TABP.WebAPI.Controllers.CRUD;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/amenity")]
public class AmenityController : ControllerBase
{
    private readonly IAmenityService _amenityService;
    private readonly IMapper _mapper;

    public AmenityController(IAmenityService amenityService, IMapper mapper)
    {
        _amenityService = amenityService ?? throw new ArgumentException(nameof(amenityService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AmenityResponseDto>> GetById(int id)
    {
        var amenity = await _amenityService.GetByIdAsync(id);

        var dto = _mapper.Map<AmenityResponseDto>(amenity);
        return Ok(dto);
    }

    [HttpGet("amenity-search")]
    public async Task<ActionResult<IEnumerable<AmenityResponseDto>>> GetFiltered([FromQuery] AmenityFilterDto query)
    {
        var newQuery = _mapper.Map<AmenityFilter>(query);

        var filteredAmenities = await _amenityService.GetAllAsync(newQuery);
        var dtos = filteredAmenities
            .Select(x => _mapper.Map<AmenityResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<AmenityResponseDto>> Create([FromBody] AmenityRequestDto amenity)
    {
        var newAmenity = _mapper.Map<Amenity>(amenity);

        var addedAmenity = await _amenityService.AddAsync(newAmenity);

        var dto = _mapper.Map<AmenityResponseDto>(addedAmenity);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] AmenityRequestDto amenity)
    {
        var newAmenity = _mapper.Map<Amenity>(amenity);
        newAmenity.Id = id;

        await _amenityService.UpdateAsync(newAmenity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _amenityService.DeleteAsync(id);
        return NoContent();
    }
}
