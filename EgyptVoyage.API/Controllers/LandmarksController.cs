using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Hotel;
using EgyptVoyage.Application.DTOs.Landmark;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LandmarksController : ControllerBase
{
    private readonly ILandmarkRepository _landmarkRepository;
    private readonly IMapper _mapper;

    public LandmarksController(ILandmarkRepository landmarkRepository, IMapper mapper)
    {
        _landmarkRepository = landmarkRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<LandmarkDto>>> GetAll()
    {
        try
        {
            var landmarks = await _landmarkRepository.GetAllAsync();
            return Ok(_mapper.Map<List<LandmarkDto>>(landmarks));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving landmarks", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LandmarkDto>> GetById(string id)
    {
        try
        {
            var landmark = await _landmarkRepository.GetByIdAsync(id);
            if (landmark == null)
                return NotFound(new { message = "Landmark not found" });

            return Ok(_mapper.Map<LandmarkDto>(landmark));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving landmark", error = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<LandmarkDto>> Create([FromBody] CreateLandmarkDto createDto)
    {
        try
        {
            var landmark = _mapper.Map<Landmark>(createDto);
            var created = await _landmarkRepository.AddAsync(landmark);
            return Ok(_mapper.Map<LandmarkDto>(created));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating landmark", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<LandmarkDto>> Update(string id, [FromBody] UpdateLandmarkDto updateDto)
    {
        try
        {
            var existing = await _landmarkRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Landmark not found" });
            updateDto.Id = id;
            var landmark = _mapper.Map<Landmark>(updateDto);
            var updated = await _landmarkRepository.UpdateAsync(landmark);
            return Ok(_mapper.Map<LandmarkDto>(updated));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating landmark", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var existing = await _landmarkRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Landmark not found" });
            await _landmarkRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting landmark", error = ex.Message });
        }
    }



}


   