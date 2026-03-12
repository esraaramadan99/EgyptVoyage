

using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Restaurant;
using EgyptVoyage.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public RestaurantsController(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<RestaurantDto>>> GetAll()
    {
        try
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return Ok(_mapper.Map<List<RestaurantDto>>(restaurants));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving restaurants", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetById(string id)
    {
        try
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
                return NotFound(new { message = "Restaurant not found" });

            return Ok(_mapper.Map<RestaurantDto>(restaurant));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving restaurant", error = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<RestaurantDto>> Create([FromBody] CreateRestaurantDto createDto)
    {
        try
        {
            var restaurant = _mapper.Map<Restaurant>(createDto);
            var created = await _restaurantRepository.AddAsync(restaurant);
            return Ok(_mapper.Map<RestaurantDto>(created));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating restaurant", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<RestaurantDto>> Update(string id, [FromBody] UpdateRestaurantDto updateDto)
    {
        try
        {
            var existing = await _restaurantRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Restaurant not found" });
            updateDto.Id = id;
            var restaurant = _mapper.Map<Restaurant>(updateDto);
            var updated = await _restaurantRepository.UpdateAsync(restaurant);
            return Ok(_mapper.Map<RestaurantDto>(updated));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating restaurant", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var existing = await _restaurantRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Restaurant not found" });
            await _restaurantRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting restaurant", error = ex.Message });
        }
    }


}



    