using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.User;

namespace TABP.WebAPI.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService ?? throw new ArgumentException(nameof(userService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        var dto = _mapper.Map<UserResponseDto>(user);
        return Ok(dto);
    }

    [HttpGet("user-search")]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetFiltered([FromQuery] UserFilterDto query)
    {
        var newQuery = _mapper.Map<UserFilter>(query);

        var filteredUsers = await _userService.GetAllAsync(newQuery);
        var dtos = filteredUsers
            .Select(x => _mapper.Map<UserResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> Create([FromBody] UserRequestDto user)
    {
        var newUser = _mapper.Map<User>(user);

        var addedUser = await _userService.AddAsync(newUser);

        var dto = _mapper.Map<UserResponseDto>(addedUser);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UserRequestDto user)
    {
        var newUser = _mapper.Map<User>(user);
        newUser.Id = id;

        await _userService.UpdateAsync(newUser);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}
