using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;
using ReactiveUI.Fody.Helpers;

namespace ReactiveUI.Fody.Tests.Issues
{
    public class Issue41Tests
    {
        [Fact]
        public void PropertyChangedRaisedForDerivedPropertyOnIntPropertySet()
        {
            // Arrange
            var model = new TestModel();
            var expectedInpcPropertyName = nameof(TestModel.DerivedProperty);
            var receivedInpcPropertyNames = new List<string>();

            var inpc = (INotifyPropertyChanged) model;
            inpc.PropertyChanged += (sender, args) => receivedInpcPropertyNames.Add(args.PropertyName);

            // Act
            model.IntProperty = 5;

            // Assert
            Assert.Contains(expectedInpcPropertyName, receivedInpcPropertyNames);
        }

        [Fact]
        public void PropertyChangedRaisedOnStringPropertySet()
        {
            // Arrange
            var model = new TestModel();
            var expectedInpcPropertyName = nameof(TestModel.DerivedProperty);
            var receivedInpcPropertyNames = new List<string>();

            var inpc = (INotifyPropertyChanged) model;
            inpc.PropertyChanged += (sender, args) => receivedInpcPropertyNames.Add(args.PropertyName);

            // Act
            model.StringProperty = "Foo";

            // Assert
            Assert.Contains(expectedInpcPropertyName, receivedInpcPropertyNames);
        }

        [Fact]
        public void PropertyChangedRaisedForDerivedPropertyAndAnotherExpressionBodiedPropertyAndCombinedExpressionBodyPropertyWithAutoPropOnIntPropertySet()
        {
            // Arrange
            var model = new TestModel();
            var expectedInpcPropertyName1 = nameof(TestModel.AnotherExpressionBodiedProperty);
            var expectedInpcPropertyName2 = nameof(TestModel.DerivedProperty);
            var expectedInpcPropertyName3 = nameof(TestModel.CombinedExpressionBodyPropertyWithAutoProp);
            var receivedInpcPropertyNames = new List<string>();

            var inpc = (INotifyPropertyChanged) model;
            inpc.PropertyChanged += (sender, args) => receivedInpcPropertyNames.Add(args.PropertyName);

            // Act
            model.IntProperty = 5;

            // Assert
            Assert.Contains(expectedInpcPropertyName1, receivedInpcPropertyNames);
            Assert.Contains(expectedInpcPropertyName2, receivedInpcPropertyNames);
            Assert.Contains(expectedInpcPropertyName3, receivedInpcPropertyNames);
        }

        [Fact]
        public void PropertyChangedRaisedForCombinedExpressionBodyPropertyWithAutoPropOnStringPropertySet()
        {
            // Arrange
            var model = new TestModel();
            var expectedInpcPropertyName = nameof(TestModel.CombinedExpressionBodyPropertyWithAutoProp);
            var receivedInpcPropertyNames = new List<string>();

            var inpc = (INotifyPropertyChanged)model;
            inpc.PropertyChanged += (sender, args) => receivedInpcPropertyNames.Add(args.PropertyName);

            // Act
            model.StringProperty = "Foo";

            // Assert
            Assert.Contains(expectedInpcPropertyName, receivedInpcPropertyNames);
        }

        [Fact]
        public void PropertyChangedRaisedForCombinedExpressionBodyPropertyWithAutoPropNonReactivePropertyOnIntPropertySet()
        {
            // Arrange
            var model = new TestModel();
            var expectedInpcPropertyName = nameof(TestModel.CombinedExpressionBodyPropertyWithAutoPropNonReactiveProperty);
            var receivedInpcPropertyNames = new List<string>();

            var inpc = (INotifyPropertyChanged)model;
            inpc.PropertyChanged += (sender, args) => receivedInpcPropertyNames.Add(args.PropertyName);

            // Act
            model.IntProperty = 5;

            // Assert
            Assert.Contains(expectedInpcPropertyName, receivedInpcPropertyNames);
        }

        [Fact]
        // Ensure that this only works with dependent properties that have the [Reactive] attribute
        public void PropertyChangedNotRaisedForCombinedExpressionBodyPropertyWithAutoPropNonReactivePropertyOnNonReactivePropertySet()
        {
            // Arrange
            var model = new TestModel();
           // var expectedInpcPropertyName = nameof(TestModel.CombinedExpressionBodyPropertyWithAutoPropNonReactiveProperty);
            var receivedInpcPropertyNames = new List<string>();

            var inpc = (INotifyPropertyChanged)model;
            inpc.PropertyChanged += (sender, args) => receivedInpcPropertyNames.Add(args.PropertyName);

            // Act
            model.NonReactiveProperty = "Foo";

            // Assert
            Assert.Empty(receivedInpcPropertyNames);
        }

        class TestModel : ReactiveObject
        {
            [Reactive]
            public int IntProperty { get; set; }

            [Reactive]
            public string StringProperty { get; set; }

            public string NonReactiveProperty { get; set; }

            [Reactive]
            // Raise property change when either StringProperty or IntProperty are set
            public string DerivedProperty => StringProperty + IntProperty;

            [Reactive]
            // Raise property change when IntProperty is set
            public int AnotherExpressionBodiedProperty => IntProperty;

            [Reactive]
            // Raise property changed when StringProperty or IntProperty is set (as AnotherExpressionBodiedProperty is dependent upon IntProperty)
            public string CombinedExpressionBodyPropertyWithAutoProp => AnotherExpressionBodiedProperty + StringProperty;

            [Reactive]
            public string CombinedExpressionBodyPropertyWithAutoPropNonReactiveProperty => CombinedExpressionBodyPropertyWithAutoProp + NonReactiveProperty;
        }
    }
}
