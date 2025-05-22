namespace Flexybox.Data;

public class Department
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required ICollection<Image> Images { get; set; }
  public int AddressId { get; set; }
  public required Address Address { get; set; }
  public int ContactId { get; set; }
  public required Contact Contact { get; set; }
  public ICollection<OpeningHours> OpenHours { get; set; } = null!;
}
