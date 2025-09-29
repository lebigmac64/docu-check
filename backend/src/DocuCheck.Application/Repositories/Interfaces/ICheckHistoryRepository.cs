using DocuCheck.Domain.Entities.ChecksHistory;

namespace DocuCheck.Application.Repositories.Interfaces;

public interface ICheckHistoryRepository
{
    Task AddAsync(CheckHistory model);
    Task<CheckHistory[]> GetCheckHistoryAsync(int pageNumber, int pageSize);
}