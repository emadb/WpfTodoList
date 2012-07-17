using XeDotNet.SimpleTodo.Models;
using Xunit;

namespace XeDotNet.SimpleTodo.Fixtures.Models
{
    public class TodoFixture
    {
        [Fact]
        public void SetIsCompleted_ShouldRaisePropertyChanged()
        {
            bool isCalled = false;
            Todo item= new Todo();

            item.PropertyChanged += (s, e) => { if (e.PropertyName == "IsCompleted") isCalled = true; };

            item.IsCompleted = true;

            Assert.True(isCalled);
        }
    }
}