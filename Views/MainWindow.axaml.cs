using Avalonia.Controls;
using Avalonia.Input;
using DemoArkanoid.ViewModels;

namespace DemoArkanoid.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.A)
            {
                ((MainWindowViewModel)DataContext).PlayerUp = true;
            }
            else if (e.Key == Key.Right || e.Key == Key.D)
            {
                ((MainWindowViewModel)DataContext).PlayerDown = true;
            }
            else if (e.Key == Key.Space)
            {
                ((MainWindowViewModel)DataContext).KeySpaceStart(e);
            }
            else if (e.Key == Key.Escape)
            {
                ((MainWindowViewModel)DataContext).KeyEscPressed(e);
            }
        }


    private void MainWindow_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Left || e.Key == Key.A)
        {
            ((MainWindowViewModel)DataContext).PlayerUp = false;
        }
        else if (e.Key == Key.Right || e.Key == Key.D)
        {
            ((MainWindowViewModel)DataContext).PlayerDown = false;
        }
        else if (e.Key == Key.Space)
        {
            ((MainWindowViewModel)DataContext).KeySpaceStart(e);
        }
    }
}
}