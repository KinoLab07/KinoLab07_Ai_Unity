using KinoLab07.AI.Models;

namespace KinoLab07.AI.Services
{
    public static class ConsoleAnalyzer
    {
        public static ConsoleError GetLastError()
        {
            string log = ConsoleCapture.GetLogs();

            return StackTraceParser.Parse(log);
        }

        public static string GetConsoleContext()
        {
            ConsoleError error = GetLastError();

            return
$@"MENSAJE:
{error.Message}

ARCHIVO:
{error.File}

LINEA:
{error.Line}

STACKTRACE:
{error.StackTrace}";
        }
    }
}