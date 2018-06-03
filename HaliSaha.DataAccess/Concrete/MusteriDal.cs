using HaliSaha.Core.Concrete;
using HaliSaha.DataAccess.Abstract;
using HaliSaha.Entity.Concrete;

namespace HaliSaha.DataAccess.Concrete
{
    public class MusteriDal : EfEntityRepositoryBase<Musteri,DatabaseContext> , IMusteriDal
    {
        
    }
}