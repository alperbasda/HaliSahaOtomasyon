using System.Data.Entity;
using HaliSaha.Entity.Concrete;

namespace HaliSaha.DataAccess.Concrete
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Musteri> Musteriler { get; set; }

        public DbSet<Saha> Sahalar { get; set; }

        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }

        public DatabaseContext()
        :base("HaliSahaDb")
        {
            
        }
    }
}