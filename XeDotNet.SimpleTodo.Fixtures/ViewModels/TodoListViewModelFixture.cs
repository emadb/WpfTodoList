using System;
using System.Collections.Generic;
using Moq;
using XeDotNet.SimpleTodo.Models;
using XeDotNet.SimpleTodo.Services;
using XeDotNet.SimpleTodo.ViewModels;
using XeDotNet.SimpleTodo.Views;
using Xunit;

namespace XeDotNet.SimpleTodo.Fixtures.ViewModels
{
    public class TodoListViewModelFixture
    {
        private readonly Mock<ITodoListView> _view;
        private readonly Mock<ITodoListService> _service;
        private readonly TodoListViewModel _viewModel;
     
        public TodoListViewModelFixture()
        {
            _view = new Mock<ITodoListView> { DefaultValue = DefaultValue.Mock };
            _service = new Mock<ITodoListService> { DefaultValue = DefaultValue.Mock };
            _viewModel = new TodoListViewModel(_view.Object, _service.Object);
        }

        [Fact]
        public void Ctor_ShouldSetTheDataContext()
        {
            _view.VerifySet(v => v.DataContext = _viewModel);   
        }

        [Fact]
        public void Initialize_ShouldShowTheListOfTodoItems()
        {
            IList<Todo> items = new List<Todo>{new Todo(), new Todo()};
            _service.Setup(s => s.GetItems()).Returns(items);

            _viewModel.Initialize();
            Assert.Equal(2, _viewModel.Items.Count);
        }

        [Fact]
        public void AddNewItem_ShouldAddTheItemToTheCollection()
        {
            _viewModel.CurrentItemDescription = "test";
            _viewModel.CurrentItemDueDate = DateTime.Today;
            
            _viewModel.AddNewItemCommand.Execute();

            Assert.Equal(1, _viewModel.Items.Count);
            Assert.Equal("test", _viewModel.Items[0].Description);
            Assert.Equal(DateTime.Today, _viewModel.Items[0].DueDate);
        }

        [Fact]
        public void AddNewItem_ShouldCallTheServiceToSaveItem()
        {
            _viewModel.CurrentItemDescription = "test";
            _viewModel.CurrentItemDueDate = DateTime.Today;

            _viewModel.AddNewItemCommand.Execute();

            _service.Verify(s => s.Save(It.Is<Todo>(t => t.Description =="test" && t.DueDate == DateTime.Today)));
        }

        [Fact]
        public void AddNewItem_TheServiceReturnTheNewId_ShouldSetTodoId()
        {
            _viewModel.CurrentItemDescription = "test";
            _viewModel.CurrentItemDueDate = DateTime.Today;

            _service.Setup(s => s.Save(It.IsAny<Todo>())).Returns(42);

            _viewModel.AddNewItemCommand.Execute();

            Assert.Equal(42, _viewModel.Items[0].Id);
        }

        [Fact]
        public void AddNewItem_ShouldClearTheTextFields()
        {
            _viewModel.CurrentItemDescription = "test";
            _viewModel.CurrentItemDueDate = DateTime.Today.AddDays(10);

            _viewModel.AddNewItemCommand.Execute();

            Assert.Equal(String.Empty, _viewModel.CurrentItemDescription);
            Assert.Equal(DateTime.Today, _viewModel.CurrentItemDueDate);
        }

        [Fact]
        public void SetCurrentItemDescription_ShouldRaisePropertyChanged()
        {
            bool isCalled = false;

            _viewModel.PropertyChanged += (s, e) =>
                                              {
                                                  if (e.PropertyName == "CurrentItemDescription")
                                                      isCalled = true;
                                              };
            _viewModel.CurrentItemDescription = "hola";
            Assert.True(isCalled);
        }

        [Fact]
        public void SetCurrentItemDueDate_ShouldRaisePropertyChanged()
        {
            bool isCalled = false;

            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "CurrentItemDueDate")
                    isCalled = true;
            };
            _viewModel.CurrentItemDueDate = DateTime.Today;
            Assert.True(isCalled);
        }

        [Fact]
        public void CanExecuteAddNewItem_DescriptionIsEmpty_ShouldReturnFalse()
        {
            _viewModel.CurrentItemDescription = "";
            bool canExecute = _viewModel.AddNewItemCommand.CanExecute();
            Assert.False(canExecute);
        }

        [Fact]
        public void SetCurrentItemDescription_ShouldRaiseCanExecuteChange()
        {
            bool isCalled = false;

            _viewModel.AddNewItemCommand.CanExecuteChanged += (s, e) =>
                                                                  {
                                                                      isCalled = true;
                                                                  };
            _viewModel.CurrentItemDescription = "hola";
            Assert.True(isCalled);
        }

        [Fact]
        public void Initialize_CurrentItemDueDateShouldBeToday()
        {
            _viewModel.Initialize();
            Assert.Equal(DateTime.Today, _viewModel.CurrentItemDueDate);
        }

        [Fact]
        public void DeleteItem_ShouldCallServiceToRemoveTheItem()
        {
            Todo item = new Todo();
            _viewModel.Initialize();
           
            _viewModel.DeleteItemCommand.Execute(item);

            _service.Verify(s => s.Delete(item));
        }

        [Fact]
        public void DeleteItem_ShouldRemoveItemFromCollection()
        {
            Todo toDelete = new Todo();
            IList<Todo> items = new List<Todo>{new Todo(), toDelete, new Todo()};
            _service.Setup(s => s.GetItems()).Returns(items);
            _viewModel.Initialize();

            _viewModel.DeleteItemCommand.Execute(toDelete);

            Assert.Equal(2, _viewModel.Items.Count);
        }

        [Fact]
        public void Completed_ShouldChangeStateOfTheItem()
        {
            Todo item = new Todo();
            IList<Todo> items = new List<Todo> { new Todo(), item, new Todo() };
            _service.Setup(s => s.GetItems()).Returns(items);
            _viewModel.Initialize();

            _viewModel.CompletedCommand.Execute(item);

            Assert.True(item.IsCompleted);
        }

       
    }
}