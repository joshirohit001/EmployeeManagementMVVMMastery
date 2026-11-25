using EmployeeManagementMVVM.Models;

namespace EmployeeManagementMVVM.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    Task SaveAsync(Employee employee, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
