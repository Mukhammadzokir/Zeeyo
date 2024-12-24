namespace Zeeyo.Service.Helpers;

public class TimeHelper
{
    public static DateTime GetCurrentServerTime()
    {
        var date = DateTime.UtcNow;
        return date.AddHours(TimeConstants.UTC);
    }
}
