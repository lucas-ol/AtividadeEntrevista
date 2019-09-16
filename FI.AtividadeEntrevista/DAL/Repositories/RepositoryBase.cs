using FI.AtividadeEntrevista.DAL.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DAL.Repositories
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        public virtual void Add(TEntity obj)
        {
            using (var ctx = new Model.BancoDeDadosEntities())
            {
                ctx.Set<TEntity>().Add(obj);
                ctx.SaveChanges();
            }
        }

        public virtual int AddRange(IEnumerable<TEntity> objs)
        {
            using (var ctx = new Model.BancoDeDadosEntities())
            {
                ctx.Set<TEntity>().AddRange(objs);
                return ctx.SaveChanges();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var ctx = new Model.BancoDeDadosEntities();
            return ctx.Set<TEntity>().ToList();
        }

        public virtual TEntity GetById(long id)
        {
            var ctx = new Model.BancoDeDadosEntities();
            return ctx.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            using (var ctx = new Model.BancoDeDadosEntities())
            {
                ctx.Set<TEntity>().Remove(obj);
            }
        }

        public void Update(TEntity obj)
        {
            using (var ctx = new Model.BancoDeDadosEntities())
            {
                ctx.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
