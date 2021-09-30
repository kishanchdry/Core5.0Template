using Data.Context;
using Data.Entities.Base;
using Data.IRepository;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data.Repository.GenericRepository
{
    public class GenericDataRepository<T> : IGenericDataRepository<T>, IDisposable where T : BaseEntity
    {
        readonly DataContext context;
        public GenericDataRepository(DataContext context)
        {
            this.context = context;
        }
        public bool Add(params T[] items)
        {
            // assume Entity base class have an Id property for all items
            var mySet = context.Set<T>();
            mySet.AddRange(items);
            context.SaveChanges();
            return true;
        }
        //public bool Add(T items)
        //{
        //    // assume Entity base class have an Id property for all items
        //    var mySet = context.Set<T>();
        //    mySet.Add(items);
        //    context.SaveChanges();
        //    return true;
        //}
        public bool Remove(params T[] items)
        {
            // assume Entity base class have an Id property for all items
            //            var mySet = context.Set<T>();
            //var entity = mySet.RemoveRange(items);
            //context.SaveChanges();
            if (items != null)
            {
                foreach (var item in items)
                {
                    if (item is BaseEntity)
                    {
                        (item as BaseEntity).IsDeleted = true;
                        (item as BaseEntity).ModifiedDate = DateTime.UtcNow.GetLocal();
                    }
                }
                this.Update(items);
            }
            return true;
        }

        public bool Remove(long id)
        {
            // assume Entity base class have an Id property for all items
            var mySet = context.Set<T>();
            var entity = mySet.Find(id);
            if (entity == null)
            {
                throw new Exception("Record not fount");
            }
            else
            {
                (entity as BaseEntity).IsDeleted = true;
                (entity as BaseEntity).IsActive = false;
                (entity as BaseEntity).ModifiedDate = DateTime.UtcNow.GetLocal();
            }
            context.Entry(entity).CurrentValues.SetValues(entity);
            context.SaveChanges();
            return true;
        }

        public bool ChangeStatus(long id)
        {
            // assume Entity base class have an Id property for all items
            var mySet = context.Set<T>();
            var entity = mySet.Find(id);
            if (entity == null)
            {
                throw new Exception("Record not fount");
            }
            else
            {
                (entity as BaseEntity).IsActive = !(entity as BaseEntity).IsActive;
                (entity as BaseEntity).ModifiedDate = DateTime.UtcNow.GetLocal();
            }
            context.Entry(entity).CurrentValues.SetValues(entity);
            context.SaveChanges();
            return true;
        }

        public bool Update(params T[] items)
        {
            var mySet = context.Set<T>();

            if (items != null)
            {
                foreach (var entityToUpdate in items)
                {
                    mySet.Attach(entityToUpdate);
                    context.Entry(entityToUpdate).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            return true;
        }

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;

            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .ToList<T>().Where(e => !e.IsDeleted && e.IsActive).ToList();

            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;

            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<T>().Where(e => !e.IsDeleted && e.IsActive).ToList();

            return list;
        }

        public virtual T GetSingle(long id, params Expression<Func<T, object>>[] navigationProperties)
        {

            // assume Entity base class have an Id property for all items
            //var mySet = context.Set<T>();
            //var entity = mySet.Find(id);
            //return (T)entity;

            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            return dbQuery.Where(e => e.Id == id).FirstOrDefault();

        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;

            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .Where(where)
                .FirstOrDefault(); //Apply where clause

            return item;
        }

        public virtual Pagination<T> GetPageingList(int pageIndex, int rowCount, Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            Pagination<T> result = new Pagination<T>();

            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            var list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<T>().Where(e => !e.IsDeleted && e.IsActive).ToList();

            var data = new CNPagedList<T>(list, pageIndex, rowCount == 0 ? 10 : rowCount);

            result.List = data.items;
            result.PageIndex = data.page;
            result.PageRowCount = data.pageSize;
            result.TotalCount = data.totalItemCount;

            return result;
        }

        public IList<T> GetAllWitnInActive(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;

            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .ToList<T>().Where(e => !e.IsDeleted).ToList();

            return list;
        }

        public void Dispose()
        {
            //IDisposable disposable = this as IDisposable;
            //if (disposable != null)
            //    disposable.Dispose();
        }
    }
}
