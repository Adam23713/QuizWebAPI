﻿using Models.Interfaces;
using Models.Models.Domain.Base;
using Models.Models.DTO;

namespace Models.Response.Quiz
{
    public class GetQuizForGameResponse : QuizeBase<QuestionForGame>, IIdentity
    {
        public int Id { get; set; } //QuizId

        public int UserId { get; set; }

        public int BeginQuestionIndex { get; set; }

        public string ApiURL { get; set; } = null!;
    }

    public class QuestionForGame : QuestionBase<CroppedAnswerDTO>, IIdentity
    {
        public int Id { get; set; }
    }
}
