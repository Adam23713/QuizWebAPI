namespace Models.BaseModles.Domain.Base
{
    /// <summary>
    /// Base model of Answer. No id in the base class
    /// </summary>
    public class AnswerBase
    {
        public bool IsRight { get; set; }

        public string AnswerText { get; set; } = null!;
    }
}
