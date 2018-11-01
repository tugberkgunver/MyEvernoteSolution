using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.Abstract;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    //Burada where T:class yapmamızın sebebi kısıtlama uygulamak. Örneğin integer bir parametre gönderilmesin entityler gönderilsin.
    public class Repository<T>:RepositoryBase,IRepository<T> where T : class
    {
        //Buradaki DatabaseContext her biri için ayrı ayrı new'leniyor. Örneğin iki tane işlemimiz oldu.
        //Örneğin Comment'in insert işleminde Note'ta belirtilmeli bu yüzden iki newlenme yapıldığı için hata oluşuyor.
        //Buda bizi Singleton Pattern kullanmamız gerektiğini gösteriyor.
        //Bunun için RepositoryBase sınıfını oluşturuyoruz.
        //Ayrıca RepositoryBase'den miras alarak context'i burada oluşturmuyoruz.
        //private DatabaseContext context = new DatabaseContext();
       
        private DbSet<T> _objectSet;

        public Repository()
        {
           
            _objectSet = context.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();   
        }


        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public int Save()
        {
           return context.SaveChanges();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUser = "system";

            }
            return Save();
        }

        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
               
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUser = "system";

            }
            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
           return Save();

        }

        //Önemli buraya debug atacağım.Burası IQueryable olabilir.
        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

    }
}
