using System;
using System.Reactive.Linq;
using Xunit;
using ReactiveUI.Fody.Helpers;
using GlobalSettings = ReactiveUI.Fody.Helpers.Settings.GlobalSettings;

namespace ReactiveUI.Fody.Tests.Issues
{
    public class Issue31Tests
    {
        [Fact]
        public void ExceptionPropertyInfoForReactiveProperty()
        {
            try
            {
                GlobalSettings.IsLogPropertyOnErrorEnabled = true;
                try
                {
                    var model = new ReactivePropertyModel();
                    model.MyProperty = "foo";
                }
                catch (LogPropertyOnErrorException ex)
                {
                    Assert.Equal(nameof(ObservableAsPropertyModel.MyProperty), ex.Property);
                }
            }
            finally
            {
                GlobalSettings.IsLogPropertyOnErrorEnabled = false;
            }
        }

        [Fact]
        public void ExceptionPropertyInfoForObservableAsProperty()
        {
            try
            {
                GlobalSettings.IsLogPropertyOnErrorEnabled = true;

                try
                {
                    new ObservableAsPropertyModel();
                }
                catch (UnhandledErrorException ex)
                {
                    var propertyException = (LogPropertyOnErrorException)ex.InnerException;
                    Assert.Equal(nameof(ObservableAsPropertyModel.MyProperty), propertyException.Property);
                }
            }
            finally
            {
                GlobalSettings.IsLogPropertyOnErrorEnabled = false;
            }
        }

        public class ObservableAsPropertyModel : ReactiveObject
        {
            public extern string MyProperty { [ObservableAsProperty]get; }

            public ObservableAsPropertyModel()
            {
                Observable.Throw<string>(new Exception("Observable error")).ToPropertyEx(this, x => x.MyProperty);
            }
        }

        public class ReactivePropertyModel : ReactiveObject
        {
            [Reactive]public string MyProperty { get; set; }

            public ReactivePropertyModel()
            {
                this.ObservableForProperty(x => x.MyProperty).Subscribe(_ =>
                {
                    throw new Exception("Subscribe error");
                });
            }
        }
    }
}