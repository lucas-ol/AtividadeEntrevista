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

                return Convert.ToInt64(result ?? default);
            }
        }
        /// <summary>
        /// Metodo que verifica a existencia de um CPF
        /// </summary>
        /// <param name="cpf">CPF a ser verificado</param>
        /// <returns>Retorna verdadeiro se o CPF ja existir na base</returns>
        public bool CheckCPF(string cpf)
        {
            using (var ctx = new Model.BancoDeDadosEntities())
            {
                return ctx.CLIENTES.Where(x => x.CPF == cpf).Any();
            }
        }

        public IEnumerable<Model.CLIENTES> Search(int pagina, int quantidade, string orderBy, bool crescente , out int qtde, string query = "")
        {
            using (var ctx = new Model.BancoDeDadosEntities())
            {
                var search = ctx.CLIENTES.Where(x => x.NOME.Contains(query) || x.EMAIL.Contains(query));                    
                qtde = search.Count();

                search.Skip(quantidade * (pagina - 1)).Take(quantidade);

                if (orderBy == "EMAIL")
                {
                    if (crescente)
                        search.OrderBy(x => x.EMAIL) ;
                    else
                        search.OrderByDescending(x => x.EMAIL);
                }
                else
                {
                    if (crescente)
                        search.OrderBy(x => x.NOME);
                    else
                        search.OrderByDescending(x => x.NOME);
                }
                
                return search.ToList();
            }

        }

    }
}
