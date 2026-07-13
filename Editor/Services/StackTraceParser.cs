using System.Text.RegularExpressions;
using KinoLab07.AI.Models;

namespace KinoLab07.AI.Services
{
    public static class StackTraceParser
    {
        public static ConsoleError Parse(string log)
        {
            ConsoleError error = new ConsoleError();

            error.Message = log;
            error.StackTrace = log;

            Match match = Regex.Match(
                log,
                @"\(at (Assets\/.*?):(\d+)\)");

            if (match.Success)
            {
                error.File = match.Groups[1].Value;
                error.Line = int.Parse(match.Groups[2].Value);
            }

            return error;
        }
    }
}