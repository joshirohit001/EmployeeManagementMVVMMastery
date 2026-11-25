# Day 1 — WPF Foundations, From Zero

**Date:** Wednesday 25 November 2026 · **Session:** 60 minutes · **Audience:** 20 developers, no WPF experience assumed

**Topics (from the course sheet):** WPF overview, .NET desktop ecosystem, WPF vs Windows Forms, project structure, environment setup, first application.

> How to use this file
> - **Rohit:** read Parts 1 to 9 fully before the session. They are written so you can understand *why*, not only *what*.
> - **In the session:** use Part 10 (session script). Everything you say comes from Parts 1 to 9.
> - **After the session:** use Parts 11 to 13 (what we covered, questions, homework).

---

## Part 1 — Start from zero: what is a "desktop application"?

There are three common kinds of applications:

| Kind | Where it runs | Example |
|---|---|---|
| **Web app** | Inside a browser (Chrome, Edge). Code lives on a server. | Gmail in the browser |
| **Mobile app** | On a phone (Android, iOS) | WhatsApp on a phone |
| **Desktop app** | Installed directly on a computer, runs in its own window | Microsoft Excel, Visual Studio, Notepad |

A desktop app:
- Is installed on the PC (an `.exe` file).
- Opens its own **window** with a title bar, minimise / maximise / close buttons.
- Can use the PC directly: files, printers, USB devices, scanners, local databases.
- Works even without internet (unless it needs a server).

Companies still build many desktop apps: hospital systems, banking teller software, factory machine control, billing counters, internal HR tools. **WPF is one of the main ways to build them on Windows.**

---

## Part 2 — What is .NET? (needed before WPF makes sense)

Developers will hear ".NET", "SDK", "runtime", "C#", "framework" all the time. Make these words clear on Day 1.

### 2.1 The words

| Word | Simple meaning | Analogy |
|---|---|---|
| **C#** | The programming **language** we write in | English (the language you write a letter in) |
| **.NET** | The **platform** that runs C# programs. It includes the runtime and a huge library | The postal system that delivers and reads the letter |
| **Runtime (CLR)** | The engine that actually **runs** your app on the PC | The postman |
| **Base Class Library (BCL)** | Ready-made code: files, strings, dates, lists, network... | A toolbox you don't have to build yourself |
| **SDK** | Tools to **build** apps: compiler, templates, the `dotnet` command | The printing press for letters |
| **Framework** | A big set of ready code + rules for one kind of app. WPF is a UI framework | A ready-made house frame; you only add rooms |

### 2.2 What happens to your C# code

1. You write C# in `.cs` files.
2. The **compiler** turns C# into **IL** (Intermediate Language), stored in a `.dll` or `.exe` file. IL is not yet machine code.
3. When the app runs, the **CLR** reads the IL and the **JIT compiler** (Just-In-Time) turns it into real machine code for your CPU, right when it's needed.

```
C# code  --(compiler)-->  IL in .dll/.exe  --(CLR + JIT at run time)-->  machine code on the CPU
```

That's why you need the **.NET runtime** on any PC that runs a .NET app (unless you publish it "self-contained", which we'll see on Day 19).

### 2.3 .NET Framework vs .NET (a very common confusion)

| | **.NET Framework** | **.NET** (formerly .NET Core) |
|---|---|---|
| Versions | 1.0 (2002) … 4.8.1 (last) | .NET Core 1–3.1, then .NET 5, 6, 7, **8**, 9… |
| Runs on | Windows only | Windows, Linux, Mac (but **WPF itself** is still Windows only) |
| Status | Old, only security fixes | Modern, actively developed, faster |
| What we use | ✗ | ✓ **.NET 8** (the version our course project targets) |

WPF was born on .NET Framework 3.0 in 2006. Since **.NET Core 3.0 (2019)** WPF also runs on modern .NET. In Visual Studio you will see **two** WPF templates. Always pick the one **without** "(.NET Framework)" in its name.

**Versions and support:** even-numbered versions (6, 8, 10) are **LTS** (Long-Term Support, 3 years); odd ones (7, 9) are shorter. .NET 8's support ended on 10 November 2026, and **.NET 10** (released November 2025) is the current LTS. Everything in this course works the same on .NET 10: pick .NET 10 in the template, and the project file says `net10.0-windows` instead of `net8.0-windows`. For a real company project, use the current LTS.

---

## Part 3 — What is WPF?

**WPF = Windows Presentation Foundation.**
- **Windows**: it runs on Windows.
- **Presentation**: it is about what the user *sees*: the user interface (UI).
- **Foundation**: it is a framework, a base to build on.

It is Microsoft's framework for building **Windows desktop user interfaces**, released in 2006, still used everywhere and open source since 2018.

### 3.1 Two languages, two jobs

A WPF screen is made of **two files that work together**:

| | **XAML** | **C#** |
|---|---|---|
| What it is | A markup language (looks like HTML/XML) | A programming language |
| Job | Describes **what the screen looks like**: controls, layout, colours | Describes **what the app does**: logic, calculations, saving data |
| Restaurant analogy | The **dining room and the menu**: what customers see | The **kitchen**: where the work happens |

