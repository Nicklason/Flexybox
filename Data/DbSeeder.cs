using Flexybox.Data;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    private static List<TimeInterval> CreateSevenToTwentyTwo() => new List<TimeInterval>() {
        new TimeInterval
        {
            Start = new TimeSpan(7, 0, 0),
            End = new TimeSpan(22, 0, 0)
        }
    };

    private static List<TimeInterval> CreateTwelveToMidnight() => new List<TimeInterval>() {
        new TimeInterval
        {
            Start = new TimeSpan(12, 0, 0),
            End = new TimeSpan(24, 0, 0)
        }
    };

    private static List<TimeInterval> CreateMultipleIntervals() => new List<TimeInterval>() {
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

    private static OpeningHours CreateOpeningHours(string name)
    {
        return new OpeningHours
        {
            Name = name,
            Days = new List<OpeningHoursDay>
            {
                new() { DayName = "Monday", Intervals = CreateSevenToTwentyTwo() },
                new() { DayName = "Tuesday", Intervals = CreateSevenToTwentyTwo() },
                new() { DayName = "Wednesday", Intervals = CreateSevenToTwentyTwo() },
                new() { DayName = "Thursday", Intervals = CreateSevenToTwentyTwo() },
                new() { DayName = "Friday", Intervals = name == "Restaurant" ? CreateSevenToTwentyTwo() : CreateTwelveToMidnight() },
                new() { DayName = "Saturday", Intervals = name == "Restaurant" ? CreateSevenToTwentyTwo() : CreateTwelveToMidnight() },
                new() { DayName = "Sunday", Intervals = name == "Restaurant" ? CreateSevenToTwentyTwo() : CreateMultipleIntervals() },
                new() { DayName = "Holiday", Intervals = new List<TimeInterval>() }
            }
        };
    }

    public static async Task SeedAsync(AppDbContext db)
    {
        var count = await db.Department.CountAsync();
        if (count > 0)
        {
            return;
        }

        var department = new Department
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
            },
            Address = new Address
            {
                Street = "Østerågade 27",
                City = "Aalborg",
                ZipCode = 9000
            },
            Contact = new Contact
            {
                PhoneCountryCode = "45",
                PhoneNumber = "11 22 33 44",
                Email = "aalborg@flexybox.com",
                HasSupportChat = true
            },
            OpenHours = new List<OpeningHours>
            {
                CreateOpeningHours("Restaurant"),
                CreateOpeningHours("Takeaway"),
                CreateOpeningHours("Buffet"),
                CreateOpeningHours("Special Events")
            }
        };

        await db.Department.AddAsync(department);
        await db.SaveChangesAsync();
    }
}