// المسار: EgyptVoyage.API/Controllers/FavoriteListsController.cs
/*
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/favoritelists")]
[Authorize(Roles = "Tourist")]
public class FavoriteListsController : ControllerBase
{
    private readonly IFavoriteListRepository _repo;

    public FavoriteListsController(IFavoriteListRepository repo)
    {
        _repo = repo;
    }

    private string GetTouristId() => User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

    // GET: api/favoritelists/my
    [HttpGet("my")]
    public async Task<ActionResult<FavoriteDto>> GetMyFavorites()
    {
        var favorite = await _repo.GetByTouristIdWithDetailsAsync(GetTouristId());
        return Ok(favorite);
    }

    // POST: api/favoritelists/my/items
    [HttpPost("my/items")]
    public async Task<ActionResult<FavoriteDto>> AddItem(AddToFavoriteDto dto)
    {
        await _repo.AddItemAsync(GetTouristId(), dto.EntityType, dto.EntityId);
        var updated = await _repo.GetByTouristIdWithDetailsAsync(GetTouristId());
        return Ok(updated);
    }

    // DELETE: api/favoritelists/my/items?entityType=Hotel&entityId=abc123
    [HttpDelete("my/items")]
    public async Task<IActionResult> RemoveItem([FromQuery] string entityType, [FromQuery] string entityId)
    {
        var removed = await _repo.RemoveItemAsync(GetTouristId(), entityType, entityId);
        if (!removed) return NotFound();
        return NoContent();
    }
}
*/



/*
using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EgyptVoyage.API.Controllers;
*/
/*
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
*/
/*
[ApiController]
[Route("api/favoritelists")]
[Authorize(Roles = "Tourist")]
public class FavoriteListsController : ControllerBase
{
    private readonly IFavoriteListRepository _repo;
    private readonly IMapper _mapper;

    public FavoriteListsController(IFavoriteListRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    private string GetTouristId() => User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

    // GET: api/favoritelists/my
    [HttpGet("my")]
    public async Task<ActionResult<FavoriteDetailDto>> GetMyFavorites()
    {
        var favorite = await _repo.GetByTouristIdAsync(GetTouristId());
        if (favorite == null) return NotFound();
        return Ok(_mapper.Map<FavoriteDetailDto>(favorite));
    }

    // POST: api/favoritelists/my/items
    [HttpPost("my/items")]
    public async Task<ActionResult<FavoriteDetailDto>> AddItem(AddToFavoriteDto dto)
    {
        var updated = await _repo.AddItemAsync(GetTouristId(), dto.EntityType, dto.EntityId);
        //return Ok(_mapper.Map<FavoriteDetailDto>(updated));
        return Ok(updated);
    }

    // DELETE: api/favoritelists/my/items?entityType=Hotel&entityId=abc123
    [HttpDelete("my/items")]
    public async Task<IActionResult> RemoveItem([FromQuery] string entityType, [FromQuery] string entityId)
    {
        var removed = await _repo.RemoveItemAsync(GetTouristId(), entityType, entityId);
        if (!removed) return NotFound();
        return NoContent();
    }
}
*/
using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/favoritelists")]
[Authorize(Roles = "Tourist")]
public class FavoriteListsController : ControllerBase
{
    private readonly IFavoriteListRepository _repo;

    public FavoriteListsController(IFavoriteListRepository repo)
    {
        _repo = repo;
    }

    private string GetTouristId() => User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

    // GET: api/favoritelists/my
    [HttpGet("my")]
    public async Task<ActionResult<FavoriteDetailDto>> GetMyFavorites()
    {
        var favorite = await _repo.GetByTouristIdWithDetailsAsync(GetTouristId());
        return Ok(favorite);
    }

    // POST: api/favoritelists/my/items
    [HttpPost("my/items")]
    public async Task<ActionResult<FavoriteDetailDto>> AddItem(AddToFavoriteDto dto)
    {
        await _repo.AddItemAsync(GetTouristId(), dto.EntityType, dto.EntityId);
        var updated = await _repo.GetByTouristIdWithDetailsAsync(GetTouristId());
        return Ok(updated);
    }

    // DELETE: api/favoritelists/my/items?entityType=Hotel&entityId=abc123
    [HttpDelete("my/items")]
    public async Task<IActionResult> RemoveItem([FromQuery] string entityType, [FromQuery] string entityId)
    {
        var removed = await _repo.RemoveItemAsync(GetTouristId(), entityType, entityId);
        if (!removed) return NotFound();
        return NoContent();
    }
}