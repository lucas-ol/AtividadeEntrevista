using FI.AtividadeEntrevista.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario : IDisposable
    {
        public Util.FeedBack Incluir(IEnumerable<DML.Beneficiario> beneficiarios)
        {
            using (var repository = new DAL.Repositories.BeneficiarioRepository())
            {
                var itens = new List<DAL.Model.BENEFICIARIOS>();
                var feed = new Util.FeedBack();
                var ucRepedidos = new List<string>();
                
                foreach (var item in beneficiarios)
                {
                    if (itens.Any(x => x.CPF != item.CPF) || itens.Count == 0)
                    {
                        itens.Add(new DAL.Model.BENEFICIARIOS
                        {
                            NOME = item.Nome,
                            CPF = item.CPF,
                            IDCLIENTE = item.ClientId
                        //       ID = 
                    });
                    }
                    else
                        ucRepedidos.Add(item.CPF);

                }
               
                int inseridos = repository.AddRange(itens);
                feed.HasErros = inseridos <= 0;
                if (feed.HasErros)
                    feed.Messages.Add("Servidor em manutenção");
                else
                    feed.Messages.Add("Beneficiarios cadastrados com sucesso<br />");

                if (ucRepedidos.Count > 0)
                    feed.Messages.Add($"Os usuarios {string.Join(", ", ucRepedidos) } já estavam inseridos. <br />");


                return feed;
            }
        }
        public void Alterar(IEnumerable<DML.Beneficiario> beneficiarios)
        {
            if (beneficiarios is null)
            {
                throw new ArgumentNullException(nameof(beneficiarios));
            }

            ExcluirVarios(beneficiarios.FirstOrDefault().ClientId);
            Incluir(beneficiarios);
        }
        public void ExcluirVarios(long usuarioId)
        {
            using (var repository = new DAL.Repositories.BeneficiarioRepository())
            {
                repository.RemoveByUser(usuarioId);
            }
        }

        public List<DML.Beneficiario> ListarBenerios(long useId)
        {
            using (var repository = new DAL.Repositories.BeneficiarioRepository())
            {
                return repository.GetByUser(useId).Select(x => new DML.Beneficiario
                {
                    Nome = x.NOME,
                    CPF = x.CPF,
                    ClientId = x.IDCLIENTE
                }).ToList();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
