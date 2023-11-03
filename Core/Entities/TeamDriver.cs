namespace Core.Entities;

public partial class Teamdriver
{
    public int Idteam { get; set; }
    public Team Team { get; set; }

    public int IdDriver { get; set; }
    public Driver Driver { get; set; }
}
