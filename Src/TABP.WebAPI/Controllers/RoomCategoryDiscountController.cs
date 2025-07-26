using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.RoomCategoryDiscount;

namespace TABP.WebAPI.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/roomcategorydiscount")]
public class RoomCategoryDiscountController : ControllerBase
{
    private readonly IRoomCategoryDiscountService _roomCategoryDiscountService;
    private readonly IMapper _mapper;

    public RoomCategoryDiscountController(IRoomCategoryDiscountService roomCategoryDiscountService, IMapper mapper)
    {
        _roomCategoryDiscountService = roomCategoryDiscountService ?? throw new ArgumentException(nameof(roomCategoryDiscountService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomCategoryDiscountResponseDto>> GetById(int id)
    {
        var roomCategoryDiscount = await _roomCategoryDiscountService.GetByIdAsync(id);

        var dto = _mapper.Map<RoomCategoryDiscountResponseDto>(roomCategoryDiscount);
        return Ok(dto);
    }

    [HttpGet("roomcategorydiscount-search")]
    public async Task<ActionResult<IEnumerable<RoomCategoryDiscountResponseDto>>> GetFiltered([FromQuery] RoomCategoryDiscountFilterDto query)
    {
        var newQuery = _mapper.Map<RoomCategoryDiscountFilter>(query);

        var filteredRoomCategoryDiscounts = await _roomCategoryDiscountService.GetAllAsync(newQuery);
        var dtos = filteredRoomCategoryDiscounts
            .Select(x => _mapper.Map<RoomCategoryDiscountResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<RoomCategoryDiscountResponseDto>> Create([FromBody] RoomCategoryDiscountRequestDto roomCategoryDiscount)
    {
        var newRoomCategoryDiscount = _mapper.Map<RoomCategoryDiscount>(roomCategoryDiscount);

        var addedRoomCategoryDiscount = await _roomCategoryDiscountService.AddAsync(newRoomCategoryDiscount);

        var dto = _mapper.Map<RoomCategoryDiscountResponseDto>(addedRoomCategoryDiscount);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] RoomCategoryDiscountRequestDto roomCategoryDiscount)
    {
        var newRoomCategoryDiscount = _mapper.Map<RoomCategoryDiscount>(roomCategoryDiscount);
        newRoomCategoryDiscount.Id = id;

        await _roomCategoryDiscountService.UpdateAsync(newRoomCategoryDiscount);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _roomCategoryDiscountService.DeleteAsync(id);
        return NoContent();
    }
}
