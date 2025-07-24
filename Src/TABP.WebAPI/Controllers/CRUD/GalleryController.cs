using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Gallery;

namespace TABP.WebAPI.Controllers.CRUD;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/gallery")]
public class GalleryController : ControllerBase
{
    private readonly IGalleryService _galleryService;
    private readonly IMapper _mapper;

    public GalleryController(IGalleryService galleryService, IMapper mapper)
    {
        _galleryService = galleryService ?? throw new ArgumentException(nameof(galleryService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GalleryResponseDto>> GetById(int id)
    {
        var gallery = await _galleryService.GetByIdAsync(id);

        var dto = _mapper.Map<GalleryResponseDto>(gallery);
        return Ok(dto);
    }

    [HttpGet("gallery-search")]
    public async Task<ActionResult<IEnumerable<GalleryResponseDto>>> GetFiltered([FromQuery] GalleryFilterDto query)
    {
        var newQuery = _mapper.Map<GalleryFilter>(query);

        var filteredGalleries = await _galleryService.GetAllAsync(newQuery);
        var dtos = filteredGalleries
            .Select(x => _mapper.Map<GalleryResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<GalleryResponseDto>> Create([FromBody] GalleryRequestDto gallery)
    {
        var newGallery = _mapper.Map<Gallery>(gallery);

        var addedGallery = await _galleryService.AddAsync(newGallery);

        var dto = _mapper.Map<GalleryResponseDto>(addedGallery);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] GalleryRequestDto gallery)
    {
        var newGallery = _mapper.Map<Gallery>(gallery);
        newGallery.Id = id;

        await _galleryService.UpdateAsync(newGallery);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _galleryService.DeleteAsync(id);
        return NoContent();
    }
}
