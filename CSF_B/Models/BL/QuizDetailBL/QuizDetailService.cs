
using CSF_B.Models.DL;
using Microsoft.EntityFrameworkCore;

namespace CSF_B.Models.BL.QuizDetailBL
{
    public class QuizDetailService : IQuizDetailService
    {
        private readonly AppDBContext dbContext;

        public QuizDetailService(AppDBContext appDbContext)
        {
            dbContext = appDbContext;
        }

        public async Task<string> CreateAsync(QuizDetailDTO dto)
        {
            List<QuizDetailDTO> quizDetails = await GetQuizDetail();
            string re = "done";
            try
            {
                QuizDetail quizDetail = new QuizDetail();
                quizDetail.Id = quizDetails.OrderByDescending(q => q.Id).FirstOrDefault().Id + 1;
                quizDetail.TopicId = dto.TopicId;
                quizDetail.Question = dto.Question;
                quizDetail.Answer1 = dto.Answer1;
                quizDetail.Answer2 = dto.Answer2;
                quizDetail.Answer3 = dto.Answer3;
                quizDetail.Answer4 = dto.Answer4;
                quizDetail.Answer = dto.Answer;

                dbContext.QuizDetails.Add(quizDetail);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }

        public async Task<string> DeleteAsync(int id)
        {
            string re = "done";
            try
            {
                QuizDetail quizDetail = await dbContext.QuizDetails.FindAsync(id);

                dbContext.QuizDetails.Remove(quizDetail);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }

        public async Task<List<QuizDetailDTO>> GetQuizDetail()
        {
            List<QuizDetail> listQuizDetail = await dbContext.QuizDetails.ToListAsync();
            List<QuizDetailDTO> listQuizDetailDTO = new List<QuizDetailDTO>();

            if (listQuizDetail != null)
            {
                foreach (QuizDetail quizDetail in listQuizDetail)
                {
                    listQuizDetailDTO.Add(new QuizDetailDTO(quizDetail.Id, quizDetail.TopicId, quizDetail.Question, quizDetail.Answer1, quizDetail.Answer2, quizDetail.Answer3, quizDetail.Answer4, quizDetail.Answer));
                }
            }

            return listQuizDetailDTO;
        }

        public async Task<List<QuizDetailDTO>> Get10QuizDetailByTopic(string topicId)
        {
            List<QuizDetail> listQuizDetail = await dbContext.QuizDetails.ToListAsync();
            List<QuizDetailDTO> listQuizDetailDTO = new List<QuizDetailDTO>();

            if (listQuizDetail != null)
            {
                foreach (QuizDetail quizDetail in listQuizDetail)
                {
                    if (quizDetail.TopicId == topicId)
                        listQuizDetailDTO.Add(new QuizDetailDTO(quizDetail.Id, quizDetail.TopicId, quizDetail.Question, quizDetail.Answer1, quizDetail.Answer2, quizDetail.Answer3, quizDetail.Answer4, quizDetail.Answer));
                }
            }

            List<QuizDetailDTO> quiz10Random = new List<QuizDetailDTO>();
            Random rng = new Random();
            Func<QuizDetailDTO, int> orderByLambda = x => rng.Next();
            quiz10Random = listQuizDetailDTO.OrderBy(orderByLambda).Take(10).ToList();

            return quiz10Random;
        }

        public async Task<string> UpdateAsync(QuizDetailDTO dto)
        {
            string re = "done";
            try
            {
                QuizDetail quizDetail = new QuizDetail();
                quizDetail.Id = dto.Id;
                quizDetail.TopicId = dto.TopicId;
                quizDetail.Question = dto.Question;
                quizDetail.Answer1 = dto.Answer1;
                quizDetail.Answer2 = dto.Answer2;
                quizDetail.Answer3 = dto.Answer3;
                quizDetail.Answer4 = dto.Answer4;
                quizDetail.Answer = dto.Answer;

                dbContext.QuizDetails.Update(quizDetail);
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
