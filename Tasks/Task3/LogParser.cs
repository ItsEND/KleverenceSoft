using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Task3;

public static class LogParser
{
    public static bool TryParse(string logText, [NotNullWhen(true)] out StandardLogEntry? log)
    {
        log = null;

        return TryParseFormat1(logText, out log)
            || TryParseFormat2(logText, out log);
    }

    public static bool TryParseFormat1(string logText, [NotNullWhen(true)] out StandardLogEntry? log)
    {
        log = null;
        var parts = logText.Split((char[]?)null, 4, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 4)
        {
            return false;
        }

        if (!DateOnly.TryParseExact(parts[0], "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            return false;
        }
        if (!TimeOnly.TryParseExact(parts[1], ["HH:mm:ss.fff", "HH:mm:ss.ffff"], CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return false;
        }
        var time = parts[1];

        if (!TryNormalizeLogLevel(parts[2], out var level))
        {
            return false;
        }

        var message = parts[3];

        log = new StandardLogEntry
        {
            Date = date.ToString("dd-MM-yyyy"),
            Time = time,
            Level = level,
            CallerMethod = "DEFAULT",
            Message = message

        };
        return true;
    }

    public static bool TryParseFormat2(string logText, [NotNullWhen(true)] out StandardLogEntry? log)
    {
        log = null;

        var parts = logText.Split('|', 5);
        if (parts.Length != 5)
        {
            return false;
        }

        var dateTimeParts = parts[0].Split((char[]?)null, 2, StringSplitOptions.RemoveEmptyEntries);
        if (dateTimeParts.Length != 2)
        {
            return false;
        }

        if (!DateOnly.TryParseExact(dateTimeParts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            return false;
        }

        if (!TimeOnly.TryParseExact(dateTimeParts[1], ["HH:mm:ss.fff", "HH:mm:ss.ffff"], CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return false;
        }

        var time = dateTimeParts[1];
        if (!TryNormalizeLogLevel(parts[1], out var level))
        {
            return false;
        }

        var callerMethod = parts[3].Trim();
        if (string.IsNullOrWhiteSpace(callerMethod))
        {
            callerMethod = "DEFAULT";
        }

        var message = parts[4].TrimStart();

        log = new StandardLogEntry
        {
            Date = date.ToString("dd-MM-yyyy"),
            Time = time,
            Level = level,
            CallerMethod = callerMethod,
            Message = message

        };
        return true;
    }

    public static bool TryNormalizeLogLevel(string levelText, [NotNullWhen(true)] out string? level)
    {
        levelText = levelText.Trim().ToUpperInvariant();
        level = levelText switch
        {
            "INFO" or "INFORMATION" => "INFO",
            "WARN" or "WARNING" => "WARN",
            "ERROR" => "ERROR",
            "DEBUG" => "DEBUG",
            _ => null
        };
        return level is not null;
    }
}
