using Core.Entities;

namespace Api.Dtos;

public class DriverDto : BaseEntity
{
    public string Name { get; set; }
    public int Age { get; set; }
}
