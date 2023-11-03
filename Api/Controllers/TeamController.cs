using Api.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TeamController : BaseApiController
{
    private readonly IUnitOfWork _unitofwork;
    private readonly IMapper _mapper;

    public TeamController(IUnitOfWork unitofwork, IMapper mapper)
    {
        _unitofwork = unitofwork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TeamDto>>> Get()
    {
        var team = await _unitofwork.Teams.GetAllAsync();
        return _mapper.Map<List<TeamDto>>(team);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeamDto>> Get(int id)
    {
        var team = await _unitofwork.Teams.GetByIdAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        return _mapper.Map<TeamDto>(team);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Team>> Post(TeamDto teamDto)
    {
        var team = _mapper.Map<Team>(teamDto);
        _unitofwork.Teams.Add(team);
        await _unitofwork.SaveAsync();
        if (team == null)
        {
            return BadRequest();
        }
        teamDto.Id = team.Id;
        return CreatedAtAction(nameof(Post), new { id = teamDto.Id }, teamDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeamDto>> Put(int id, [FromBody] TeamDto teamDto)
    {
        if (teamDto == null)
        {
            return NotFound();
        }
        var team = _mapper.Map<Team>(teamDto);
        _unitofwork.Teams.Update(team);
        await _unitofwork.SaveAsync();
        return teamDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var team = await _unitofwork.Teams.GetByIdAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        _unitofwork.Teams.Remove(team);
        await _unitofwork.SaveAsync();
        return NoContent();
    }
}
