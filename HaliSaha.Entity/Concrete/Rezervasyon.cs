using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HaliSaha.Core.Entity;

namespace HaliSaha.Entity.Concrete
{
    public class Rezervasyon : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int MusteriId { get; set; }

        [ForeignKey("MusteriId")]
        public virtual  Musteri Musteri { get; set; }

        public int SahaId { get; set; }

        [ForeignKey("SahaId")]
        public virtual Saha Saha { get; set; }

        public DateTime Tarih { get; set; }
    }
}
