# WPF Day-by-Day Teaching Plan

**Trainer:** Rohit Joshi · **Team:** 20 developers · **Session length:** 1 hour per day · **Start:** Wednesday 25 November 2026

How each day works:
1. **Before the session:** Rohit learns the day's topic from this plan (the "Learn it" part).
2. **Session (60 min):** teach with the "Session script" and do the live demo.
3. **After the session:** ask the team the "Questions for the team", give the homework, and tick the day in the tracker below.

The final project the team builds up to is the **Employee Management app** (WPF + MVVM + SQL Server + EF Core + dependency injection), extended on the later days with **architecture-level design** and **role-based access**.

---

## Course calendar (weekdays only)

| Day | Date | Topic | Excel module | Status |
|---|---|---|---|---|
| 1 | Wed 25 Nov | WPF overview, .NET desktop, WPF vs WinForms, environment setup, first app | WPF Foundations | Today |
| 2 | Thu 26 Nov | XAML fundamentals, namespaces, property-element syntax, code-behind, app lifecycle | WPF Foundations | |
| 3 | Fri 27 Nov | Layout panels: Grid, StackPanel, DockPanel, WrapPanel, Canvas, sizing, alignment | Layout and Controls | |
| 4 | Mon 30 Nov | Controls, items controls, menus, toolbars, dialogs, input gestures | Layout and Controls | |
| 5 | Tue 1 Dec | Data binding, DataContext, binding modes, UpdateSourceTrigger, converters | Data Binding and MVVM | |
| 6 | Wed 2 Dec | INotifyPropertyChanged, ObservableCollection, validation, error templates | Data Binding and MVVM | |
| 7 | Thu 3 Dec | MVVM, ViewModel design, ICommand / RelayCommand, CanExecute, MVVM folder structure | Data Binding and MVVM | |
| 8 | Fri 4 Dec | Resources, StaticResource vs DynamicResource, styles, triggers, visual states | Styling and Templates | |
| 9 | Mon 7 Dec | ControlTemplate, DataTemplate, ItemsPanelTemplate, brushes, animation | Styling and Templates | |
| 10 | Tue 8 Dec | Routed events, dependency properties, attached properties, UserControls | Advanced WPF | |
| 11 | Wed 9 Dec | DataGrid, CollectionViewSource, sorting, filtering, grouping, master-detail | Advanced WPF | |
| 12 | Thu 10 Dec | Async/await, UI thread, Dispatcher, progress, cancellation | Advanced WPF | |
| 13 | Fri 11 Dec | Configuration, JSON, logging, exception handling | Application Development | |
| 14 | Mon 14 Dec | Service layer and Repository pattern | Application Development | |
| 15 | Tue 15 Dec | SQL connectivity: SQL Server, connection strings, EF Core, migrations | Application Development | |
| 16 | Wed 16 Dec | Dependency injection in WPF (Generic Host, lifetimes) | Application Development | |
| 17 | Thu 17 Dec | **Architecture-level WPF design:** layers, navigation, dialog service, messaging, solution structure | Added | |
| 18 | Fri 18 Dec | **Role-based access:** login, users and roles, permission checks, hiding/disabling UI | Added | |
| 19 | Mon 21 Dec | Performance, diagnostics, unit testing, deployment (ClickOnce / MSIX) | Application Development | |
| 20 | Tue 22 Dec | Common mistakes + hands-on workshop: build the Employee Management app | Application Development | |
| 21 | Wed 23 Dec | Final integration, review, demo and assessment | Application Development | |

If a day gets skipped (holiday, meeting), move everything one working day later. Don't squeeze two days into one hour.

---

# DAY 1 — Wednesday 25 November 2026
## WPF Foundations, from zero

Day 1 is now a full, detailed lesson in its own file: **days/Day01_WPF_Foundations.md**.
It covers: desktop apps, C# vs .NET (CLR, IL, JIT, SDK), .NET Framework vs .NET, what WPF is and how it draws, the .NET desktop family, WPF vs WinForms, setup, **every project file explained line by line**, **what XAML really is**, **what happens at build time (BAML and generated .g.cs files)**, **what InitializeComponent() does behind the scenes**, where Main() comes from, the full startup sequence, the UI thread, a 60-minute session script with a live demo, recap, 17 questions for the team, and homework.

---

# Days 2–21: outline

Each day will be delivered in the same depth as Day 1, in its own file under days/, in the same format (Learn it, Session script with live demo, What we covered, Questions for the team, Homework). Below is what each day contains.

### Day 2 — Thu 26 Nov · XAML fundamentals and app lifecycle
Elements = objects, attributes = properties, property-element syntax (`<Grid.ColumnDefinitions>`), namespaces (`xmlns`, `x:`), `x:Name` / `x:Key` / `x:Class`, code-behind, `InitializeComponent`, `Application` events (`Startup`, `Exit`, `DispatcherUnhandledException`). Demo: rebuild yesterday's window and add an app-wide error handler.

### Day 3 — Fri 27 Nov · Layout panels
Grid (rows/columns, `*` / `Auto` / fixed, spans), StackPanel, DockPanel, WrapPanel, Canvas, ScrollViewer, Margin vs Padding, alignment. Demo: the Employee form layout (header, form left, table right, status bar).

