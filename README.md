# QR Animal

[![Build Status](https://dev.azure.com/masnymikhail/QR%20Animal/_apis/build/status/MikhailMasny.QR-Animal?branchName=master)](https://dev.azure.com/masnymikhail/QR%20Animal/_build/latest?definitionId=1&branchName=master)

The main idea of the web application is to develop a system for the registration of animals and quickly obtain data about them thanks to the QR code, thereby reducing the human factor in document management, allowing the system to be as autonomous as possible.

## Getting Started

When you go to the web application site, the user sees the main page and the opportunity to register or log in. Registration is based on the principle: fill in the data and confirm the registration. After that, access to the web application as a user appears.

### Installing

To run the entire infrastructure, you should run the command from the project folder:

```
docker-compose up -d
```

### Notes
1. [Docker](https://www.docker.com/products/docker-desktop) should be installed on the computer and in the files ([appsetting.json](https://github.com/MikhailMasny/QR-Animal/blob/master/src/Web/appsettings.json) and [appsetting.json](https://github.com/MikhailMasny/QR-Animal/blob/master/src/Worker/appsettings.json)) of the projects ***.Web** and ***.Worker** the true flag should be set opposite the value "IsDockerSupport".
2. If the table to interact with the Serilog has not been added, restart the container / application.

### User features
1. Register animals;
2. Perform various operations with animal data;
3. View a list of previously registered animals;
4. Share animal information by QR code;
5. Use chat to communicate;

### Administrator features (only through [SSMS](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15))
1. Edit user's animals and QR codes;
2. Edit user profiles;
3. Respond to users and resolve conflict situations;

## Application usage scenario
The user registers, confirms his account through a letter in the mail and gets access to the system. Then it registers the animal, enters data about it, after which it can share a QR code by which another person can get all the information about a particular animal by clicking on the link if it is in the public domain.

## Built with
- [Clean architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures);
- [Docker](https://www.docker.com/);
- [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/);
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/);
- [CQRS pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs);
- [MediatR](https://github.com/jbogard/MediatR);
- [Fluent Validation](https://fluentvalidation.net/);
- [Serilog](https://serilog.net/);
- [MimeKit](http://www.mimekit.net/);
- [Automapper](https://automapper.org/);
- [Background tasks](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio);
- [Health service](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1);
- [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr);
- [xUnit](https://xunit.net/);
- [WebApplicationFactory](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1);
- [Moq](https://github.com/Moq/moq4/wiki/Quickstart);
- [Shouldly](https://github.com/shouldly/shouldly);
- [Fluent Assertions](https://fluentassertions.com/);
- [Feature flags](https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core);
- Web stack (HTML, CSS, JS);

## Author
[Mikhail M.](https://mikhailmasny.github.io/) - Software Engineer;

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/MikhailMasny/QR-Animal/blob/master/LICENSE) file for details.
