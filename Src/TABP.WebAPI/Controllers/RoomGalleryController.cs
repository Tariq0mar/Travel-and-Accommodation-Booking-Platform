using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.RoomGallery;

namespace TABP.WebAPI.Controllers;

[ApiController]
[Route("api/roomgallery")]
public class RoomGalleryController : ControllerBase
{
    private readonly IRoomGalleryService _roomGalleryService;
    private readonly IMapper _mapper;

    public RoomGalleryController(IRoomGalleryService roomGalleryService, IMapper mapper)
    {
        _roomGalleryService = roomGalleryService ?? throw new ArgumentException(nameof(roomGalleryService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomGalleryResponseDto>> GetById(int id)
    {
        var roomGallery = await _roomGalleryService.GetByIdAsync(id);

        var dto = _mapper.Map<RoomGalleryResponseDto>(roomGallery);
        return Ok(dto);
    }

    [HttpGet("roomgallery-search")]
    public async Task<ActionResult<IEnumerable<RoomGalleryResponseDto>>> GetFiltered([FromQuery] RoomGalleryFilterDto query)
    {
        var newQuery = _mapper.Map<RoomGalleryFilter>(query);

        var filteredRoomGalleries = await _roomGalleryService.GetAllAsync(newQuery);
        var dtos = filteredRoomGalleries
            .Select(x => _mapper.Map<RoomGalleryResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<RoomGalleryResponseDto>> Create([FromBody] RoomGalleryRequestDto roomGallery)
    {
        var newRoomGallery = _mapper.Map<RoomGallery>(roomGallery);

        var addedRoomGallery = await _roomGalleryService.AddAsync(newRoomGallery);

        var dto = _mapper.Map<RoomGalleryResponseDto>(addedRoomGallery);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] RoomGalleryRequestDto roomGallery)
    {
        var newRoomGallery = _mapper.Map<RoomGallery>(roomGallery);
        newRoomGallery.Id = id;

        await _roomGalleryService.UpdateAsync(newRoomGallery);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _roomGalleryService.DeleteAsync(id);
        return NoContent();
    }
}
