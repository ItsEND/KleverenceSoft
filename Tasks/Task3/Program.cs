using Task3;

var inputPath = args.ElementAtOrDefault(0) ?? "input.txt";
var outputPath = args.ElementAtOrDefault(1) ?? "output.txt";
var problemsPath = args.ElementAtOrDefault(2) ?? "problems.txt";

if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"Файл '{inputPath}' не найден.");
    return;
}

LogFileProcessor.Process(inputPath, outputPath, problemsPath);

