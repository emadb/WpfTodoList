using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Trilance.Trimp.Common;
using XeDotNet.SimpleTodo.Models;
using XeDotNet.SimpleTodo.Services;
using XeDotNet.SimpleTodo.Views;

namespace XeDotNet.SimpleTodo.ViewModels
{
    public class TodoListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _currentItemDescription;
        private DateTime _currentItemDueDate;
        private readonly ITodoListView _view;
        private readonly ITodoListService _service;
        public ObservableCollection<Todo> Items { get; private set; }

        public DelegateCommand AddNewItemCommand { get; private set; }

        public string CurrentItemDescription
        {
            get { return _currentItemDescription; }
            set
            {
                _currentItemDescription = value;
                OnPropertyChanged("CurrentItemDescription");
                AddNewItemCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime CurrentItemDueDate
        {
            get { return _currentItemDueDate; }
            set
            {
                _currentItemDueDate = value;
                OnPropertyChanged("CurrentItemDueDate");
            }
        }

        public DelegateCommand<Todo> DeleteItemCommand { get; private set; }
        public DelegateCommand<Todo> CompletedCommand { get; private set; }

        public TodoListViewModel(ITodoListView view, ITodoListService service)
        {
            _view = view;
            _service = service;
            Items = new ObservableCollection<Todo>();
            AddNewItemCommand = new DelegateCommand(ExecuteAddNewItemCommand, CanExecuteAddNewItemCommand);
            DeleteItemCommand = new DelegateCommand<Todo>(ExecuteDeleteCommand);
            CompletedCommand = new DelegateCommand<Todo>(ExecuteCompletedCommand);
            
            _view.DataContext = this;
        }

        private void ExecuteCompletedCommand(Todo obj)
        {
            obj.IsCompleted = true;
        }

        private void ExecuteDeleteCommand(Todo item)
        {
            _service.Delete(item);
            Items.Remove(item);
        }

        private bool CanExecuteAddNewItemCommand()
        {
            return !String.IsNullOrEmpty(CurrentItemDescription);
        }

        private void ExecuteAddNewItemCommand()
        {
            Todo item = new Todo();
            item.Description = CurrentItemDescription;
            item.DueDate = CurrentItemDueDate;
            Items.Add(item);
            item.Id = _service.Save(item);
            CurrentItemDescription = String.Empty;
            CurrentItemDueDate = DateTime.Today;
        }

        public void Initialize()
        {
            IList<Todo> todos = _service.GetItems();
            foreach (var todo in todos)
            {
                Items.Add(todo);
            }
            CurrentItemDueDate = DateTime.Today;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}