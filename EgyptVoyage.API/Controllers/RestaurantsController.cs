

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

   
}


    