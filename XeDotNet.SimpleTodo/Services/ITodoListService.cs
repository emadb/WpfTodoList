using System;
using System.Collections.Generic;
using XeDotNet.SimpleTodo.Models;

namespace XeDotNet.SimpleTodo.Services
{
    public interface ITodoListService
    {
        IList<Todo> GetItems();
        int Save(Todo todo);
        void Delete(Todo item);
    }

    class TodoListFakeService : ITodoListService
    {
        private IList<Todo> _items;

        public TodoListFakeService()
        {
            _items = new List<Todo>();
            _items.Add(new Todo { Id = 1, Description = "Todo numero 1", DueDate = DateTime.Today });
            _items.Add(new Todo { Id = 2, Description = "Todo numero 2", DueDate = DateTime.Today.AddDays(2) });
            _items.Add(new Todo { Id = 3, Description = "Todo numero 3", DueDate = DateTime.Today.AddDays(5) });
            _items.Add(new Todo { Id = 4, Description = "Todo numero 4", DueDate = DateTime.Today.AddDays(10) });

        }

        public IList<Todo> GetItems()
        {
            return _items;
        }

        public int Save(Todo todo)
        {
            return 42;
        }

        public void Delete(Todo item)
        {
            
        }
    }
}