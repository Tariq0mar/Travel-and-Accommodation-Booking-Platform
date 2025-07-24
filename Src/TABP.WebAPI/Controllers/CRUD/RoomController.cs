using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Room;

namespace TABP.WebAPI.Controllers.CRUD;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/room")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly IMapper _mapper;

    public RoomController(IRoomService roomService, IMapper mapper)
    {
        _roomService = roomService ?? throw new ArgumentException(nameof(roomService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomResponseDto>> GetById(int id)
    {
        var room = await _roomService.GetByIdAsync(id);

        var dto = _mapper.Map<RoomResponseDto>(room);
        return Ok(dto);
    }

    [HttpGet("room-search")]
    public async Task<ActionResult<IEnumerable<RoomResponseDto>>> GetFiltered([FromQuery] RoomFilterDto query)
    {
        var newQuery = _mapper.Map<RoomFilter>(query);

        var filteredRooms = await _roomService.GetAllAsync(newQuery);
        var dtos = filteredRooms
            .Select(x => _mapper.Map<RoomResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<RoomResponseDto>> Create([FromBody] RoomRequestDto room)
    {
        var newRoom = _mapper.Map<Room>(room);

        var addedRoom = await _roomService.AddAsync(newRoom);

        var dto = _mapper.Map<RoomResponseDto>(addedRoom);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] RoomRequestDto room)
    {
        var newRoom = _mapper.Map<Room>(room);
        newRoom.Id = id;

        await _roomService.UpdateAsync(newRoom);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _roomService.DeleteAsync(id);
        return NoContent();
    }
}
