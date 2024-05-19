
using CSF_B.Models.DL;
using Microsoft.EntityFrameworkCore;

namespace CSF_B.Models.BL.QuizBL
{
    public class QuizService : IQuizService
    {
        private readonly AppDBContext dbContext;

        public QuizService(AppDBContext appDbContext)
        {
            dbContext = appDbContext;
        }

        public async Task<string> CreateAsync(QuizDTO dto)
        {
            string re = "done";
            try
            {
                Quiz quiz = new Quiz();
                quiz.Id = dto.Id;
                quiz.Name = dto.Name;
                quiz.Description1 = dto.Description1;
                quiz.Description2 = dto.Description2;

                dbContext.Quizzes.Add(quiz);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }

        public async Task<string> DeleteAsync(string id)
        {
            string re = "done";
            try
            {
                Quiz quiz = await dbContext.Quizzes.FindAsync(id);

                dbContext.Quizzes.Remove(quiz);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }

        public async Task<List<QuizDTO>> GetQuiz()
        {
            List<Quiz> listQuiz = await dbContext.Quizzes.ToListAsync();
            List<QuizDTO> listQuizDTO = new List<QuizDTO>();

            if (listQuiz != null)
            {
                foreach (Quiz quiz in listQuiz)
                {
                    listQuizDTO.Add(new QuizDTO(quiz.Id, quiz.Name, quiz.Description1, quiz.Description2));
                }
            }

            return listQuizDTO;
        }

        public async Task<string> UpdateAsync(QuizDTO dto)
        {
            string re = "done";
            try
            {
                Quiz quiz = new Quiz();
                quiz.Id = dto.Id;
                quiz.Name = dto.Name;
                quiz.Description1 = dto.Description1;
                quiz.Description2 = dto.Description2;

                dbContext.Quizzes.Update(quiz);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }
    }
}
