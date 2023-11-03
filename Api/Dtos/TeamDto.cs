using Core.Entities;

namespace Api.Dtos;

public class TeamDto : BaseEntity
{
    public string Name { get; set; } = null!;
}
