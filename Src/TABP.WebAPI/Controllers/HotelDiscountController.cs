using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.HotelDiscount;

namespace TABP.WebAPI.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/hoteldiscount")]
public class HotelDiscountController : ControllerBase
{
    private readonly IHotelDiscountService _hotelDiscountService;
    private readonly IMapper _mapper;

    public HotelDiscountController(IHotelDiscountService hotelDiscountService, IMapper mapper)
    {
        _hotelDiscountService = hotelDiscountService ?? throw new ArgumentException(nameof(hotelDiscountService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HotelDiscountResponseDto>> GetById(int id)
    {
        var hotelDiscount = await _hotelDiscountService.GetByIdAsync(id);

        var dto = _mapper.Map<HotelDiscountResponseDto>(hotelDiscount);
        return Ok(dto);
    }

    [HttpGet("hoteldiscount-search")]
    public async Task<ActionResult<IEnumerable<HotelDiscountResponseDto>>> GetFiltered([FromQuery] HotelDiscountFilterDto query)
    {
        var newQuery = _mapper.Map<HotelDiscountFilter>(query);

        var filteredHotelDiscounts = await _hotelDiscountService.GetAllAsync(newQuery);
        var dtos = filteredHotelDiscounts
            .Select(x => _mapper.Map<HotelDiscountResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<HotelDiscountResponseDto>> Create([FromBody] HotelDiscountRequestDto hotelDiscount)
    {
        var newHotelDiscount = _mapper.Map<HotelDiscount>(hotelDiscount);

        var addedHotelDiscount = await _hotelDiscountService.AddAsync(newHotelDiscount);

        var dto = _mapper.Map<HotelDiscountResponseDto>(addedHotelDiscount);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] HotelDiscountRequestDto hotelDiscount)
    {
        var newHotelDiscount = _mapper.Map<HotelDiscount>(hotelDiscount);
        newHotelDiscount.Id = id;

        await _hotelDiscountService.UpdateAsync(newHotelDiscount);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _hotelDiscountService.DeleteAsync(id);
        return NoContent();
    }
}
