
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

    // ✅ Feature 1: Generate Share Link
    // POST: api/favoritelists/my/share
    [HttpPost("my/share")]
    public async Task<ActionResult<ShareLinkDto>> GenerateShareLink()
    {
        var shareToken = await _repo.GenerateShareTokenAsync(GetTouristId());

        // بنبني الـ URL الكامل من الـ request الحالي
        var shareLink = $"{Request.Scheme}://{Request.Host}/api/favoritelists/shared/{shareToken}";

        return Ok(new ShareLinkDto
        {
            ShareLink = shareLink,
            GeneratedAt = DateTime.UtcNow
        });
    }

    //  Feature 1: Get Shared List - أي حد يقدر يشوفها من غير Login
    // GET: api/favoritelists/shared/{shareToken}
    [HttpGet("shared/{shareToken}")]
    [AllowAnonymous]
    public async Task<ActionResult<FavoriteDetailDto>> GetSharedList(string shareToken)
    {
        var favorite = await _repo.GetByShareTokenAsync(shareToken);

        if (favorite == null)
            return NotFound(new { message = "This link is invalid or has been disabled" });

        return Ok(favorite);
    }
    














}