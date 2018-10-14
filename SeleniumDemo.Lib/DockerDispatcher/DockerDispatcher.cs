using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace SeleniumDemo.Lib.DockerDispatcher
{
    public class DockerDispatcher
    {
        private DockerClient GetDockerClient() => new DockerClientConfiguration(new Uri("unix:/var/run/docker.sock")).CreateClient();
        private static int _port = 9515;
        private static readonly Object _lock = new Object();

        public async Task<IList<ContainerListResponse>> GetWebdriverContainerAsync ()
        {
            using (var client = GetDockerClient()) {
                IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
                new ContainersListParameters()
                {
                    Limit = 10,
                });
                return containers.Where(container => container.State == "running").ToList();
            }
        }

        public async Task<Tuple<string, string>> SpawnContainer ()
        {
            using (var client = GetDockerClient())
            {
                var port = GetAvailablePort().ToString();
                var config = new CreateContainerParameters {
                    Image = "dockerchromedriver",
                    HostConfig = new HostConfig {
                        PortBindings = new Dictionary<string, IList<PortBinding>> {
                            {"9515", new List<PortBinding> { new PortBinding { HostPort = port } }}
                        }
                    },
                    ExposedPorts = new Dictionary<string, EmptyStruct> {
                        { "9515", new EmptyStruct() }
                    }
                };
                var createResponse = await client.Containers.CreateContainerAsync(config);

                await client.Containers.StartContainerAsync(createResponse.ID, new ContainerStartParameters());

                return new Tuple<string,string>(createResponse.ID, port);
            }
        }

        public async Task StopContainer (string containerId)
        {
            using (var client = GetDockerClient())
            {
                await client.Containers.KillContainerAsync(containerId, new ContainerKillParameters());
            }
        }

        private int GetAvailablePort()
        {
            lock (_lock)
            {
                var currentAvailablePort = _port;
                _port++;
                return currentAvailablePort;
            }
        }
    }
}
