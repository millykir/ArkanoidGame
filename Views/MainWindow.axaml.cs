using Avalonia.Controls;

namespace ArkanoidGameEasy.Views;

public partial class MainWindow : Window
{
     public MainWindow()
	{
     InitializeComponent();
	}

	private void InitializeComponent()
	{
     AvaloniaXamlLoader.Load(this);
	}
}