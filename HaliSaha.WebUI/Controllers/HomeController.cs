using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Services;
using HaliSaha.DataAccess.Concrete;
using HaliSaha.Entity.Concrete;

namespace HaliSaha.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string message = "")
        {
            if (string.IsNullOrEmpty(message))
                ViewBag.PostBack = message;

            return View();
        }

        public ActionResult RezerveEt(string sahaId, string tarih, string musteriAd, string musteriSoyad, string telefon, string mail, string saat)
        {
            if (string.IsNullOrEmpty(sahaId) || string.IsNullOrEmpty(tarih) || string.IsNullOrEmpty(musteriAd) || string.IsNullOrEmpty(musteriSoyad) || string.IsNullOrEmpty(telefon) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(saat))
                return RedirectToAction("Index", new { message = "1" });

            DateTime date;
            TimeSpan span;
            int nSahaId = Convert.ToInt32(sahaId);
            if (DateTime.TryParse(tarih, out date) && TimeSpan.TryParse(saat, out span))
            {
                date = date.Add(span);

                RezervasyonDal dal = new RezervasyonDal();
                if (dal.Get(s => s.Tarih == date && s.SahaId == nSahaId) != null)
                {
                    return RedirectToAction("Index", new { message = "2" });
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
                return RedirectToAction("Index", new { message = "3" });
            }
            return RedirectToAction("Index", new { message = "4" });
        }

        [HttpGet]
        [WebMethod]
        public JsonResult Sorgula(string zaman)
        {
            if (!string.IsNullOrEmpty(zaman))
            {
                RezervasyonDal dal = new RezervasyonDal();
                StringBuilder[] sb = dal.GetRezervasyon(zaman);
                return Json(new[] { sb[0].ToString(), sb[1].ToString(), sb[2].ToString() }, JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }
    }
}