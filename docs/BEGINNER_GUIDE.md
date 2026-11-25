# Employee Management WPF App: A Beginner's Guide

This guide teaches the Employee Management app step by step, in very simple words.
It follows the same order as the **WPF Course Content** sheet. For each topic you get:

- **What it means** (plain English)
- **How the app uses it** (which part of the app)
- **Say this to your students** (one or two lines you can repeat in class)

It also has a **Backend basics** part (service layer, SQL connectivity, dependency injection) and a **1-hour session plan** for building the app live.

---

## 0. The big picture (read this first)

The app is a small Employee Management screen. On the left you fill a form. On the right you see a table of employees. You can Add, Edit, Delete, Search and Refresh. The data is saved in a SQL Server database.

The code is split into layers. Each layer has one job:

```
 Window (XAML)   <-- what the user sees
      |  Binding (no button-click code)
 ViewModel       <-- screen data + commands
      |  calls
 Service         <-- business rules / validation
      |  calls
 Repository      <-- talks to the database
      |  uses
 DbContext       <-- Entity Framework Core
      |
 SQL Server (table: Employees)
```

The **Employee model** is the "shape" of one employee and is used by every layer.
The **App startup class** is where the app starts and where all these pieces are connected (dependency injection).

**One sentence to remember:** *The View shows, the ViewModel thinks, the Service checks rules, the Repository saves.*

### The parts of the app

| Part | Job | Simple analogy |
|---|---|---|
| Model (`Employee`) | Data shape of an employee | A blank form template |
| View (main window XAML) | The screen layout | The shop front |
| Code-behind | C# behind the window, kept almost empty | The back door, rarely used |
| `ViewModelBase` | Tells the screen "a value changed" | A notification bell |
| `MainViewModel` | All screen data and actions | The shop manager |
| `RelayCommand` | Turns a method into a button action | A remote control button |
| Service (`EmployeeService`) | Validation and business rules | The quality checker |
| Repository (`EmployeeRepository`) | Database read/write | The store room keeper |
| `AppDbContext` | EF Core link to SQL Server | The door to the store room |
| App resources | Shared colours and styles | The paint and dress code |
| App startup | Startup + dependency injection | The building's main switch |

---

## Module 1: WPF Foundations

### 1.1 WPF overview, .NET desktop ecosystem, WPF vs Windows Forms, project structure, first app

**What it means**
- WPF (Windows Presentation Foundation) is Microsoft's framework for Windows desktop apps.
- The screen is written in **XAML** (an XML-like language). The logic is written in **C#**.
- WinForms: you drag controls and write click events. WPF: you describe the screen in XAML and **bind** it to data. WPF is better for MVVM, styling and scaling to any screen size.

**How the app uses it**
- The project file has three key settings:
  - `<TargetFramework>net8.0-windows</TargetFramework>` means .NET 8, Windows only.
  - `<UseWPF>true</UseWPF>` switches on WPF.
  - `<OutputType>WinExe</OutputType>` means a Windows app (no console window).
- The code is organised into folders by layer: Models, Views, ViewModels, Commands, Services, Repositories, Data.

**Say this to your students:** "A WPF app is just a .NET project with `UseWPF=true`. XAML draws the screen, C# does the work."

### 1.2 XAML fundamentals, namespaces, elements, attributes, property-element syntax, code-behind, application lifecycle

**What it means**
- An **element** is a tag, like `<TextBox/>`. It creates an object.
- An **attribute** sets a property: `FontSize="25"`.
- **Property-element syntax** sets a property using a child tag when the value is too big for one attribute, for example `<Grid.ColumnDefinitions>`.
- **Namespaces** (`xmlns`) tell XAML where the controls come from. `x:` is the XAML language itself (`x:Class`, `x:Key`, `x:Name`).
- **Code-behind** is the C# file paired with a XAML file.
- **Application lifecycle**: the app starts (`OnStartup`), shows a window, and closes (`OnExit`).