### 3.2 How WPF draws the screen (behind the scenes)

This is what makes WPF different from older technology:

- WPF draws everything using **DirectX**, the same graphics engine used by games. It uses the **graphics card (GPU)**.
- Everything is drawn as **vectors** (shapes and lines described by maths), not fixed pixels. That's why WPF apps stay **sharp at any zoom or screen size**.
- WPF sizes are in **device-independent units**: 1 unit = 1/96 of an inch. `Width="96"` is always about 1 inch wide, whether the monitor is a normal one or a 4K one. Windows' display scaling (125%, 150%) is handled automatically.
- WPF **draws its own controls**. A WPF button is not a Windows "native" button; WPF paints it. That's why you can completely change how any control looks (Day 9: templates).

Internally there are layers:

```
Your app (XAML + C#)
   ↓
PresentationFramework   ← controls: Button, TextBox, Window, layout panels, data binding
   ↓
PresentationCore        ← visual tree, input (mouse, keyboard), basic drawing
   ↓
milcore (Media Integration Layer, native code)   ← composes the picture
   ↓
DirectX  →  graphics card  →  your monitor
```

You only work in the top layer. But knowing this explains why WPF is smooth, scalable and fully stylable.

---

## Part 4 — The .NET desktop family: where WPF fits

| Technology | Released | Runs on | UI language | Use it when |
|---|---|---|---|---|
| **Windows Forms** | 2002 | Windows | C# (designer generated) | Small, quick internal tools |
| **WPF** | 2006 | Windows | **XAML + C#** | Serious Windows business apps. Most mature, most jobs, most examples online |
| **UWP** | 2015 | Windows 10+ | XAML + C# | Mostly replaced. Don't start new projects |
| **WinUI 3** | 2021 | Windows 10/11 | XAML + C# | New Windows-only apps with the newest Windows 11 look |
| **.NET MAUI** | 2022 | Windows, Mac, Android, iOS | XAML + C# | One app for phones and desktop |
| **Avalonia** (open source) | – | Windows, Mac, Linux, web | XAML + C# (very WPF-like) | WPF style but cross-platform |

**Key message for the team:** XAML, data binding and MVVM — the three big things in this course — are **the same ideas** in WPF, WinUI, MAUI and Avalonia. Learning WPF well means you can move to any of them quickly.

---

## Part 5 — WPF vs Windows Forms, explained properly

This is the first question every developer asks. Explain each row with the "why".

### 5.1 Comparison

| Topic | Windows Forms | WPF | Why it matters |
|---|---|---|---|
| **How the screen is made** | Drag controls; Visual Studio writes C# code that sets X/Y positions | XAML describes the screen; layout is automatic | XAML is readable, reviewable and easy to change |
| **Drawing** | Old Windows drawing (GDI+), pixel based | DirectX, vector based | Sharp on 4K / high-DPI screens; animations are smooth |
| **Layout** | Fixed positions (`Location = new Point(10, 20)`) | Panels (Grid, StackPanel…) that resize automatically | Windows can be resized; works on any screen |
| **Styling** | Change each control's properties one by one | Styles and templates: change all buttons in one place; change a control's entire look | Consistent, professional UI |
| **Data** | Copy values in and out of controls yourself | **Data binding**: control and data stay in sync | Much less code, fewer bugs |
| **Architecture** | Logic usually sits in click event handlers | Built for **MVVM**: screen and logic separated | Testable, maintainable large apps |
| **Learning curve** | Easy to start | Harder at first | WPF pays back in medium and big apps |

### 5.2 The same small task in both

Task: show an employee's first name in a text box and read it back when the user edits it.

**Windows Forms way** — you do the copying:
```csharp
// When loading the screen
txtFirstName.Text = employee.FirstName;

// When the user clicks Save
employee.FirstName = txtFirstName.Text;
```
If you forget one of these lines, the screen and the data no longer match. In a screen with 30 fields, that's 60 lines of copying.

**WPF way** — you declare the link once:
```xml
<TextBox Text="{Binding FirstName}"/>
```
WPF keeps the text box and the data in sync in both directions. (Day 5 explains exactly how.)

**Line to say in class:** "WinForms is quick for small tools. WPF is better for big, good-looking, maintainable apps, because of layout, styling, data binding and MVVM."

---

## Part 6 — Environment setup

### 6.1 What to install

| Tool | Why we need it | How to check it works |
|---|---|---|
| **Visual Studio 2022** (Community edition is free) | Code editor, XAML designer, debugger | It opens |
| Workload **".NET desktop development"** | Adds WPF templates, the WPF designer and **SQL Server LocalDB** (Day 15) | Visual Studio Installer → Modify → the workload is ticked |
| **.NET 8 SDK** (usually installed with the workload) | Compiler and `dotnet` command | Open Command Prompt → `dotnet --list-sdks` shows a line starting with `8.` |
| **Git** (optional today) | To download course code later | `git --version` |

Notes:
- **WPF runs only on Windows.** Developers with a Mac need a Windows machine or a Windows virtual machine. (VS Code + the `dotnet` command can also build WPF on Windows, but Visual Studio is much easier for beginners.)
- Installing Visual Studio takes **30 to 60 minutes**. Ask everyone to install it **before** Day 1.