### Day 4 — Mon 30 Nov · Controls
TextBox, ComboBox, RadioButton, CheckBox, DatePicker, ListBox, DataGrid basics, Menu, ToolBar, StatusBar, MessageBox and dialogs, keyboard shortcuts (KeyBinding). Demo: all Employee form controls.

### Day 5 — Tue 1 Dec · Data binding
DataContext, `{Binding}`, OneWay / TwoWay / OneTime, UpdateSourceTrigger, StringFormat, value converters (`IValueConverter`, BooleanToVisibility). Demo: replace Day 1's click code with binding.

### Day 6 — Wed 2 Dec · Observable data and validation
INotifyPropertyChanged, a reusable base class with `SetProperty`, `[CallerMemberName]`, ObservableCollection vs List, validation with `INotifyDataErrorInfo`, red-border error templates.

### Day 7 — Thu 3 Dec · MVVM and commands
Model / View / ViewModel roles, ICommand, RelayCommand, CanExecute, keeping code-behind empty, MVVM folder structure, the full "Save button" flow. Intro to CommunityToolkit.Mvvm as the modern shortcut.

### Day 8 — Fri 4 Dec · Resources and styles
Resources and resource dictionaries, StaticResource vs DynamicResource, implicit vs keyed styles, `BasedOn`, triggers, data triggers, light/dark theme switch.

### Day 9 — Mon 7 Dec · Templates and graphics
ControlTemplate (a rounded button), TemplateBinding, DataTemplate for list items, ItemsPanelTemplate, brushes, shapes, simple animations with Storyboard.

### Day 10 — Tue 8 Dec · Advanced XAML
Routed events (bubbling/tunnelling), dependency properties, attached properties, building a reusable UserControl (an "employee card").

### Day 11 — Wed 9 Dec · DataGrid and CollectionViewSource
Columns, editing, sorting, filtering and grouping with ICollectionView, master-detail screens, row virtualization.

### Day 12 — Thu 10 Dec · Async and the UI thread
Why the window freezes, async/await, Dispatcher, IsBusy pattern, progress with `IProgress<T>`, Cancel button with CancellationToken.

### Day 13 — Fri 11 Dec · Configuration, JSON, logging, exceptions
appsettings.json, reading/writing JSON files, logging with `ILogger`, global exception handling, user-friendly error messages.

### Day 14 — Mon 14 Dec · Service layer and Repository pattern
Why layers, interfaces, Repository (data access) vs Service (business rules), validation in services, throwing friendly errors.

### Day 15 — Tue 15 Dec · SQL connectivity
SQL Server and LocalDB, connection strings explained, EF Core, DbContext, DbSet, Fluent API rules, migrations (`Add-Migration`, `Update-Database`), CRUD and what SQL EF Core produces.

### Day 16 — Wed 16 Dec · Dependency injection in WPF
Why DI, Generic Host, removing StartupUri, registering services, lifetimes (Singleton / Scoped / Transient), constructor injection, DbContext lifetime in desktop apps (`IDbContextFactory`).

### Day 17 — Thu 17 Dec · Architecture-level WPF design *(added)*
How a real WPF app is organised end to end:
- Layered / clean architecture: UI → ViewModels → Services → Repositories → Database; which layer may call which.
- Solution structure: splitting into projects (UI, Core/Domain, Data, Tests).
- **Navigation** between screens with a navigation service and ViewModel-first navigation (ContentControl + DataTemplates).
- **Dialog service** so ViewModels never call MessageBox directly.
- **Messaging** between ViewModels (CommunityToolkit `WeakReferenceMessenger`).
- Shell window with a side menu and multiple modules (Employees, Departments, Reports).
- Design checklist: what to put where, naming conventions, common anti-patterns.

### Day 18 — Fri 18 Dec · Role-based access *(added)*
- Authentication vs authorization in a desktop app.
- Login screen (MVVM), password hashing (never store plain passwords).
- Users, Roles, Permissions tables in SQL Server (e.g. Admin, HR Manager, Viewer).
- A `CurrentUser` / session service registered with DI.
- Checking permissions in **services** (the real guard) and in **commands** (`CanExecute`).
- Hiding or disabling menu items and buttons by role (binding + converter).
- Example: Viewer can only see the list; HR Manager can add and edit; only Admin can delete and see salaries.
- Option: Windows authentication / Active Directory groups for company apps.

### Day 19 — Mon 21 Dec · Performance, diagnostics, testing, deployment
Binding errors in the Output window, Live Visual Tree, virtualization, profiling, unit testing ViewModels with fake services, publishing (self-contained, ClickOnce, MSIX).

### Day 20 — Tue 22 Dec · Common mistakes + hands-on workshop
Top 10 beginner mistakes, then build the Employee Management app layer by layer in one hour (Model → DbContext → Repository → Service → ViewModel → View → DI → migration → run).

### Day 21 — Wed 23 Dec · Final integration, review and assessment
Each developer (or pair) demos the full app with role-based login, a short written quiz, code review checklist, and next steps.

---

## Daily tracker (copy this after each session)

| Day | Date taught | Attendance (/20) | Covered fully? | Blockers / people needing help | Notes for next day |
|---|---|---|---|---|---|
| 1 | 25 Nov | | | | |
| 2 | | | | | |
