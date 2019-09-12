using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario : IDisposable
    {
        public long Incluir(IEnumerable<DML.Beneficiario> beneficiarios) {
        }
        public void Alterar(DML.Beneficiario beneficiario) {
        }
        public void ExcluirVarios(long usuarioId) {

        }
        public void Excluir(long Id) {

        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
