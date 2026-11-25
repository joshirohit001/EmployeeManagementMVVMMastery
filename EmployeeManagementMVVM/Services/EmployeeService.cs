using EmployeeManagementMVVM.Models;
using EmployeeManagementMVVM.Repositories;

namespace EmployeeManagementMVVM.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }

    public async Task SaveAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        Validate(employee);

        var emailExists = await _repository.EmailExistsAsync(
            employee.Email.Trim(), employee.Id == 0 ? null : employee.Id, cancellationToken);

        if (emailExists)
            throw new InvalidOperationException("An employee with this email address already exists.");

        employee.FirstName = employee.FirstName.Trim();
        employee.LastName = employee.LastName.Trim();
        employee.Email = employee.Email.Trim();
        employee.Phone = employee.Phone.Trim();

        if (employee.Id == 0)
            await _repository.AddAsync(employee, cancellationToken);
        else
            await _repository.UpdateAsync(employee, cancellationToken);
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return _repository.DeleteAsync(id, cancellationToken);
    }

    private static void Validate(Employee employee)
    {
        if (string.IsNullOrWhiteSpace(employee.FirstName))
            throw new InvalidOperationException("First name is required.");

        if (string.IsNullOrWhiteSpace(employee.LastName))
            throw new InvalidOperationException("Last name is required.");

        if (string.IsNullOrWhiteSpace(employee.Email) ||
            !employee.Email.Contains('@') ||
            !employee.Email.Contains('.'))
            throw new InvalidOperationException("Enter a valid email address.");

        if (string.IsNullOrWhiteSpace(employee.Department))
            throw new InvalidOperationException("Please select a department.");

        if (employee.Salary < 0)
            throw new InvalidOperationException("Salary cannot be negative.");

        if (employee.ExperienceYears < 0 || employee.ExperienceYears > 60)
            throw new InvalidOperationException("Experience must be between 0 and 60 years.");

        if (employee.DateOfBirth.Date > DateTime.Today)
            throw new InvalidOperationException("Date of birth cannot be in the future.");
    }
}
