using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    internal class DaoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(DML.Cliente cliente)
        {
            using (var repository = new Repositories.ClientRepository())
            {
                return repository.AddClient(new Model.CLIENTES
                {
                    NOME = cliente.Nome,
                    SOBRENOME = cliente.Sobrenome,
                    NACIONALIDADE = cliente.Nacionalidade,
                    CEP = cliente.CEP,
                    ESTADO = cliente.Estado,
                    CIDADE = cliente.Cidade,
                    LOGRADOURO = cliente.Logradouro,
                    EMAIL = cliente.Email,
                    TELEFONE = cliente.Telefone,
                    CPF = cliente.CPF
                });
            }
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal DML.Cliente Consultar(long Id)
        {
            using (var rp = new Repositories.ClientRepository())
            {
                var cliente = rp.GetById(Id);
                if (cliente != null)
                {
                    return new Cliente
                    {
                        Id = cliente.ID,
                        Nome = cliente.NOME,
                        Sobrenome = cliente.SOBRENOME,
                        Nacionalidade = cliente.NACIONALIDADE,
                        CEP = cliente.CEP,
                        Estado = cliente.ESTADO,
                        Cidade = cliente.CIDADE,
                        Logradouro = cliente.LOGRADOURO,
                        Email = cliente.EMAIL,
                        Telefone = cliente.TELEFONE,
                        CPF = cliente.CPF,
                        Beneficiarios = cliente.BENEFICIARIOS.Select(x => new Beneficiario
                        {
                            Nome = x.NOME,
                            CPF = x.CPF,
                            Id = x.ID,
                            ClientId = x.IDCLIENTE
                        }).ToList()
                    };
                }
                return null;
            }
        }

        internal bool VerificarExistencia(string CPF)
        {
            using (var repository = new Repositories.ClientRepository())
            {
                return repository.CheckCPF(CPF);
            }
        }

        internal List<Cliente> Pesquisa(int pagina, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            using (var repository = new Repositories.ClientRepository())
            {
                var result = repository.Search(pagina, quantidade, campoOrdenacao, crescente, out qtd).Select(c => new DML.Cliente
                {
                    Id = c.ID,
                    Nome = c.NOME,
                    Sobrenome = c.SOBRENOME,
                    Nacionalidade = c.NACIONALIDADE,
                    CEP = c.CEP,
                    Estado = c.ESTADO,
                    Cidade = c.CIDADE,
                    Logradouro = c.LOGRADOURO,
                    Email = c.EMAIL,
                    Telefone = c.TELEFONE,
                    CPF = c.CPF,
                    Beneficiarios = c.BENEFICIARIOS.Select(x => new Beneficiario
                    {
                        ClientId = x.IDCLIENTE,
                        Nome = x.NOME,
                        CPF = x.CPF
                    }).ToList()
                });

                return result.ToList();
            }
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<DML.Cliente> Listar()
        {
            using (var rep = new Repositories.ClientRepository())
            {
                return rep.GetAll().Select(c => new Cliente
                {
                    Id = c.ID,
                    Nome = c.NOME,
                    Sobrenome = c.SOBRENOME,
                    Nacionalidade = c.NACIONALIDADE,
                    CEP = c.CEP,
                    Estado = c.ESTADO,
                    Cidade = c.CIDADE,
                    Logradouro = c.LOGRADOURO,
                    Email = c.EMAIL,
                    Telefone = c.TELEFONE,
                    CPF = c.CPF,
                    Beneficiarios = c.BENEFICIARIOS.Select(x => new Beneficiario
                    {
                        ClientId = x.IDCLIENTE,
                        Nome = x.NOME,
                        CPF = x.CPF
                    }).ToList()
                }).ToList();
            }
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(DML.Cliente c)
        {
            using (var repository = new Repositories.ClientRepository())
            {
                repository.Update(new Model.CLIENTES
                {
                    ID = c.Id,
                    NOME = c.Nome,
                    SOBRENOME = c.Sobrenome,
                    NACIONALIDADE = c.Nacionalidade,
                    CEP = c.CEP,
                    ESTADO = c.Estado,
                    CIDADE = c.Cidade,
                    LOGRADOURO = c.Logradouro,
                    EMAIL = c.Email,
                    TELEFONE = c.Telefone,
                    CPF = c.CPF
                });
            }
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            using (var repository = new Repositories.ClientRepository())
            {
                repository.Remove(repository.GetById(Id));
            }
        }

        private List<DML.Cliente> Converter(DataSet ds)
        {
            List<DML.Cliente> lista = new List<DML.Cliente>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Cliente cli = new DML.Cliente();
                    cli.Id = row.Field<long>("Id");
                    cli.CEP = row.Field<string>("CEP");
                    cli.Cidade = row.Field<string>("Cidade");
                    cli.Email = row.Field<string>("Email");
                    cli.Estado = row.Field<string>("Estado");
                    cli.Logradouro = row.Field<string>("Logradouro");
                    cli.Nacionalidade = row.Field<string>("Nacionalidade");
                    cli.Nome = row.Field<string>("Nome");
                    cli.Sobrenome = row.Field<string>("Sobrenome");
                    cli.Telefone = row.Field<string>("Telefone");
                    cli.CPF = row.Field<string>("CPF");
                    lista.Add(cli);
                }
            }

            return lista;
        }
    }
}
