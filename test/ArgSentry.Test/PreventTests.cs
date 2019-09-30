namespace ArgSentry.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PreventTests
    {
        #region Collections::CollectionWithAnyValuesLessThanOrEqualTo

        [TestMethod]
        public void CollectionWithAnyValuesLessThanOrEqualTo_WhenCollectionIsValid_ShouldNotThrow()
        {
            // Arrange
            const int MustBeGreaterThan = 0;
            var obj = new[] { 1, 2, 3, 4, 5, 6 };

            // Act
            Action act = () => Prevent.CollectionWithAnyValuesLessThanOrEqualTo(obj, MustBeGreaterThan, nameof(obj));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void CollectionWithAnyValuesLessThanOrEqualTo_WhenCollectionContainsMinValue_ShouldThrow()
        {
            // Arrange
            const int MustBeGreaterThan = 1;
            var obj = new[] { 1, 2, 3, 4, 5, 6 };

            var expectedMessage =
                $"All collection values must be greater than {MustBeGreaterThan}.\r\nParameter name: {nameof(obj)}";

            // Act
            Action act = () => Prevent.CollectionWithAnyValuesLessThanOrEqualTo(obj, MustBeGreaterThan, nameof(obj));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(obj));
        }

        [TestMethod]
        public void CollectionWithAnyValuesLessThanOrEqualTo_WhenCollectionContainsValueLessThanMinValue_ShouldThrow()
        {
            // Arrange
            const int MustBeGreaterThan = 3;
            var obj = new[] { 1, 2, 3, 4, 5, 6 };

            var expectedMessage =
                $"All collection values must be greater than {MustBeGreaterThan}.\r\nParameter name: {nameof(obj)}";

            // Act
            Action act = () => Prevent.CollectionWithAnyValuesLessThanOrEqualTo(obj, MustBeGreaterThan, nameof(obj));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(obj));
        }

        [TestMethod]
        public void CollectionWithAnyValuesLessThanOrEqualTo_WhenCollectionIsNull_ShouldDoNothing()
        {
            // Arrange
            const int MustBeGreaterThan = 3;
            int[] obj = null;

            // Act
            Action act = () => Prevent.CollectionWithAnyValuesLessThanOrEqualTo(obj, MustBeGreaterThan, "Whatevs. Does not matter.'");

            // Assert
            act.Should().NotThrow();
        }

        #endregion

        #region Collections::NullOrEmptyCollection

        [TestMethod]
        public void NullOrEmptyCollection_WhenCollectionIsValid_ShouldNotThrow()
        {
            // Arrange
            var obj = new List<string> { "This", "is", "a", "test" };

            // Act
            Action act = () => Prevent.NullOrEmptyCollection(obj, nameof(obj));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void NullOrEmptyCollection_WhenCollectionIsNull_ShouldThrow()
        {
            // Arrange
            List<string> obj = null;
            var expectedMessage = $"Collection cannot be null or empty.\r\nParameter name: {nameof(obj)}";

            // Act
            Action act = () => Prevent.NullOrEmptyCollection(obj, nameof(obj));

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(obj));
        }

        [TestMethod]
        public void NullOrEmptyCollection_WhenCollectionIsEmpty_ShouldThrow()
        {
            // Arrange
            var obj = new List<string>();
            var expectedMessage = $"Collection cannot be null or empty.\r\nParameter name: {nameof(obj)}";

            // Act
            Action act = () => Prevent.NullOrEmptyCollection(obj, nameof(obj));

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(obj));
        }

        #endregion

        #region Collections::NullOrEmptyReadonlyCollection

        [TestMethod]
        public void NullOrEmptyReadOnlyCollection_WhenCollectionIsValid_ShouldNotThrow()
        {
            // Arrange
            var obj = new List<string> { "This", "is", "a", "test" }.AsReadOnly();

            // Act
            Action act = () => Prevent.NullOrEmptyReadOnlyCollection(obj, nameof(obj));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void NullOrEmptyReadOnlyCollection_WhenCollectionIsNull_ShouldThrow()
        {
            // Arrange
            IReadOnlyCollection<string> obj = null;
            var expectedMessage = $"Collection cannot be null or empty.\r\nParameter name: {nameof(obj)}";

            // Act
            Action act = () => Prevent.NullOrEmptyReadOnlyCollection(obj, nameof(obj));

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage(expectedMessage).And.ParamName.Should().Be(nameof(obj));
        }

        [TestMethod]
        public void NullOrEmptyReadOnlyCollection_WhenCollectionIsEmpty_ShouldThrow()
        {
            // Arrange
            var obj = new List<string>().AsReadOnly();
            var expectedMessage = $"Collection cannot be null or empty.\r\nParameter name: {nameof(obj)}";

            // Act
            Action act = () => Prevent.NullOrEmptyReadOnlyCollection(obj, nameof(obj));

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage(expectedMessage).And.ParamName.Should().Be(nameof(obj));
        }

        #endregion

        #region Object::NullObject

        [TestMethod]
        public void NullObject_IfParameterIsNull_ShouldThrow()
        {
            // Arrange
            object obj = null;
            var expectedMessage = $"Value cannot be null.\r\nParameter name: {nameof(obj)}";

            // Act
            Action act = () => Prevent.NullObject(obj, nameof(obj));

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(obj));
        }

        [TestMethod]
        public void NullObject_IfParameterIsNotNull_ShouldNotThrow()
        {
            // Arrange
            var obj = new object();

            // Act
            Action act = () => Prevent.NullObject(obj, nameof(obj));

            // Assert
            act.Should().NotThrow();
        }

        #endregion

        #region Strings::EmptyOrWhiteSpaceString

        [DataRow("")]
        [DataRow("  ")]
        [TestMethod]
        public void EmptyOrWhiteSpaceString_WhenEmptyNullOrWhitespace_ShouldThrow(string param)
        {
            // Arrange
            var expectedMessage = $"Value cannot be empty or white space.\r\nParameter name: {nameof(param)}";

            // Act
            Action act = () => Prevent.EmptyOrWhiteSpaceString(param, nameof(param));

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(param));
        }

        [TestMethod]
        public void EmptyOrWhiteSpaceString_WhenNull_ShouldNotThrow()
        {
            // Arrange
            string param = null;

            // Act
            Action act = () => Prevent.EmptyOrWhiteSpaceString(param, nameof(param));

            // Assert
            act.Should().NotThrow();
        }

        #endregion

        #region Strings::NullOrEmptyString

        [DataRow("")]
        [DataRow(null)]
        [TestMethod]
        public void NullOrEmptyString_WhenNullOrEmpty_ShouldThrow(string param)
        {
            // Arrange
            var expectedMessage = $"Value cannot be null or empty.\r\nParameter name: {nameof(param)}";

            // Act
            Action act = () => Prevent.NullOrEmptyString(param, nameof(param));

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(param));
        }

        [TestMethod]
        public void NullOrEmptyString_WhenWhitespace_ShouldNotThrow()
        {
            // Arrange
            var param = "  ";

            // Act
            Action act = () => Prevent.NullOrEmptyString(param, nameof(param));

            // Assert
            act.Should().NotThrow();
        }

        #endregion

        #region Strings::NullOrWhiteSpaceString

        [DataRow("")]
        [DataRow(null)]
        [DataRow("   ")]
        [TestMethod]
        public void NullOrWhiteSpaceString_WhenEmptyNullOrWhiteSpace_ShouldThrow(string param)
        {
            // Arrange
            var expectedMessage = $"Value cannot be null, empty, or white space.\r\nParameter name: {nameof(param)}";

            // Act
            Action act = () => Prevent.NullOrWhiteSpaceString(param, nameof(param));

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(param));
        }

        [TestMethod]
        public void NullOrWhiteSpaceString_WhenNotEmptyNullOrWhiteSpace_ShouldNotThrow()
        {
            // Arrange
            var param = "Some valid string";

            // Act
            Action act = () => Prevent.NullOrWhiteSpaceString(param, nameof(param));

            // Assert
            act.Should().NotThrow();
        }


        #endregion

        #region GreaterThan & GreaterThanOrEqualTo::ValueGreaterThan

        [TestMethod]
        public void ValueGreaterThan_WhenValueIsNotGreater_ShouldNotThrow()
        {
            // Arrange
            const int Parameter = 2;
            const int GreaterThan = 5;

            // Act
            Action act = () => Prevent.ValueGreaterThan(Parameter, GreaterThan, nameof(Parameter));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void ValueGreaterThan_WhenValueIsEqual_ShouldNotThrow()
        {
            // Arrange
            const int Parameter = 5;
            const int GreaterThan = 5;

            // Act
            Action act = () => Prevent.ValueGreaterThan(Parameter, GreaterThan, nameof(Parameter));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void ValueGreaterThan_WhenValueIsGreater_ShouldThrow()
        {
            // Arrange
            const int Parameter = 5;
            const int GreaterThan = 2;
            var expectedMessage = $"Value must be less than or equal to 2.\r\nParameter name: {nameof(Parameter)}";

            // Act
            Action act = () => Prevent.ValueGreaterThan(Parameter, GreaterThan, nameof(Parameter));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(Parameter));
        }

        #endregion

        #region GreaterThan & GreaterThanOrEqualTo::ValueGreaterThanOrEqualTo

        [TestMethod]
        public void ValueGreaterThanOrEqualTo_WhenValueIsNotGreater_ShouldNotThrow()
        {
            // Arrange
            const int Parameter = 2;
            const int GreaterThan = 5;

            // Act
            Action act = () => Prevent.ValueGreaterThanOrEqualTo(Parameter, GreaterThan, nameof(Parameter));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void ValueGreaterThanOrEqualTo_WhenValueIsEqual_ShouldThrow()
        {
            // Arrange
            const int Parameter = 5;
            const int GreaterThan = 5;
            var expectedMessage = $"Value must be less than 5.\r\nParameter name: {nameof(Parameter)}";

            // Act
            Action act = () => Prevent.ValueGreaterThanOrEqualTo(Parameter, GreaterThan, nameof(Parameter));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(Parameter));
        }

        [TestMethod]
        public void ValueGreaterThanOrEqualTo_WhenValueIsGreater_ShouldNotThrow()
        {
            // Arrange
            const int Parameter = 5;
            const int GreaterThan = 2;
            var expectedMessage = $"Value must be less than 2.\r\nParameter name: {nameof(Parameter)}";

            // Act
            Action act = () => Prevent.ValueGreaterThanOrEqualTo(Parameter, GreaterThan, nameof(Parameter));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(Parameter));
        }


        #endregion

        #region LessThan & LessThanOrEqualTo::ValueLessThan

        [TestMethod]
        public void ValueLessThan_WhenValueIsLess_ShouldThrow()
        {
            // Arrange
            const int Parameter = 2;
            const int LessThan = 5;
            var expectedMessage = $"Value must be greater than or equal to 5.\r\nParameter name: {nameof(Parameter)}";

            // Act
            Action act = () => Prevent.ValueLessThan(Parameter, LessThan, nameof(Parameter));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(Parameter));
        }

        [TestMethod]
        public void ValueLessThan_WhenValueIsEqual_ShouldNotThrow()
        {
            // Arrange
            const int Parameter = 5;
            const int LessThan = 5;

            // Act
            Action act = () => Prevent.ValueLessThan(Parameter, LessThan, nameof(Parameter));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void ValueLessThan_WhenValueIsNotLess_ShouldNotThrow()
        {
            // Arrange
            const int Parameter = 5;
            const int LessThan = 2;

            // Act
            Action act = () => Prevent.ValueLessThan(Parameter, LessThan, nameof(Parameter));

            // Assert
            act.Should().NotThrow();
        }

        #endregion

        #region LessThan & LessThanOrEqualTo::ValueLessThanOrEqualTo

        [TestMethod]
        public void ValueLessThanOrEqualTo_WhenValueIsLess_ShouldThrow()
        {
            // Arrange
            const int Parameter = 2;
            const int LessThan = 5;
            var expectedMessage = $"Value must be greater than 5.\r\nParameter name: {nameof(Parameter)}";

            // Act
            Action act = () => Prevent.ValueLessThanOrEqualTo(Parameter, LessThan, nameof(Parameter));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(Parameter));
        }

        [TestMethod]
        public void ValueLessThanOrEqualTo_WhenValueIsEqual_ShouldThrow()
        {
            // Arrange
            const int Parameter = 5;
            const int LessThan = 5;
            var expectedMessage = $"Value must be greater than 5.\r\nParameter name: {nameof(Parameter)}";

            // Act
            Action act = () => Prevent.ValueLessThanOrEqualTo(Parameter, LessThan, nameof(Parameter));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(Parameter));
        }

        [TestMethod]
        public void ValueLessThanOrEqualTo_WhenValueIsNotLess_ShouldNotThrow()
        {
            // Arrange
            const int Parameter = 5;
            const int LessThan = 2;

            // Act
            Action act = () => Prevent.ValueLessThan(Parameter, LessThan, nameof(Parameter));

            // Assert
            act.Should().NotThrow();
        }

        #endregion

        #region Ranges::ValueOutsideOfRange

        [TestMethod]
        public void ValueOutsideOfRange_WhenValueInsideRange_ShouldNotThrow()
        {
            // Assert
            const int Parameter = 5;
            const int BeginningRange = 1;
            const int EndingRange = 10;

            // Act
            Action act = () => Prevent.ValueOutsideOfRange(Parameter, BeginningRange, EndingRange, nameof(Parameter));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void ValueOutsideOfRange_WhenValueMatchesBeginningRange_ShouldNotThrow()
        {
            // Assert
            const int Parameter = 1;
            const int BeginningRange = 1;
            const int EndingRange = 10;

            // Act
            Action act = () => Prevent.ValueOutsideOfRange(Parameter, BeginningRange, EndingRange, nameof(Parameter));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void ValueOutsideOfRange_WhenValueMatchesEndingRange_ShouldNotThrow()
        {
            // Assert
            const int Parameter = 10;
            const int BeginningRange = 1;
            const int EndingRange = 10;

            // Act
            Action act = () => Prevent.ValueOutsideOfRange(Parameter, BeginningRange, EndingRange, nameof(Parameter));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void ValueOutsideOfRange_WhenValueBelowBeginningRange_ShouldThrow()
        {
            // Assert
            const int Parameter = 0;
            const int BeginningRange = 1;
            const int EndingRange = 10;
            var expectedMessage = $"Value outside of specified range; 1 - 10.\r\nParameter name: {nameof(Parameter)}";

            // Act
            Action act = () => Prevent.ValueOutsideOfRange(Parameter, BeginningRange, EndingRange, nameof(Parameter));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(Parameter));
        }

        [TestMethod]
        public void ValueOutsideOfRange_WhenValueAboveEndingRange_ShouldThrow()
        {
            // Assert
            const int Parameter = 11;
            const int BeginningRange = 1;
            const int EndingRange = 10;
            var expectedMessage = $"Value outside of specified range; 1 - 10.\r\nParameter name: {nameof(Parameter)}";

            // Act
            Action act = () => Prevent.ValueOutsideOfRange(Parameter, BeginningRange, EndingRange, nameof(Parameter));

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(Parameter));
        }

        #endregion

        #region Guids::EmptyGuid

        [TestMethod]
        public void EmptyGuid_WhenNotEmpty_ShouldNotThrow()
        {
            // Assert
            var parameter = Guid.NewGuid();

            // Act
            Action act = () => Prevent.EmptyGuid(parameter, nameof(parameter));

            // Assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void EmptyGuid_WhenEmpty_ShouldThrow()
        {
            // Assert
            var parameter = Guid.Empty;
            var expectedMessage = $"Value cannot be empty.\r\nParameter name: {nameof(parameter)}";

            // Act
            Action act = () => Prevent.EmptyGuid(parameter, nameof(parameter));

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage)
                .And.ParamName.Should().Be(nameof(parameter));
        }

        #endregion
    }
}
