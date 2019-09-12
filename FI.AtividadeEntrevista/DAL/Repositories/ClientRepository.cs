using FI.AtividadeEntrevista.DAL.Model;
using FI.AtividadeEntrevista.DAL.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FI.AtividadeEntrevista.DAL.Repositories
{
    internal class ClientRepository : RepositoryBase<Model.CLIENTES>, IClienteRepository
    {
        public long AddClient(CLIENTES obj)
        {
            using (var ctx = new Model.BancoDeDadosEntities())
            {
                var result = ctx.FI_SP_IncClienteV2(obj.NOME, obj.SOBRENOME, obj.NACIONALIDADE, obj.CEP, obj.ESTADO, obj.CIDADE, obj.LOGRADOURO, obj.EMAIL, obj.TELEFONE, obj.CPF).FirstOrDefault();                
            
                return  Convert.ToInt64(result ?? default) ;
            }
        }

        public 


    }
}
