using EmployeeManagementMVVM.Data;
using EmployeeManagementMVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementMVVM.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Employees
            .AsNoTracking()
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .ToListAsync(cancellationToken);
    }

    public Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Employees
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public Task<bool> EmailExistsAsync(
        string email, int? exceptId = null, CancellationToken cancellationToken = default)
    {
        return _dbContext.Employees.AnyAsync(
            e => e.Email == email && (!exceptId.HasValue || e.Id != exceptId.Value),
            cancellationToken);
    }

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _dbContext.Employees.FindAsync(new object[] { id }, cancellationToken);
        if (employee is null)
            return;

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
