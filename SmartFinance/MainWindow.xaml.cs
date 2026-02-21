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
using SmartFinance.ViewModels;

namespace SmartFinance;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void TransactionsGrid_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        var newFocus = e.NewFocus as DependencyObject;

        // Если фокус ушёл на кнопку Delete или внутрь неё — не сбрасываем выбор
        if (newFocus != null && IsDescendantOf(newFocus, DeleteButton))
            return;

        if (DataContext is MainViewModel vm)
            vm.SelectedTransaction = null;
    }

    private static bool IsDescendantOf(DependencyObject node, DependencyObject ancestor)
    {
        var current = node;
        while (current != null)
        {
            if (ReferenceEquals(current, ancestor))
                return true;

            current = VisualTreeHelper.GetParent(current);
        }
        return false;
    }
}