### 6.2 Visual Studio areas to point out (2 minutes in class)
- **Solution Explorer** (right): all files of the project.
- **Editor** (middle): where you type code. For XAML files you also get the **Designer** (a live preview).
- **Properties window**: properties of the selected control.
- **Toolbox**: controls you can drag (we will mostly type XAML instead).
- **Output window** (bottom): build messages and later **binding errors** (Day 19).
- **F5** = run with debugger. **Ctrl+F5** = run without debugger. **Ctrl+Shift+B** = build only.

---

## Part 7 — The project structure: every file, every line

Create a new project (Part 10 shows the exact clicks) named **`MyFirstWpfApp`**. Solution Explorer shows:

```
Solution 'MyFirstWpfApp'
└── MyFirstWpfApp                 ← the project
    ├── Dependencies              ← frameworks and NuGet packages
    ├── App.xaml                  ← application definition (XAML part)
    │   └── App.xaml.cs           ← application definition (C# part)
    ├── AssemblyInfo.cs           ← assembly-level settings (themes)
    └── MainWindow.xaml           ← the first window (XAML part)
        └── MainWindow.xaml.cs    ← the first window (C# part, "code-behind")
```
On disk there are also two hidden-from-view folders: **bin** and **obj** (explained in 7.8).

Let's go through each one, line by line.

### 7.1 The solution file (`.sln`)
- A **solution** is a container that can hold **many projects**. Example: a UI project, a data project and a test project (Day 17).
- The `.sln` file just lists the projects and build settings. You open the `.sln` in Visual Studio to open everything at once.
- Today we have 1 solution containing 1 project.

### 7.2 The project file (`MyFirstWpfApp.csproj`)
Double-click the project name in Solution Explorer to see it:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UseWPF>true</UseWPF>
  </PropertyGroup>

</Project>
```

| Line | Meaning |
|---|---|
| `<Project Sdk="Microsoft.NET.Sdk">` | "This is a .NET project; use the standard .NET build rules." |
| `<PropertyGroup>` | A group of settings. |
| `<OutputType>WinExe</OutputType>` | Build a **Windows .exe** that opens a window. (`Exe` would also open a black console window behind it.) |
| `<TargetFramework>net8.0-windows</TargetFramework>` | Build for **.NET 8**, the **Windows** flavour (WPF needs Windows APIs). |
| `<Nullable>enable</Nullable>` | The compiler warns you when something might be `null`. Helps avoid "Object reference not set to an instance of an object" crashes. |
| `<ImplicitUsings>enable</ImplicitUsings>` | Common `using` lines (System, System.Collections.Generic, System.Linq…) are added automatically to every C# file. |
| `<UseWPF>true</UseWPF>` | **Switch WPF on.** Adds the WPF libraries and the XAML compiler to the build. Without this line, XAML files won't compile. |

Later (Day 15) NuGet packages appear here as `<PackageReference Include="..." Version="..."/>` lines.

### 7.3 Dependencies
Expand **Dependencies** → **Frameworks**: you see `Microsoft.NETCore.App` (base .NET) and `Microsoft.WindowsDesktop.App.WPF` (WPF). These came from `UseWPF=true`. Later, **Packages** will list NuGet packages such as Entity Framework Core.

### 7.4 `App.xaml` — the application definition
```xml
<Application x:Class="MyFirstWpfApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:MyFirstWpfApp"
             StartupUri="MainWindow.xaml">
    <Application.Resources>

    </Application.Resources>
</Application>
```

| Line | Meaning |
|---|---|
| `<Application ...>` | This file describes the **application object** (one per running app). |
| `x:Class="MyFirstWpfApp.App"` | "The C# class for this XAML is `App` in namespace `MyFirstWpfApp`." This is how the XAML and the `.cs` file are joined. |
| `xmlns="…/xaml/presentation"` | The **default namespace**: all WPF controls (`Window`, `Button`, `Grid`…). Tags without a prefix come from here. |
| `xmlns:x="…/xaml"` | The **XAML language** namespace, prefix `x:`. Gives us `x:Class`, `x:Name`, `x:Key`. |
| `xmlns:local="clr-namespace:MyFirstWpfApp"` | Prefix `local:` points to **your own C# classes** in this project. Used later for converters, user controls. |
| `StartupUri="MainWindow.xaml"` | **Which window to open when the app starts.** |
| `<Application.Resources>` | A place for things shared by the **whole app**: colours, styles, converters (Day 8). Empty for now. |

Analogy: App is the **reception desk** of a building. It decides which room you go to first, and holds things everyone shares.

### 7.5 `App.xaml.cs` — the application's C# part
```csharp
using System.Configuration;
using System.Data;
using System.Windows;

