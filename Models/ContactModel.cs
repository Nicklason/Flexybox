public class ContactModel
{
    public required string PhoneCountryCode { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required bool HasSupportChat { get; set; }

    public string GetPhoneNumber()
    {
        return $"+{PhoneCountryCode} {PhoneNumber}";
    }
}
