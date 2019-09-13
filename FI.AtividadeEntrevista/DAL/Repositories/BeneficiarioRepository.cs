using FI.AtividadeEntrevista.DAL.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DAL.Repositories
{
    class BeneficiarioRepository:RepositoryBase<Model.BENEFICIARIOS>,IBeneficiarioRepository
    {
        public void RemoveByUser(long userId) {
            using (var ctx = new Model.BancoDeDadosEntities())
            {
                var bnf = ctx.BENEFICIARIOS.Where(b => b.IDCLIENTE == userId);
                ctx.BENEFICIARIOS.RemoveRange(bnf);
                ctx.SaveChanges();
            }
        }
    }
}
