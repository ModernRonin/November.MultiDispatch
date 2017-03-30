using FluentAssertions;
using NUnit.Framework;

namespace November.MultiDispatch.Tests
{
    [TestFixture]
    public class DoubleDispatcherTests
    {
        [Test]
        public void Dispatch_Calls_FallbackHandler_If_No_Registered_Handler()
        {
            var underTest = new DoubleDispatcher<object>();
            var wasCalled = false;
            underTest.FallbackHandler = (l, r) => wasCalled = true;

            underTest.Dispatch(1, 3);

            wasCalled.Should().BeTrue();
        }
        [Test]
        public void Dispatch_Considers_Predicates_If_Registered_With_Left_Then_Right()
        {
            var underTest = new DoubleDispatcher<object>();
            var called = 0;
            underTest.OnLeft<int>(i => i > 0).OnRight<string>(s => s.Length > 3).Do((l, r) => ++called);

            underTest.Dispatch(0, string.Empty);
            underTest.Dispatch(1, string.Empty);
            underTest.Dispatch(0, "alpha");
            underTest.Dispatch(1, "alpha");

            called.Should().Be(1);
        }
        [Test]
        public void Dispatch_Considers_Predicates_If_Registered_With_Right_Then_Left()
        {
            var underTest = new DoubleDispatcher<object>();
            var called = 0;
            underTest.OnRight<int>(i => i > 0).OnLeft<string>(s => s.Length > 3).Do((l, r) => ++called);

            underTest.Dispatch(string.Empty, 0);
            underTest.Dispatch(string.Empty, 1);
            underTest.Dispatch("alpha", 0);
            underTest.Dispatch("alpha", 1);

            called.Should().Be(1);
        }
        [Test]
        public void Dispatch_Picks_The_Right_Handler_If_Handlers_Were_Registered_With_On()
        {
            var underTest = new DoubleDispatcher<object>();
            var called = 0;
            underTest.On<int, string>((c, v) => called += 2);
            underTest.On<int, int>((a, m) => called += 4);

            underTest.Dispatch(3, 4);

            called.Should().Be(4);
        }
        [Test]
        public void Dispatch_Picks_The_Right_Handler_If_Handlers_Were_Registered_With_On_And_Left_And_Right()
        {
            var underTest = new DoubleDispatcher<object>();
            var called = 0;
            underTest.FallbackHandler = (l, r) => called += 2;
            underTest.On<string, int>((l, r) => called += 4);
            underTest.OnLeft<int>().OnRight<int>().Do((l, r) => called += 8);
            underTest.OnRight<bool>().OnLeft<int>().Do((l, r) => called += 16);

            underTest.Dispatch(15, true);

            called.Should().Be(16);
        }
        [Test]
        public void Dispatch_Picks_The_Right_Handler_With_Left_Being_Set_To_RootType()
        {
            var underTest = new DoubleDispatcher<object>();
        }
    }
}