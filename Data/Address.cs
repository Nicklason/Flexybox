namespace Flexybox.Data;

public class Address
{
    public int Id { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required int ZipCode { get; set; }

    public string GetStreetAndZipCode()
    {
        return $"{Street}, {ZipCode}";
    }
}
