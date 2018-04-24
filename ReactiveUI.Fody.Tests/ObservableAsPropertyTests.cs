using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using Xunit;

namespace ReactiveUI.Fody.Tests
{
    public class ObservableAsPropertyTests
    {
        [Fact]
        public void TestPropertyReturnsFoo()
        {
            var model = new TestModel();
            Assert.Equal("foo", model.TestProperty);
        }

        class TestModel : ReactiveObject
        {
            [ObservableAsProperty]
            public string TestProperty { get; private set; }

            public TestModel()
            {
                Observable.Return("foo").ToPropertyEx(this, x => x.TestProperty);
            }
        }
    }
}