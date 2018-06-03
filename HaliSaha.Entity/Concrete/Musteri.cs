using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HaliSaha.Core.Entity;

namespace HaliSaha.Entity.Concrete
{
    public class Musteri : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }

        [DataType(DataType.EmailAddress)]
        public string MailAdresi { get; set; }

        public virtual ICollection<Rezervasyon> Rezervasyonlar { get; set; }
    }
}