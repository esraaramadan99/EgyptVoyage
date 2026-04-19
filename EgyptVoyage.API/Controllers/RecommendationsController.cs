using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net.Http.Json;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecommendationsController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public RecommendationsController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("FlaskAI");
    }

    // ─── POST /api/recommendations/predict ───────────────────────────
    // Single entity score — used to track a user interaction
    [HttpPost("predict")]
    [Authorize(Roles = "Tourist")]
    public async Task<IActionResult> Predict([FromBody] PredictRequestDto request)
    {
        try
        {
            // FIX 1: Read the claim that your JWT actually uses.
            // If your token stores the tourist ID in NameIdentifier use that;
            // otherwise swap for the correct claim type (e.g. "sub", "userId").
            var touristId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(touristId))
                return Unauthorized(new { message = "Cannot identify user from token." });

            var payload = new
            {
                user_id = touristId,
                entity_id = request.EntityId,
                interaction_type = request.InteractionType
            };

            // FIX 2: Add the ngrok header BEFORE every request (not just Remove+Add once)
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/predict");
            httpRequest.Headers.Add("ngrok-skip-browser-warning", "true");
            httpRequest.Content = JsonContent.Create(payload);

            var response = await _httpClient.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
            {
                var errContent = await response.Content.ReadAsStringAsync();
                return StatusCode(500, new { message = "AI service error", details = errContent });
            }

            var result = await response.Content.ReadFromJsonAsync<object>();
            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(503, new { message = "AI service unavailable", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error", error = ex.Message });
        }
    }

    // ─── POST /api/recommendations/recommend ─────────────────────────
    // Pass a list of entity IDs → get them ranked by the model
    [HttpPost("recommend")]
    [Authorize(Roles = "Tourist")]
    public async Task<IActionResult> Recommend([FromBody] RecommendRequestDto request)
    {
        try
        {
            var touristId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(touristId))
                return Unauthorized(new { message = "Cannot identify user from token." });

            if (request.EntityIds == null || request.EntityIds.Count == 0)
                return BadRequest(new { message = "entity_ids is required and must not be empty." });

            var payload = new
            {
                user_id = touristId,
                entity_ids = request.EntityIds,
                interaction_type = request.InteractionType,
                top_n = request.TopN
            };

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/recommend");
            httpRequest.Headers.Add("ngrok-skip-browser-warning", "true");
            httpRequest.Content = JsonContent.Create(payload);

            var response = await _httpClient.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
            {
                var errContent = await response.Content.ReadAsStringAsync();
                return StatusCode(500, new { message = "AI service error", details = errContent });
            }

            var result = await response.Content.ReadFromJsonAsync<object>();
            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(503, new { message = "AI service unavailable", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error", error = ex.Message });
        }
    }

    // ─── GET /api/recommendations/health ─────────────────────────────
    // Quick connectivity check — useful for debugging
    [HttpGet("health")]
    public async Task<IActionResult> Health()
    {
        try
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/health");
            httpRequest.Headers.Add("ngrok-skip-browser-warning", "true");

            var response = await _httpClient.SendAsync(httpRequest);
            var body = await response.Content.ReadAsStringAsync();
            return Ok(new { flaskStatus = response.StatusCode.ToString(), body });
        }
        catch (Exception ex)
        {
            return StatusCode(503, new { message = "Cannot reach Flask AI", error = ex.Message });
        }
    }
}

// ─── DTOs ──────────────────────────────────────────────────────────────
public class PredictRequestDto
{
    public string EntityId { get; set; } = string.Empty;
    public string InteractionType { get; set; } = "view";
}

public class RecommendRequestDto
{
    public List<string> EntityIds { get; set; } = new();
    public string InteractionType { get; set; } = "view";
    public int? TopN { get; set; }
}