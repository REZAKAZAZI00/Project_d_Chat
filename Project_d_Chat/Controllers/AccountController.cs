
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

using System.Security.Claims;
using Project_d_Chat.DataLeaer.Entities.User;
using Project_d_Chat.Core.Security;

namespace Project_d_Chat.Controllers
{
    public class AccountController : Controller
    {
#nullable enable
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;   
        }
        #region Register
        [HttpGet,Route("Register"),Route("Account/Register")]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost,Route("Register")]
        public async Task<IActionResult> Register(RegiserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_userService.IsExistUsername(model.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(model);
            }
            DataLeaer.Entities.User.User user=new User()
            {
                 Password=PasswordHelper.EncodePasswordSHA1(model.Password),
                 UserNmae= model.UserName,
            };
            _userService.AddUser(user);

            return View("Login");
        }
        #endregion
        #region Login
        [HttpGet]
        [Route("Login")]
        [Route("Account/Login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login"), Route("Account/Login")]
        public async Task<IActionResult> Login(LoginViewModel login, string ReturnUrl = "/")
        {


            var user = _userService.LoginUser(login);
            if (user != null)
            {

                
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserNmae),

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };
                    _ = HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    if (ReturnUrl != "/")
                    {
                        return Redirect(ReturnUrl);
                    }
                    return View();
                
                
               
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");

            return View();
        }


        #endregion
        #region Logout
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            _ = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion

        public async Task<IActionResult> IsExistUsername(string UserName)
        {
            var user = _userService.IsExistUsername(UserName);
            if (user == false) { return Json(true); }
            return Json("نام کاربری وارد شده از قبل موجود است");
        }
    }
}
