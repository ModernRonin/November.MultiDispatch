using System;
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

        interface IThirdInterface { }
        class DerivedImplementation : AnotherImplementation, IThirdInterface {}
        class Deepest : DerivedImplementation { }

        [Test]
        public void GetTypeDistanceFromAncestor_To_Unrelated_Type_Returns_IntMax()
        {
            typeof(SomeImplementation).GetTypeDistanceFromAncestor(typeof(IDerivedInterface)).Should().Be(int.MaxValue);
        }
        [Test]
        public void GetTypeDistanceFromAncestor_Of_Directly_Derived_Interface_Returns_1()
        {
            typeof(IDerivedInterface).GetTypeDistanceFromAncestor(typeof(ISomeInterface)).Should().Be(1);
        }
        [Test]
        public void GetTypeDistanceFromAncestor_To_Base_Class_Returns_1()
        {
            typeof(DerivedImplementation).GetTypeDistanceFromAncestor(typeof(AnotherImplementation)).Should().Be(1);
        }
        [Test]
        public void GetTypeDistanceFromAncestor_To_Implemented_Interface_Returns_1()
        {
            typeof(DerivedImplementation).GetTypeDistanceFromAncestor(typeof(IThirdInterface)).Should().Be(1);
        }
        [Test]
        public void GetTypeDistanceFromAncestor_To_Grandfather_Class_Returns_2()
        {
            typeof(Deepest).GetTypeDistanceFromAncestor(typeof(AnotherImplementation)).Should().Be(2);
        }
        [Test]
        public void GetTypeDistanceFromAncestor_In_Complex_Hierarchy()
        {
            var deepest = typeof(Deepest);
            deepest.GetTypeDistanceFromAncestor(typeof(ISomeInterface)).Should().Be(4);
        }
        [Test]
        public void GetTypeDistanceFromAncestor_Object_And_Object_Returns_0()
        {
            typeof(object).GetTypeDistanceFromAncestor(typeof(object)).Should().Be(0);
        }
        [Test]
        public void GetTypeDistanceFromAncestor_String_And_Object_Returns_1()
        {
            typeof(string).GetTypeDistanceFromAncestor(typeof(object)).Should().Be(1);
        }
        [Test]
        public void GetTypeDistanceFromAncestor_To_Child_Type_Returns_IntMax()
        {
            typeof(object).GetTypeDistanceFromAncestor(typeof(string)).Should().Be(int.MaxValue);
        }
        [Test]
        public void GetAssignmentTargetTypes_Of_Class()
        {
            typeof(SomeImplementation).GetAssignmentTargetTypes().ShouldAllBeEquivalentTo(new[]
            {
                typeof(SomeImplementation),
                typeof(ISomeInterface),
                typeof(object)
            });
        }
        [Test]
        public void GetAssignmentTargetTypes_Of_Derived_Class_With_Complex_Interface_Hierarchy
            ()
        {
            var result = typeof(DerivedImplementation).GetAssignmentTargetTypes();
            result.ShouldAllBeEquivalentTo(new[]
            {
                typeof(DerivedImplementation),
                typeof(AnotherImplementation),
                typeof(object),
                typeof(IThirdInterface),
                typeof(IDerivedInterface),
                typeof(IOtherInterface),
                typeof(ISomeInterface),
            });
        }
        [Test]
        public void GetAssignmentTargetTypes_Of_Derived_Interface_Returns_Interface_And_Ancestor()
        {
            typeof(IDerivedInterface).GetAssignmentTargetTypes().ShouldAllBeEquivalentTo(new[]
            {
                typeof(IDerivedInterface), typeof(ISomeInterface), typeof(IOtherInterface)
            });
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