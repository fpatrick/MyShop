using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess
{
    public class SQLRepository<Generic> : IRepository<Generic> where Generic : BaseEntity
    {
        internal DataContext context; //From the class datacontext
        internal DbSet<Generic> dbSet; //underline the table we will want to access (From data entity)

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Generic>(); //set the underline table, referencing the context and calling set comand setting in the model we want to work with, if we set generic to be product, it will be product table and so on
        }

        public IQueryable<Generic> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var generic = Find(Id);
            if (context.Entry(generic).State == EntityState.Detached)
            {
                dbSet.Attach(generic);
            }
            dbSet.Remove(generic); //once the object we passing throught is connect to the entity framework, we can remove
        }

        public Generic Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(Generic generic)
        {
            dbSet.Add(generic);
        }

        public void Update(Generic generic)
        {
            dbSet.Attach(generic); //Attach the passed object to the entity framework table
            context.Entry(generic).State = EntityState.Modified; //Then we set that entry to state of modified. this tells the entity framework that when we call the save changes method to look for the passed object and persist in the database
        }
    }
}
