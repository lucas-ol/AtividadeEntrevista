﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    internal class DaoCliente : AcessoDados
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
                return new Cliente
                {
                    Nome = cliente.NOME,
                    Sobrenome = cliente.SOBRENOME,
                    Nacionalidade = cliente.NACIONALIDADE,
                    CEP = cliente.CEP,
                    Estado = cliente.ESTADO,
                    Cidade = cliente.CIDADE,
                    Logradouro = cliente.LOGRADOURO,
                    Email = cliente.EMAIL,
                    Telefone = cliente.TELEFONE,
                    CPF = cliente.CPF
                };
            }
        }

        internal bool VerificarExistencia(string CPF)
        {
            using (var repository = new Repositories.ClientRepository())
            {
                return repository.CheckCPF(CPF);
            }
        }

        internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<DML.Cliente> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(DML.Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Sobrenome", cliente.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nacionalidade", cliente.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CEP", cliente.CEP));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Estado", cliente.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cidade", cliente.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Logradouro", cliente.Logradouro));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Email", cliente.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Telefone", cliente.Telefone));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", cliente.Id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", cliente.CPF));
            base.Executar("FI_SP_AltCliente", parametros);
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelCliente", parametros);
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
