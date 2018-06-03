using HaliSaha.Core.Concrete;
using HaliSaha.DataAccess.Abstract;
using HaliSaha.Entity.Concrete;

namespace HaliSaha.DataAccess.Concrete
{
    public class SahaDal : EfEntityRepositoryBase<Saha,DatabaseContext> , ISahaDal
    {
        
    }
}