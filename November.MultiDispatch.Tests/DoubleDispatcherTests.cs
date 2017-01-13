using FluentAssertions;
using NUnit.Framework;

namespace November.MultiDispatch.Tests
{
    [TestFixture]
    public class DoubleDispatcherTests
    {
        [Test] // TODO: remove
        public void If_No_Fitting_Handler_Defined_Calls_FallbackHandler()
        {
            var wasFallbackCalled = false;
            var underTest = new DoubleDispatcher { FallbackHandler = (l, r) => wasFallbackCalled = true};
            underTest.OnLeft<int>().OnRight<string>().Do((c, v) => { Assert.Fail(); });
            underTest.OnLeft<int>().OnRight<int>().Do((a, m) => { Assert.Fail(); });
            underTest.Dispatch(true, "alpha");

            wasFallbackCalled.Should().BeTrue();
        }
        [Test] // TODO: remove
        public void Picks_Right_Handler_If_Defined()
        {
            var underTest = new DoubleDispatcher { FallbackHandler = (l, r) => Assert.Fail()};
            underTest.OnLeft<int>().OnRight<string>().Do((c, v) => { Assert.Fail(); });
            var wasRightComboPicked = false;
            underTest.OnLeft<int>().OnRight<int>().Do((a, m) => { wasRightComboPicked = true; });

            underTest.Dispatch(3, 4);

            wasRightComboPicked.Should().BeTrue();
        }
        [Test]
        public void Dispatch_Picks_The_Right_Handler_If_Handlers_Were_Registered_With_On()
        {
            var underTest = new DoubleDispatcher();
            var called = 0;
            underTest.On<int, string>((c, v) => called+= 2 );
            underTest.On<int, int>((a, m) => called+= 4);

            underTest.Dispatch(3, 4);

            called.Should().Be(4);
        }
        [Test]
        public void Dispatch_Picks_The_Right_Handler_If_Handlers_Were_Registered_With_On_And_Left_And_Right()
        {
            var underTest= new DoubleDispatcher();
            var called = 0;
            underTest.FallbackHandler = (l, r) => called += 2;
            underTest.On<string, int>((l, r)=> called+=4);
            underTest.OnLeft<int>().OnRight<int>().Do((l, r)=>called+=8);
            underTest.OnRight<bool>().OnLeft<int>().Do((l, r) => called += 16);

            underTest.Dispatch(15, true);

            called.Should().Be(16);
        }
        [Test]
        public void Dispatch_Calls_FallbackHandler_If_No_Registered_Handler()
        {
            var underTest= new DoubleDispatcher();
            var wasCalled = false;
            underTest.FallbackHandler = (l, r) => wasCalled = true;

            underTest.Dispatch(1, 3);

            wasCalled.Should().BeTrue();
        }
    }
}