namespace Flexybox.Data;

public class Image
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}