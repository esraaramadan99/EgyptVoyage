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
    private readonly IMapper _mapper;

    public ReviewsController(IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    // GET: api/reviews
    [HttpGet]
    public async Task<ActionResult<List<ReviewDto>>> GetAll()
    {
        var reviews = await _reviewRepository.GetAllAsync();
        return Ok(_mapper.Map<List<ReviewDto>>(reviews));
    }

    // GET: api/reviews/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetById(string id)
    {
        var review = await _reviewRepository.GetByIdAsync(id);

        if (review == null)
            return NotFound();

        return Ok(_mapper.Map<ReviewDto>(review));
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

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdReview.Id },
            _mapper.Map<ReviewDto>(createdReview)
        );
    }

    // DELETE: api/reviews/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Tourist")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _reviewRepository.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}