namespace MyFirstWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
}
```

| Line | Meaning |
|---|---|
| `using System.Windows;` | Lets us write `Application` instead of `System.Windows.Application`. |
| `namespace MyFirstWpfApp` | Must match the namespace in `x:Class`. |
| `public partial class App` | **`partial`** = "this class is split across more than one file". The other part is generated from App.xaml (Part 8). |
| `: Application` | `App` **inherits** from WPF's `Application` class, so it gets everything an application needs: `Run()`, `Startup` and `Exit` events, `Resources`, `MainWindow`, `Shutdown()`. |
| Empty body | Nothing needed yet. Later (Day 16) we add startup code here: `OnStartup`, `OnExit`. |

**Where is `Main()`?** Every C# program starts at a `Main` method. In WPF you don't write it — it is **generated** from App.xaml during build. (Part 8.4 shows it.)

### 7.6 `MainWindow.xaml` — the first window's look
```xml
<Window x:Class="MyFirstWpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:MyFirstWpfApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>

    </Grid>
</Window>
```

| Line | Meaning |
|---|---|
| `<Window` | The root element: this XAML describes a **Window**. |
| `x:Class="MyFirstWpfApp.MainWindow"` | Joins this XAML with the C# class `MainWindow`. |
| `xmlns`, `xmlns:x`, `xmlns:local` | Same as in App.xaml. |
| `xmlns:d="…/blend/2008"` | **Design-time** namespace. `d:` attributes are only used by the Visual Studio designer, e.g. `d:DesignHeight="300"`, sample data. |
| `xmlns:mc="…/markup-compatibility/2006"` | **Markup compatibility** namespace. Needed for the next line. |
| `mc:Ignorable="d"` | "When the app really runs, **ignore** everything with the `d:` prefix." So designer-only settings never affect the real app. |
| `Title="MainWindow"` | Text in the window's title bar. |
| `Height="450" Width="800"` | Starting size in device-independent units (1/96 inch). |
| `<Grid>` | The window's **content**. A Window can hold **only one child**, so we put a layout panel (Grid) inside, and the panel holds many controls (Day 3). |

### 7.7 `MainWindow.xaml.cs` — the code-behind
```csharp
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyFirstWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
```

| Line | Meaning |
|---|---|
| The `using` lines | Shortcuts to WPF namespaces: `Controls` (Button, TextBox), `Media` (colours, brushes), `Input` (keyboard, mouse), `Data` (binding)… Most are unused for now; that's fine. |
| `public partial class MainWindow` | Partial again: the other half is generated from MainWindow.xaml. |
| `: Window` | Inherits WPF's `Window` class: title bar, `Show()`, `Close()`, `Title`, `Width`, events like `Loaded` and `Closing`. |
| `public MainWindow()` | The **constructor**: runs when the window object is created. |
| `InitializeComponent();` | **Builds the screen from the XAML.** Without this line the window is empty. Part 8 explains exactly how. |

"Code-behind" = the C# file "behind" a XAML file. In MVVM (Day 7) we keep it almost empty, but today we'll put a click handler in it.

### 7.8 `AssemblyInfo.cs`
```csharp
using System.Windows;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly
)]
```
- An **assembly** is the compiled output (`.exe` or `.dll`).
- `ThemeInfo` tells WPF where to look for **theme** resources (default control styles). `None` = no Windows-theme-specific styles; `SourceAssembly` = generic styles are inside this assembly (used when you make custom controls, Day 10).
- Beginners never need to touch this file. Just know what it is.

### 7.9 The `bin` and `obj` folders (not shown in Solution Explorer by default)
Click **"Show All Files"** at the top of Solution Explorer to see them.

| Folder | What's inside | Commit to Git? |
|---|---|---|
| **obj** | **Intermediate** files made during the build: generated C# (`.g.cs`), compiled XAML (`.baml`), caches | No |
| **bin** | The **final output**: `MyFirstWpfApp.exe`, `MyFirstWpfApp.dll`, config files. This is what runs | No |

You can delete both at any time; the next build recreates them. **"Clean Solution"** does that.

### 7.10 Summary table

| File | Type | Job | Analogy |
|---|---|---|---|
| `.sln` | Solution | Groups projects | The campus |
| `.csproj` | Project settings | .NET version, WPF on, packages | The building blueprint |
| `App.xaml` | XAML | Startup window, app-wide resources | Reception desk |
| `App.xaml.cs` | C# | App startup/exit code | Building manager |
| `MainWindow.xaml` | XAML | Window layout and controls | The room's furniture |
| `MainWindow.xaml.cs` | C# | Window logic (code-behind) | The room's switchboard |
| `AssemblyInfo.cs` | C# | Theme settings | Building registration paper |
| `obj` / `bin` | Build output | Generated files / final exe | Workshop / finished product |

---

## Part 8 — What XAML really is, and what `InitializeComponent()` does behind the scenes

This is the part most WPF developers never learn properly. Teach it slowly.

### 8.1 XAML basics: it's XML

XAML (pronounced "zammel") = **eXtensible Application Markup Language**. It follows XML rules:

| Rule | Example | Wrong |
|---|---|---|
| Every tag must close | `<Grid></Grid>` or self-closing `<Button/>` | `<Grid>` without `</Grid>` |
| Names are **case-sensitive** | `<TextBox/>` | `<textbox/>` |
| Attribute values in quotes | `Width="100"` | `Width=100` |
| Exactly **one root** element per file | `<Window>…</Window>` | Two `<Window>` at the top |
| Comments | `<!-- this is a comment -->` | `// comment` |

