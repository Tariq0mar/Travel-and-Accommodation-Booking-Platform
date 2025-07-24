using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.RoomCategory;

namespace TABP.WebAPI.Controllers.CRUD;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/roomcategory")]
public class RoomCategoryController : ControllerBase
{
    private readonly IRoomCategoryService _roomCategoryService;
    private readonly IMapper _mapper;

    public RoomCategoryController(IRoomCategoryService roomCategoryService, IMapper mapper)
    {
        _roomCategoryService = roomCategoryService ?? throw new ArgumentException(nameof(roomCategoryService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomCategoryResponseDto>> GetById(int id)
    {
        var roomCategory = await _roomCategoryService.GetByIdAsync(id);

        var dto = _mapper.Map<RoomCategoryResponseDto>(roomCategory);
        return Ok(dto);
    }

    [HttpGet("roomcategory-search")]
    public async Task<ActionResult<IEnumerable<RoomCategoryResponseDto>>> GetFiltered([FromQuery] RoomCategoryFilterDto query)
    {
        var newQuery = _mapper.Map<RoomCategoryFilter>(query);

        var filteredRoomCategories = await _roomCategoryService.GetAllAsync(newQuery);
        var dtos = filteredRoomCategories
            .Select(x => _mapper.Map<RoomCategoryResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<RoomCategoryResponseDto>> Create([FromBody] RoomCategoryRequestDto roomCategory)
    {
        var newRoomCategory = _mapper.Map<RoomCategory>(roomCategory);

        var addedRoomCategory = await _roomCategoryService.AddAsync(newRoomCategory);

        var dto = _mapper.Map<RoomCategoryResponseDto>(addedRoomCategory);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] RoomCategoryRequestDto roomCategory)
    {
        var newRoomCategory = _mapper.Map<RoomCategory>(roomCategory);
        newRoomCategory.Id = id;

        await _roomCategoryService.UpdateAsync(newRoomCategory);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _roomCategoryService.DeleteAsync(id);
        return NoContent();
    }
}
