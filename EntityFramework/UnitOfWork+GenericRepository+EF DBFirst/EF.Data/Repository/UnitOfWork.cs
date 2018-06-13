using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Data.Entity;
using EF.Model;

namespace EF.Data
{
   public class UnitOfWork : IDisposable
    {
       private readonly EFTestEntities context;
       private bool disposed;
       private Dictionary<string,object> repositories;

       public UnitOfWork(EFTestEntities context)
       {
           this.context = context;
       }

       public UnitOfWork()
       {
           context = new EFTestEntities();
       }

       public void Dispose()
       {
           Dispose(true);
           GC.SuppressFinalize(this);
       }

       public int Save()
       {
           AddAuditInfo();
           return context.SaveChanges();
       }

       public virtual void Dispose(bool disposing)
       {
           if (!disposed)
           {
               if (disposing)
               {
                   context.Dispose();
               }
           }
           disposed = true;
       }

       
        /// <summary>
        /// Add和Edit中，朝context中记录Audit信息
        /// </summary>
       private void AddAuditInfo()
        {
            var entities = context.ChangeTracker
                    .Entries()
                    .Where(x => x.Entity is AuditModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
                ? HttpContext.Current.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((AuditModel)entity.Entity).CreatedAt = DateTime.UtcNow;
                    ((AuditModel)entity.Entity).CreatedBy = currentUsername;
                }

                ((AuditModel)entity.Entity).UpdatedAt = DateTime.UtcNow;
                ((AuditModel)entity.Entity).UpdatedBy = currentUsername;
            }
        }

       public Repository<T> Repository<T>() where T : class
       {
           if (repositories == null)
           {
               repositories = new Dictionary<string,object>();
           }

           var type = typeof(T).Name;

           if (!repositories.ContainsKey(type))
           {
               var repositoryType = typeof(Repository<>);
               var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
               repositories.Add(type, repositoryInstance);
           }
           return (Repository<T>)repositories[type];
       }
    }
}