**How the app uses it**
- The main window XAML starts with the two standard namespaces.
- The page title is a simple element with attributes: `<TextBlock Text="Employee Management System" FontSize="25" FontWeight="Bold"/>`.
- The layout grid uses property-element syntax to define its two columns.
- The window's code-behind only calls `InitializeComponent()`, sets the DataContext, and has one small event handler. In MVVM we keep it almost empty.
- Lifecycle: the App class overrides `OnStartup` (builds the app and shows the main window) and `OnExit` (cleans up).
- Note: the App XAML has **no** `StartupUri`. The window is created in code so that dependency injection can give it its ViewModel.

**Say this to your students:** "Every tag is an object, every attribute is a property. The app starts in `OnStartup`."

---

## Module 2: Layout and Controls

### 2.1 Layout panels: Grid, StackPanel, DockPanel, WrapPanel, Canvas, margins, sizing, alignment

**What it means**

| Panel | What it does |
|---|---|
| `Grid` | Rows and columns, like a table |
| `StackPanel` | Puts children one after another (top to bottom, or left to right) |
| `DockPanel` | Sticks children to Top, Bottom, Left, Right; the last child fills the rest |
| `WrapPanel` | Like StackPanel but moves to the next line when there is no space |
| `Canvas` | Exact X/Y positions (rarely used for forms) |

- `Margin="0,4,0,10"` means Left, Top, Right, Bottom space outside the control.
- `Padding` is space inside the control.
- `Width="*"` means "take the remaining space". `Width="Auto"` means "only as big as needed". `Width="390"` is fixed.

