using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using EmployeeManagementMVVM.Commands;
using EmployeeManagementMVVM.Models;
using EmployeeManagementMVVM.Services;

namespace EmployeeManagementMVVM.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IEmployeeService _employeeService;
    private readonly RelayCommand _saveCommand;
    private readonly RelayCommand _deleteCommand;

    private int _selectedEmployeeId;
    private string _firstName = "";
    private string _lastName = "";
    private string _email = "";
    private string _phone = "";
    private string _gender = "Male";
    private string _department = "";
    private string _city = "";
    private DateTime? _dateOfBirth = DateTime.Today.AddYears(-25);
    private DateTime? _joiningDate = DateTime.Today;
    private string _salary = "";
    private string _experienceYears = "0";
    private bool _isActive = true;
    private bool _isPermanent;
    private string _selectedSkillsText = "";
    private Employee? _selectedEmployee;
    private string _searchText = "";
    private string _statusMessage = "Ready";
    private bool _isBusy;

    public MainViewModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;

        _saveCommand = new RelayCommand(async _ => await SaveAsync(), _ => !IsBusy);
        _deleteCommand = new RelayCommand(async _ => await DeleteAsync(),
            _ => !IsBusy && SelectedEmployee is not null);

        SaveCommand = _saveCommand;
        DeleteCommand = _deleteCommand;
        ClearCommand = new RelayCommand(_ => ClearForm(), _ => !IsBusy);
        RefreshCommand = new RelayCommand(async _ => await LoadEmployeesAsync(), _ => !IsBusy);
        SearchCommand = new RelayCommand(_ => ApplySearch());

        LoadEmployeesAsync();
    }

    public ObservableCollection<Employee> Employees { get; } = new();
    public ObservableCollection<Employee> FilteredEmployees { get; } = new();

    public string[] Departments { get; } = ["IT", "HR", "Finance", "Marketing", "Operations", "Sales"];
    public string[] Cities { get; } = ["Noida", "Ghaziabad", "Delhi", "Greater Noida", "Other"];
    public string[] AvailableSkills { get; } = [".NET", "C#", "SQL Server", "WPF", "Azure", "JavaScript", "React", "Python"];

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand ClearCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand SearchCommand { get; }

    public int SelectedEmployeeId { get => _selectedEmployeeId; set => SetProperty(ref _selectedEmployeeId, value); }
    public string FirstName { get => _firstName; set => SetProperty(ref _firstName, value); }
    public string LastName { get => _lastName; set => SetProperty(ref _lastName, value); }
    public string Email { get => _email; set => SetProperty(ref _email, value); }
    public string Phone { get => _phone; set => SetProperty(ref _phone, value); }
    public string Gender
    {
        get => _gender;
        set
        {
            if (SetProperty(ref _gender, value))
            {
                OnPropertyChanged(nameof(IsMale));
                OnPropertyChanged(nameof(IsFemale));
                OnPropertyChanged(nameof(IsOther));
            }
        }
    }

    public bool IsMale
    {
        get => Gender == "Male";
        set { if (value) Gender = "Male"; }
    }

    public bool IsFemale
    {
        get => Gender == "Female";
        set { if (value) Gender = "Female"; }
    }

    public bool IsOther
    {
        get => Gender == "Other";
        set { if (value) Gender = "Other"; }
    }
    public string Department { get => _department; set => SetProperty(ref _department, value); }
    public string City { get => _city; set => SetProperty(ref _city, value); }
    public DateTime? DateOfBirth { get => _dateOfBirth; set => SetProperty(ref _dateOfBirth, value); }
    public DateTime? JoiningDate { get => _joiningDate; set => SetProperty(ref _joiningDate, value); }
    public string Salary { get => _salary; set => SetProperty(ref _salary, value); }
    public string ExperienceYears { get => _experienceYears; set => SetProperty(ref _experienceYears, value); }
    public bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }
    public bool IsPermanent { get => _isPermanent; set => SetProperty(ref _isPermanent, value); }
    public string SelectedSkillsText { get => _selectedSkillsText; set => SetProperty(ref _selectedSkillsText, value); }
    public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); }
    public string StatusMessage { get => _statusMessage; set => SetProperty(ref _statusMessage, value); }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (SetProperty(ref _isBusy, value))
            {
                _saveCommand.RaiseCanExecuteChanged();
                _deleteCommand.RaiseCanExecuteChanged();
                ((RelayCommand)ClearCommand).RaiseCanExecuteChanged();
                ((RelayCommand)RefreshCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public Employee? SelectedEmployee
    {
        get => _selectedEmployee;
        set
        {
            if (SetProperty(ref _selectedEmployee, value))
            {
                _deleteCommand.RaiseCanExecuteChanged();

                if (value is not null)
                    LoadEmployeeIntoForm(value);
            }
        }
    }

    private async Task LoadEmployeesAsync()
    {
        try
        {
            IsBusy = true;
            var employees = await _employeeService.GetAllAsync();

            Employees.Clear();
            foreach (var employee in employees)
                Employees.Add(employee);

            ApplySearch();
            StatusMessage = $"Loaded {Employees.Count} employee(s).";
        }
        catch (Exception ex)
        {
            ShowError("Could not load employees.", ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task SaveAsync()
    {
        try
        {
            if (!decimal.TryParse(Salary, out var salary))
                throw new InvalidOperationException("Enter a valid salary.");

            if (!int.TryParse(ExperienceYears, out var experience))
                throw new InvalidOperationException("Enter valid experience in years.");

            var employee = new Employee
            {
                Id = SelectedEmployeeId,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Gender = Gender,
                Department = Department,
                City = City,
                DateOfBirth = DateOfBirth ?? DateTime.Today.AddYears(-25),
                JoiningDate = JoiningDate ?? DateTime.Today,
                Salary = salary,
                ExperienceYears = experience,
                IsActive = IsActive,
                IsPermanent = IsPermanent,
                Skills = SelectedSkillsText
            };

            IsBusy = true;
            await _employeeService.SaveAsync(employee);
            await LoadEmployeesWithoutBusyAsync();

            StatusMessage = SelectedEmployeeId == 0
                ? "Employee added successfully."
                : "Employee updated successfully.";

            ClearForm();
        }
        catch (Exception ex)
        {
            ShowError("Could not save employee.", ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task DeleteAsync()
    {
        if (SelectedEmployee is null)
            return;

        var answer = MessageBox.Show(
            $"Are you sure you want to delete {SelectedEmployee.FirstName} {SelectedEmployee.LastName}?",
            "Confirm delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (answer != MessageBoxResult.Yes)
            return;

        try
        {
            IsBusy = true;
            await _employeeService.DeleteAsync(SelectedEmployee.Id);
            await LoadEmployeesWithoutBusyAsync();
            ClearForm();
            StatusMessage = "Employee deleted successfully.";
        }
        catch (Exception ex)
        {
            ShowError("Could not delete employee.", ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task LoadEmployeesWithoutBusyAsync()
    {
        var employees = await _employeeService.GetAllAsync();
        Employees.Clear();

        foreach (var employee in employees)
            Employees.Add(employee);

        ApplySearch();
    }

    private void LoadEmployeeIntoForm(Employee employee)
    {
        SelectedEmployeeId = employee.Id;
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        Email = employee.Email;
        Phone = employee.Phone;
        Gender = employee.Gender;
        Department = employee.Department;
        City = employee.City;
        DateOfBirth = employee.DateOfBirth;
        JoiningDate = employee.JoiningDate;
        Salary = employee.Salary.ToString("0.00");
        ExperienceYears = employee.ExperienceYears.ToString();
        IsActive = employee.IsActive;
        IsPermanent = employee.IsPermanent;
        SelectedSkillsText = employee.Skills;
        StatusMessage = $"Editing employee #{employee.Id}.";
    }

    private void ClearForm()
    {
        SelectedEmployee = null;
        SelectedEmployeeId = 0;
        FirstName = "";
        LastName = "";
        Email = "";
        Phone = "";
        Gender = "Male";
        Department = "";
        City = "";
        DateOfBirth = DateTime.Today.AddYears(-25);
        JoiningDate = DateTime.Today;
        Salary = "";
        ExperienceYears = "0";
        IsActive = true;
        IsPermanent = false;
        SelectedSkillsText = "";
        StatusMessage = "Form cleared.";
    }

    private void ApplySearch()
    {
        var search = SearchText.Trim();

        var results = string.IsNullOrWhiteSpace(search)
            ? Employees
            : Employees.Where(e =>
                $"{e.FirstName} {e.LastName}".Contains(search, StringComparison.OrdinalIgnoreCase) ||
                e.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                e.Department.Contains(search, StringComparison.OrdinalIgnoreCase));

        FilteredEmployees.Clear();
        foreach (var employee in results)
            FilteredEmployees.Add(employee);
    }

    private static void ShowError(string title, Exception ex)
    {
        MessageBox.Show($"{title}\n\n{ex.GetBaseException().Message}",
            "Employee Management", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
