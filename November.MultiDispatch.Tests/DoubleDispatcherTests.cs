using FluentAssertions;
using NUnit.Framework;

namespace November.MultiDispatch.Tests
{
    [TestFixture]
    public class DoubleDispatcherTests
    {
        [Test]
        public void If_No_Fitting_Handler_Defined_Calls_FallbackHandler()
        {
            var wasFallbackCalled = false;
            var dispatcher = new DoubleDispatcher {FallbackHandler = (l, r) => wasFallbackCalled = true};
            dispatcher.OnLeft<int>().OnRight<string>().Do((c, v) => { Assert.Fail(); });
            dispatcher.OnLeft<int>().OnRight<int>().Do((a, m) => { Assert.Fail(); });
            dispatcher.Dispatch(true, "alpha");

            wasFallbackCalled.Should().BeTrue();
        }
        [Test]
        public void Picks_Right_Handler_If_Defined()
        {
            var dispatcher = new DoubleDispatcher {FallbackHandler = (l, r) => Assert.Fail()};

            dispatcher.OnLeft<int>().OnRight<string>().Do((c, v) => { Assert.Fail(); });
            var wasRightComboPicked = false;
            dispatcher.OnLeft<int>().OnRight<int>().Do((a, m) => { wasRightComboPicked = true; });

            dispatcher.Dispatch(3, 4);

            wasRightComboPicked.Should().BeTrue();
        }
        [Test]
        public void Usage_With_Unified_On()
        {
            var wasRightComboPicked = false;

            var dispatcher = new DoubleDispatcher();
            dispatcher.On<int, string>((c, v) => { Assert.Fail(); });
            dispatcher.On<int, int>((a, m) => { wasRightComboPicked = true; });
            dispatcher.Dispatch(3, 4);

            wasRightComboPicked.Should().BeTrue();
        }
    }
}