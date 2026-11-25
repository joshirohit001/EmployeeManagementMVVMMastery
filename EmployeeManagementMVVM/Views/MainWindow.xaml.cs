using System.Windows;
using System.Windows.Controls;
using EmployeeManagementMVVM.ViewModels;

namespace EmployeeManagementMVVM.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void SkillsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataContext is not MainViewModel viewModel ||
            sender is not ListBox listBox)
            return;

        viewModel.SelectedSkillsText = string.Join(", ",
            listBox.SelectedItems.Cast<string>());
    }
}
