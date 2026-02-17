

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

    [HttpGet("search")]
    public async Task<ActionResult<List<RestaurantDto>>> SearchByName([FromQuery] string name)
    {
        try
        {
            var restaurants = await _restaurantRepository.SearchByNameAsync(name);
            return Ok(_mapper.Map<List<RestaurantDto>>(restaurants));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error searching restaurants", error = ex.Message });
        }
    }
}


    /*
    [HttpPost]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<RestaurantDto>> Create([FromBody] CreateRestaurantDto createDto)
    {
        try
        {
            var restaurant = _mapper.Map<Restaurant>(createDto);
            var createdRestaurant = await _restaurantRepository.AddAsync(restaurant);
            var restaurantDto = _mapper.Map<RestaurantDto>(createdRestaurant);

            return CreatedAtAction(nameof(GetById), new { id = createdRestaurant.Id }, restaurantDto);
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
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(id);
            if (existingRestaurant == null)
                return NotFound(new { message = "Restaurant not found" });

            _mapper.Map(updateDto, existingRestaurant);
            var updatedRestaurant = await _restaurantRepository.UpdateAsync(existingRestaurant);

            return Ok(_mapper.Map<RestaurantDto>(updatedRestaurant));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating restaurant", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _restaurantRepository.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Restaurant not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting restaurant", error = ex.Message });
        }
    }
}
    
    */