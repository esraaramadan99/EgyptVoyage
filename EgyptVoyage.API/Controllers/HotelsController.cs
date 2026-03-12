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

    // POST: Clerk بس
    [HttpPost]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<HotelDto>> Create([FromBody] CreateHotelDto createDto)
    {
        try
        {
            var hotel = _mapper.Map<Hotel>(createDto);
            var created = await _hotelRepository.AddAsync(hotel);
            return Ok(_mapper.Map<HotelDto>(created));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating hotel", error = ex.Message });
        }
    }

    // PUT: Clerk بس
    [HttpPut("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<HotelDto>> Update(string id, [FromBody] UpdateHotelDto updateDto)
    {
        try
        {
            var existing = await _hotelRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Hotel not found" });
            updateDto.Id = id;
            var hotel = _mapper.Map<Hotel>(updateDto);
            var updated = await _hotelRepository.UpdateAsync(hotel);
            return Ok(_mapper.Map<HotelDto>(updated));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating hotel", error = ex.Message });
        }
    }

    // DELETE: Clerk بس
    [HttpDelete("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var existing = await _hotelRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Hotel not found" });
            await _hotelRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting hotel", error = ex.Message });
        }
    }






}