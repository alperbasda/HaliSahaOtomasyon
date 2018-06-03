using System.Text;
using HaliSaha.Core.Abstract;
using HaliSaha.Entity.Concrete;

namespace HaliSaha.DataAccess.Abstract
{
    public interface IRezervasyonDal : IEntityRepository<Rezervasyon>
    {
        StringBuilder[] GetRezervasyon(string zaman);
    }
}