### 8.2 XAML is a way to create objects

**The single most important idea of today:** every XAML tag creates a **.NET object**, and every attribute sets a **property** on that object.

This XAML:
```xml
<Button Content="Save" Width="100" Height="30"/>
```
means exactly the same as this C#:
```csharp
var button = new Button();
button.Content = "Save";
button.Width = 100;
button.Height = 30;
```

And nesting means "put this object inside that one":
```xml
<StackPanel>
    <TextBlock Text="Name"/>
    <TextBox/>
</StackPanel>
```
```csharp
var panel = new StackPanel();
panel.Children.Add(new TextBlock { Text = "Name" });
panel.Children.Add(new TextBox());
```

So **anything you can do in XAML, you can do in C#.** XAML is just shorter, more readable, and lets designers and tools (the Visual Studio designer) work on the screen.

Notice: in XAML you write `Width="100"` as a **text**. WPF has **type converters** that turn the text into the right type: `"100"` → `double`, `"Red"` → a red `Brush`, `"10,5,10,5"` → a `Thickness`.

### 8.3 Property-element syntax (a preview)
When a value is too complex for a text attribute, you write the property as a child tag `<Type.Property>`:
```xml
<Button Content="Save">
    <Button.Background>
        <SolidColorBrush Color="LightBlue"/>
    </Button.Background>
</Button>
```
Same as `Background="LightBlue"`, but now you could put a gradient there. Day 2 goes deep into this.

### 8.4 What happens when you press Build — step by step

When you build, MSBuild (the .NET build engine) runs a special **XAML compile step** (the "markup compiler") **before** compiling your C#.

```
                    BUILD
MainWindow.xaml ──► XAML compiler ──► MainWindow.g.cs   (generated C#, in obj folder)
                                  └─► MainWindow.baml   (compiled binary XAML)
App.xaml        ──► XAML compiler ──► App.g.cs          (generated C#, contains Main())
                                  
All .cs + .g.cs ──► C# compiler   ──► MyFirstWpfApp.dll (+ .exe launcher)
                                       └─ the .baml files are embedded inside as resources
```

Two outputs per XAML file:

