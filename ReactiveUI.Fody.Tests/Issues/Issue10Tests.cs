using System;
using Xunit;
using ReactiveUI.Fody.Helpers;

namespace ReactiveUI.Fody.Tests.Issues
{
    public class Issue10Tests
    {
        [Fact]
        public void UninitializedObservableAsPropertyHelperDoesntThrowAndReturnsDefaultValue()
        {
            var model = new TestModel();
            Assert.Null(model.MyProperty);
            Assert.Equal(0, model.MyIntProperty);
            Assert.Equal(default(DateTime), model.MyDateTimeProperty);
        }

        class TestModel : ReactiveObject
        {
            [ObservableAsProperty]
            public string MyProperty { get; private set; }

            [ObservableAsProperty]
            public int MyIntProperty { get; private set; }

            [ObservableAsProperty]
            public DateTime MyDateTimeProperty { get; private set; }

            public string OtherProperty { get; private set; }

            public TestModel()
            {
                OtherProperty = MyProperty;
            }
        }
    }
}