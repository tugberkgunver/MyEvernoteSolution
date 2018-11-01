using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Myevernote.Entities.ViewModels;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;

namespace Myevernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        NoteManager nm = new NoteManager();

        public ActionResult Index()
        {
           
            List<Note> notes = nm.GetNotes();
            return View(notes);
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value);
            if (cat == null)
            {
                return HttpNotFound();
            }
            //Kategorinin notlarını dönmek için cat.Notes kullanıldı!. Burada index view olarak verildi çünkü ByCategory için ek bir view oluşturulmadı.
            return View("Index",cat.Notes);
        }

        public ActionResult LastNotes()
        {
           List<Note> LastNotes = nm.GetNotes().OrderByDescending(x => x.ModifiedOn).ToList();
            //IQueryable Alternatif kullanım.
           // List<Note> LastNotes2 = nm.GetNotesQueryable().OrderByDescending(x => x.ModifiedOn).ToList();


            return View("Index",LastNotes);
        }

        public ActionResult MostLiked()
        {
            List<Note> MostLiked = nm.GetNotes().OrderByDescending(x => x.LikeCount).ToList();
            return View("Index",MostLiked);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //Giriş kontrolü ve yönlendirme
            //Session kullanıcı bilgileri saklama
            if (ModelState.IsValid)
            {
                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.LoginUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                Session["login"] = res.Result;
                return RedirectToAction("Index");
            }

            

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //Kullanıcı username ve email kontrolü
            //Insert ile kayıt işlemi
            //Aktivasyon emaili

            if (ModelState.IsValid)
            {

                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.RegisterUser(model);

                if (res.Errors.Count > 0 )
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
            
                

                    return RedirectToAction("RegisterOK");
            }


            return View();
        }

        //Kullanıcı emailden aktivasyon için geldiğinde bu Action çalışacak.
        public ActionResult ActivateUser(Guid activate_id)
        {
            //Kullanıcı Aktivasyonu sağlanacak.
            return View();
        }


        public ActionResult LogOut()
        {
            return View();
        }
    }
}