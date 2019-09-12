﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DAL
{
    internal class DaoBeneficiario : AcessoDados
    {
        internal long Incluir(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                     
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ClientId", beneficiario.ClientId));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            DataSet ds = base.Consultar("", parametros);

            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }
        internal void Alterar(DML.Beneficiario beneficiario)
        {
        }
        internal void ExcluirVarios(long usuarioId)
        {

        }
        internal void Excluir(long Id)
        {

        }
    }
}
