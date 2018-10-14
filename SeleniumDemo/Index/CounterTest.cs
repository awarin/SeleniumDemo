using Xunit;

namespace SeleniumDemo.Index
{
    public class CounterTest : IndexTest, IClassFixture<DockerDispatcherFixture>
    {
        public CounterTest (DockerDispatcherFixture dockerDispatcher) : base(dockerDispatcher)
        {
        }

        [Fact]
        public void ShouldIncreaseCounter()
        {
            var initValue = _indexPage.GetCounterValue();
            Assert.Equal(0, initValue);

            _indexPage.IncreaseCounterValue();

            Assert.Equal(1, _indexPage.GetCounterValue());
        }

        [Fact]
        public void ShouldDecreaseCounter()
        {
            var initValue = _indexPage.GetCounterValue();
            Assert.Equal(0, initValue);

            _indexPage.DecreaseCounterValue();

            Assert.Equal(-1, _indexPage.GetCounterValue());
        }
    }
}
