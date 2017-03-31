using FluentAssertions;
using NUnit.Framework;

namespace November.MultiDispatch.Tests
{
    [TestFixture]
    public class TypesToContextMapTests
    {
        interface ISomeInterface {}

        class SomeImplementation : ISomeInterface {}

        class AnotherImplementation : ISomeInterface {}

        class DerivedImplementation : AnotherImplementation {}

        [Test]
        public void GetFor_Returns_Context_For_Exact_Type_Match()
        {
            var underTest = new TypesToContextMap();
            var context = new CallContext();
            underTest.Add(typeof(string), typeof(int), context);

            underTest.GetFor(typeof(string), typeof(int)).Should().BeSameAs(context);
        }
        [Test]
        public void GetFor_Returns_Context_For_Left_And_Right_Base_Type_Match()
        {
            var underTest = new TypesToContextMap();
            var context = new CallContext();
            underTest.Add(typeof(ISomeInterface), typeof(AnotherImplementation), context);

            underTest.GetFor(typeof(AnotherImplementation), typeof(DerivedImplementation)).Should().BeSameAs(context);
        }
        [Test]
        public void GetFor_Returns_Context_For_Left_Base_Type_Match()
        {
            var underTest = new TypesToContextMap();
            var context = new CallContext();
            underTest.Add(typeof(ISomeInterface), typeof(SomeImplementation), context);

            underTest.GetFor(typeof(DerivedImplementation), typeof(SomeImplementation)).Should().BeSameAs(context);
        }
        [Test]
        public void GetFor_Returns_Context_For_Right_Base_Type_Match()
        {
            var underTest = new TypesToContextMap();
            var context = new CallContext();
            underTest.Add(typeof(ISomeInterface), typeof(AnotherImplementation), context);

            underTest.GetFor(typeof(ISomeInterface), typeof(DerivedImplementation)).Should().BeSameAs(context);
        }
        [Test]
        public void GetFor_Returns_Context_For_First_Match()
        {
            var underTest = new TypesToContextMap();
            var a = new CallContext();
            var b = new CallContext();
            var c = new CallContext();
            underTest.Add(typeof(DerivedImplementation), typeof(DerivedImplementation), c);
            underTest.Add(typeof(AnotherImplementation), typeof(AnotherImplementation), b);
            underTest.Add(typeof(ISomeInterface), typeof(AnotherImplementation), a);

            underTest.GetFor(typeof(DerivedImplementation), typeof(DerivedImplementation)).Should().BeSameAs(c);
            underTest.GetFor(typeof(DerivedImplementation), typeof(AnotherImplementation)).Should().BeSameAs(b);
            underTest.GetFor(typeof(AnotherImplementation), typeof(DerivedImplementation)).Should().BeSameAs(b);
            underTest.GetFor(typeof(AnotherImplementation), typeof(AnotherImplementation)).Should().BeSameAs(b);
            underTest.GetFor(typeof(SomeImplementation), typeof(DerivedImplementation)).Should().BeSameAs(a);
            underTest.GetFor(typeof(SomeImplementation), typeof(AnotherImplementation)).Should().BeSameAs(a);
            underTest.GetFor(typeof(ISomeInterface), typeof(DerivedImplementation)).Should().BeSameAs(a);
            underTest.GetFor(typeof(ISomeInterface), typeof(AnotherImplementation)).Should().BeSameAs(a);
        }
        [Test]
        public void GetFor_Returns_Null_If_No_Match_Found()
        {
            var underTest = new TypesToContextMap();
            var a = new CallContext();
            var b = new CallContext();
            var c = new CallContext();
            underTest.Add(typeof(ISomeInterface), typeof(AnotherImplementation), a);
            underTest.Add(typeof(AnotherImplementation), typeof(AnotherImplementation), b);
            underTest.Add(typeof(DerivedImplementation), typeof(DerivedImplementation), c);

            underTest.GetFor(typeof(SomeImplementation), typeof(SomeImplementation)).Should().BeNull();
        }
        [Test]
        public void GetFor_Returns_Null_If_Nothing_Was_Added_()
        {
            new TypesToContextMap().GetFor(typeof(string), typeof(int)).Should().BeNull();
        }
    }
}