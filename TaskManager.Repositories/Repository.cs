﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace TaskManager.DAO
{
    public class RepositoryDAO<T> : IRepositoryDAO<T> where T : class
    {
        private TaskManagerDbContext context;

        private DbSet<T> dbSet;

        public RepositoryDAO()
        {
            context = new TaskManagerDbContext();
            dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public T GetById(object id)
        {
            return dbSet.Find(id);
        }
        public T Insert(T entity)
        {
            dbSet.Add(entity);
            Save();
            return entity;
        }
        public void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            Save();
        }
        public void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);

            }
            dbSet.Remove(entityToDelete);
        }
        public T Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            Save();
            return entity;
        }
        public void Save()
        {
            try
            {
                context.SaveChanges();
            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                    }
                }
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}
