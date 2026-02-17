using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Tourist")]
public class FavoriteListsController : ControllerBase
{
    private readonly IFavoriteListRepository _favoriteListRepository;
    private readonly IMapper _mapper;

    public FavoriteListsController(IFavoriteListRepository favoriteListRepository, IMapper mapper)
    {
        _favoriteListRepository = favoriteListRepository;
        _mapper = mapper;
    }

    // GET: api/favoritelists/my
    [HttpGet("my")]
    public async Task<ActionResult<FavoriteDto>> GetMyFavorites()
    {
        var touristId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var favorite = await _favoriteListRepository.GetByTouristIdAsync(touristId);

        if (favorite == null)
            return NotFound();

        return Ok(_mapper.Map<FavoriteDto>(favorite));
    }

    // PUT: api/favoritelists/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<FavoriteDto>> Update(string id, FavoriteDto updateDto)
    {
        var existingFavorite = await _favoriteListRepository.GetByIdAsync(id);

        if (existingFavorite == null)
            return NotFound();

        _mapper.Map(updateDto, existingFavorite);

        var updatedFavorite = await _favoriteListRepository.UpdateAsync(existingFavorite);

        return Ok(_mapper.Map<FavoriteDto>(updatedFavorite));
    }

    // DELETE: api/favoritelists/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _favoriteListRepository.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
