using System;
using System.Threading.Tasks;
using SeleniumDemo.Lib;
using SeleniumDemo.Lib.DockerDispatcher;
using SeleniumDemo.Lib.Pages;
using Xunit;

namespace SeleniumDemo
{
    public abstract class IndexTest : IAsyncLifetime
    {
        protected IndexPage _indexPage;
        private readonly DockerDispatcher _dockerDispatcher;
        private string _dockerContainerId;

        protected IndexTest (DockerDispatcherFixture dockerDispatcher)
        {
            _dockerDispatcher = dockerDispatcher.Dispatcher;
        }

        public async Task DisposeAsync()
        {
            _indexPage.Dispose();
            await _dockerDispatcher.StopContainer(_dockerContainerId);
        }

        public async Task InitializeAsync()
        {
            var container = await _dockerDispatcher.SpawnContainer();
            var website = new UriBuilder
            {
                Scheme = "http",
                Host = "host.docker.internal",
                Port = 5000
            };

            var dockerInstanceUri = new UriBuilder
            {
                Scheme = "http",
                Host = "0.0.0.0",
                Port = int.Parse(container.Item2)
            };

            _dockerContainerId = container.Item1; 

            var chromeDriver = new ChromeBootstrapper()
                .Bootstrap(website.ToString(), dockerInstanceUri.Uri);
            _indexPage = new IndexPage(chromeDriver);
        }
    }

    public class DockerDispatcherFixture
    {
        public DockerDispatcher Dispatcher { get; }

        public DockerDispatcherFixture () {
            Dispatcher = new DockerDispatcher();
        }
    }
}
