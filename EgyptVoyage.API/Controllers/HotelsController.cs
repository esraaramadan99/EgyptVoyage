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

    [HttpGet("search")]
    public async Task<ActionResult<List<HotelDto>>> SearchByName([FromQuery] string name)
    {
        try
        {
            var hotels = await _hotelRepository.SearchByNameAsync(name);
            return Ok(_mapper.Map<List<HotelDto>>(hotels));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error searching hotels", error = ex.Message });
        }
    }
    /*

    [HttpPost]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<HotelDto>> Create([FromBody] CreateHotelDto createDto)
    {
        try
        {
            var hotel = _mapper.Map<Hotel>(createDto);
            var createdHotel = await _hotelRepository.AddAsync(hotel);
            var hotelDto = _mapper.Map<HotelDto>(createdHotel);

            return CreatedAtAction(nameof(GetById), new { id = createdHotel.Id }, hotelDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating hotel", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<HotelDto>> Update(string id, [FromBody] UpdateHotelDto updateDto)
    {
        try
        {
            var existingHotel = await _hotelRepository.GetByIdAsync(id);
            if (existingHotel == null)
                return NotFound(new { message = "Hotel not found" });

            _mapper.Map(updateDto, existingHotel);
            var updatedHotel = await _hotelRepository.UpdateAsync(existingHotel);

            return Ok(_mapper.Map<HotelDto>(updatedHotel));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating hotel", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _hotelRepository.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Hotel not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting hotel", error = ex.Message });
        }
    }
}
    */
}