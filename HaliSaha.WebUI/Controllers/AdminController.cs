using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using HaliSaha.DataAccess.Concrete;
using HaliSaha.Entity.Concrete;

namespace HaliSaha.WebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string kadi, string sifre)
        {
            if (kadi == "AdminHaliSaha" && sifre == "Admin123")
            {
                Session["loged"] = true;
                return RedirectToAction("Index");
            }
            return View();

        }


        public ActionResult LogOut()
        {
            Session["loged"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            if (Session["loged"] == null) return RedirectToAction("Login");
            RezervasyonDal dal = new RezervasyonDal();
            return View(dal.GetList(s => s.Tarih >= DateTime.Now));
        }

        [WebMethod]
        public JsonResult Remove(int id)
        {
            if (Session["loged"] == null) return Json("Tekrar Kullanıcı girişi yapın", JsonRequestBehavior.AllowGet);
            RezervasyonDal dal = new RezervasyonDal();
            dal.Delete(dal.Get(s => s.Id == id));
            return Json("Silme Başarılı", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(string sahaId, string tarih, string musteriAd, string musteriSoyad, string telefon, string mail, string saat)
        {
            if (Session["loged"] == null) return Json("Tekrar Kullanıcı girişi yapın", JsonRequestBehavior.AllowGet);

            if (string.IsNullOrEmpty(sahaId) || string.IsNullOrEmpty(tarih) || string.IsNullOrEmpty(musteriAd) || string.IsNullOrEmpty(musteriSoyad) || string.IsNullOrEmpty(telefon) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(saat))
                return Json("Eksik yada yanlış veri girildi", JsonRequestBehavior.AllowGet);

            DateTime date;
            TimeSpan span;
            int nSahaId = Convert.ToInt32(sahaId);
            if (DateTime.TryParse(tarih, out date) && TimeSpan.TryParse(saat, out span))
            {
                date = date.Add(span);

                RezervasyonDal dal = new RezervasyonDal();
                if (dal.Get(s => s.Tarih == date && s.SahaId == nSahaId) != null)
                {
                    return Json("Tarih veya saat bilgisi hatalı", JsonRequestBehavior.AllowGet);
                }
                MusteriDal mdal = new MusteriDal();
                Musteri m = mdal.Add(new Musteri()
                {
                    Ad = musteriAd,
                    Soyad = musteriSoyad,
                    MailAdresi = mail,
                    Telefon = telefon,
                });
                dal.Add(new Rezervasyon()
                {
                    Tarih = date,
                    MusteriId = m.Id,
                    SahaId = Convert.ToInt32(sahaId),
                });
                return Json("Kayıt Başarılı", JsonRequestBehavior.AllowGet);
            }
            return Json("Tarih dönüşümlerinde hata var tekrar deneyin", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Yukle(HttpPostedFileBase file, string path)
        {
            try
            {
                System.IO.File.Delete(Server.MapPath("~/Content/UploadedImage/" + path + ".jpg"));
                file.SaveAs(Server.MapPath("~/Content/UploadedImage/" + path + ".jpg"));

                return Json("Yukleme Basarılı", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Resim Yüklenemedi", JsonRequestBehavior.AllowGet);
            }

        }
    }
}