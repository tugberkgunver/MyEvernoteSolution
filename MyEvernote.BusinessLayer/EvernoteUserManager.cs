using Myevernote.Entities.ViewModels;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
   public class EvernoteUserManager
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();

        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
        {

            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
           layerResult.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);

           

            if (layerResult.Result != null)
            {
                if (!layerResult.Result.IsActive)
                {
                    layerResult.Errors.Add("Kullanıcı aktifleştirilmemiştir. Lütfen e-mailinizi kontrol ediniz.");
                }
                
            }
            else
            {
                layerResult.Errors.Add("Kullanıcı adı yada şifre uyuşmuyor.");

            }
            return layerResult;

        }

        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {
            //Kullanıcı Username ve email kontrolü
            //Kayıt işlemi
            //Aktivasyon email

            EvernoteUser user = repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);

            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            //user null değilse kullanılıyor demektir.
            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    layerResult.Errors.Add("Kullanıcı Adı kayıtlı.");
                }
                if (user.Email == data.Email)
                {

                    layerResult.Errors.Add("Email adresi kayıtlı.");
                }
            }
            else
            {
                int dbResult = repo_user.Insert(new EvernoteUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false
                    
                    
                });

                if (dbResult > 0)
                {
                    layerResult.Result = repo_user.Find(x => x.Username == data.Username && x.Email == data.Email);
                    //Todo Aktivasyon email atılacak.
                }

               

            }


            return layerResult;

        }
    }
}
