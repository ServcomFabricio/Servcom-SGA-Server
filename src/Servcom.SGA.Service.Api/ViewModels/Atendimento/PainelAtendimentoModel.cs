using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servcom.SGA.Service.Api.ViewModels.Atendimento
{
    public class PainelAtendimentoModel
    {
        public bool Status { get; set; }
        public string Senha { get; set; }
        public string Guiche { get; set; }
        public bool Prioritario { get; set; }
    }
}
