namespace Models.Models.Domain.Base
{
    /// <summary>
    /// Base model of Answer. No id in the base class
    /// </summary>
    public class AnswerBase : CroppedAnswer
    {
        public bool IsRight { get; set; }
    }

    public class CroppedAnswer
    {
        public string AnswerText { get; set; } = null!;
    }
}
