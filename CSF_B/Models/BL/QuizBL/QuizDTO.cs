using System;
using System.Collections.Generic;

namespace CSF_B.Models.BL.QuizBL
{
    public class QuizDTO
    {
        public QuizDTO() { }

        public QuizDTO(string id, string name, string description1, string description2) 
        {
            this.Id = id;
            this.Name = name;
            this.Description1 = description1 ?? string.Empty;
            this.Description2 = description2 ?? string.Empty;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description1 { get; set; }
        public string? Description2 { get; set; }
    }
}
