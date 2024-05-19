using System;
using System.Collections.Generic;

namespace CSF_B.Models.BL.QuizDetailBL
{
    public class QuizDetailDTO
    {
        public QuizDetailDTO() { }

        public QuizDetailDTO(int id, string topicId, string question, string answer1, string answer2, string answer3, string answer4, int answer) 
        {
            this.Id = id;
            this.TopicId = topicId;
            this.Question = question;
            this.Answer1 = answer1;
            this.Answer2 = answer2;
            this.Answer3 = answer3;
            this.Answer4 = answer4;
            this.Answer = answer;
        }

        public int Id { get; set; }
        public string TopicId { get; set; } = null!;
        public string Question { get; set; } = null!;
        public string Answer1 { get; set; } = null!;
        public string Answer2 { get; set; } = null!;
        public string Answer3 { get; set; } = null!;
        public string Answer4 { get; set; } = null!;
        public int Answer { get; set; }
    }
}
