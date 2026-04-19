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

    // ─── POST /api/recommendations/predict ───────────────────
    [HttpPost("predict")]
    [Authorize(Roles = "Tourist")]
    public async Task<IActionResult> Predict([FromBody] PredictRequestDto request)
    {
        try
        {
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

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/predict");
            httpRequest.Headers.Add("ngrok-skip-browser-warning", "true");
            httpRequest.Content = JsonContent.Create(payload);

            var response = await _httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return StatusCode(500, new { message = "AI service error", details = err });
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

    // ─── POST /api/recommendations/recommend ─────────────────
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
                return BadRequest(new { message = "entity_ids is required." });

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
                var err = await response.Content.ReadAsStringAsync();
                return StatusCode(500, new { message = "AI service error", details = err });
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

    // ─── GET /api/recommendations/trending  ← NEW ────────────
    // No auth required — trending is public info
    [HttpGet("trending")]
    public async Task<IActionResult> Trending(
        [FromQuery] int limit = 8,
        [FromQuery] int days = 7)
    {
        try
        {
            using var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"/trending?limit={limit}&days={days}"
            );
            httpRequest.Headers.Add("ngrok-skip-browser-warning", "true");

            var response = await _httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return StatusCode(500, new { message = "AI service error", details = err });
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

    // ─── GET /api/recommendations/health ─────────────────────
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

// ─── DTOs ────────────────────────────────────────────────────
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
