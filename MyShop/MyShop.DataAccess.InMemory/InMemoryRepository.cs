using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<Generic> where Generic : BaseEntity //Add <> to create a generic class and inside <> you can put anything ----- Whenever we pass a object it must be the type of BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<Generic> items;
        string className; //To help handle how we store objects in cache

        public InMemoryRepository()
        {
            className = typeof(Generic).Name; //this is called refletion. We pass the object we are using and from that we get the internal name. Ex: If we pass product, the name will be product
            items = cache[className] as List<Generic>; //check if there is any item in our cache
            if (items == null)
            {
                items = new List<Generic>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(Generic generic)
        {
            items.Add(generic);
        }

        public void Update(Generic generic)
        {
            Generic genericToUpdate = items.Find(i => i.Id == generic.Id);

            if (genericToUpdate != null)
            {
                genericToUpdate = generic;
            }
            else
            {
                throw new Exception(className + " not found!");
            }
        }

        public Generic Find(String Id)
        {
            Generic generic = items.Find(i => i.Id == Id);
            if (generic != null)
            {
                return generic;
            }
            else
            {
                throw new Exception(className + " not found!");
            }
        }

        public IQueryable<Generic> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete (string Id)
        {
            Generic genericToDelete = items.Find(i => i.Id == Id);

            if (genericToDelete != null)
            {
                items.Remove(genericToDelete);
            }
            else
            {
                throw new Exception(className + " not found!");
            }

        }
    }
}
