using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Models.Domain.Base
{
    /// <summary>
    /// Base model of User Answer. No id in the base class
    /// </summary>

    [Keyless]
    public class UserAnswerBase
    {
        public int UserId { get; set; }

        public int QuizId { get; set; }

        public int QuestionId { get; set; }

        [JsonIgnore]
        public string Answers
        {
            get { return string.Join(";", AnswersList); }
            set 
            {
                try
                {
                    AnswersList = value.Split(';')
                                    .Where(s => !string.IsNullOrWhiteSpace(s))
                                    .Select(int.Parse)
                                    .ToList();
                }
                catch (Exception ex) 
                {
                    AnswersList = new List<int>();
                }
            }
        }

        [NotMapped]
        public List<int> AnswersList { get; set; } = null!;
    }
}
