using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if (program == null)
                return NotFound(new { message = "Program not found" });

            return Ok(_mapper.Map<ProgramDto>(program));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving program", error = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Tourist")]
    public async Task<ActionResult<ProgramDto>> Create([FromBody] CreateProgramDto createDto)
    {
        try
        {
            var program = _mapper.Map<EgyptVoyage.Domain.Entities.Program>(createDto);
            var createdProgram = await _programRepository.AddAsync(program);
            var programDto = _mapper.Map<ProgramDto>(createdProgram);

            return CreatedAtAction(nameof(GetById), new { id = createdProgram.Id }, programDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating program", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Tourist")]
    public async Task<ActionResult<ProgramDto>> Update(string id, [FromBody] UpdateProgramDto updateDto)
    {
        try
        {
            var existingProgram = await _programRepository.GetByIdAsync(id);
            if (existingProgram == null)
                return NotFound(new { message = "Program not found" });

            _mapper.Map(updateDto, existingProgram);
            var updatedProgram = await _programRepository.UpdateAsync(existingProgram);

            return Ok(_mapper.Map<ProgramDto>(updatedProgram));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating program", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Tourist")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _programRepository.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Program not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting program", error = ex.Message });
        }
    }
}
