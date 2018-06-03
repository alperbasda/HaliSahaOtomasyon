using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaliSaha.Entity.Concrete;

namespace HaliSaha.DataAccess.HtmlHelpers
{
    public class RezervasyonToHtml
    {
        private static RezervasyonToHtml _rezervasyonToHtml;
        private static readonly object Lock = new object();
        private RezervasyonToHtml()
        {

        }
        public static RezervasyonToHtml GetInstance()
        {
            lock (Lock)
            {
                if (_rezervasyonToHtml == null)
                {
                    _rezervasyonToHtml = new RezervasyonToHtml();
                }
            }
            return _rezervasyonToHtml;
        }

        public StringBuilder RezervasyonListToHtmlString(List<Rezervasyon> rezervasyonlar, bool bucukluMu, decimal fiyat)
        {

            DateTime time = bucukluMu ? Bucuklu() : Tam();
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 24; i++)
            {
                
                if (i == 0)
                    builder.Append("<div class='row'>");
                if (i % 4 == 0)
                {
                    builder.Append("</div>");
                    builder.Append("<div class='row'>");
                }



                if (rezervasyonlar.Any(s => s.Tarih.Hour == time.Hour && s.Tarih.Minute == time.Minute))
                {
                    Rezervasyon rezervasyon = rezervasyonlar.First(s => s.Tarih.Hour == time.Hour && s.Tarih.Minute == time.Minute);
                    builder.Append(
                        "<div class='col-lg-3 col-md-3'><div class='card'><div class='avatar'><img src = '../Content/img/Emoji/7.png' alt ='Emoji' /></div><div class='content'><p>" + rezervasyon.Musteri.Ad + " " + rezervasyon.Musteri.Soyad[0] + ". <br>" + rezervasyon.Tarih.ToString("HH:mm:ss") + "</p><p><button type = 'button' class='btn btn-sm btn-red btn-effect2 rezerve'>Rezerve Edildi</button></p></div></div></div> ");
                }
                else
                {
                    builder.Append(
                        "<div class='col-lg-3 col-md-3'><div class='card'><div class='avatar'><img src = '../Content/img/Emoji/" +
                        random.Next(1, 6) + ".png' alt ='Emoji' /></div><div class='content'><p>" + fiyat + " YTL<br>" + time.TimeOfDay + " </p><p><button data-saat='"+time.TimeOfDay+"' type = 'button' onclick='reserve(this)' class='btn btn-sm btn-blue btn-effect2 rezerve'>Rezerve Et</button></p></div></div></div> ");
                }


                time = time.AddHours(1);
            }

            return builder;
        }

        private DateTime Bucuklu()
        {
            return new DateTime(1, 1, 1, 0, 30, 0);
        }

        private DateTime Tam()
        {
            return new DateTime(1, 1, 1, 0, 0, 0);
        }
    }
}