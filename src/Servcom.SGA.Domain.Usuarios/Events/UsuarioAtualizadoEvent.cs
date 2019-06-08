using Servcom.SGA.Domain.Core.Events;
using System;

namespace Servcom.SGA.Domain.Usuarios.Events
{
    public class UsuarioAtualizadoEvent : Event
    {
        public UsuarioAtualizadoEvent(Guid id, string sigla, string nome, string setor)
        {
            Id = id;
            Sigla = sigla;
            Nome = nome;
            Setor=setor;
        }

        public Guid Id { get; private set; }
        public string Sigla { get; private set; }
        public string Nome { get; private set; }
        public string Setor { get; private set; }
    }
}
