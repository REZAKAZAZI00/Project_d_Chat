
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project_d_Chat.Core.DTOs.User
{
    public class LoginViewModel
    {
#nullable disable
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(6, ErrorMessage = "{0}نمی تواند کم تر از {1}کلمه باشد")]
      public string Password { get; set; }
        [Display(Name = "نام کاربری")]
        [MaxLength(110, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public string Username { get; set; }
       

       
    }
    public class RegiserViewModel
    {
        [Remote("IsExistUsername", "Account")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]

        public string UserName { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(6, ErrorMessage = "{0}نمی تواند کم تر از {1}کلمه باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "کلمه های عبور یکسان نیست")]

        public string RePassword { get; set; }

    }

}
