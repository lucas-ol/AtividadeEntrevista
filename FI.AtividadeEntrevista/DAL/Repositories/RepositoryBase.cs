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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> GetAll(int QTDE)
        {
            using (var ctx = new Model.BancoDeDadosEntities())
            {
                return ctx.Set<TEntity>().Take(QTDE).ToList();
            }
        }
        public IEnumerable<TEntity> GetAll()
        {
            var ctx = new Model.BancoDeDadosEntities();
            return ctx.Set<TEntity>().ToList();
        }

        public TEntity GetById(long id)
        {
            using (var ctx = new Model.BancoDeDadosEntities())
            {
                return ctx.Set<TEntity>().Find(id);
            }
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
