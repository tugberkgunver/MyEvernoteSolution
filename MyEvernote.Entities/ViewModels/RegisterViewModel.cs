using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myevernote.Entities.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"),Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(25,ErrorMessage ="{0} max. {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("Şifre"),Required(ErrorMessage = "{0} alanı boş geçilemez."), 
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalıdır."), 
            DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"),Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalıdır."), 
            DataType(DataType.Password),Compare("Password",ErrorMessage ="{0} ile {1} uyuşmamaktadır.")]
        public string RePass { get; set; }

        [DisplayName("Email adresi"),Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(70, 
            ErrorMessage = "{0} max. {1} karakter olmalıdır."),
            EmailAddress(ErrorMessage ="{0} alanı için geçerli bir email adresi yazınız.")]
        public string Email { get; set; }


    }
}