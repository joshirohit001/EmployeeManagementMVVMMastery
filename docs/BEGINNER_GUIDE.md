# Employee Management WPF App: A Beginner's Guide

This guide teaches the project in this repository step by step, in very simple words.
It follows the same order as the **WPF Course Content** Excel sheet. For each topic you get:

- **What it means** (plain English)
- **Where it is in this project** (file and line)
- **Say this to your students** (one or two lines you can repeat in class)

At the end there is a **1-hour session plan** for building the app live with developers.

---

## 0. The big picture (read this first)

The app is a small Employee Management screen. On the left you fill a form. On the right you see a table of employees. You can Add, Edit, Delete, Search and Refresh. The data is saved in a SQL Server database.

The code is split into layers. Each layer has one job:

```
 Window (XAML)  <-- what the user sees            Views/MainWindow.xaml
      |  Binding (no button-click code)
 ViewModel      <-- screen data + commands        ViewModels/MainViewModel.cs
      |  calls
 Service        <-- business rules / validation   Services/EmployeeService.cs
      |  calls
 Repository     <-- talks to the database         Repositories/EmployeeRepository.cs
      |  uses
 DbContext      <-- Entity Framework Core         Data/AppDbContext.cs
      |
 SQL Server LocalDB (table: Employees)
```

`Models/Employee.cs` is the "shape" of one employee and is used by every layer.
`App.xaml.cs` is where the app starts and where all these pieces are connected (dependency injection).

**One sentence to remember:** *The View shows, the ViewModel thinks, the Service checks rules, the Repository saves.*

### Folder map

| Folder / file | Job | Simple analogy |
|---|---|---|
| `Models/Employee.cs` | Data shape of an employee | A blank form template |
| `Views/MainWindow.xaml` | The screen layout (XAML) | The shop front |
| `Views/MainWindow.xaml.cs` | Code-behind (kept almost empty) | The back door, rarely used |
| `ViewModels/ViewModelBase.cs` | Tells the screen "a value changed" | A notification bell |
| `ViewModels/MainViewModel.cs` | All screen data and actions | The shop manager |
| `Commands/RelayCommand.cs` | Turns a method into a button action | A remote control button |
| `Services/` | Validation and business rules | The quality checker |
| `Repositories/` | Database read/write | The store room keeper |
| `Data/AppDbContext.cs` | EF Core link to SQL Server | The door to the store room |
| `App.xaml` | Shared colours and styles | The paint and dress code |
| `App.xaml.cs` | Startup + dependency injection | The building's main switch |

---

## Module 1: WPF Foundations

### 1.1 WPF overview, .NET desktop ecosystem, WPF vs Windows Forms, project structure, first app

**What it means**
- WPF (Windows Presentation Foundation) is Microsoft's framework for Windows desktop apps.
- The screen is written in **XAML** (an XML-like language). The logic is written in **C#**.
- WinForms: you drag controls and write click events. WPF: you describe the screen in XAML and **bind** it to data. WPF is better for MVVM, styling and scaling to any screen size.

**Where it is in this project**
- `EmployeeManagementMVVM.csproj` lines 3 to 7:
  - `<TargetFramework>net8.0-windows</TargetFramework>` means .NET 8, Windows only.
  - `<UseWPF>true</UseWPF>` switches on WPF.
  - `<OutputType>WinExe</OutputType>` means a Windows app (no console window).
- Project structure: see the folder map above.

**Say this to your students:** "A WPF app is just a .NET project with `UseWPF=true`. XAML draws the screen, C# does the work."

### 1.2 XAML fundamentals, namespaces, elements, attributes, property-element syntax, code-behind, application lifecycle

**What it means**
- An **element** is a tag, like `<TextBox/>`. It creates an object.
- An **attribute** sets a property: `FontSize="25"`.
- **Property-element syntax** sets a property using a child tag when the value is too big for one attribute, for example `<Grid.ColumnDefinitions>`.
- **Namespaces** (`xmlns`) tell XAML where the controls come from. `x:` is the XAML language itself (`x:Class`, `x:Key`, `x:Name`).
- **Code-behind** is the `.xaml.cs` file paired with a `.xaml` file.
- **Application lifecycle**: the app starts (`OnStartup`), shows a window, and closes (`OnExit`).

