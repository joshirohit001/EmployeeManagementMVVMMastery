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
## WPF overview, the .NET desktop world, WPF vs WinForms, setup, and your first WPF app

### Goal for today
By the end of the hour, every developer should be able to say **what WPF is**, **why we use it**, and should have **created and run their first WPF app** with a button that does something.

---

### Part A — Learn it (Rohit reads this before the session)

#### 1. What is WPF?
WPF stands for **Windows Presentation Foundation**. It is Microsoft's framework for building **desktop applications for Windows**: the kind of app you install and run on a PC, not in a browser.

Think of an app like a restaurant:
- **XAML** is the **menu card and the dining room**: what the customer sees. It describes the screen: buttons, text boxes, tables, colours.
- **C#** is the **kitchen**: the logic that does the work when the customer orders something.

In WPF you write the screen in **XAML** (a language that looks like HTML/XML) and the logic in **C#**.

```xml
<Button Content="Click me" Width="120" Height="40"/>
```
That one line creates a button 120 wide and 40 high with the text "Click me".

#### 2. Where WPF sits in the .NET desktop world
Microsoft has several ways to build desktop apps. Students will ask "why WPF?", so know the map:

| Technology | Year | Runs on | In one line |
|---|---|---|---|
| **Windows Forms (WinForms)** | 2002 | Windows | Oldest. Drag-and-drop, simple, but looks dated and is hard to style. |
| **WPF** | 2006 | Windows | XAML + data binding + MVVM. Very mature, huge community, used by most business desktop apps. |
| **UWP** | 2015 | Windows 10+ | Store apps. Mostly replaced now. |
| **WinUI 3** | 2021 | Windows 10+ | Newest Microsoft Windows UI. Similar XAML to WPF. |
| **.NET MAUI** | 2022 | Windows, Mac, Android, iOS | Cross-platform. XAML too, but less mature for big business desktop apps. |
| **Avalonia** (open source) | – | Windows, Mac, Linux | "WPF-like" cross-platform. |

**Key message:** WPF is still the most common choice for serious Windows business apps (banking, hospitals, factories, internal tools). And the XAML + MVVM skills you learn in WPF **transfer directly** to WinUI, MAUI and Avalonia.

WPF used to run only on the old ".NET Framework". Since .NET Core 3.0 it runs on modern .NET. We use **.NET 8**, which is the current long-term support version.

#### 3. WPF vs Windows Forms (the most asked question)

| Topic | Windows Forms | WPF |
|---|---|---|
| How you build the screen | Drag controls, Visual Studio writes C# for you | Write XAML (you can also drag, but XAML is the real thing) |
| Drawing | Uses old Windows drawing (GDI+), pixel based | Uses DirectX, **vector based**: sharp on any screen size or zoom |
| Layout | Mostly fixed X/Y positions | Panels (Grid, StackPanel...) that **resize automatically** |
| Styling | Hard. Change each control one by one | Styles and templates: change the look of every button in one place |
| Data | You copy values into controls by hand: `txtName.Text = emp.Name;` | **Data binding**: the control and the data stay in sync automatically |
| Architecture | Logic usually sits in button click events | Built for **MVVM**: screen and logic are separate and testable |
| Learning curve | Easy to start | Harder to start, much better for big apps |

The one-line answer: **"WinForms is quick for small tools. WPF is better for large, good-looking, maintainable apps because of binding, styling and MVVM."**

Example of the difference:
```csharp
// WinForms way: you push data into the control yourself
txtFirstName.Text = employee.FirstName;
// ...and later read it back yourself
employee.FirstName = txtFirstName.Text;
```
```xml
<!-- WPF way: you declare the link once; WPF keeps them in sync -->
<TextBox Text="{Binding FirstName}"/>
```
We will go deep into binding on Day 5. Today just show it as a "teaser".

#### 4. What you need installed (environment setup)

| Tool | Why | How to check |
|---|---|---|
| **Visual Studio 2022** (Community is free) | The editor + designer + debugger | Open it |
| Workload: **.NET desktop development** | Adds WPF templates and **SQL Server LocalDB** (needed on Day 15) | Visual Studio Installer → Modify → tick the workload |
| **.NET 8 SDK** | Compiles and runs the app | Command prompt: `dotnet --version` → starts with `8.` |
| **Git** (optional today) | To get the course code later | `git --version` |

