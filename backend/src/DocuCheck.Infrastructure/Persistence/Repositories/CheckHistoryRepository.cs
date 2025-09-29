using DocuCheck.Application.Repositories.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory;
using Microsoft.EntityFrameworkCore;

namespace DocuCheck.Infrastructure.Persistence.Repositories;

internal class CheckHistoryRepository(DocuCheckDbContext context) : ICheckHistoryRepository
{
    public async Task AddAsync(CheckHistory model)
    {
        await context.CheckHistory.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task<CheckHistory[]> GetCheckHistoryAsync(int pageNumber, int pageSize)
    {
        var data = context.CheckHistory.AsQueryable();

        var skip = pageNumber - 1;
        if (skip < 0) skip = 0;
        
        return await data
            .OrderByDescending(ch => ch.CheckedAt)
            .Skip((skip) * pageSize)
            .Take(pageSize)
            .ToArrayAsync();
    }
}