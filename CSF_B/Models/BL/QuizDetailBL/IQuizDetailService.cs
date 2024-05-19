using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSF_B.Models.BL.QuizDetailBL
{
    public interface IQuizDetailService
    {
        public Task<List<QuizDetailDTO>> GetQuizDetail();
        public Task<List<QuizDetailDTO>> GetQuizDetailByTopic(string topicId);
        public Task<string> DeleteAsync(int id);
        public Task<string> CreateAsync(QuizDetailDTO dto);
        public Task<string> UpdateAsync(QuizDetailDTO dto);
    }
}