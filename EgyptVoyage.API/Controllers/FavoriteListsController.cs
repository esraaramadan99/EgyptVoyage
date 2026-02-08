/*using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Favorite;
using EgyptVoyage.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("tourist/{touristId}")]
    public async Task<ActionResult<FavoriteDto>> GetByTourist(string touristId)
    {
        try
        {
            var favorite = await _favoriteListRepository.GetByTouristIdAsync(touristId);
            if (favorite == null)
                return NotFound(new { message = "Favorite list not found" });

            return Ok(_mapper.Map<FavoriteDto>(favorite));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving favorites", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FavoriteDto>> GetById(string id)
    {
        try
        {
            var favorite = await _favoriteListRepository.GetByIdAsync(id);
            if (favorite == null)
                return NotFound(new { message = "Favorite not found" });

            return Ok(_mapper.Map<FavoriteDto>(favorite));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving favorite", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FavoriteDto>> Update(string id, [FromBody] FavoriteDto updateDto)
    {
        try
        {
            var existingFavorite = await _favoriteListRepository.GetByIdAsync(id);
            if (existingFavorite == null)
                return NotFound(new { message = "Favorite list not found" });

            _mapper.Map(updateDto, existingFavorite);
            var updatedFavorite = await _favoriteListRepository.UpdateAsync(existingFavorite);

            return Ok(_mapper.Map<FavoriteDto>(updatedFavorite));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating favorites", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _favoriteListRepository.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Favorite not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting favorite", error = ex.Message });
        }
    }
}
*/