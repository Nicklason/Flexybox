using Microsoft.EntityFrameworkCore;
using Flexybox.Data;

public class DepartmentsService
{
  private readonly AppDbContext db;

  public DepartmentsService(AppDbContext db)
  {
    this.db = db;
  }

  public async Task<Department?> GetByIdAsync(int id)
  {
    return await db.Department
        .Include(d => d.Images)
        .Include(d => d.Address)
        .Include(d => d.Contact)
        .Include(d => d.OpenHours)
            .ThenInclude(oh => oh.Days)
                .ThenInclude(day => day.Intervals)
        .FirstOrDefaultAsync(d => d.Id == id);
  }

  public async Task<List<(int Id, string Name)>> GetAllIdsAndNamesAsync()
  {
    var results = await db.Department.ToListAsync();

    return results.Select(d => (d.Id, d.Name)).ToList();
  }
}
