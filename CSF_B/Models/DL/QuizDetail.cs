using System;
using System.Collections.Generic;

namespace CSF_B.Models.DL
{
    public partial class QuizDetail
    {
        public int Id { get; set; }
        public string TopicId { get; set; } = null!;
        public string Question { get; set; } = null!;
        public string Answer1 { get; set; } = null!;
        public string Answer2 { get; set; } = null!;
        public string Answer3 { get; set; } = null!;
        public string Answer4 { get; set; } = null!;
        public int Answer { get; set; }

        public virtual Quiz Topic { get; set; } = null!;
    }
}
