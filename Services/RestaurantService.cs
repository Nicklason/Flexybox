public static class RestaurantService
{
  private static List<TimeIntervalModel> SevenToTwentTwo = new List<TimeIntervalModel>() {
    new TimeIntervalModel
    {
      Start = new TimeSpan(7, 0, 0),
      End = new TimeSpan(22, 0, 0)
    }
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
            Intervals = SevenToTwentTwo
          },
          Tuesday = new OpeningHoursDayModel
          {
            DayName = "Tuesday",
            Intervals = SevenToTwentTwo
          },
          Wednesday = new OpeningHoursDayModel
          {
            DayName = "Wednesday",
            Intervals = SevenToTwentTwo
          },
          Thursday = new OpeningHoursDayModel
          {
            DayName = "Thursday",
            Intervals = SevenToTwentTwo
          },
          Friday = new OpeningHoursDayModel
          {
            DayName = "Friday",
            Intervals = SevenToTwentTwo
          },
          Saturday = new OpeningHoursDayModel
          {
            DayName = "Saturday",
            Intervals = SevenToTwentTwo
          },
          Sunday = new OpeningHoursDayModel
          {
            DayName = "Sunday",
            Intervals = SevenToTwentTwo
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
