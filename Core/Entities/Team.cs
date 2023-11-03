namespace Core.Entities;

public partial class Team : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<Driver> IdDrivers { get; set; } = new List<Driver>();
}
