using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts
{
    public interface IRepository<Generic> where Generic : BaseEntity
    {
        IQueryable<Generic> Collection();
        void Commit();
        void Delete(string Id);
        Generic Find(string Id);
        void Insert(Generic generic);
        void Update(Generic generic);
    }
}