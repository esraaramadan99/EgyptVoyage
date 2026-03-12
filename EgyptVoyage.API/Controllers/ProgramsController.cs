using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramEntity = EgyptVoyage.Domain.Entities.Program;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgramsController : ControllerBase
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;

    public ProgramsController(IProgramRepository programRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProgramDto>>> GetAll()
    {
        try
        {
            var programs = await _programRepository.GetAllAsync();
            return Ok(_mapper.Map<List<ProgramDto>>(programs));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving programs", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProgramDto>> GetById(string id)
    {
        try
        {
            var program = await _programRepository.GetByIdAsync(id);
            if (program == null) return NotFound(new { message = "Program not found" });
            return Ok(_mapper.Map<ProgramDto>(program));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving program", error = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<ProgramDto>> Create([FromBody] CreateProgramDto createDto)
    {
        try
        {
            var program = _mapper.Map<ProgramEntity>(createDto);
            var created = await _programRepository.AddAsync(program);
            return Ok(_mapper.Map<ProgramDto>(created));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating program", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<ActionResult<ProgramDto>> Update(string id, [FromBody] UpdateProgramDto updateDto)
    {
        try
        {
            var existing = await _programRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Program not found" });
            updateDto.Id = id;
            var program = _mapper.Map<ProgramEntity>(updateDto);
            var updated = await _programRepository.UpdateAsync(program);
            return Ok(_mapper.Map<ProgramDto>(updated));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating program", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Clerk")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var existing = await _programRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Program not found" });
            await _programRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting program", error = ex.Message });
        }
    }
}