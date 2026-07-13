using System.Text;
using KinoLab07.AI.Controllers;
using KinoLab07.AI.Unity;

namespace KinoLab07.AI.Services
{
    public static class ContextBuilder
    {
        public static string Build()
        {
            StringBuilder sb = new();

            sb.AppendLine(UnityContext.GetCurrentContext());

            sb.AppendLine();

            sb.AppendLine(ProjectController.GetScripts());

            return sb.ToString();
        }
    }
}