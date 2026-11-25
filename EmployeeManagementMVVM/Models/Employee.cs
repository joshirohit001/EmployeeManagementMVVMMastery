namespace EmployeeManagementMVVM.Models;

public class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Gender { get; set; } = "Male";
    public string Department { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-25);
    public DateTime JoiningDate { get; set; } = DateTime.Today;
    public decimal Salary { get; set; }
    public int ExperienceYears { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsPermanent { get; set; }
    public string Skills { get; set; } = string.Empty;
}