**How the app uses it**
- `DockPanel` is the page frame: blue header docked Top, status bar docked Bottom, the main Grid fills the middle.
- `Grid` has two columns: the form on the left is `390` wide, the table on the right is `*`.
- `StackPanel` stacks the form fields vertically, and the radio buttons horizontally.
- `WrapPanel` holds the Save and Clear buttons.
- `ScrollViewer` lets the form scroll if the window is small.
- `Canvas` is **not used** (forms don't need exact positions).

**Say this to your students:** "DockPanel for the page frame, Grid for columns, StackPanel for the form."

### 2.2 Core and content controls, items controls, menus, toolbars, dialogs, commands, input gestures

**What it means**
- Core controls: `TextBlock` (read-only text), `TextBox` (input), `Button`, `CheckBox`, `RadioButton`, `DatePicker`, `ComboBox`.
- **Content controls** have one `Content` (a Button can contain text or even an image).
- **Items controls** show a list: `ComboBox`, `ListBox`, `DataGrid`. They use `ItemsSource`.
- **Dialogs**: `MessageBox.Show(...)`.
- **Commands**: a button runs a command instead of a click event (more in Module 3.3).

**How the app uses it**

| Control | Bound to |
|---|---|
| TextBoxes (First Name, Last Name, Email, Phone, Salary, Experience) | `FirstName`, `LastName`, ... |
| RadioButtons Male / Female / Other | `IsMale`, `IsFemale`, `IsOther` |
| ComboBox Department | list `Departments`, selected `Department` |
| ComboBox City | list `Cities`, selected `City` |
| DatePickers | `DateOfBirth`, `JoiningDate` |
| ListBox Skills (multi-select) | `AvailableSkills` |
| CheckBoxes Active / Permanent | `IsActive`, `IsPermanent` |
| Buttons Save / Clear / Delete | `SaveCommand`, `ClearCommand`, `DeleteCommand` |
| DataGrid | `FilteredEmployees` |
| StatusBar | `StatusMessage`, `Employees.Count` |

- Dialogs: the ViewModel shows a Yes/No box before deleting, and an error box when something fails.
- **Not in this app:** Menu, ToolBar, and input gestures (keyboard shortcuts). Easy demo to add inside `<Window>`:
  ```xml
  <Window.InputBindings>
      <KeyBinding Key="S" Modifiers="Ctrl" Command="{Binding SaveCommand}"/>
  </Window.InputBindings>
  ```
  Now Ctrl+S saves.

**Say this to your students:** "Lists use `ItemsSource`. Buttons use `Command`. We never write `Button_Click` here."

---

## Module 3: Data Binding and MVVM (the heart of this app)

### 3.1 Data binding fundamentals, DataContext, binding modes, UpdateSourceTrigger, value converters

**What it means**
- **Binding** connects a control property to a C# property. `Text="{Binding FirstName}"` means "show and edit the ViewModel's FirstName".
- **DataContext** is "the object the bindings look at". Set it once on the Window and every child control can use it.
- **Binding modes**:
  - `OneWay`: C# → screen only.
  - `TwoWay`: both directions (default for TextBox.Text, CheckBox.IsChecked, ComboBox.SelectedItem).
  - `OneTime`, `OneWayToSource`: less common.
- **UpdateSourceTrigger**: *when* the screen pushes the value back to C#.
  - `PropertyChanged` = on every key press.
  - `LostFocus` = when you leave the TextBox (the default for TextBox).
- **Value converter**: changes a value on the way, for example `bool` → `Visibility`.

**How the app uses it**
- DataContext: the window's constructor receives the ViewModel and does `DataContext = viewModel;`
- Binding + UpdateSourceTrigger: `Text="{Binding FirstName, UpdateSourceTrigger=PropertyChanged}"`.
- Explicit TwoWay: the radio buttons, and the DataGrid `SelectedItem` (selecting a row fills the form).
- Formatting without a converter: the Salary column uses `StringFormat=N2` (shows 25,000.00).
- **Not in this app:** a custom `IValueConverter`. Simple demo: show "Busy..." only while loading:
  ```xml
  <!-- in App resources -->
  <BooleanToVisibilityConverter x:Key="BoolToVis"/>
  <!-- in the window -->
  <TextBlock Text="Busy..." Visibility="{Binding IsBusy, Converter={StaticResource BoolToVis}}"/>
  ```

**Say this to your students:** "Set DataContext once, then `{Binding Name}` everywhere."

### 3.2 INotifyPropertyChanged, ObservableCollection, validation, error templates

**What it means**
- When a C# property changes, the screen does not know unless we **tell** it. `INotifyPropertyChanged` is that "bell".
- `ObservableCollection<T>` is a list that tells the screen when items are added or removed. A normal `List<T>` does not.
- **Validation**: checking input is correct. **Error templates**: the red border WPF draws around invalid controls.

**How the app uses it**
- `ViewModelBase`:
  - `OnPropertyChanged` rings the bell.
  - `SetProperty(ref field, value)` sets the value and rings the bell only if the value really changed.
  - `[CallerMemberName]` fills in the property name automatically, so you don't type `"FirstName"` by hand.
- Every ViewModel property looks like: `public string FirstName { get => _firstName; set => SetProperty(ref _firstName, value); }`
- The `Gender` setter also rings the bell for `IsMale`, `IsFemale`, `IsOther`, so the radio buttons update.
- `Employees` and `FilteredEmployees` are `ObservableCollection`s. The DataGrid updates by itself when we `Add` or `Clear`.
- Validation is done in the **service layer** (see Backend basics B1). Errors are shown in a MessageBox.
- **Not in this app:** WPF's built-in validation with red borders (`INotifyDataErrorInfo`) and custom error templates. Good "next step" exercise.

**Say this to your students:** "No `OnPropertyChanged`, no screen update. Use `ObservableCollection` for lists the screen shows."

### 3.3 MVVM architecture, ViewModel design, ICommand / RelayCommand, CanExecute

**What it means**
- **M**odel = data (`Employee`). **V**iew = XAML. **V**iew**M**odel = the C# class the View binds to.
- The ViewModel never touches controls directly. It only has properties and commands.
- `ICommand` has two parts: `Execute` (do the work) and `CanExecute` (is the button enabled?).
- `RelayCommand` is a small reusable class so we don't write a new command class for every button.

**How the app uses it**
- `RelayCommand` stores two delegates, "what to do" and "am I allowed". `RaiseCanExecuteChanged()` tells buttons to re-check if they should be enabled.
- The ViewModel constructor creates the commands:
  - Save: runs `SaveAsync`, enabled when not busy.
  - Delete: enabled only when not busy **and** a row is selected.
  - Clear, Refresh, Search.
- When `IsBusy` or `SelectedEmployee` changes, the ViewModel raises `CanExecuteChanged`, so the Delete button turns on/off automatically.
- XAML side: `Command="{Binding SaveCommand}"` on the buttons.
- Full flow of clicking **Save**:
  1. Button → `SaveCommand` → `SaveAsync()` in the ViewModel.
  2. ViewModel converts text to numbers (salary, experience) and builds an `Employee`.
  3. Calls the service's `SaveAsync(employee)` → validation + duplicate email check.
  4. Service calls the repository: `AddAsync` (Id is 0) or `UpdateAsync` (Id is not 0).
  5. Repository calls EF Core `SaveChangesAsync()` → row in SQL Server.
  6. ViewModel reloads the list, shows a status message, clears the form.

**Say this to your students:** "Buttons call commands, commands call the ViewModel, the ViewModel never knows about buttons."

---

## Module 4: Styling and Templates

### 4.1 Resources, StaticResource vs DynamicResource, styles, setters, triggers, visual states

**What it means**
- **Resources** are reusable things (colours, styles) with a key.
- `StaticResource`: looked up once at load. Fast. Most common.
- `DynamicResource`: looked up again if it changes (for example, switching themes at runtime).
- A **Style** is a set of **Setters** (property = value) applied to controls.
  - With `TargetType` and **no** key: applies to *all* controls of that type (implicit style).
  - With `x:Key`: only applies where you write `Style="{StaticResource Key}"`.
- **Triggers** change a style when something happens (for example mouse over).

**How the app uses it**
- App-level resources hold three colour brushes (primary, page background, text).
- Implicit styles for `TextBox`, `ComboBox`, `DatePicker`, `Button` (every button gets padding and a hand cursor).
- A keyed style `FieldLabel` used on every form label.
- The window background uses `{StaticResource PageBrush}`.
- **Not in this app:** DynamicResource, Triggers, Visual States. Quick trigger demo for the Button style:
  ```xml
  <Style.Triggers>
      <Trigger Property="IsEnabled" Value="False">
          <Setter Property="Opacity" Value="0.5"/>
      </Trigger>
  </Style.Triggers>
  ```

**Say this to your students:** "Put look-and-feel in the App resources once, not on every control."

### 4.2 ControlTemplate, DataTemplate, ItemsPanelTemplate, TemplateBinding, graphics, brushes, animation

**What it means**
- `ControlTemplate` replaces how a control *looks* (for example a round button).
- `DataTemplate` describes how one *data item* looks in a list.
- `ItemsPanelTemplate` chooses the panel a list uses (for example WrapPanel instead of StackPanel).
- `TemplateBinding` links a template part to the control's property.

**How the app uses it**
- Brushes and simple graphics: `Border` with `CornerRadius="10"` makes rounded white cards; colours are hex values like `#172554`.
- DataGrid columns (`DataGridTextColumn`, `DataGridCheckBoxColumn`) are a built-in kind of per-column template.
- **Not in this app:** custom ControlTemplate, DataTemplate, ItemsPanelTemplate, animation. Easy exercise: give the Skills ListBox a `DataTemplate` that shows a coloured dot before each skill.

---

## Module 5: Advanced WPF

### 5.1 Routed events, dependency properties, attached properties, custom user controls

**What it means**
- **Routed events** travel up (bubble) or down (tunnel) the control tree.
- **Dependency properties** are special WPF properties that support binding, styles and animation. `TextBox.Text` is one.
- **Attached properties** are set on a child but belong to a parent, for example `Grid.Column="1"` or `DockPanel.Dock="Top"`.
- **UserControl**: your own reusable piece of screen.

**How the app uses it**
- Attached properties: `DockPanel.Dock="Top"` on the header, `Grid.Column="0"` / `"1"` on the two cards.
- Routed event: `SelectionChanged` on the Skills ListBox, handled in the window's code-behind.
  - Why code-behind here? A ListBox's `SelectedItems` (plural) cannot be bound directly. So a tiny handler joins the selected skills into `SelectedSkillsText`. This is the only event handler in the app.
- Dependency properties: every property you bind to (`Text`, `IsChecked`, `SelectedItem`, `ItemsSource`) is one.
- **Not in this app:** writing your own dependency property or UserControl. Good exercise: move the form into an `EmployeeFormView` UserControl.

### 5.2 DataGrid and CollectionViewSource: editing, sorting, filtering, grouping, master-detail

**How the app uses it**
- DataGrid settings: `AutoGenerateColumns="False"` (we define columns), `IsReadOnly="True"` (edit in the form, not the grid), `SelectionMode="Single"`.
- **Master-detail:** select a row (master) → the left form fills (detail). The `SelectedEmployee` setter calls `LoadEmployeeIntoForm`.
- **Filtering:** `ApplySearch()` filters by name, email or department into `FilteredEmployees`. Click Search to apply.
- **Sorting:** clicking a column header sorts (built in). The database query also orders by first name, then last name.
- **Not in this app:** `CollectionViewSource` and grouping. Exercise: replace the manual filter with `ICollectionView.Filter` and group by Department.

### 5.3 Async programming, UI thread, Dispatcher, async/await, progress, cancellation

**What it means**
- WPF has **one UI thread**. If you do slow work on it, the window freezes.
- `async`/`await` lets the database work happen without freezing, and comes back to the UI thread automatically.
- `Dispatcher` is how a background thread updates the UI (not needed when you use `await` correctly).

**How the app uses it**
- Every database method is async: `GetAllAsync`, `SaveAsync`, `DeleteAsync`, `SaveChangesAsync`.
- An `IsBusy` flag is true while working, which disables buttons through `CanExecute`.
- Every service and repository method accepts a `CancellationToken` (ready for cancellation; the screen doesn't use it yet).
- **Not in this app:** Dispatcher, progress reporting (`IProgress<T>`), a Cancel button.
- Honest note: the ViewModel constructor starts loading with `LoadEmployeesAsync();` without `await` ("fire and forget"). It works because the method catches its own errors, but Visual Studio shows a warning. Explain it, don't copy it blindly.

---

## Module 6: Application Development

### 6.1 File handling, configuration, JSON, logging, exception handling, dependency injection

**How the app uses it**
- **Dependency injection:** see Backend basics B3 below.
- **Exception handling:** every ViewModel action has `try / catch / finally`. `catch` shows a friendly error box; `finally` resets `IsBusy`. The service throws `InvalidOperationException` with clear messages like "First name is required."
- **Configuration:** the connection string is written directly in the startup code. Improvement: move it to an `appsettings.json` file (see B2).
- **Not in this app:** file handling, JSON, logging. (The .NET Host already includes logging; you could inject `ILogger<MainViewModel>`.)

### 6.2 Performance, diagnostics, virtualization, binding diagnostics, profiling, testing, deployment

**How the app uses it**
- **Virtualization:** DataGrid virtualizes rows by default, so 10,000 employees stay fast.
- **Performance:** the repository uses `AsNoTracking()` for read-only queries, which is faster.
- **Binding diagnostics:** if a binding name is wrong, nothing crashes, the field just stays empty. Look in the Visual Studio **Output** window for `System.Windows.Data Error`. Demo this live by misspelling `FirstName`.
- **Testing:** the ViewModel depends on `IEmployeeService` (an interface), so you can unit test it with a fake service. No test project exists yet.
- **Deployment:** not set up. Simplest: `dotnet publish -c Release -r win-x64 --self-contained`. ClickOnce / MSIX are in Visual Studio's Publish wizard.

### 6.3 Final MVVM application integration, review, troubleshooting, demo

This *is* the app. Use the 1-hour plan at the end.

---

## Backend basics (service layer, SQL connectivity, dependency injection)

These three topics are what make the app "real". Teach them in this order: first **where data lives** (SQL), then **who guards it** (service), then **how the pieces are connected** (DI).

### B1. The service layer

**What it is**
A service is a class that holds the **business rules**. It sits between the ViewModel and the database code.

- The ViewModel should not know SQL.
- The Repository should not know business rules.
- The Service is the "middle person" that says *"Is this allowed?"* before anything is saved.

**Why use an interface (`IEmployeeService`)?**
The ViewModel asks for `IEmployeeService`, not `EmployeeService`. This means:
- You can swap the real service for a fake one in unit tests.
- The ViewModel doesn't care *how* the work is done, only *what* it can ask for.

**The contract (interface)**
```csharp
public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    Task SaveAsync(Employee employee, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
```

**What `SaveAsync` does in this app, step by step**
1. **Validate**: first name and last name required, email must contain `@` and `.`, department required, salary not negative, experience 0 to 60, date of birth not in the future.
2. **Duplicate check**: asks the repository "does this email already exist for a *different* employee?"
3. **Clean up**: trims spaces from names, email and phone.
4. **Decide**: `Id == 0` means new → `AddAsync`; otherwise → `UpdateAsync`.

If a rule fails, the service **throws** an exception with a friendly message. The ViewModel catches it and shows it.

**Repository vs Service (students always ask)**

| Repository | Service |
|---|---|
| "How do I talk to the database?" | "What is allowed?" |
| `Add`, `Update`, `Delete`, `GetAll`, `EmailExists` | Validation, duplicate rules, cleanup, add-or-update decision |
| Knows EF Core | Knows nothing about EF Core |

**Say this to your students:** "The repository is the store room keeper. The service is the security guard at the door."

### B2. SQL connectivity (SQL Server + Entity Framework Core)

**The pieces**

| Piece | What it is |
|---|---|
| **SQL Server LocalDB** | A free, small SQL Server that comes with Visual Studio. Good for learning. |
| **Connection string** | The "address" of the database: which server, which database, how to log in. |
| **Entity Framework Core (EF Core)** | Lets you use C# classes instead of writing SQL by hand. |
| **DbContext** | The C# class that represents your database session. |
| **DbSet&lt;Employee&gt;** | Represents the `Employees` table. |
| **Migration** | A generated C# file that describes how to create or change tables. |

**NuGet packages needed**
- `Microsoft.EntityFrameworkCore.SqlServer`: EF Core for SQL Server.
- `Microsoft.EntityFrameworkCore.Tools`: the `Add-Migration` / `Update-Database` commands.
- `Microsoft.EntityFrameworkCore.Design`: needed by the migration tools.

**Reading a connection string**
```
Server=(localdb)\MSSQLLocalDB;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;
```
- `Server=(localdb)\MSSQLLocalDB`: use the LocalDB instance on this PC. For a full SQL Server, use its name, like `Server=localhost` or `Server=MYSERVER\SQLEXPRESS`.
- `Database=EmployeeManagementDb`: the database name.
- `Trusted_Connection=True`: log in with your Windows account (no password in the string). For SQL login use `User Id=...;Password=...;` instead.
- `TrustServerCertificate=True`: accept the local server's certificate (fine for development).

**Good practice:** keep the connection string out of code, in `appsettings.json`, and never commit real passwords:
```json
{ "ConnectionStrings": { "Default": "Server=(localdb)\\MSSQLLocalDB;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;" } }
```
Then in startup: `options.UseSqlServer(context.Configuration.GetConnectionString("Default"))`.
(In this app it is still written directly in the startup code. Changing it is a nice exercise.)

**The DbContext in this app**
```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Employee> Employees => Set<Employee>();
}
```
In `OnModelCreating` it sets the table rules: maximum text lengths, required fields, a **unique index on Email**, and salary as `decimal(18,2)`.

**Creating the database (one time)**
The database is **not** created automatically. In Visual Studio → Tools → NuGet Package Manager → Package Manager Console:
```powershell
Add-Migration InitialCreate
Update-Database
```
Or from a terminal in the project folder:
```bash
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet ef migrations add InitialCreate
dotnet ef database update
```
`Add-Migration` writes the "plan". `Update-Database` runs it against SQL Server. Whenever you change the `Employee` class, add a new migration and update again.

**Common EF Core calls used by the repository**

| C# | Roughly the SQL |
|---|---|
| `Employees.AsNoTracking().OrderBy(...).ToListAsync()` | `SELECT * FROM Employees ORDER BY ...` |
| `Employees.AnyAsync(e => e.Email == email)` | `SELECT CASE WHEN EXISTS(...)` |
| `Employees.Add(e)` + `SaveChangesAsync()` | `INSERT` |
| `Employees.Update(e)` + `SaveChangesAsync()` | `UPDATE` |
| `Employees.Remove(e)` + `SaveChangesAsync()` | `DELETE` |

Nothing is sent to SQL Server until `SaveChangesAsync()` is called.

**Say this to your students:** "The connection string says *where*, the DbContext says *what*, migrations create the tables, `SaveChangesAsync` actually saves."

### B3. Dependency injection (DI) in WPF

**The problem DI solves**
Without DI, the window would have to build everything itself:
```csharp
var db = new AppDbContext(options);
var repo = new EmployeeRepository(db);
var service = new EmployeeService(repo);
var vm = new MainViewModel(service);
var window = new MainWindow(vm);
```
That's a lot of wiring, and every class is glued to the others.

**With DI**
Each class just **asks** for what it needs in its constructor:
```csharp
public EmployeeService(IEmployeeRepository repository) { ... }
public MainViewModel(IEmployeeService employeeService) { ... }
public MainWindow(MainViewModel viewModel) { ... }
```
A **DI container** reads a list of registrations and builds the whole chain for you.

**How WPF gets DI (it doesn't have it built in)**
WPF has no DI out of the box, so this app uses the **.NET Generic Host** (`Microsoft.Extensions.Hosting` NuGet package), the same system ASP.NET Core uses.

Three steps:

1. **Remove `StartupUri`** from the App XAML, so WPF doesn't create the window by itself.
2. **Register services** in `OnStartup`:
   ```csharp
   _host = Host.CreateDefaultBuilder()
       .ConfigureServices((context, services) =>
       {
           services.AddDbContext<AppDbContext>(o => o.UseSqlServer("...connection string..."));
           services.AddScoped<IEmployeeRepository, EmployeeRepository>();
           services.AddScoped<IEmployeeService, EmployeeService>();
           services.AddTransient<MainViewModel>();
           services.AddTransient<MainWindow>();
       })
       .Build();
   await _host.StartAsync();
   ```
3. **Ask for the window** and show it:
   ```csharp
   var mainWindow = _host.Services.GetRequiredService<MainWindow>();
   mainWindow.Show();
   ```
   The container sees `MainWindow` needs `MainViewModel` → needs `IEmployeeService` → needs `IEmployeeRepository` → needs `AppDbContext`, and builds all of them in the right order.

And in `OnExit`, stop and dispose the host to clean up.

**Lifetimes (how long an object lives)**

| Registration | Meaning | Used here for |
|---|---|---|
| `AddSingleton` | One object for the whole app | (not used) |
| `AddScoped` | One object per "scope" | DbContext, Repository, Service |
| `AddTransient` | A new object every time you ask | ViewModel, Window |

Note for curious students: in a desktop app with one window there is only the root scope, so "scoped" objects effectively live for the whole app. That's fine for this small app. In bigger apps, create a scope per operation or use `IDbContextFactory`.

**Say this to your students:** "Classes say what they need. The container gives it to them. We register once, at startup."

---

## Before the session: checklist for you (the trainer)

1. Install **Visual Studio 2022** with the **.NET desktop development** workload (includes SQL Server LocalDB).
2. Check .NET 8: run `dotnet --version` → should start with `8.`.
3. Get the project source and open the project file in Visual Studio.
4. Run the migration commands from B2 once.
5. Press **F5**. Add 2 or 3 employees so the grid is not empty during the demo.
6. Ask students to do steps 1 and 2 **before** the session. Installing Visual Studio takes longer than one hour.

### Common problems and fixes

| Problem | Fix |
|---|---|
| "Cannot open database EmployeeManagementDb" | You did not run `Update-Database`. |
| "A network-related error..." with LocalDB | Run `sqllocaldb start MSSQLLocalDB`, or change the connection string to your SQL Server. |
| `Add-Migration` not recognized | Run it in **Package Manager Console**, with this project selected as Default project. |
| "Unable to resolve service for type ..." | A class was not registered in `ConfigureServices`. |
| Two windows open, or an empty window | `StartupUri` is still in the App XAML. Remove it. |
| A field stays empty | Binding name typo. Check the Output window for `System.Windows.Data Error`. |
| Delete button always disabled | Select a row in the grid first (that's `CanExecute` working). |
| "An employee with this email address already exists" | Email must be unique (database index + service check). |
| Project doesn't open on Mac/Linux | WPF is Windows-only (`net8.0-windows`). |

### Known limitations (good discussion points)

- When you edit an employee, the Skills ListBox does **not** re-select their saved skills (the text is kept, but the boxes look empty). Selecting skills again replaces them.
- Search runs only when you click **Search**, not while typing.
- The ViewModel calls `MessageBox.Show` directly. Purists would put it behind an `IDialogService` so the ViewModel can be unit tested.
- The connection string is in code, not in a config file.

---

## 1-hour session plan: "Build the Employee Management app"

Goal: developers understand and build the app layer by layer. You type, they follow.

| Time | Step | What to do | Layer |
|---|---|---|---|
| 0:00 to 0:05 | Intro | Show the finished app running. Draw the layer diagram from section 0. | - |
| 0:05 to 0:10 | 1. Create project | `dotnet new wpf -n EmployeeManagementMVVM -f net8.0` (or Visual Studio → New → WPF Application). Show `UseWPF` in the project file. Add NuGet packages: EF Core SqlServer, Tools, Design (8.0.11) and Microsoft.Extensions.Hosting (8.0.1). Create the layer folders. | Project |
| 0:10 to 0:14 | 2. Model | Write the `Employee` class. "This becomes a table row." | Model |
| 0:14 to 0:20 | 3. Database | `AppDbContext` with `DbSet<Employee>` and the rules. Explain the connection string (B2). | Data |
| 0:20 to 0:26 | 4. Repository + Service | Interfaces first, then classes. "Repository saves, Service checks rules" (B1). | Repository, Service |
| 0:26 to 0:32 | 5. MVVM plumbing | `ViewModelBase` (the bell) and `RelayCommand` (the remote button). | ViewModel, Commands |
| 0:32 to 0:42 | 6. ViewModel | Properties, the two `ObservableCollection`s, commands, then `SaveAsync`, `DeleteAsync`, `ApplySearch`. Paste the long property list to save time. | ViewModel |
| 0:42 to 0:50 | 7. View | Move the main window into the Views folder (fix `x:Class` and namespace). Build: DockPanel → Grid (2 columns) → form StackPanel + DataGrid. Add styles to App resources. | View |
| 0:50 to 0:54 | 8. Wire up (DI) | Generic Host + registrations in `OnStartup` (B3). **Remove `StartupUri`**. Set `DataContext` in the window constructor. | Startup |
| 0:54 to 0:58 | 9. Run | `Add-Migration InitialCreate`, `Update-Database`, F5. Add, edit, search, delete an employee. | - |
| 0:58 to 1:00 | Wrap-up | Recap: "View shows, ViewModel thinks, Service checks, Repository saves." | - |

**Tips to stay on time**
- Don't type the 15 fields by hand. Type 3 (FirstName, Email, Department) to explain, then paste the rest.
- Paste the full form XAML; explain only one TextBox, one ComboBox, one RadioButton and the DataGrid.
- If something breaks, switch to the finished version and keep going. Debug later.

### Quick self-check questions (for you, and for the end of the session)

1. Where is the DataContext set? *(In the window's constructor)*
2. Why `ObservableCollection` and not `List`? *(It tells the grid when items change)*
3. What makes the Delete button disabled? *(`CanExecute`: not busy and a row selected)*
4. Who checks that the email is valid? *(The service layer)*
5. Who creates `EmployeeService`? *(The DI container, at startup)*
6. What happens if you forget `OnPropertyChanged`? *(The screen does not update)*
7. Why is there code-behind for Skills? *(ListBox.SelectedItems cannot be bound)*
8. What does `Update-Database` do? *(Runs the migration to create the tables in SQL Server)*
9. Why do we remove `StartupUri` when using DI? *(So the container creates the window with its ViewModel)*
10. What's the difference between AddScoped and AddTransient? *(Scoped: one per scope; Transient: new every time)*
