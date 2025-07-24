using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.CartItem;

namespace TABP.WebAPI.Controllers.CRUD;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/cartitem")]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;
    private readonly IMapper _mapper;

    public CartItemController(ICartItemService cartItemService, IMapper mapper)
    {
        _cartItemService = cartItemService ?? throw new ArgumentException(nameof(cartItemService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartItemResponseDto>> GetById(int id)
    {
        var cartItem = await _cartItemService.GetByIdAsync(id);

        var dto = _mapper.Map<CartItemResponseDto>(cartItem);
        return Ok(dto);
    }

    [HttpGet("cartitem-search")]
    public async Task<ActionResult<IEnumerable<CartItemResponseDto>>> GetFiltered([FromQuery] CartItemFilterDto query)
    {
        var newQuery = _mapper.Map<CartItemFilter>(query);

        var filteredCartItems = await _cartItemService.GetAllAsync(newQuery);
        var dtos = filteredCartItems
            .Select(x => _mapper.Map<CartItemResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<CartItemResponseDto>> Create([FromBody] CartItemRequestDto cartItem)
    {
        var newCartItem = _mapper.Map<CartItem>(cartItem);

        var addedCartItem = await _cartItemService.AddAsync(newCartItem);

        var dto = _mapper.Map<CartItemResponseDto>(addedCartItem);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] CartItemRequestDto cartItem)
    {
        var newCartItem = _mapper.Map<CartItem>(cartItem);
        newCartItem.Id = id;

        await _cartItemService.UpdateAsync(newCartItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _cartItemService.DeleteAsync(id);
        return NoContent();
    }
}
