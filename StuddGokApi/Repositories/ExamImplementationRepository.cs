using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;

namespace StuddGokApi.Repositories;

public class ExamImplementationRepository : IExamImplementationRepository
{
    private readonly StuddGokDbContext _dbContext;

    public ExamImplementationRepository(StuddGokDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ExamImplementation?> GetExamImplementationById(int id)
    {
        IEnumerable<ExamImplementation> eis = await _dbContext.ExamImplementations.Where(x => x.Id == id).ToListAsync();
        return eis.FirstOrDefault();
    }
}
