namespace Task3;

public static class LogFileProcessor
{
    public static void Process(string inputPath, string outputPath, string problemsPath)
    {
        var logs = File.ReadLines(inputPath);

        using StreamWriter swOut = new(outputPath);
        using StreamWriter swProblems = new(problemsPath);

        foreach (var logText in logs)
        {
            if (LogParser.TryParse(logText, out var log))
            {
                swOut.WriteLine(log.ToOutputString());
            }
            else
            {
                swProblems.WriteLine(logText);
            }
        }
    }
}
