using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Discount;

namespace TABP.WebAPI.Controllers;

[ApiController]
[Route("api/discount")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;

    public DiscountController(IDiscountService discountService, IMapper mapper)
    {
        _discountService = discountService ?? throw new ArgumentException(nameof(discountService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DiscountResponseDto>> GetById(int id)
    {
        var discount = await _discountService.GetByIdAsync(id);

        var dto = _mapper.Map<DiscountResponseDto>(discount);
        return Ok(dto);
    }

    [HttpGet("discount-search")]
    public async Task<ActionResult<IEnumerable<DiscountResponseDto>>> GetFiltered([FromQuery] DiscountFilterDto query)
    {
        var newQuery = _mapper.Map<DiscountFilter>(query);

        var filteredDiscounts = await _discountService.GetAllAsync(newQuery);
        var dtos = filteredDiscounts
            .Select(x => _mapper.Map<DiscountResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<DiscountResponseDto>> Create([FromBody] DiscountRequestDto discount)
    {
        var newDiscount = _mapper.Map<Discount>(discount);

        var addedDiscount = await _discountService.AddAsync(newDiscount);

        var dto = _mapper.Map<DiscountResponseDto>(addedDiscount);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] DiscountRequestDto discount)
    {
        var newDiscount = _mapper.Map<Discount>(discount);
        newDiscount.Id = id;

        await _discountService.UpdateAsync(newDiscount);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _discountService.DeleteAsync(id);
        return NoContent();
    }
}
