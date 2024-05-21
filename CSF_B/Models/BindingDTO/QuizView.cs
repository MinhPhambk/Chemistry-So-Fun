using CSF_B.Models.BL.QuizBL;
using CSF_B.Models.BL.QuizDetailBL;

namespace CSF_B.Models.BindingDTO
{
    public class QuizView
    {
        public QuizView() { }

        public QuizView(List<QuizDTO> quizzes, List<QuizDetailDTO> details) 
        {
            ListQuiz = quizzes;
            ListDetail = details;
        }

        public List<QuizDTO> ListQuiz { get; set; } = new List<QuizDTO>();
        public List<QuizDetailDTO> ListDetail { get; set; } = new List<QuizDetailDTO>();
        public string TopicName { get; set; }
    }
}
