using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaliSaha.Core.Concrete;
using HaliSaha.DataAccess.Abstract;
using HaliSaha.DataAccess.HtmlHelpers;
using HaliSaha.Entity.Concrete;

namespace HaliSaha.DataAccess.Concrete
{
    public class RezervasyonDal : EfEntityRepositoryBase<Rezervasyon, DatabaseContext>, IRezervasyonDal
    {
        public StringBuilder[] GetRezervasyon(string zaman)
        {
            StringBuilder[] builders = new StringBuilder[3];
            DateTime date;
            if (!DateTime.TryParse(zaman, out date)) return null;

            builders[0] = RezervasyonToHtml.GetInstance().RezervasyonListToHtmlString(GetList(s =>
                    s.Tarih.Year == date.Year && s.Tarih.Day == date.Day && s.Tarih.Month == date.Month &&
                    s.SahaId == 1)
                .ToList(),false,100);

            builders[1] = RezervasyonToHtml.GetInstance().RezervasyonListToHtmlString(GetList(s =>
                    s.Tarih.Year == date.Year && s.Tarih.Day == date.Day && s.Tarih.Month == date.Month &&
                    s.SahaId == 2)
                .ToList(), false,80);

            builders[2] = RezervasyonToHtml.GetInstance().RezervasyonListToHtmlString(GetList(s =>
                    s.Tarih.Year == date.Year && s.Tarih.Day == date.Day && s.Tarih.Month == date.Month &&
                    s.SahaId == 3)
                .ToList(), true,120);

            return builders;
        }
    }
}