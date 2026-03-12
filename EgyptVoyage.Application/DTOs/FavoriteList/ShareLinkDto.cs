namespace EgyptVoyage.Application.DTOs.Favorite;

public class ShareLinkDto
{
    public string ShareLink { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}
