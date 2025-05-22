using Flexybox.Data;

public static class DepartmentsService
{
  private static List<TimeInterval> SevenToTwentyTwo = new List<TimeInterval>() {
    new TimeInterval
    {
      Start = new TimeSpan(7, 0, 0),
      End = new TimeSpan(22, 0, 0)
    }
  };

  private static List<TimeInterval> TwelveToMidnight = new List<TimeInterval>() {
    new TimeInterval
    {
      Start = new TimeSpan(12, 0, 0),
      End = new TimeSpan(24, 0, 0)
    }
  };

  private static List<TimeInterval> MultipleIntervals = new List<TimeInterval>() {
    new TimeInterval
    {
      Start = new TimeSpan(7, 0, 0),
      End = new TimeSpan(12, 0, 0)
    },
    new TimeInterval
    {
      Start = new TimeSpan(16, 0, 0),
      End = new TimeSpan(22, 0, 0)
    },
  };

  private static List<Department> Departments = new()
  {
    new Department
    {
      Name = "Aalborg",
      Images = new List<Image> {
        new Image
        {
          Url = "/red.png"
        },
        new Image
        {
          Url = "/green.png"
        },
        new Image
        {
          Url = "/blue.png"
        },
        new Image
        {
          Url = "/red.png"
        },
        new Image
        {
          Url = "/green.png"
        },
        new Image
        {
          Url = "/blue.png"
        },
        new Image
        {
          Url = "/red.png"
        },
        new Image
        {
          Url = "/green.png"
        },
        new Image
        {
          Url = "/blue.png"
        },
      },
      Address = new Address {
        Street = "Østerågade 27",
        City = "Aalborg",
        ZipCode = 9000
      },
      Contact = new Contact {
        PhoneCountryCode = "45",
        PhoneNumber = "11 22 33 44",
        Email = "aalborg@flexybox.com",
        HasSupportChat = true
      },
      OpenHours = new [] { "Restaurant", "Takeaway", "Buffet", "Special Events" }.Select((name) =>
        new OpeningHours
        {
          Name = name,
          Days = new List<OpeningHoursDay> {new OpeningHoursDay
          {
            DayName = "Monday",
            Intervals = SevenToTwentyTwo
          },
          new OpeningHoursDay
          {
            DayName = "Tuesday",
            Intervals = SevenToTwentyTwo
          },
          new OpeningHoursDay
          {
            DayName = "Wednesday",
            Intervals = SevenToTwentyTwo
          },
          new OpeningHoursDay
          {
            DayName = "Thursday",
            Intervals = SevenToTwentyTwo
          },
          new OpeningHoursDay
          {
            DayName = "Friday",
            Intervals = name == "Restaurant" ? SevenToTwentyTwo : TwelveToMidnight
          },
          new OpeningHoursDay
          {
            DayName = "Saturday",
            Intervals = name == "Restaurant" ? SevenToTwentyTwo : TwelveToMidnight
          },
          new OpeningHoursDay
          {
            DayName = "Sunday",
            Intervals = name == "Restaurant" ? SevenToTwentyTwo : MultipleIntervals
          },
          new OpeningHoursDay
          {
            DayName = "Holiday",
            Intervals = new List<TimeInterval>() {}
          }
        } }).ToList()
    },
  };

  public static Department? GetByName(string name)
  {
    return Departments.FirstOrDefault(r => r.Name == name);
  }

  public static List<Department> GetAll()
  {
    return Departments;
  }
}
