using CSF_B.Models;
using CSF_B.Models.BindingDTO;
using CSF_B.Models.BL.QuizBL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace CSF_B.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuizService _quizService;

        public HomeController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<QuizDTO> listQuiz = await _quizService.GetQuiz();

            foreach (QuizDTO quiz in listQuiz)
            {
                if (quiz.Description2 != string.Empty)
                {
                    quiz.Description2 = "<h6 class=\"card-text\" style=\"color:black; font-weight: 400; margin-left:30px; margin-right:30px; margin-bottom:10px\">"
                                        + quiz.Description2 + "</h6>";
                    quiz.Description2 = quiz.Description2.Replace("\\n",
                        "</h6><h6 class=\"card-text\" style=\"color:black; font-weight: 400; margin-left:30px; margin-right:30px; margin-bottom:10px\">");
                }    
            }    

            return View(listQuiz);
        }

        public async Task<IActionResult> Quiz(int id = 0)
        {
            QuizView viewDTO = new QuizView();

            if (id != 0)
            {
                List<QuizDTO> listQuiz = await _quizService.GetQuiz();

                foreach (QuizDTO quiz in listQuiz)
                {
                    if (quiz.Description2 != string.Empty)
                    {
                        quiz.Description2 = "<h6 class=\"card-text\" style=\"color:black; font-weight: 400; margin-left:30px; margin-right:30px; margin-bottom:10px\">"
                                            + quiz.Description2 + "</h6>";
                        quiz.Description2 = quiz.Description2.Replace("\\n",
                            "</h6><h6 class=\"card-text\" style=\"color:black; font-weight: 400; margin-left:30px; margin-right:30px; margin-bottom:10px\">");
                    }
                }

                viewDTO.ListQuiz = listQuiz;
            }

            return View(viewDTO);
        }

        public IActionResult Video()
        {
            return View();
        }

        public IActionResult Lab()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Theory(int id = 0)
        {
            List<string> pathImg = new List<string>();

            if (id > 0)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/topics/topic" + id.ToString());

                string[] fileNames = Directory.GetFiles(folderPath);

                foreach (string fileName in fileNames)
                {
                    string res = "data:image/jpg;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
                    pathImg.Add(res);
                }
            }

            return View(pathImg);
        }
    }
}
