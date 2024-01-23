using Models.Modles.Domain;

namespace Models.BaseModles.Domain.Base
{
    /// <summary>
    /// Base model of Questin. No id in the base class
    /// </summary>
    public class QuestionBase<T>
    {
        public string QuestionText { get; set; } = null!;

        public List<T> Answers { get; set; } = null!;
    }
}
