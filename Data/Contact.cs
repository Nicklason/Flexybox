namespace Flexybox.Data;

public class Contact
{
    public int Id { get; set; }
    public string PhoneCountryCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool HasSupportChat { get; set; }

    public string GetPhoneNumber()
    {
        return $"+{PhoneCountryCode} {PhoneNumber}";
    }
}
