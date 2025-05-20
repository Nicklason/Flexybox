public class AddressModel
{

    public required string Street { get; set; }
    public required string City { get; set; }
    public required int ZipCode { get; set; }

    public string GetStreetAndZipCode()
    {
        return $"{Street}, {ZipCode}";
    }
}