**Important:** Visual Studio takes 30–60 minutes to install. Send the install instructions to all 20 developers **before** Day 1, or at least ask them to start the install the moment the session begins.

WPF only runs on **Windows**. Developers on Mac need a Windows VM or a Windows machine.

#### 5. The structure of a new WPF project
When you create a new WPF app, Visual Studio gives you these files:

| File | What it is | Analogy |
|---|---|---|
| **Project file** (`.csproj`) | Settings: which .NET version, "this is a WPF app" | The building's blueprint |
| **App.xaml** | App-wide settings and shared resources (colours, styles). Also says which window opens first (`StartupUri`) | The reception desk |
| **App.xaml.cs** | C# behind App: code that runs when the app starts or closes | The building manager |
| **MainWindow.xaml** | The first window's screen design | The first room |
| **MainWindow.xaml.cs** | C# behind the window ("code-behind") | The room's switchboard |

Three settings in the project file make it a WPF app:
```xml
<OutputType>WinExe</OutputType>                <!-- a Windows app, no black console window -->
<TargetFramework>net8.0-windows</TargetFramework> <!-- .NET 8, Windows only -->
<UseWPF>true</UseWPF>                           <!-- switch WPF on -->
```

**Every XAML file is paired with a C# file.** Together they make one class. The XAML file says `x:Class="MyFirstWpfApp.MainWindow"` and the C# file says `public partial class MainWindow`. The word `partial` means "this class is split across two files".

#### 6. How a WPF app starts (simple version)
1. You press F5.
2. .NET starts the **App** class.
3. App reads `StartupUri="MainWindow.xaml"` and opens that window.
4. The window runs `InitializeComponent()`, which reads the XAML and creates all the controls.
5. The app keeps running until the last window closes.

(On Day 16 we will remove `StartupUri` and start the window ourselves, for dependency injection. Mention this only as a "coming later".)

---

### Part B — Session script (60 minutes)

| Time | What you do | What you say (key lines) |
|---|---|---|
| 0:00–0:05 | Welcome. Explain the 21-day plan and the final project. Show the finished Employee Management app running (if ready). | "In 4 weeks you'll build this app yourself, step by step, one topic per day." |
| 0:05–0:15 | **What is WPF** + the restaurant analogy. Show the .NET desktop table. | "XAML is the dining room, C# is the kitchen." "WPF is the standard for Windows business apps, and these skills transfer to MAUI and WinUI." |
| 0:15–0:22 | **WPF vs WinForms** table and the two code snippets. | "WinForms: you copy data in and out by hand. WPF: you bind it once." |
| 0:22–0:27 | **Setup check.** Everyone runs `dotnet --version`. Note who isn't ready. | "If yours doesn't say 8, tell me after the session." |
| 0:27–0:50 | **Live demo: first WPF app** (steps below). Everyone follows. | |
| 0:50–0:55 | Walk through the project files table using the app you just made. | "Every XAML file has a C# partner. `partial` joins them." |
| 0:55–1:00 | Recap + Questions for the team + homework. | |

#### Live demo: "Hello WPF" (about 20 minutes)

