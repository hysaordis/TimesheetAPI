using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TimesheetAPI.api.Repositories.Interfaces;

namespace TimesheetAPI.api.Repositories
{
    public abstract class RawCRUDRepository<ID, E> : RawOnlyReadableRepository<ID, E>, ICRUDRepository<ID, E>
        where E : class
    {
        public RawCRUDRepository(MainContext Context) : base(Context)
        {
            this.Context = Context;
            this.Entities = this.Context.Set<E>();
        }

        virtual public IEnumerable<E> GetAll()
        {
            return this.Entities;
        }

        virtual public E Add(E entity)
        {
            if (entity == null)
            {
                return null;
            }
            this.Entities.Add(entity);

            return entity;
        }

        virtual public IEnumerable<E> AddRange(IEnumerable<E> entities)
        {
            if (entities == null)
            {
                return new List<E>();
            }
            Entities.AddRange(entities);

            return entities;
        }

        virtual public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        virtual public E Create(E entity)
        {
            if (entity == null)
            {
                return null;
            }

            this.Entities.Add(entity);
            this.Context.SaveChanges();

            return entity;
        }

        virtual public IEnumerable<E> CreateRange(IEnumerable<E> entities)
        {
            if (entities == null)
            {
                return null;
            }

            Entities.AddRange(entities);
            Context.SaveChanges();

            return entities;
        }

        virtual public void Update(E entity)
        {
            UpdateEntity(entity);

            this.Context.SaveChanges();
        }

        virtual public void UpdateRange(IEnumerable<E> entities)
        {
            foreach (var entity in entities)
            {
                UpdateEntity(entity);
            }
            Context.SaveChanges();
        }

        virtual public void Delete(E entity)
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }

            else
            {
                this.Entities.Attach(entity);
                this.Entities.Remove(entity);
            }

            this.Context.SaveChanges();
        }

        virtual public void Delete(ID id)
        {
            E entity = Find(id);

            Delete(entity);
        }

        virtual public void Delete(Expression<Func<E, bool>> predicate)
        {
            var entitiesToDelete = Entities.Where(predicate);

            if (entitiesToDelete != null && entitiesToDelete.Any())
            {
                Context.RemoveRange(entitiesToDelete);
            }
            Context.SaveChanges();
        }

        private void UpdateEntity(E entity)
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.Entities.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}