using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.UserDiscount;

namespace TABP.WebAPI.Controllers;

[ApiController]
[Route("api/userdiscount")]
public class UserDiscountController : ControllerBase
{
    private readonly IUserDiscountService _userDiscountService;
    private readonly IMapper _mapper;

    public UserDiscountController(IUserDiscountService userDiscountService, IMapper mapper)
    {
        _userDiscountService = userDiscountService ?? throw new ArgumentException(nameof(userDiscountService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDiscountResponseDto>> GetById(int id)
    {
        var userDiscount = await _userDiscountService.GetByIdAsync(id);

        var dto = _mapper.Map<UserDiscountResponseDto>(userDiscount);
        return Ok(dto);
    }

    [HttpGet("userdiscount-search")]
    public async Task<ActionResult<IEnumerable<UserDiscountResponseDto>>> GetFiltered([FromQuery] UserDiscountFilterDto query)
    {
        var newQuery = _mapper.Map<UserDiscountFilter>(query);

        var filteredUserDiscounts = await _userDiscountService.GetAllAsync(newQuery);
        var dtos = filteredUserDiscounts
            .Select(x => _mapper.Map<UserDiscountResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<UserDiscountResponseDto>> Create([FromBody] UserDiscountRequestDto userDiscount)
    {
        var newUserDiscount = _mapper.Map<UserDiscount>(userDiscount);

        var addedUserDiscount = await _userDiscountService.AddAsync(newUserDiscount);

        var dto = _mapper.Map<UserDiscountResponseDto>(addedUserDiscount);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UserDiscountRequestDto userDiscount)
    {
        var newUserDiscount = _mapper.Map<UserDiscount>(userDiscount);
        newUserDiscount.Id = id;

        await _userDiscountService.UpdateAsync(newUserDiscount);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _userDiscountService.DeleteAsync(id);
        return NoContent();
    }
}
