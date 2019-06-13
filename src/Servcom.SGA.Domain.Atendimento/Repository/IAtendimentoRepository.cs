using Servcom.SGA.Domain.Core.Interfaces;
using System;

namespace Servcom.SGA.Domain.Atendimentos.Repository
{
    public interface IAtendimentoRepository:IRepository<Atendimento>
    {
        int obterUltimoAtendimento(Guid? tipoId,DateTime dataCriacao);
        Atendimento obterPrimeiroAtendimentoSemUsuario(Guid? tipoId, DateTime dataCriacao, Guid? usuarioId);
        int SaveChangesIncluir(Atendimento atendimento);
        Atendimento atendimentoFormatado(Guid id);
    }
}