**Where it is in this project**
- Namespaces: `Views/MainWindow.xaml` lines 1 to 3.
- Element + attributes: `MainWindow.xaml` line 13, `<TextBlock Text="Employee Management System" FontSize="25" .../>`.
- Property-element syntax: `MainWindow.xaml` lines 31 to 34, `<Grid.ColumnDefinitions>`.
- Code-behind: `Views/MainWindow.xaml.cs`. Look how small it is. In MVVM we keep it almost empty.
- Lifecycle: `App.xaml.cs` lines 18 to 40 (`OnStartup` builds the app and shows `MainWindow`) and lines 42 to 51 (`OnExit` cleans up).
- Note: `App.xaml` has **no** `StartupUri`. The window is created in code so that dependency injection can give it its ViewModel.

**Say this to your students:** "Every tag is an object, every attribute is a property. The app starts in `App.xaml.cs` → `OnStartup`."

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

**Where it is in this project** (`Views/MainWindow.xaml`)
- `DockPanel` line 10: header docked Top (line 11), status bar docked Bottom (line 20), the Grid fills the middle.
- `Grid` lines 30 to 34: two columns, left form is `390` wide, right table is `*`.
- `StackPanel` line 39: the form fields stack vertically. Line 56: radio buttons stacked horizontally.
- `WrapPanel` line 97: Save and Clear buttons.
- `ScrollViewer` line 38: the form scrolls if the window is small.
- `Canvas`: **not used** in this project (forms don't need exact positions).

**Say this to your students:** "DockPanel for the page frame, Grid for columns, StackPanel for the form."

### 2.2 Core and content controls, items controls, menus, toolbars, dialogs, commands, input gestures

**What it means**
- Core controls: `TextBlock` (read-only text), `TextBox` (input), `Button`, `CheckBox`, `RadioButton`, `DatePicker`, `ComboBox`.
- **Content controls** have one `Content` (a Button can contain text or even an image).
- **Items controls** show a list: `ComboBox`, `ListBox`, `DataGrid`. They use `ItemsSource`.
- **Dialogs**: `MessageBox.Show(...)`.
- **Commands**: a button runs a command instead of a click event (more in Module 3.3).

**Where it is in this project** (`Views/MainWindow.xaml`)
| Control | Line | Bound to |
|---|---|---|
| TextBox First Name | 44 | `FirstName` |
| RadioButton Male/Female/Other | 57 to 62 | `IsMale`, `IsFemale`, `IsOther` |
| ComboBox Department | 66 to 68 | `Departments` list, selected `Department` |
| ComboBox City | 71 to 72 | `Cities`, `City` |
| DatePicker | 75, 78 | `DateOfBirth`, `JoiningDate` |
| ListBox Skills (multi-select) | 87 to 90 | `AvailableSkills` |
| CheckBox Active / Permanent | 92 to 95 | `IsActive`, `IsPermanent` |
| Buttons Save / Clear / Delete | 98 to 104 | `SaveCommand`, `ClearCommand`, `DeleteCommand` |
| DataGrid | 130 onward | `FilteredEmployees` |
| StatusBar | 20 to 28 | `StatusMessage`, `Employees.Count` |

- Dialogs: `ViewModels/MainViewModel.cs` line 222 (delete confirmation) and line 319 (error box).
- **Not in this project:** Menu, ToolBar, and input gestures (keyboard shortcuts). Easy demo to add:
  ```xml
  <Window.InputBindings>
      <KeyBinding Key="S" Modifiers="Ctrl" Command="{Binding SaveCommand}"/>
  </Window.InputBindings>
  ```
  Put this inside `<Window>` and Ctrl+S will save.

**Say this to your students:** "Lists use `ItemsSource`. Buttons use `Command`. We never write `Button_Click` here."

---

## Module 3: Data Binding and MVVM (the heart of this project)

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

**Where it is in this project**
- DataContext: `Views/MainWindow.xaml.cs` line 12, `DataContext = viewModel;`
- Binding + UpdateSourceTrigger: `MainWindow.xaml` line 44, `{Binding FirstName, UpdateSourceTrigger=PropertyChanged}`.
- Explicit TwoWay: line 58 (radio buttons) and the DataGrid `SelectedItem` (selecting a row fills the form).
- Formatting without a converter: the Salary column uses `StringFormat=N2` (shows 25,000.00).
- **Not in this project:** a custom `IValueConverter`. Simple demo you can add: show a "Busy..." text only when `IsBusy` is true using the built-in `BooleanToVisibilityConverter`:
  ```xml
  <!-- in App.xaml resources -->
  <BooleanToVisibilityConverter x:Key="BoolToVis"/>
  <!-- in MainWindow.xaml -->
  <TextBlock Text="Busy..." Visibility="{Binding IsBusy, Converter={StaticResource BoolToVis}}"/>
  ```

**Say this to your students:** "Set DataContext once, then `{Binding Name}` everywhere."

### 3.2 INotifyPropertyChanged, ObservableCollection, validation, error templates

**What it means**
- When a C# property changes, the screen does not know unless we **tell** it. `INotifyPropertyChanged` is that "bell".
- `ObservableCollection<T>` is a list that tells the screen when items are added or removed. A normal `List<T>` does not.
- **Validation**: checking input is correct. **Error templates**: the red border WPF draws around invalid controls.

**Where it is in this project**
- `ViewModels/ViewModelBase.cs`:
  - `OnPropertyChanged` rings the bell.
  - `SetProperty(ref field, value)` sets the value and rings the bell only if the value really changed.
  - `[CallerMemberName]` fills in the property name automatically, so you don't type `"FirstName"` by hand.
- Use of it: `MainViewModel.cs`, for example `public string FirstName { get => _firstName; set => SetProperty(ref _firstName, value); }`
- A property that rings *other* bells: `Gender` setter also calls `OnPropertyChanged(nameof(IsMale))` etc., so radio buttons update.
- `ObservableCollection`: `MainViewModel.cs` lines 53 to 54, `Employees` and `FilteredEmployees`. The DataGrid updates by itself when we `Add` or `Clear`.
- Validation: done in the service, `Services/EmployeeService.cs` method `Validate` (first name required, valid email, department selected, salary not negative, experience 0 to 60, birth date not in future) plus a duplicate email check. Errors are shown in a MessageBox.
- **Not in this project:** WPF's built-in validation with red borders (`INotifyDataErrorInfo` / `ValidatesOnDataErrors`) and custom error templates. Good "next step" exercise.

**Say this to your students:** "No `OnPropertyChanged`, no screen update. Use `ObservableCollection` for lists the screen shows."

### 3.3 MVVM architecture, ViewModel design, ICommand / RelayCommand, CanExecute

**What it means**
- **M**odel = data (`Employee`). **V**iew = XAML. **V**iew**M**odel = the C# class the View binds to.
- The ViewModel never touches controls directly. It only has properties and commands.
- `ICommand` has two parts: `Execute` (do the work) and `CanExecute` (is the button enabled?).
- `RelayCommand` is a small reusable class so we don't write a new command class for every button.

**Where it is in this project**
- `Commands/RelayCommand.cs`: stores two delegates, `_execute` and `_canExecute`. `RaiseCanExecuteChanged()` tells buttons to re-check if they should be enabled.
- `MainViewModel.cs` lines 40 to 48 create the commands:
  - Save: runs `SaveAsync`, enabled when not busy.
  - Delete: enabled only when not busy **and** a row is selected.
  - Clear, Refresh, Search.
- `MainViewModel.cs` lines 121 to 124 and 136: when `IsBusy` or `SelectedEmployee` changes, we raise `CanExecuteChanged`, so the Delete button turns on/off automatically.
- XAML side: `Command="{Binding SaveCommand}"` on the buttons.
- Full flow of clicking **Save**:
  1. Button → `SaveCommand` → `SaveAsync()` in the ViewModel.
  2. ViewModel converts text to numbers (salary, experience) and builds an `Employee`.
  3. Calls `_employeeService.SaveAsync(employee)` → validation + duplicate email check.
  4. Service calls Repository `AddAsync` (Id is 0) or `UpdateAsync` (Id is not 0).
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

**Where it is in this project**
- `App.xaml`: brushes `PrimaryBrush`, `PageBrush`, `TextBrush`.
- Implicit styles: `TextBox`, `ComboBox`, `DatePicker`, `Button` (every button gets padding and a hand cursor).
- Keyed style: `FieldLabel` used on every label, `Style="{StaticResource FieldLabel}"`.
- StaticResource use: `MainWindow.xaml` line 8, `Background="{StaticResource PageBrush}"`.
- **Not in this project:** DynamicResource, Triggers, Visual States. Quick trigger demo for `App.xaml` Button style:
  ```xml
  <Style.Triggers>
      <Trigger Property="IsEnabled" Value="False">
          <Setter Property="Opacity" Value="0.5"/>
      </Trigger>
  </Style.Triggers>
  ```

**Say this to your students:** "Put look-and-feel in `App.xaml` once, not on every control."

### 4.2 ControlTemplate, DataTemplate, ItemsPanelTemplate, TemplateBinding, graphics, brushes, animation

**What it means**
- `ControlTemplate` replaces how a control *looks* (for example a round button).
- `DataTemplate` describes how one *data item* looks in a list.
- `ItemsPanelTemplate` chooses the panel a list uses (for example WrapPanel instead of StackPanel).
- `TemplateBinding` links a template part to the control's property.

**Where it is in this project**
- Brushes and simple graphics: `Border` with `CornerRadius="10"` (rounded white cards) and hex colours like `#172554`.
- DataGrid columns (`DataGridTextColumn`, `DataGridCheckBoxColumn`) are a built-in kind of template per column.
- **Not in this project:** custom ControlTemplate, DataTemplate, ItemsPanelTemplate, animation. Easy exercise: give the Skills ListBox a `DataTemplate` that shows a small coloured dot before each skill.

---

## Module 5: Advanced WPF

### 5.1 Routed events, dependency properties, attached properties, custom user controls

**What it means**
- **Routed events** travel up (bubble) or down (tunnel) the control tree.
- **Dependency properties** are special WPF properties that support binding, styles and animation. `TextBox.Text` is one.
- **Attached properties** are set on a child but belong to a parent, for example `Grid.Column="1"` or `DockPanel.Dock="Top"`.
- **UserControl**: your own reusable piece of screen.

**Where it is in this project**
- Attached properties: `DockPanel.Dock="Top"` (line 11), `Grid.Column="0"` / `"1"`, and in the search bar grid.
- Routed event: `SelectionChanged="SkillsListBox_SelectionChanged"` on the Skills ListBox (line 89), handled in `MainWindow.xaml.cs`.
  - Why code-behind here? A ListBox's `SelectedItems` (plural) cannot be bound directly. So a tiny handler joins the selected skills into `SelectedSkillsText`. This is the only event handler in the app.
- Dependency properties: every property you bind to (`Text`, `IsChecked`, `SelectedItem`, `ItemsSource`) is one.
- **Not in this project:** writing your own dependency property or UserControl. Good exercise: move the form into an `EmployeeFormView` UserControl.

### 5.2 DataGrid and CollectionViewSource: editing, sorting, filtering, grouping, master-detail

**Where it is in this project**
- `MainWindow.xaml` DataGrid: `AutoGenerateColumns="False"` (we define columns), `IsReadOnly="True"` (edit in the form, not in the grid), `SelectionMode="Single"`.
- **Master-detail:** select a row (master) → the left form fills (detail). This happens in `MainViewModel.SelectedEmployee` setter → `LoadEmployeeIntoForm`.
- **Filtering:** `MainViewModel.ApplySearch()` (line 301) filters by name, email or department into `FilteredEmployees`. Click Search to apply.
- **Sorting:** clicking a DataGrid column header sorts (built in). The database query also orders by first name then last name (`EmployeeRepository.GetAllAsync`).
- **Not in this project:** `CollectionViewSource` and grouping. Exercise: replace the manual filter with `ICollectionView.Filter` and group by Department.

### 5.3 Async programming, UI thread, Dispatcher, async/await, progress, cancellation

**What it means**
- WPF has **one UI thread**. If you do slow work on it, the window freezes.
- `async`/`await` lets the database work happen without freezing, and comes back to the UI thread automatically.
- `Dispatcher` is how a background thread updates the UI (not needed when you use `await` correctly).

**Where it is in this project**
- Every database method is async: `GetAllAsync`, `SaveAsync`, `DeleteAsync`, `SaveChangesAsync`.
- `IsBusy` flag: set to true while working, which disables buttons through `CanExecute`.
- `CancellationToken` parameters exist on every service and repository method (ready for cancellation, but the UI does not pass one yet).
- **Not in this project:** Dispatcher, progress reporting (`IProgress<T>`), a Cancel button.
- Honest note: the constructor calls `LoadEmployeesAsync();` without `await` (line 50). That's "fire and forget". It works here because the method catches its own errors, but Visual Studio shows a warning. Explain it, don't copy the pattern blindly.

---

## Module 6: Application Development

### 6.1 File handling, configuration, JSON, logging, exception handling, dependency injection

**Where it is in this project**
- **Dependency injection (DI):** `App.xaml.cs` lines 22 to 34 use the .NET Generic Host.
  - `AddDbContext<AppDbContext>` registers EF Core with the connection string.
  - `AddScoped<IEmployeeRepository, EmployeeRepository>()` means "when someone asks for `IEmployeeRepository`, give an `EmployeeRepository`".
  - `AddTransient<MainViewModel>()` and `AddTransient<MainWindow>()`.
  - `GetRequiredService<MainWindow>()` builds the window; DI automatically creates the ViewModel → Service → Repository → DbContext chain through the constructors.
- **Exception handling:** every ViewModel action has `try / catch / finally`. `catch` shows `ShowError(...)`; `finally` resets `IsBusy`. The service throws `InvalidOperationException` with friendly messages.
- **Configuration:** the connection string is hard-coded in `App.xaml.cs` line 27. Improvement: move it to `appsettings.json`.
- **Not in this project:** file handling, JSON, logging. (The Host already includes logging; you could inject `ILogger<MainViewModel>`.)

**Say this to your students:** "We never write `new EmployeeService(new EmployeeRepository(...))`. DI does it for us."

### 6.2 Performance, diagnostics, virtualization, binding diagnostics, profiling, testing, deployment

**Where it is in this project**
- **Virtualization:** DataGrid virtualizes rows by default, so 10,000 employees stay fast.
- **Performance:** `AsNoTracking()` in `EmployeeRepository.GetAllAsync` makes read-only queries faster.
- **Binding diagnostics:** if a binding name is wrong, nothing crashes, the field just stays empty. Look in Visual Studio **Output** window for `System.Windows.Data Error`. Demo this live by misspelling `FirstName`.
- **Testing:** because the ViewModel depends on `IEmployeeService` (an interface), you can unit test it with a fake service. No test project exists yet.
- **Deployment:** not set up. Simplest is `dotnet publish -c Release -r win-x64 --self-contained`. ClickOnce / MSIX are options in Visual Studio's Publish wizard.

### 6.3 Final MVVM application integration, review, troubleshooting, demo

This *is* the project. Use the 1-hour plan below.

---

## Database part (EF Core + SQL Server) — not in the Excel, but needed

- `Data/AppDbContext.cs`: `DbSet<Employee> Employees` becomes a table called `Employees`. `OnModelCreating` sets rules: max lengths, required fields, **unique email index**, salary as `decimal(18,2)`.
- `Repositories/EmployeeRepository.cs`: plain EF Core calls: `ToListAsync`, `AnyAsync`, `Add`, `Update`, `Remove`, `SaveChangesAsync`.
- **Important:** the database is **not** created automatically. Run once in Package Manager Console:
  ```powershell
  Add-Migration InitialCreate
  Update-Database
  ```
  Or in a terminal inside the project folder:
  ```bash
  dotnet tool install --global dotnet-ef --version 8.0.11
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```
  `dotnet ef` works because `AppDbContext` has a constructor taking options and the app uses the Generic Host.

---

## Before the session: checklist for you (the trainer)

1. Install **Visual Studio 2022** with the **.NET desktop development** workload (includes SQL Server LocalDB).
2. Check .NET 8: run `dotnet --version` → should start with `8.`.
3. Clone: `git clone https://github.com/joshirohit001/EmployeeManagementMVVMMastery`
4. Open `EmployeeManagementMVVM/EmployeeManagementMVVM.csproj` in Visual Studio.
5. Run the migration commands above once.
6. Press **F5**. Add 2 or 3 employees so the grid is not empty during the demo.
7. Ask students to do steps 1 to 2 **before** the session. Installing Visual Studio takes longer than one hour.

### Common problems and fixes

| Problem | Fix |
|---|---|
| "Cannot open database EmployeeManagementDb" | You did not run `Update-Database`. |
| "A network-related error... LocalDB" | Run `sqllocaldb start MSSQLLocalDB`, or change the connection string to your SQL Server. |
| `Add-Migration` not recognized | Run it in **Package Manager Console**, with this project selected as Default project. |
| A field stays empty | Binding name typo. Check the Output window for `System.Windows.Data Error`. |
| Delete button always disabled | Select a row in the grid first (that's `CanExecute` working). |
| "An employee with this email address already exists" | Email must be unique (rule in `AppDbContext` + check in `EmployeeService`). |
| Project doesn't open on Mac/Linux | WPF is Windows-only (`net8.0-windows`). |

### Known limitations (good discussion points)

- When you edit an employee, the Skills ListBox does **not** re-select their saved skills (the text is kept, but the boxes look empty). Selecting skills again replaces them.
- Search runs only when you click **Search**, not while typing.
- The ViewModel calls `MessageBox.Show` directly. Purists would put it behind an `IDialogService` so the ViewModel can be unit tested.
- The project README says radio buttons use code-behind; actually they use bindings (`IsMale`, `IsFemale`, `IsOther`). Only the Skills ListBox uses code-behind.

---

## 1-hour session plan: "Build the Employee Management app"

Goal: developers understand and build the app layer by layer. You type, they follow (or they follow from the repo).

| Time | Step | What to do | Files |
|---|---|---|---|
| 0:00 to 0:05 | Intro | Show the finished app running. Draw the layer diagram from section 0. | - |
| 0:05 to 0:10 | 1. Create project | `dotnet new wpf -n EmployeeManagementMVVM -f net8.0` (or Visual Studio → New → WPF Application). Show `.csproj` (`UseWPF`). Add NuGet packages: `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Tools`, `Microsoft.EntityFrameworkCore.Design` (all 8.0.11), `Microsoft.Extensions.Hosting` (8.0.1). Create folders. | `.csproj` |
| 0:10 to 0:14 | 2. Model | Write the `Employee` class. "This becomes a table row." | `Models/Employee.cs` |
| 0:14 to 0:20 | 3. Database | `AppDbContext` with `DbSet<Employee>` and the rules. | `Data/AppDbContext.cs` |
| 0:20 to 0:26 | 4. Repository + Service | Interfaces first, then classes. Explain "Repository saves, Service checks rules". | `Repositories/`, `Services/` |
| 0:26 to 0:32 | 5. MVVM plumbing | `ViewModelBase` (the bell) and `RelayCommand` (the remote button). | `ViewModels/ViewModelBase.cs`, `Commands/RelayCommand.cs` |
| 0:32 to 0:42 | 6. ViewModel | Properties, the two `ObservableCollection`s, commands, then `SaveAsync`, `DeleteAsync`, `ApplySearch`. Copy the long property list from the repo to save time. | `ViewModels/MainViewModel.cs` |
| 0:42 to 0:50 | 7. View | Move `MainWindow` into `Views/` (fix `x:Class` and namespace). Build the layout: DockPanel → Grid (2 columns) → form StackPanel + DataGrid. Add styles to `App.xaml`. | `Views/MainWindow.xaml`, `App.xaml` |
| 0:50 to 0:54 | 8. Wire up | `App.xaml.cs`: Generic Host + DI. **Remove `StartupUri` from `App.xaml`**. Set `DataContext` in `MainWindow` constructor. | `App.xaml.cs`, `MainWindow.xaml.cs` |
| 0:54 to 0:58 | 9. Run | `Add-Migration InitialCreate`, `Update-Database`, F5. Add, edit, search, delete an employee. | - |
| 0:58 to 1:00 | Wrap-up | Recap: "View shows, ViewModel thinks, Service checks, Repository saves." Share the repo link and this guide. | - |

**Tips to stay on time**
- Don't type the 15 fields by hand. Type 3 (FirstName, Email, Department) to explain, then paste the rest from the repo.
- Paste the full XAML for the form; explain only one TextBox, one ComboBox, one RadioButton and the DataGrid.
- If something breaks, switch to the finished repo version and keep going. Debug later.

### Quick self-check questions (for you, and for the end of the session)

1. Where is the DataContext set? *(MainWindow.xaml.cs constructor)*
2. Why `ObservableCollection` and not `List`? *(It tells the grid when items change)*
3. What makes the Delete button disabled? *(`CanExecute`: not busy and a row selected)*
4. Who checks that the email is valid? *(EmployeeService.Validate)*
5. Who creates `EmployeeService`? *(The DI container in App.xaml.cs)*
6. What happens if you forget `OnPropertyChanged`? *(The screen does not update)*
7. Why is there code-behind for Skills? *(ListBox.SelectedItems cannot be bound)*
