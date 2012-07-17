using System;
using System.Windows;
using XeDotNet.SimpleTodo.Services;
using XeDotNet.SimpleTodo.ViewModels;
using XeDotNet.SimpleTodo.Views;

namespace XeDotNet.SimpleTodo
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            MainWindow main = new MainWindow();
            TodoListView view = new TodoListView();
            TodoListViewModel viewModel = new TodoListViewModel(view, new TodoListFakeService());
            viewModel.Initialize();
            main.AddChild(view);

            Application app = new Application();
            app.Run(main);
        }
    }
}