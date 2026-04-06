


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpPost("predict")]
    [Authorize(Roles = "Tourist")]
    public async Task<IActionResult> Predict([FromBody] PredictRequestDto request)
    {
        try
        {
            var touristId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var payload = new
            {
                user_id = touristId,
                entity_id = request.EntityId,
                interaction_type = request.InteractionType
            };

            _httpClient.DefaultRequestHeaders.Remove("ngrok-skip-browser-warning");
            _httpClient.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "true");

            var response = await _httpClient.PostAsJsonAsync("/predict", payload);

            if (!response.IsSuccessStatusCode)
            {
                var errContent = await response.Content.ReadAsStringAsync();
                return StatusCode(500, new { message = "AI service error", details = errContent });
            }

            var result = await response.Content.ReadFromJsonAsync<object>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error", error = ex.Message });
        }
    }
}

public class PredictRequestDto
{
    public string EntityId { get; set; } = string.Empty;
    public string InteractionType { get; set; } = "view";
}
