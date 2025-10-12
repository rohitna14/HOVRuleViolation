namespace HOVLaneViolation.Entities;

public class HOVMasterData
{
    public int Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Weight { get; set; }
}