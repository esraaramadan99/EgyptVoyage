using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Program;
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

    // =====================================
    // GET: api/programs
    // =====================================
    [HttpGet]
    public async Task<ActionResult<List<ProgramDto>>> GetAll()
    {
        var programs = await _programRepository.GetAllAsync();
        var programsDto = _mapper.Map<List<ProgramDto>>(programs);

        return Ok(programsDto);
    }

    // =====================================
    // GET: api/programs/{id}
    // عرض برنامج واحد
    // =====================================
    [HttpGet("{id}")]
    public async Task<ActionResult<ProgramDto>> GetById(string id)
    {
        var program = await _programRepository.GetByIdAsync(id);

        if (program == null)
            return NotFound(new { message = "Program not found" });

        var programDto = _mapper.Map<ProgramDto>(program);

        return Ok(programDto);
    }
}
