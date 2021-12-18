using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TimesheetAPI.api.Repositories.Interfaces;

namespace TimesheetAPI.api.Repositories
{
    public abstract class RawOnlyReadableRepository<ID, E> : IDisposable, IOnlyReadableRepository<ID, E>
        where E : class
    {
        protected MainContext Context = null;
        protected DbSet<E> Entities = null;

        public RawOnlyReadableRepository(MainContext Context)
        {
            this.Context = Context;
            this.Entities = this.Context.Set<E>();
        }

        public IQueryable<E> Find()
        {
            return Entities.AsQueryable();
        }

        public E Find(Expression<Func<E, bool>> predicate, string navigationProperties = null)
        {
            if (!string.IsNullOrEmpty(navigationProperties))
                return Entities.Include(navigationProperties).FirstOrDefault(predicate);

            return Entities.FirstOrDefault(predicate);
        }

        public E Find(ID id)
        {
            E entity = Entities.Find(id);

            return entity;
        }

        public IQueryable<E> Filter(Expression<Func<E, bool>> predicate)
        {
            return Entities.Where(predicate).AsQueryable<E>();
        }

        public IQueryable<E> Filter(Expression<Func<E, bool>> predicate, string navigationProperties = null)
        {
            if (!string.IsNullOrEmpty(navigationProperties))
            {
                return Entities.Include(navigationProperties).Where(predicate);
            }
            return Entities.Where(predicate);
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public int Count(Expression<Func<E, bool>> filterPredicate = null)
        {
            if (filterPredicate == null)
            {
                filterPredicate = src => true;
            }
            return Entities.Count(filterPredicate);
        }
    }
}