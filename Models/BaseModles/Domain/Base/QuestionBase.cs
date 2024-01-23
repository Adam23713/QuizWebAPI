using Models.Modles.Domain;

namespace Models.BaseModles.Domain.Base
{
    /// <summary>
    /// Base model of Questin. No id in the base class
    /// </summary>
    public class QuestionBase
    {
        public string QuestionText { get; set; } = null!;
    }
}
