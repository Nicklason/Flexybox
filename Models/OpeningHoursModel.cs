public class TimeIntervalModel
{
    public required TimeSpan Start { get; set; }
    public required TimeSpan End { get; set; }
}

public class OpeningHoursDayModel
{
    public required string DayName { get; set; }
    public required List<TimeIntervalModel> Intervals { get; set; } = new();

    public bool IsClosed => Intervals.Count == 0;
}

public class OpeningHoursModel
{
    public required string Name { get; set; }
    public required OpeningHoursDayModel Monday { get; set; }
    public required OpeningHoursDayModel Tuesday { get; set; }
    public required OpeningHoursDayModel Wednesday { get; set; }
    public required OpeningHoursDayModel Thursday { get; set; }
    public required OpeningHoursDayModel Friday { get; set; }
    public required OpeningHoursDayModel Saturday { get; set; }
    public required OpeningHoursDayModel Sunday { get; set; }
    public required OpeningHoursDayModel Holiday { get; set; }

    public bool IsOpen(DateTime dateTime)
    {
        var dayOfWeek = dateTime.DayOfWeek;
        var openingHoursDay = dayOfWeek switch
        {
            DayOfWeek.Monday => Monday,
            DayOfWeek.Tuesday => Tuesday,
            DayOfWeek.Wednesday => Wednesday,
            DayOfWeek.Thursday => Thursday,
            DayOfWeek.Friday => Friday,
            DayOfWeek.Saturday => Saturday,
            DayOfWeek.Sunday => Sunday,
            // Should never happen because all days are covered
            _ => throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, null)
        };

        // TODO: Check if the date is a holiday
        var isHoliday = false;

        if (isHoliday)
        {
            // It is a holiday, use the holiday opening hours
            openingHoursDay = Holiday;
        }

        if (openingHoursDay.IsClosed)
        {
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

    // TODO: Collapse days in a row with same opening hours into a single entry
    public List<(string, string)> GetOpeningHoursSummary(int maxCollapse = int.MaxValue)
    {
        var summary = new List<(string, string)>();

        foreach (var day in new[]
        {
            Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, Holiday
        })
        {
            string description;

            if (day.IsClosed)
            {
                description = "Closed";
            }
            else
            {
                description = string.Join(", ", day.Intervals.Select(i => $"{FormatTimeSpan(i.Start)} - {FormatTimeSpan(i.End)}"));
            }

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
