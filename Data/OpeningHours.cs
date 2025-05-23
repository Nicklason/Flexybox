namespace Flexybox.Data;

using PublicHoliday;

public class TimeInterval
{
    public int Id { get; set; }
    public required TimeSpan Start { get; set; }
    public required TimeSpan End { get; set; }

    public int OpeningHoursDayId { get; set; }
    public OpeningHoursDay OpeningHoursDay { get; set; } = null!;
}

public class OpeningHoursDay
{
    public int Id { get; set; }
    public required string DayName { get; set; }
    public required ICollection<TimeInterval> Intervals { get; set; }
    public int OpeningHoursId { get; set; }
    public OpeningHours OpeningHours { get; set; } = null!;

    public bool IsClosed => Intervals.Count == 0;
}

public class OpeningHours
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required ICollection<OpeningHoursDay> Days { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public bool IsOpen(DateTime dateTime)
    {
        var isHoliday = new DenmarkPublicHoliday().IsPublicHoliday(dateTime);

        OpeningHoursDay? openingHoursDay;

        if (isHoliday)
        {
            openingHoursDay = Days.FirstOrDefault(d => d.DayName == "Holiday");
        }
        else
        {
            openingHoursDay = Days.FirstOrDefault(d => d.DayName == dateTime.DayOfWeek.ToString());
        }

        if (openingHoursDay == null || openingHoursDay.IsClosed)
        {
            // No opening hours for this day
            return false;
        }

        var currentTime = dateTime.TimeOfDay;

        foreach (var interval in openingHoursDay.Intervals)
        {
            if (currentTime >= interval.Start && currentTime <= interval.End)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsOpenNow() => IsOpen(DateTime.Now);

    public List<(string, string)> GetOpeningHoursSummary(int maxCollapse = int.MaxValue)
    {
        var summary = new List<(string, string)>();

        var days = new Dictionary<string, OpeningHoursDay>
        {
            { "Monday", new OpeningHoursDay { DayName = "Monday", Intervals = new List<TimeInterval>() } },
            { "Tuesday", new OpeningHoursDay { DayName = "Tuesday", Intervals = new List<TimeInterval>() } },
            { "Wednesday", new OpeningHoursDay { DayName = "Wednesday", Intervals = new List<TimeInterval>() } },
            { "Thursday", new OpeningHoursDay { DayName = "Thursday", Intervals = new List<TimeInterval>() } },
            { "Friday", new OpeningHoursDay { DayName = "Friday", Intervals = new List<TimeInterval>() } },
            { "Saturday", new OpeningHoursDay { DayName = "Saturday", Intervals = new List<TimeInterval>() } },
            { "Sunday", new OpeningHoursDay { DayName = "Sunday", Intervals = new List<TimeInterval>() } },
            { "Holiday", new OpeningHoursDay { DayName = "Holiday", Intervals = new List<TimeInterval>() } }
        };

        foreach (var day in Days)
        {
            if (days.ContainsKey(day.DayName))
            {
                days[day.DayName].Intervals = day.Intervals;
            }
        }

        foreach (var dayName in new[]
        {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Holiday"
        })
        {
            var day = days[dayName];
            if (day == null) {
                // This should not happen, but just in case
                throw new Exception($"Day {dayName} not found in opening hours.");
            }

            var intervals = day.Intervals
                .Select(i => $"{FormatTimeSpan(i.Start)} - {FormatTimeSpan(i.End)}")
                .ToList();

            string description = intervals.Count switch
            {
                0 => "Closed",
                1 => intervals[0],
                2 => string.Join(" and ", intervals),
                _ => string.Join(", ", intervals.Take(intervals.Count - 1)) + " and " + intervals.Last()
            };

            summary.Add((day.DayName, description));
        }

        if (maxCollapse > 0)
        {
            var collapsedSummary = new List<(string, string)>();
            string? lastDescription = null, firstDay = null, lastDay = null;

            foreach (var (day, description) in summary)
            {
                if (firstDay == null)
                {
                    firstDay = day;
                    lastDescription = description;
                }

                if (description == lastDescription && maxCollapse > 0)
                {
                    lastDay = day;
                }
                else
                {
                    collapsedSummary.Add((firstDay + (lastDay != null ? " - " + lastDay : ""), lastDescription!));
                    firstDay = day;
                    lastDay = null;
                    lastDescription = description;
                    maxCollapse--;
                }
            }

            if (firstDay != null)
            {
                collapsedSummary.Add((firstDay + (lastDay != null ? " - " + lastDay : ""), lastDescription!));
            }

            summary = collapsedSummary;
        }


        return summary;
    }

    private static string FormatTimeSpan(TimeSpan timeSpan)
    {
        return timeSpan.ToString(@"hh\:mm");
    }
}
