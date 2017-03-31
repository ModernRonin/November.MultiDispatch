using FluentAssertions;
using NUnit.Framework;

namespace November.MultiDispatch.Tests
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        interface ISomeInterface {}

        interface IDerivedInterface : ISomeInterface, IOtherInterface {}

        interface IOtherInterface {}

        class SomeImplementation : ISomeInterface {}

        class AnotherImplementation : IDerivedInterface {}

        interface IThirdInterface {}

        class DerivedImplementation : AnotherImplementation, IThirdInterface {}

        [Test]
        public void GetAssignmentTargetTypes_Of_Class()
        {
            typeof(SomeImplementation).GetAssignmentTargetTypes()
                .ShouldAllBeEquivalentTo(new[] {typeof(SomeImplementation), typeof(ISomeInterface), typeof(object)});
        }
        [Test]
        public void GetAssignmentTargetTypes_Of_Derived_Class_With_Complex_Interface_Hierarchy()
        {
            var result = typeof(DerivedImplementation).GetAssignmentTargetTypes();
            result.ShouldAllBeEquivalentTo(new[]
            {
                typeof(DerivedImplementation), typeof(AnotherImplementation), typeof(object), typeof(IThirdInterface),
                typeof(IDerivedInterface), typeof(IOtherInterface), typeof(ISomeInterface)
            });
        }
        [Test]
        public void GetAssignmentTargetTypes_Of_Derived_Interface_Returns_Interface_And_Ancestor()
        {
            typeof(IDerivedInterface).GetAssignmentTargetTypes()
                .ShouldAllBeEquivalentTo(new[]
                    {typeof(IDerivedInterface), typeof(ISomeInterface), typeof(IOtherInterface)});
        }
        [Test]
        public void GetAssignmentTargetTypes_Of_Interface_Returns_Interface()
        {
            typeof(ISomeInterface).GetAssignmentTargetTypes()
                .Should()
                .ContainSingle()
                .Which.Should()
                .Be<ISomeInterface>();
        }
        [Test]
        public void GetAssignmentTargetTypes_Of_Object_Returns_Object()
        {
            typeof(object).GetAssignmentTargetTypes().Should().ContainSingle().Which.Should().Be<object>();
        }
    }
}