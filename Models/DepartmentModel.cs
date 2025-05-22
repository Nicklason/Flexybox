public class DepartmentModel
{
  public required string Name { get; set; }
  public required List<string> Images { get; set; }
  public required AddressModel Address { get; set; }
  public required ContactModel Contact { get; set; }
  public required List<OpeningHoursModel> OpenHours { get; set; }
}
