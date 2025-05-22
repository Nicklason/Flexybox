namespace Flexybox.Data;

public class Department
{
  public int Id { get; set; }
  public string Name { get; set; }
  public ICollection<Image> Images { get; set; }
  public int AddressId { get; set; }
  public Address Address { get; set; }
  public int ContactId { get; set; }
  public Contact Contact { get; set; }
  public ICollection<OpeningHours> OpenHours { get; set; }
}
