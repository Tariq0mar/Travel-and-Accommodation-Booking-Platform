using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.RoomAmenity;

namespace TABP.WebAPI.Controllers.CRUD;

[ApiController]
[Route("api/roomamenity")]
public class RoomAmenityController : ControllerBase
{
    private readonly IRoomAmenityService _roomAmenityService;
    private readonly IMapper _mapper;

    public RoomAmenityController(IRoomAmenityService roomAmenityService, IMapper mapper)
    {
        _roomAmenityService = roomAmenityService ?? throw new ArgumentException(nameof(roomAmenityService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomAmenityResponseDto>> GetById(int id)
    {
        var roomAmenity = await _roomAmenityService.GetByIdAsync(id);

        var dto = _mapper.Map<RoomAmenityResponseDto>(roomAmenity);
        return Ok(dto);
    }

    [HttpGet("roomamenity-search")]
    public async Task<ActionResult<IEnumerable<RoomAmenityResponseDto>>> GetFiltered([FromQuery] RoomAmenityFilterDto query)
    {
        var newQuery = _mapper.Map<RoomAmenityFilter>(query);

        var filteredRoomAmenities = await _roomAmenityService.GetAllAsync(newQuery);
        var dtos = filteredRoomAmenities
            .Select(x => _mapper.Map<RoomAmenityResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<RoomAmenityResponseDto>> Create([FromBody] RoomAmenityRequestDto roomAmenity)
    {
        var newRoomAmenity = _mapper.Map<RoomAmenity>(roomAmenity);

        var addedRoomAmenity = await _roomAmenityService.AddAsync(newRoomAmenity);

        var dto = _mapper.Map<RoomAmenityResponseDto>(addedRoomAmenity);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] RoomAmenityRequestDto roomAmenity)
    {
        var newRoomAmenity = _mapper.Map<RoomAmenity>(roomAmenity);
        newRoomAmenity.Id = id;

        await _roomAmenityService.UpdateAsync(newRoomAmenity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _roomAmenityService.DeleteAsync(id);
        return NoContent();
    }
}
