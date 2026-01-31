using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Review;
using EgyptVoyage.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<ActionResult<List<ReviewDto>>> GetAll()
    {
        try
        {
            var reviews = await _reviewRepository.GetAllAsync();
            return Ok(_mapper.Map<List<ReviewDto>>(reviews));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving reviews", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetById(string id)
    {
        try
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
                return NotFound(new { message = "Review not found" });

            return Ok(_mapper.Map<ReviewDto>(review));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving review", error = ex.Message });
        }
    }

    [HttpGet("entity/{entityId}")]
    public async Task<ActionResult<List<ReviewDto>>> GetByEntity(string entityId)
    {
        try
        {
            var reviews = await _reviewRepository.GetByEntityAsync(entityId);
            return Ok(_mapper.Map<List<ReviewDto>>(reviews));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving reviews", error = ex.Message });
        }
    }

    [HttpGet("tourist/{touristId}")]
    [Authorize]
    public async Task<ActionResult<List<ReviewDto>>> GetByTourist(string touristId)
    {
        try
        {
            var reviews = await _reviewRepository.GetByTouristAsync(touristId);
            return Ok(_mapper.Map<List<ReviewDto>>(reviews));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving reviews", error = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Tourist")]
    public async Task<ActionResult<ReviewDto>> Create([FromBody] CreateReviewDto createDto)
    {
        try
        {
            var review = _mapper.Map<Review>(createDto);
            var createdReview = await _reviewRepository.AddAsync(review);
            var reviewDto = _mapper.Map<ReviewDto>(createdReview);

            return CreatedAtAction(nameof(GetById), new { id = createdReview.Id }, reviewDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating review", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Tourist")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _reviewRepository.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Review not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting review", error = ex.Message });
        }
    }
}
