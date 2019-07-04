using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Servcom.SGA.Domain.Atendimentos;
using Servcom.SGA.Domain.Atendimentos.Repository;
using Servcom.SGA.Infra.Data.Context;

namespace Servcom.SGA.Infra.Data.Repository
{
    public class AtendimentoRepository : Repository<Atendimento>, IAtendimentoRepository
    {
        public readonly ServcomSGAContext _context;
        private readonly ITipoAtendimentoRepository _tipoAtendimentoRepository;
        public AtendimentoRepository(ServcomSGAContext context, ITipoAtendimentoRepository tipoAtendimentoRepository) : base(context)
        {
            _context = context;
            _tipoAtendimentoRepository = tipoAtendimentoRepository;
        }

        public int ObterUltimoAtendimento(Guid? tipoId, DateTime dataCriacao)
        {
            int ultAtendimento = 0;
            try
            {
                ultAtendimento = DbSet.Where(a => a.TipoId == tipoId && a.DataCriacao == dataCriacao).Max(a =>
                  a.Sequencia);
            }
            catch { }
            return ultAtendimento;
        }

        public Atendimento ObterPrimeiroAtendimentoSemUsuario(Guid? tipoId, DateTime dataCriacao, Guid? usuarioId, bool? prioritario)
        {

            try
            {
                var ultAtendimento = DbSet.Where(a => a.TipoId == tipoId && a.DataCriacao == dataCriacao && a.UsuarioId == null).FirstOrDefault(a => a.Prioritario == prioritario);

                return ultAtendimento;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public (int, Atendimento) SaveChangesUsuario(Atendimento atendimento)
        {
            bool saveFailed;
            int qtdSave = 0;
            do
            {
                saveFailed = false;
                try
                {
                    qtdSave = Db.SaveChanges();

                }
                catch (Exception ex)
                {

                    saveFailed = true;
                    var guiche = atendimento.Guiche;
                    var usuarioID = atendimento.UsuarioId;
                    atendimento = ObterPrimeiroAtendimentoSemUsuario(atendimento.TipoId, atendimento.DataCriacao, atendimento.UsuarioId, atendimento.Prioritario);
                    atendimento.SetNovoAtendimento(usuarioID, guiche);
                }

            } while (saveFailed);

            return (qtdSave, atendimento);
        }

        public int SaveChangesIncluir(Atendimento atendimento)
        {
            bool saveFailed;
            int qtdSave = 0;
            do
            {
                saveFailed = false;
                try
                {
                    qtdSave = Db.SaveChanges();

                }
                catch (DbUpdateConcurrencyException ex)
                {

                    saveFailed = true;
                    var ultAtendimento = (int)ObterUltimoAtendimento(atendimento.TipoId, atendimento.DataCriacao);
                    var tipoA = _tipoAtendimentoRepository.ObterPorId((Guid)atendimento.TipoId);
                    atendimento.SetSequencia(ultAtendimento + 1, tipoA.Tipo,tipoA.Prioritario);
                }

            } while (saveFailed);

            return qtdSave;
        }

        public IEnumerable<Atendimento> PainelAtendimento(int quantidadeAtendiementos=1)
        {
            return DbSet.Where(a => a.DataCriacao == DateTime.Now.Date && a.DataHoraChamada != DateTime.MinValue).OrderByDescending(a => a.DataHoraChamada).ToList().Take(quantidadeAtendiementos);
        }
    }
}