**1. BAML — Binary Application Markup Language**
- Your XAML text is converted into a compact **binary** form.
- It is **embedded inside the .dll** as a resource (you won't find a separate .xaml file next to the exe).
- Why? Reading binary is much faster than reading text at startup, and the XAML has already been checked for errors at build time.

**2. A generated C# file: `MainWindow.g.cs`** (`g` = generated)
- It is the **other half** of the `partial class MainWindow`.
- It contains the real `InitializeComponent()` method.
- You can see it: build once, then in Solution Explorer → Show All Files → `obj` → `Debug` → `net8.0-windows` → `MainWindow.g.cs`. (Or put the cursor on `InitializeComponent` and press **F12 / Go to Definition**.)

### 8.5 Inside `MainWindow.g.cs` (simplified)

Suppose MainWindow.xaml has a TextBox with `x:Name="NameTextBox"` and a Button with `Click="SayHelloButton_Click"`. The generated file looks like this (simplified, real one has more attributes):

```csharp
public partial class MainWindow : System.Windows.Window,
                                  System.Windows.Markup.IComponentConnector
{
    // (1) A FIELD is created for every element that has x:Name
    internal System.Windows.Controls.TextBox NameTextBox;

    private bool _contentLoaded;

    // (2) The real InitializeComponent
    public void InitializeComponent()
    {
        if (_contentLoaded) return;          // only load once
        _contentLoaded = true;

        // Where the compiled BAML is stored inside the assembly
        var resourceLocator = new System.Uri(
            "/MyFirstWpfApp;component/mainwindow.xaml", System.UriKind.Relative);

        // Read the BAML and build all the objects
        System.Windows.Application.LoadComponent(this, resourceLocator);
    }

    // (3) Called by the BAML reader for each named element / event
    void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
    {
        switch (connectionId)
        {
            case 1:
                this.NameTextBox = (System.Windows.Controls.TextBox)target;   // fill the field
                return;
            case 2:
                ((System.Windows.Controls.Button)target).Click +=
                    new System.Windows.RoutedEventHandler(this.SayHelloButton_Click); // wire the event
                return;
        }
        this._contentLoaded = true;
    }
}
```

### 8.6 So what does `InitializeComponent()` do? (the full story)

When `new MainWindow()` runs:

1. The constructor calls `InitializeComponent()` (the one in the **generated** half).
2. It checks `_contentLoaded` so the work happens only once.
3. It calls `Application.LoadComponent(this, uri)`.
4. `LoadComponent` finds the **BAML** inside the assembly and reads it with the **BAML reader**.
5. For each element in the BAML it **creates the object** (`new Grid()`, `new TextBox()`, `new Button()`…), **sets the properties** (`Width = 100`…), and **adds children** to parents. Instead of creating a new Window, it fills **`this`** — the window that is already being constructed.
6. For every element with `x:Name` or an event like `Click=`, the reader calls **`Connect(id, object)`**, which:
   - stores the object in the matching **field** (that's why you can write `NameTextBox.Text` in C#), and
   - **subscribes your method** to the event (that's why `SayHelloButton_Click` runs on click).
7. Control returns to your constructor. Everything after `InitializeComponent()` can safely use the named controls.

**Consequences developers must understand:**
- **Using a named control before `InitializeComponent()` → crash** (`NullReferenceException`), because the field is still `null`.
- **Deleting `InitializeComponent()` → empty window**: the XAML is never loaded.
- **`x:Name` is how C# "sees" a control.** No `x:Name` → no field → you can't reach it from code-behind. (In MVVM we rarely need names because we use binding.)
- **Event names must match exactly**, or the build fails: the generated `Connect` method refers to a method that doesn't exist.
- **Why the class must be `partial`:** you write one half, the compiler writes the other half; C# joins them into one class.
- If IntelliSense says "`NameTextBox` does not exist" right after you added it, **build once**: the `.g.cs` file is regenerated on build.

### 8.7 The same for `App` — where `Main()` comes from

`App.g.cs` (generated from App.xaml), simplified:

```csharp
public partial class App : System.Windows.Application
{
    public void InitializeComponent()
    {
        // From StartupUri="MainWindow.xaml"
        this.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
    }

    [System.STAThread]                 // the UI thread must be "single-threaded apartment"
    public static void Main()          // ← THE PROGRAM STARTS HERE
    {
        MyFirstWpfApp.App app = new MyFirstWpfApp.App();
        app.InitializeComponent();
        app.Run();                     // starts the message loop; returns when the app closes
    }
}
```

- `[STAThread]`: WPF (like most Windows UI) requires the UI thread to be a **Single-Threaded Apartment** thread. It's a Windows/COM rule for UI components such as the clipboard and drag-and-drop. You don't need more detail than that.
- `app.Run()` starts the **message loop** (Part 9).

### 8.8 The complete startup story (draw this on the board)

```
1. You double-click the .exe (or press F5)
2. Windows starts the .NET runtime (CLR), which calls  App.Main()        ← generated
3. Main() creates the App object                     new App()
4. Main() calls app.InitializeComponent()            sets StartupUri = "MainWindow.xaml"
5. Main() calls app.Run()                            the app is now "running"
6. Application raises the Startup event              (OnStartup)
7. WPF sees StartupUri → creates the window:         new MainWindow()
8. MainWindow constructor → InitializeComponent()    BAML → objects, fields, events
9. Window is shown; Loaded event fires
10. Message loop: WPF waits for mouse/keyboard events and redraws the screen
11. User closes the last window → app shuts down     (ShutdownMode = OnLastWindowClose)
12. Application raises the Exit event (OnExit) → Run() returns → Main() ends → process exits
```

---

## Part 9 — The UI thread and the message loop (one simple idea)

- A WPF app has **one special thread**: the **UI thread**. Every window and control belongs to it.
- `app.Run()` makes this thread run a **loop** forever: *"Is there a click? A key press? Something to redraw? Handle it. Repeat."* This is the **message loop**, managed by the **Dispatcher**.
- If your click handler does something slow on the UI thread (for example `Thread.Sleep(5000)` or a slow database call), the loop is **stuck** → the window **freezes** and Windows shows "Not Responding".
- Live demo idea (30 seconds): put `Thread.Sleep(5000);` in the click handler, click, try to move the window. It freezes. Remove it.
- Day 12 teaches the fix: `async` / `await`.

---

## Part 10 — Session script (60 minutes)

The content above is more than 60 minutes. **Priorities:** Parts 3, 5, 7 and 8.6 are a must. Parts 2, 4, 8.7–8.8 and 9 can be shorter.

| Time | Activity | Key lines to say |
|---|---|---|
| 0:00–0:03 | Welcome, course overview (21 days, final project with database and role-based login) | "We go from zero to architecture level, one step per day." |
| 0:03–0:08 | Part 1–2: desktop apps; C#, .NET, runtime, SDK; .NET Framework vs .NET | "C# is the language, .NET runs it. We use modern .NET 8, never '.NET Framework' templates." |
| 0:08–0:14 | Part 3: what WPF is, XAML vs C#, how WPF draws (DirectX, vectors) | "XAML is the dining room, C# is the kitchen." |
| 0:14–0:19 | Part 4–5: desktop family, WPF vs WinForms with the two code samples | "In WinForms you copy data by hand; in WPF you bind it once." |
| 0:19–0:22 | Setup check: everyone runs `dotnet --list-sdks` | "If you don't see 8, tell me after the session." |
| 0:22–0:27 | **Demo step 1–2**: create the project, run it empty | "This is already a real Windows app." |
| 0:27–0:37 | **Part 7: walk through every file**, line by line, in Visual Studio | "Every XAML file has a C# partner. `partial` joins them." |
| 0:37–0:45 | **Demo step 3–5**: build the Hello screen and click handler | "Every tag is an object, every attribute is a property." |
| 0:45–0:53 | **Part 8: open `MainWindow.g.cs` together**, explain fields, `InitializeComponent`, `Connect`, BAML. Show the crash when using a control before `InitializeComponent()` | "InitializeComponent reads the compiled XAML and builds the screen into *this* window." |
| 0:53–0:55 | Board: the 12-step startup story; freeze demo with `Thread.Sleep` | "One UI thread. Block it and the window freezes." |
| 0:55–1:00 | Recap (Part 11) + 3–4 questions (Part 12) + homework (Part 13) | |

### Live demo, step by step

**Step 1 — Create the project**
Visual Studio → **Create a new project** → search **WPF Application** → pick the **C#** one **without "(.NET Framework)"** → Project name `MyFirstWpfApp` → Next → Framework **.NET 8.0** → Create.

Or in a terminal:
```bash
dotnet new wpf -n MyFirstWpfApp -f net8.0
cd MyFirstWpfApp
dotnet run
```

**Step 2 — Run it (F5).** An empty 800×450 window. Point out: title bar text = `Title`, size = `Width`/`Height`. Close it.

**Step 3 — Replace MainWindow.xaml with:**
```xml
<Window x:Class="MyFirstWpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="My First WPF App" Height="250" Width="400"
        FontSize="16">
    <!-- A StackPanel places its children one under another -->
    <StackPanel Margin="20">
        <TextBlock Text="What is your name?"/>
        <TextBox x:Name="NameTextBox" Margin="0,8,0,8"/>
        <Button Content="Say Hello" Width="120" HorizontalAlignment="Left"
                Click="SayHelloButton_Click"/>
        <TextBlock x:Name="GreetingTextBlock" Margin="0,12,0,0" Foreground="DarkBlue"/>
    </StackPanel>
</Window>
```
Explain each line:
- `FontSize="16"` on the **Window**: every text inside inherits it (property value inheritance).
- `StackPanel Margin="20"`: 20 units of space around the panel.
- `TextBlock`: shows read-only text. `TextBox`: user can type.
- `x:Name="NameTextBox"`: creates a **field** in the generated class so C# can use it.
- `Margin="0,8,0,8"`: Left, Top, Right, Bottom.
- `HorizontalAlignment="Left"`: don't stretch the button across the whole width.
- `Click="SayHelloButton_Click"`: on click, call this C# method (wired in `Connect`).
- `Foreground="DarkBlue"`: text colour; the text "DarkBlue" is converted to a Brush by a type converter.

**Step 4 — Add the handler in MainWindow.xaml.cs** (inside the class, after the constructor):
```csharp
private void SayHelloButton_Click(object sender, RoutedEventArgs e)
{
    // sender = the button that was clicked
    // e      = information about the click event
    string name = NameTextBox.Text;
    GreetingTextBlock.Text = $"Hello, {name}! Welcome to WPF.";
}
```
- `object sender`: which control raised the event.
- `RoutedEventArgs e`: details of the event (routed events: Day 10).
- `$"…{name}…"`: C# string interpolation.

**Step 5 — Run (F5).** Type a name, click. It works.

**Step 6 — Behind the scenes (the most important 8 minutes)**
1. Put the cursor on `InitializeComponent` → **F12**. `MainWindow.g.cs` opens.
2. Show the field `internal TextBox NameTextBox;`. "This is why you can write `NameTextBox.Text`."
3. Show `InitializeComponent()` → `LoadComponent(...)`. "This reads the compiled BAML and builds everything."
4. Show `Connect(...)`: the field is filled; `Click +=` wires your method.
5. **Break it on purpose:** move `NameTextBox.Text = "Rohit";` to **before** `InitializeComponent();` in the constructor → run → `NullReferenceException`. Move it after → works, and the box starts with "Rohit". Ask: "Why?"
6. **Break it again:** comment out `InitializeComponent();` → run → empty window. Restore it.
7. Rename the method in C# only → build error, because the generated `Connect` can't find it.

**Step 7 — Tell the truth about this code**
"This click-event style is how WinForms works, and it's fine for learning. From Day 5 (binding) and Day 7 (MVVM), this code-behind will disappear and the screen will talk to a ViewModel instead."

### Common problems during the demo

| Problem | Cause → fix |
|---|---|
| No "WPF Application" template | ".NET desktop development" workload missing → install it |
| Project targets "net48" or similar | Picked the ".NET Framework" template → recreate with the right one |
| `SayHelloButton_Click does not exist` | Name in XAML ≠ name in C# (case-sensitive) |
| `NameTextBox does not exist in the current context` | Typo in `x:Name`, or not built yet → build once (regenerates `.g.cs`) |
| Window opens empty | `InitializeComponent()` missing or the XAML content is outside `<StackPanel>` |
| "The property 'Content' is set more than once" | Two controls put directly inside `<Window>` → wrap them in a panel |
| App doesn't start, error about StartupUri | App.xaml's `StartupUri` doesn't match the window file name |

---

## Part 11 — What we covered today (send this to the team)

1. **Desktop apps** run installed on the PC in their own window; WPF builds them for Windows.
2. **C#** is the language; **.NET** is the platform (runtime CLR + libraries + SDK). C# → IL → machine code by the JIT at run time. We use **.NET 8**, not the old **.NET Framework**.
3. **WPF** = Windows Presentation Foundation. **XAML** describes the screen; **C#** contains the logic.
4. WPF draws with **DirectX**, using **vectors** and **device-independent units** (1/96 inch), so it is sharp on any screen and fully stylable.
5. **WPF vs WinForms:** automatic layout, styles/templates, **data binding**, **MVVM**.
6. **Setup:** Visual Studio 2022 + ".NET desktop development" + .NET 8 SDK.
7. **Project files:** `.sln`, `.csproj` (`WinExe`, `net8.0-windows`, `UseWPF`), `App.xaml`/`.cs` (startup + shared resources), `MainWindow.xaml`/`.cs` (window + code-behind), `AssemblyInfo.cs`, `bin`/`obj`.
8. **XAML = object creation:** tag = object, attribute = property, nesting = child objects. Namespaces `xmlns` and `x:`.
9. **Build:** each XAML becomes **BAML** (embedded in the dll) + a generated **`.g.cs`** file.
10. **`InitializeComponent()`** loads the BAML, creates all objects into `this`, fills **`x:Name` fields**, and wires **events** through `Connect`.
11. **`partial` class** = your half + generated half. **`Main()`** is generated from App.xaml.
12. **Startup:** Main → new App → Run → Startup → new MainWindow → InitializeComponent → Show → message loop → Exit.
13. **One UI thread**; blocking it freezes the window.

---

## Part 12 — Questions to ask the team after the session

Pick 5–6. Ask different people. Answers in *italics*.

**Basics**
1. What is the difference between C# and .NET? *(Language vs the platform that runs it.)*
2. Which template must you **not** pick in Visual Studio, and why? *("WPF App (.NET Framework)": old framework.)*
3. What is the job of XAML, and the job of C#? *(Screen vs logic.)*
4. Name three differences between WPF and WinForms. *(Vector/DirectX, automatic layout, styles, data binding, MVVM.)*
5. Why does a WPF window stay sharp on a 4K monitor? *(Vector drawing + device-independent units.)*

**Project structure**
6. Which three settings in the project file make it a WPF app? *(WinExe, net8.0-windows, UseWPF.)*
7. Which file decides which window opens first? *(App.xaml, `StartupUri`.)*
8. What does `mc:Ignorable="d"` do? *(Ignores designer-only `d:` attributes at run time.)*
9. What's in the `bin` folder and what's in the `obj` folder? *(Final output vs intermediate/generated files.)*
10. Why can a Window contain only one child, and how do we add many controls? *(Content holds one object → use a panel.)*

**Behind the scenes**
11. What does `partial` mean, and who writes the other half? *(Class split across files; the XAML compiler generates it.)*
12. What is BAML and why does WPF use it? *(Compiled binary XAML embedded in the assembly; faster loading, checked at build.)*
13. Explain in your own words what `InitializeComponent()` does. *(Loads BAML, creates objects into this window, fills x:Name fields, wires events.)*
14. What happens if you use `NameTextBox` **before** `InitializeComponent()`? Why? *(NullReferenceException: the field isn't set yet.)*
15. Where does `Main()` come from in a WPF app? *(Generated from App.xaml into App.g.cs.)*
16. Why does the window freeze if you call `Thread.Sleep(5000)` in a click handler? *(It blocks the single UI thread / message loop.)*

**Thinking question**
17. Today we put logic in a Click handler. Why could that become a problem in an app with 50 screens? *(Logic mixed with UI, hard to test and reuse → MVVM.)*

**Blocker check:** "Who could not create or run the app today?" Note names; help them before Day 2.

---

## Part 13 — Homework (20–30 minutes)

1. Make sure the "Hello WPF" app runs on your machine.
2. Add a second TextBox for **age** and show: "Hello Rohit, you are 30 years old."
3. Add a **Clear** button that empties both text boxes and the greeting.
4. Open `MainWindow.g.cs` and find: (a) the field for each `x:Name`, (b) the `Connect` case for each button. Write down how many cases there are and why.
5. Write, in 5 lines, the startup steps from double-clicking the exe to seeing the window.
6. **Bonus:** create the same screen **entirely in C#** (no XAML): in the constructor, remove `InitializeComponent()`, create a `StackPanel`, `TextBlock`, `TextBox` and `Button` with `new`, add them to the panel, and set `this.Content = panel;`. This proves "XAML is just a way to create objects".

---

## Part 14 — Trainer notes for Rohit

- Speak slowly in Part 8. It's the part developers will remember from Day 1.
- Show, don't tell: **F12 on `InitializeComponent`** is the "aha" moment.
- Don't teach binding, MVVM or layout panels in depth today. Mention them as "coming on Day 3 / 5 / 7".
- If many people are not set up, do the demo on your screen and let them repeat it as homework.
- **Tomorrow (Day 2: XAML fundamentals)** starts from today's XAML: namespaces in depth, property-element syntax, markup extensions (`{Binding}`, `{StaticResource}`), attached properties, `x:` keywords, the code-behind, and application lifecycle events (`Startup`, `Exit`, `DispatcherUnhandledException`).
