using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockContext<Generic> : IRepository<Generic> where Generic : BaseEntity
    {
        List<Generic> items;
        string className; //To help handle how we store objects in cache

        public MockContext()
        {
            items = new List<Generic>();
        }

        public void Commit()
        {
            return;
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

        public void Delete(string Id)
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