
using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Review;
using EgyptVoyage.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ITouristRepository _touristRepository;
    private readonly IMapper _mapper;

    public ReviewsController(
        IReviewRepository reviewRepository,
        ITouristRepository touristRepository,
        IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _touristRepository = touristRepository;
        _mapper = mapper;
    }

    // GET: api/reviews
    [HttpGet]
    public async Task<ActionResult<List<ReviewDto>>> GetAll()
    {
        var reviews = await _reviewRepository.GetAllAsync();
        var reviewDtos = new List<ReviewDto>();

        foreach (var review in reviews)
        {
            var dto = _mapper.Map<ReviewDto>(review);
            var tourist = await _touristRepository.GetByIdAsync(review.TouristId);
            dto.TouristName = tourist?.Name ?? "Unknown";
            reviewDtos.Add(dto);
        }

        return Ok(reviewDtos);
    }

  

    // POST: api/reviews
    [HttpPost]
    [Authorize(Roles = "Tourist")]
    public async Task<ActionResult<ReviewDto>> Create(CreateReviewDto createDto)
    {
        var touristId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var review = _mapper.Map<Review>(createDto);
        review.TouristId = touristId;

        var createdReview = await _reviewRepository.AddAsync(review);

        var dto = _mapper.Map<ReviewDto>(createdReview);
        var tourist = await _touristRepository.GetByIdAsync(touristId);
        dto.TouristName = tourist?.Name ?? "Unknown";

        return Ok(_mapper.Map<ReviewDto>(createdReview));
    }

    // DELETE: api/reviews/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Tourist")]
    public async Task<IActionResult> Delete(string id)
    {
        var touristId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var review = await _reviewRepository.GetByIdAsync(id);

        if (review == null) return NotFound();
        if (review.TouristId != touristId) return Forbid();

        await _reviewRepository.DeleteAsync(id);
        return NoContent();
    }
}

