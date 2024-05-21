using CSF_B.Models;
using CSF_B.Models.BindingDTO;
using CSF_B.Models.BL.QuizBL;
using CSF_B.Models.BL.QuizDetailBL;
using CSF_B.Models.BL.VideoBL;
using CSF_B.Models.DL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace CSF_B.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuizService _quizService;
        private readonly IQuizDetailService _quizDetailService;
        private readonly IVideoService _videoService;

        public HomeController(IQuizService quizService, IQuizDetailService quizDetailService, IVideoService videoService)
        {
            _quizService = quizService;
            _quizDetailService = quizDetailService;
            _videoService = videoService;
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

        public async Task<IActionResult> Quiz(string id)
        {
            QuizView viewDTO = new QuizView();

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

            if (id != string.Empty)
            {
                List<QuizDetailDTO> listQuizDetail = await _quizDetailService.Get10QuizDetailByTopic(id);
                if (listQuizDetail.Count > 0)
                {
                    foreach (QuizDetailDTO quiz in listQuizDetail)
                    {
                        quiz.Question = quiz.Question.Replace("\\n", "<br/>");
                        quiz.Answer1 = quiz.Answer1.Replace("\\n", "<br/>");
                        quiz.Answer2 = quiz.Answer2.Replace("\\n", "<br/>");
                        quiz.Answer3 = quiz.Answer3.Replace("\\n", "<br/>");
                        quiz.Answer4 = quiz.Answer4.Replace("\\n", "<br/>");

                        quiz.Question = ConvertFormular(quiz.Question);
                        quiz.Answer1 = ConvertFormular(quiz.Answer1);
                        quiz.Answer2 = ConvertFormular(quiz.Answer2);
                        quiz.Answer3 = ConvertFormular(quiz.Answer3);
                        quiz.Answer4 = ConvertFormular(quiz.Answer4);
                    }
                    viewDTO.TopicName = listQuiz.FirstOrDefault(q => q.Id == id).Name;
                }
                viewDTO.ListDetail = listQuizDetail;
            }

            return View(viewDTO);
        }

        public async Task<IActionResult> Video()
        {
            string baseUrl = "https://img.youtube.com/vi/";
            string maxresUrl = "/maxresdefault.jpg";
            List<VideoDTO> listVideo = await _videoService.GetVideo();

            foreach (VideoDTO vid in listVideo)
            {
                int startIndex = vid.Link.IndexOf("?v=");

                if (startIndex != -1)
                {
                    vid.LinkImg = vid.Link.Substring(startIndex + 3);

                    int ampersandIndex = vid.LinkImg.IndexOf('&');
                    if (ampersandIndex != -1)
                    {
                        vid.LinkImg = vid.LinkImg.Substring(0, ampersandIndex);
                    }

                    vid.LinkImg = baseUrl + vid.LinkImg + maxresUrl;
                }
            }    

            return View(listVideo);
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

        public string ConvertFormular(string input)
        {
            StringBuilder output = new StringBuilder();
            bool isSubscript = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '$')
                {
                    isSubscript = !isSubscript;
                    continue;
                }

                if (isSubscript && char.IsDigit(input[i]))
                {
                    output.Append("<sub>");
                    output.Append(input[i]);
                    output.Append("</sub>");
                }
                else
                {
                    output.Append(input[i]);
                }
            }

            return output.ToString();
        }

        public async Task<IActionResult> Login(UserLogin model)
        {
            if (model.UserName == "sa" && model.Password == "123456")
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties()
                    {
                        //IsPersistent = objLoginModel.RememberLogin
                    }
                );

                return RedirectToAction("Admin");
            }

            return RedirectToAction("LoginPage", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("LoginPage", "Home");
        }

        public IActionResult LoginPage()
        {
            return View();
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            QuizView viewDTO = new QuizView();
            List<QuizDTO> listQuiz = await _quizService.GetQuiz();
            List<QuizDetailDTO> listQuizDetail = await _quizDetailService.GetQuizDetail();

            viewDTO.ListQuiz = listQuiz;
            viewDTO.ListDetail = listQuizDetail.OrderByDescending(q => q.Id).ToList();

            return View(viewDTO);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteQuiz([FromBody] string id)
        {
            int idx = Convert.ToInt32(id);
            try
            {
                string re = await _quizDetailService.DeleteAsync(idx);
                return Json(re);
            }
            catch
            {
                return Json("error");
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateQuiz([FromBody] QuizDetailDTO dto)
        {
            try
            {
                string re = await _quizDetailService.UpdateAsync(dto);
                return Json(re);
            }
            catch
            {
                return Json("error");
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddQuiz([FromBody] QuizDetailDTO dto)
        {
            try
            {
                string re = await _quizDetailService.CreateAsync(dto);
                return Json(re);
            }
            catch
            {
                return Json("error");
            }
        }
    }
}
