using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Location;

namespace TABP.WebAPI.Controllers.CRUD;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/location")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;
    private readonly IMapper _mapper;

    public LocationController(ILocationService locationService, IMapper mapper)
    {
        _locationService = locationService ?? throw new ArgumentException(nameof(locationService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LocationResponseDto>> GetById(int id)
    {
        var location = await _locationService.GetByIdAsync(id);

        var dto = _mapper.Map<LocationResponseDto>(location);
        return Ok(dto);
    }

    [HttpGet("location-search")]
    public async Task<ActionResult<IEnumerable<LocationResponseDto>>> GetFiltered([FromQuery] LocationFilterDto query)
    {
        var newQuery = _mapper.Map<LocationFilter>(query);

        var filteredLocations = await _locationService.GetAllAsync(newQuery);
        var dtos = filteredLocations
            .Select(x => _mapper.Map<LocationResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<LocationResponseDto>> Create([FromBody] LocationRequestDto location)
    {
        var newLocation = _mapper.Map<Location>(location);

        var addedLocation = await _locationService.AddAsync(newLocation);

        var dto = _mapper.Map<LocationResponseDto>(addedLocation);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] LocationRequestDto location)
    {
        var newLocation = _mapper.Map<Location>(location);
        newLocation.Id = id;

        await _locationService.UpdateAsync(newLocation);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _locationService.DeleteAsync(id);
        return NoContent();
    }
}
