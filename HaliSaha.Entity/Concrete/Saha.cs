using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HaliSaha.Core.Entity;

namespace HaliSaha.Entity.Concrete
{
    public class Saha : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Fiyat { get; set; }

        public virtual ICollection<Rezervasyon> Rezervasyonlar { get; set; }

    }
}