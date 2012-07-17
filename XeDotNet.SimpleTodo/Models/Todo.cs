using System;
using System.ComponentModel;

namespace XeDotNet.SimpleTodo.Models
{
    public class Todo : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        private bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}