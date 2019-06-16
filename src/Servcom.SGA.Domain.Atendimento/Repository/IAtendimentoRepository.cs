using Servcom.SGA.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Servcom.SGA.Domain.Atendimentos.Repository
{
    public interface IAtendimentoRepository:IRepository<Atendimento>
    {
        int obterUltimoAtendimento(Guid? tipoId,DateTime dataCriacao);
        Atendimento obterPrimeiroAtendimentoSemUsuario(Guid? tipoId, DateTime dataCriacao, Guid? usuarioId,bool? prioritario);
        int SaveChangesIncluir(Atendimento atendimento);
        (int,Atendimento) SaveChangesUsuario(Atendimento atendimento);
        IEnumerable<Atendimento> painelAtendimento(int qtdAtendimento);
    }
}
