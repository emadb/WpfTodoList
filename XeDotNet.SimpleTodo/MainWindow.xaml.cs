using System.Windows;
using System.Windows.Controls;

namespace XeDotNet.SimpleTodo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddChild(UserControl control)
        {
            _host.Children.Add(control);
        }
    }
}
