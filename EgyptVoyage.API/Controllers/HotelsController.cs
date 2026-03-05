using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Hotel;
using EgyptVoyage.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public HotelsController(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<HotelDto>>> GetAll()
    {
        try
        {
            var hotels = await _hotelRepository.GetAllAsync();
            return Ok(_mapper.Map<List<HotelDto>>(hotels));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving hotels", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HotelDto>> GetById(string id)
    {
        try
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null)
                return NotFound(new { message = "Hotel not found" });

            return Ok(_mapper.Map<HotelDto>(hotel));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving hotel", error = ex.Message });
        }
    }

    
   
}