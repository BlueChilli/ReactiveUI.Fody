using System.Reactive.Linq;
using Xunit;
using ReactiveUI.Fody.Helpers;

namespace ReactiveUI.Fody.Tests.Issues
{
    public class Issue11Tests
    {
        [Fact]
        public void AllowObservableAsPropertyAttributeOnAccessor()
        {
            var model = new TestModel("foo");
            Assert.Equal("foo", model.MyProperty);
        }

        public class TestModel : ReactiveObject
        {
            public extern string MyProperty { [ObservableAsProperty]get; }

            public TestModel(string myProperty)
            {
                Observable.Return(myProperty).ToPropertyEx(this, x => x.MyProperty);
            }
        }
    }
}