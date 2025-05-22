public static class RestaurantService
{
  private static List<TimeIntervalModel> SevenToTwentyTwo = new List<TimeIntervalModel>() {
    new TimeIntervalModel
    {
      Start = new TimeSpan(7, 0, 0),
      End = new TimeSpan(22, 0, 0)
    }
  };

  private static List<TimeIntervalModel> TwelveToMidnight = new List<TimeIntervalModel>() {
    new TimeIntervalModel
    {
      Start = new TimeSpan(12, 0, 0),
      End = new TimeSpan(24, 0, 0)
    }
  };

  private static List<TimeIntervalModel> MultipleIntervals = new List<TimeIntervalModel>() {
    new TimeIntervalModel
    {
      Start = new TimeSpan(7, 0, 0),
      End = new TimeSpan(12, 0, 0)
    },
    new TimeIntervalModel
    {
      Start = new TimeSpan(16, 0, 0),
      End = new TimeSpan(22, 0, 0)
    },
  };

  private static List<RestaurantModel> Restaurants = new()
  {
    new RestaurantModel
    {
      Name = "Aalborg",
      Images = new List<string> {
        "/red.png",
        "/green.png",
        "/blue.png",
        "/red.png",
        "/green.png",
        "/blue.png",
        "/red.png",
        "/green.png",
        "/blue.png"
      },
      Address = new AddressModel {
        Street = "Østerågade 27",
        City = "Aalborg",
        ZipCode = 9000
      },
      Contact = new ContactModel {
        PhoneCountryCode = "45",
        PhoneNumber = "11 22 33 44",
        Email = "aalborg@flexybox.com",
        HasSupportChat = true
      },
      OpenHours = new [] { "Restaurant", "Takeaway", "Buffet", "Special Events" }.Select((name) =>
        new OpeningHoursModel
        {
          Name = name,
          Monday = new OpeningHoursDayModel
          {
            DayName = "Monday",
            Intervals = SevenToTwentyTwo
          },
          Tuesday = new OpeningHoursDayModel
          {
            DayName = "Tuesday",
            Intervals = SevenToTwentyTwo
          },
          Wednesday = new OpeningHoursDayModel
          {
            DayName = "Wednesday",
            Intervals = SevenToTwentyTwo
          },
          Thursday = new OpeningHoursDayModel
          {
            DayName = "Thursday",
            Intervals = SevenToTwentyTwo
          },
          Friday = new OpeningHoursDayModel
          {
            DayName = "Friday",
            Intervals = name == "Restaurant" ? SevenToTwentyTwo : TwelveToMidnight
          },
          Saturday = new OpeningHoursDayModel
          {
            DayName = "Saturday",
            Intervals = name == "Restaurant" ? SevenToTwentyTwo : TwelveToMidnight
          },
          Sunday = new OpeningHoursDayModel
          {
            DayName = "Sunday",
            Intervals = name == "Restaurant" ? SevenToTwentyTwo : MultipleIntervals
          },
          Holiday = new OpeningHoursDayModel
          {
            DayName = "Holiday",
            Intervals = new () {}
          }
        }).ToList()
    },
  };

  public static RestaurantModel? GetByName(string name)
  {
    return Restaurants.FirstOrDefault(r => r.Name == name);
  }

  public static List<RestaurantModel> GetAll()
  {
    return Restaurants;
  }
}
