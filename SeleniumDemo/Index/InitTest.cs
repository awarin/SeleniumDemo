using Xunit;

namespace SeleniumDemo
{
    public class InitTest : IndexTest, IClassFixture<DockerDispatcherFixture>
    {
        public InitTest(DockerDispatcherFixture dockerDispatcher) : base(dockerDispatcher)
        {
        }

        [Fact]
        public void ShouldHaveATitle()
        {
            var title = _indexPage.GetTabTitle();
            Assert.Equal("A simple counter", title);
        }

        [Fact]
        public void ShouldHaveACounterInitiated()
        {
            var initValue = _indexPage.GetCounterValue();
            Assert.Equal(0, initValue);
        }
    }
}
