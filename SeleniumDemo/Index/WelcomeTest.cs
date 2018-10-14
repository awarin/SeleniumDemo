using Xunit;

namespace SeleniumDemo.WelcomeTest
{
    public class WelcomeTest : IndexTest, IClassFixture<DockerDispatcherFixture>
    {
        public WelcomeTest(DockerDispatcherFixture dockerDispatcher) : base(dockerDispatcher)
        {
        }

        [Fact]
        public void ShouldWelcome()
        {
            var welcomeText = _indexPage.GetWelcomeMessage();
            Assert.Contains("Welcome", welcomeText);
        }
    }
}
