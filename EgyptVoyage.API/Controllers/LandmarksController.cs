using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Landmark;
using EgyptVoyage.Domain.Entities;
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

    [HttpGet("search")]
    public async Task<ActionResult<List<LandmarkDto>>> SearchByName([FromQuery] string name)
    {
        try
        {
            var landmarks = await _landmarkRepository.SearchByNameAsync(name);
            return Ok(_mapper.Map<List<LandmarkDto>>(landmarks));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error searching landmarks", error = ex.Message });
        }
    }
}

    /*
    [HttpPost]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<LandmarkDto>> Create([FromBody] CreateLandmarkDto createDto)
    {
        try
        {
            var landmark = _mapper.Map<Landmark>(createDto);
            var createdLandmark = await _landmarkRepository.AddAsync(landmark);
            var landmarkDto = _mapper.Map<LandmarkDto>(createdLandmark);

            return CreatedAtAction(nameof(GetById), new { id = createdLandmark.Id }, landmarkDto);
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
            var existingLandmark = await _landmarkRepository.GetByIdAsync(id);
            if (existingLandmark == null)
                return NotFound(new { message = "Landmark not found" });

            _mapper.Map(updateDto, existingLandmark);
            var updatedLandmark = await _landmarkRepository.UpdateAsync(existingLandmark);

            return Ok(_mapper.Map<LandmarkDto>(updatedLandmark));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating landmark", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _landmarkRepository.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Landmark not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting landmark", error = ex.Message });
        }
    }
}
    */