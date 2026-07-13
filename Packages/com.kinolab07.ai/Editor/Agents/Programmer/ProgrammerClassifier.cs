namespace KinoLab07.AI.Agents.Programmer
{
    public static class ProgrammerClassifier
    {
        public static ProgrammerAction Classify(string prompt)
        {
            string p = prompt.ToLower();

            if (p.Contains("crear script") ||
                p.Contains("create script") ||
                p.Contains("genera un script"))
                return ProgrammerAction.CreateScript;

            if (p.Contains("modifica") ||
                p.Contains("modificar") ||
                p.Contains("cambia este script"))
                return ProgrammerAction.ModifyScript;

            if (p.Contains("explica") ||
                p.Contains("cómo funciona") ||
                p.Contains("como funciona"))
                return ProgrammerAction.ExplainCode;

            if (p.Contains("error") ||
                p.Contains("exception") ||
                p.Contains("nullreference"))
                return ProgrammerAction.AnalyzeError;

            if (p.Contains("buscar clase") ||
                p.Contains("find class"))
                return ProgrammerAction.FindClass;

            if (p.Contains("buscar método") ||
                p.Contains("buscar metodo") ||
                p.Contains("find method"))
                return ProgrammerAction.FindMethod;

            if (p.Contains("buscar variable") ||
                p.Contains("find variable"))
                return ProgrammerAction.FindVariable;

            if (p.Contains("optimiza"))
                return ProgrammerAction.OptimizeCode;

            if (p.Contains("refactor"))
                return ProgrammerAction.RefactorCode;

            return ProgrammerAction.GeneralQuestion;
        }
    }
}