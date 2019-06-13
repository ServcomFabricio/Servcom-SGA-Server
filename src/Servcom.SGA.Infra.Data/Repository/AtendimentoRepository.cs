using System;
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
        public AtendimentoRepository(ServcomSGAContext context) : base(context)
        {
            _context = context;
        }
        public Atendimento atendimentoFormatado(Guid id)
        {
            var atendimento = ObterPorId(id);
            var tipoAtendimento= _context.TipoAtendimentos.AsNoTracking().FirstOrDefault(t => t.Id == atendimento.TipoId);
            atendimento.setSenha(tipoAtendimento.Tipo);
            return  atendimento;

        }
        public int obterUltimoAtendimento(Guid? tipoId, DateTime dataCriacao)
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

        public Atendimento obterPrimeiroAtendimentoSemUsuario(Guid? tipoId, DateTime dataCriacao,Guid? usuarioId)
        {
            
            try
            {
                var ultAtendimento = DbSet.Where(a => a.TipoId == tipoId && a.DataCriacao == dataCriacao && a.UsuarioId == null).FirstOrDefault();
                ultAtendimento.setUsuario(usuarioId);
                return ultAtendimento;
            }
            catch (Exception ex) {
                return null;
            }

            
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
                    qtdSave=Db.SaveChanges();
           
                }
                catch (Exception ex)
                {

                    saveFailed = true;
                    atendimento= obterPrimeiroAtendimentoSemUsuario(atendimento.TipoId,atendimento.DataCriacao, atendimento.UsuarioId);
                    atendimento.setUsuario(atendimento.UsuarioId);
                }

            } while (saveFailed);

            return qtdSave;
        }

        public int SaveChangesUsuario(Atendimento atendimento)
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
                    var ultAtendimento = (int)obterUltimoAtendimento(atendimento.TipoId, atendimento.DataCriacao);
                    atendimento.setSequencia(ultAtendimento + 1);
                }

            } while (saveFailed);

            return qtdSave;
        }
    }
}
