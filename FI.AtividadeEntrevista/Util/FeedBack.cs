using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.Util
{
    public class FeedBack
    {
        public bool HasErros { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}
