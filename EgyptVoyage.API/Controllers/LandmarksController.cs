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

   
}

   