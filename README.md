# SeleniumDemo

## Motivation
This C# solution aims to test a simple webpage with Selenium and ChromeDriver.
The problem that can occur when running tests in parallel is state management.

Cookies or local storage can make the tests flaky.

In order to avoid that, Chromedriver is run in an isolated Docker container.
Therefore the tests will run against a clean environment.

In this example, we test a simple counter application exposed on the host machine. See [CounterExample](https://github.com/awarin/counterExample).

## Requirements
* Docker Engine
* A docker image containing Chrome Web Browser and Chromedriver named `dockerchromedriver`
* Dotnet Core sdk
* CounterExample running

## Launch the tests
Like every dotnet Core projects, tests can be run in Visual Studio or with `dotnet test`

## Dockerfile

```Dockerfile
# Use an official Ubuntu runtime as a parent image
FROM ubuntu:16.04

RUN apt-get update
RUN apt-get install -yf wget unzip

RUN wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
RUN dpkg -i google-chrome-stable_current_amd64.deb; exit 0

RUN apt-get install -yf
RUN rm google-chrome-stable_current_amd64.deb

RUN wget https://chromedriver.storage.googleapis.com/2.42/chromedriver_linux64.zip
RUN unzip chromedriver_linux64.zip

EXPOSE 9515

CMD ["./chromedriver", "--verbose=info", "--whitelisted-ips=172.17.0.1"]
```