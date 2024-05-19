using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSF_B.Models.BL.QuizBL
{
    public interface IQuizService
    {
        public Task<List<QuizDTO>> GetQuiz();
        public Task<string> DeleteAsync(string id);
        public Task<string> CreateAsync(QuizDTO dto);
        public Task<string> UpdateAsync(QuizDTO dto);
    }
}