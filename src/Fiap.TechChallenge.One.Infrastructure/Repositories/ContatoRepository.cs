using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Domain.Ddds;
using Fiap.TechChallenge.One.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Fiap.TechChallenge.One.Infrastructure.Repositories;

internal sealed class ContatoRepository(ApplicationDbContext dbContext) : IContatoRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<Contato>> ListarAsync(Codigo? codigo, CancellationToken cancellationToken = default)
    {
        IQueryable<Contato> query = _dbContext.Contatos.AsNoTracking();

        if (codigo is not null)
        {
            query = query.Where(c => c.Ddd.CodigoRegiao == codigo);
        }

        return await query.Include(c => c.Ddd).ToListAsync(cancellationToken);
    }

    public async Task<Contato?> ObterPorIdAsync(Guid contatoId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Contatos
            .FirstOrDefaultAsync(c => c.Id == contatoId, cancellationToken);
    }

    public void Adicionar(Contato contato)
    {
        _dbContext.Contatos.Add(contato);
    }

    public void Atualizar(Contato contato)
    {
        _dbContext.Contatos.Update(contato);
    }

    public void Excluir(Contato contato)
    {
        _dbContext.Contatos.Remove(contato);
    }
}