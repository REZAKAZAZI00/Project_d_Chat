using Project_d_Chat.Core.DTOs.Chat;
using Project_d_Chat.Core.Security;
using System.Diagnostics;

namespace Project_d_Chat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly ChatDbContext _chatDbContext;
        private readonly IUserService _userService;

        public HomeController(IChatRoomService chatRoomService,ChatDbContext chatDbContext, IUserService userService)
        {
            _chatRoomService = chatRoomService;
            _chatDbContext = chatDbContext;
            _userService = userService;
        }
        [HttpGet("/")]
        [HttpGet("index")]
        [HttpGet("home/index")]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
               
               return RedirectToRoute("/Login");
            }
            return View();
        }

        [HttpGet("GetAllMassage")]
        public async Task<IActionResult> GetAllMassage()
        {
           

            var listMassage = _chatDbContext.ChatMassages.Select(c=> new ChatViewModel() 
            { 
                 sendAt=c.SendAt,
             name=_chatDbContext.Users.Single(u=> u.UserId== c.SerderID).UserNmae,
              text=AlgorithmRSA.Decryption(c.Text),
            }).ToList();
           

            return Json(listMassage);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}