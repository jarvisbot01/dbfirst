using Api.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class DriverController : BaseApiController
{
    private readonly IUnitOfWork _unitofwork;
    private readonly IMapper _mapper;

    public DriverController(IUnitOfWork unitofwork, IMapper mapper)
    {
        _unitofwork = unitofwork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DriverDto>>> Get()
    {
        var driver = await _unitofwork.Drivers.GetAllAsync();
        return _mapper.Map<List<DriverDto>>(driver);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DriverDto>> Get(int id)
    {
        var driver = await _unitofwork.Drivers.GetByIdAsync(id);
        if (driver == null)
        {
            return NotFound();
        }
        return _mapper.Map<DriverDto>(driver);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Driver>> Post(DriverDto driverDto)
    {
        var driver = _mapper.Map<Driver>(driverDto);
        _unitofwork.Drivers.Add(driver);
        await _unitofwork.SaveAsync();
        if (driver == null)
        {
            return BadRequest();
        }
        driverDto.Id = driver.Id;
        return CreatedAtAction(nameof(Post), new { id = driverDto.Id }, driverDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DriverDto>> Put(int id, [FromBody] DriverDto driverDto)
    {
        if (driverDto == null)
        {
            return NotFound();
        }
        var driver = _mapper.Map<Driver>(driverDto);
        _unitofwork.Drivers.Update(driver);
        await _unitofwork.SaveAsync();
        return driverDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var driver = await _unitofwork.Drivers.GetByIdAsync(id);
        if (driver == null)
        {
            return NotFound();
        }
        _unitofwork.Drivers.Remove(driver);
        await _unitofwork.SaveAsync();
        return NoContent();
    }
}