**Step 1. Create the project**
Visual Studio → **Create a new project** → search **"WPF Application"** (make sure it's the C# one, **not** ".NET Framework") → Name: `MyFirstWpfApp` → Framework: **.NET 8.0** → Create.

Or from the command line:
```bash
dotnet new wpf -n MyFirstWpfApp -f net8.0
cd MyFirstWpfApp
dotnet run
```

**Step 2. Run it empty** (F5). An empty window appears. Say: "This is already a working Windows app."

**Step 3. Replace the content of `MainWindow.xaml`** (inside `<Window>`) with:
```xml
<Window x:Class="MyFirstWpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="My First WPF App" Height="250" Width="400">
    <StackPanel Margin="20">
        <TextBlock Text="What is your name?" FontSize="16"/>
        <TextBox x:Name="NameTextBox" Margin="0,8,0,8" FontSize="16"/>
        <Button Content="Say Hello" Width="120" HorizontalAlignment="Left"
                Click="SayHelloButton_Click"/>
        <TextBlock x:Name="GreetingTextBlock" Margin="0,12,0,0"
                   FontSize="18" Foreground="DarkBlue"/>
    </StackPanel>
</Window>
```
Explain each line in one sentence:
- `StackPanel` puts things one under another.
- `TextBlock` shows text. `TextBox` lets the user type.
- `x:Name` gives the control a name so C# can find it.
- `Click="SayHelloButton_Click"` says which C# method runs on click.

**Step 4. Add the click code in `MainWindow.xaml.cs`:**
```csharp
private void SayHelloButton_Click(object sender, RoutedEventArgs e)
{
    GreetingTextBlock.Text = $"Hello, {NameTextBox.Text}! Welcome to WPF.";
}
```

**Step 5. Run (F5).** Type a name, click the button. 🎉

**Step 6. Show the "WPF power" in 2 minutes:** resize the window and show the layout adapts. Then remove `FontSize` from the controls and put `FontSize="20"` on the **Window** once: *all* the text inside grows (this is called property inheritance). Students love this.

**Step 7. Tell the truth about this code:** "This is the WinForms-style way (click event + names). It works, but from Day 5 we'll learn the WPF way (binding + commands), and this click code will disappear."

Common problems in the demo:

| Problem | Fix |
|---|---|
| No "WPF Application" template | The ".NET desktop development" workload is missing. Install it. |
| Picked "WPF App (.NET Framework)" | Delete and create again with the non-Framework template. |
| "SayHelloButton_Click does not exist" | Method name in XAML and C# must match exactly (case sensitive). |
| `NameTextBox` not found in C# | Check `x:Name` spelling, and rebuild once. |

---

### Part C — What we covered today (send this to the team after the session)
- WPF = Windows Presentation Foundation: Microsoft's framework for Windows desktop apps.
- XAML describes the screen; C# holds the logic.
- Where WPF fits: WinForms (old), WPF (mature, most used), WinUI 3 / MAUI / Avalonia (newer). XAML + MVVM skills transfer.
- WPF vs WinForms: vector drawing, automatic layout, styles, **data binding**, **MVVM**.
- Setup: Visual Studio 2022 + .NET desktop development workload + .NET 8 SDK.
- New project structure: project file, App.xaml/.cs, MainWindow.xaml/.cs; XAML + C# = one `partial` class.
- How the app starts: App → StartupUri → MainWindow → InitializeComponent.
- Built a first app with TextBlock, TextBox, Button and a Click event.

### Part D — Questions to ask the team after the session
Ask 4–5 of these out loud; pick different people each time.
1. In one sentence, what is WPF?
2. What is the job of XAML, and what is the job of C#?
3. Name two differences between WPF and Windows Forms.
4. Why does a WPF window look sharp even when you zoom or use a big monitor? *(vector-based drawing with DirectX)*
5. Which three settings in the project file make it a WPF app? *(WinExe, net8.0-windows, UseWPF)*
6. What does the word `partial` mean in `public partial class MainWindow`?
7. Which file decides which window opens first? *(App.xaml, StartupUri)*
8. What does `x:Name` do?
9. Can a WPF app run on Mac or Linux? *(No, Windows only; look at MAUI or Avalonia for that)*
10. *(Thinking question)* Today we wrote a Click event. What do you think is the problem with putting all logic in click events in a big app? *(hard to test, hard to reuse, screen and logic mixed: this is why we'll learn MVVM)*

**Check for blockers:** "Who could not create or run the app today?" Write the names down and help them before Day 2.

### Part E — Homework (15–20 minutes)
1. Make sure Visual Studio + .NET 8 work, and the "Hello WPF" app runs.
2. Add a second TextBox for **age** and show: "Hello Rohit, you are 30 years old."
3. Add a **Clear** button that empties both text boxes and the greeting.
4. Bonus: change the window's `Background` to a colour of your choice, and set `FontSize` on the Window.

### Part F — Trainer's note for Rohit
- Don't go deep into binding, MVVM or XAML syntax today. Show binding only as a one-line teaser.
- If half the room isn't set up, do the demo on your screen and let them follow later. Don't wait.
- Tomorrow (Day 2) starts with: "Look at the XAML you wrote yesterday. Let's understand every piece of it."

---

# Days 2–21: outline

Each day will be delivered in the same full format (Learn it, Session script with live demo, What we covered, Questions for the team, Homework). Below is what each day contains.

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
