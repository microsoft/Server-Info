---
ArtifactType: website
Language: C#
Platform: Linux Container
Tags: Container, Azure, WebSite, Inventory, ServerInfo
---

![GitHub](https://img.shields.io/github/license/microsoft/server-info) 
![GitHub repo size](https://img.shields.io/github/repo-size/microsoft/server-info) 
[![Azure](https://badgen.net/badge/icon/azure?icon=azure&label)](https://azure.microsoft.com)

![GitHub last commit](https://img.shields.io/github/last-commit/microsoft/server-info)
![GitHub top language](https://img.shields.io/github/languages/top/microsoft/server-info)

# Server Info

A lightweight debugging and diagnostic tool that exposes server environment information and client request details for .NET applications. This tool helps developers and IT administrators understand their application's runtime environment and incoming HTTP requests.

## 📋 Overview

Server Info is a web-based diagnostic tool built with ASP.NET Core that provides:

- **Server Environment Information**: View environment variables, system details, and runtime configuration
- **Client Request Analysis**: Inspect HTTP headers, client IP addresses, and request metadata
- **Container-Friendly**: Optimized for containerized environments and cloud deployments
- **Real-time Data**: Live information about the current server state and incoming requests

## 🎯 Use Cases

**For Developers:**
- Debug environment-specific issues in different deployment contexts
- Understand what environment variables and configuration are available
- Analyze HTTP request flow and headers during development

**For IT Administrators:**
- Quickly inspect server environment details in container environments
- Troubleshoot network configuration and proxy settings
- Verify environment variable propagation in deployments

**For DevOps Teams:**
- Validate container deployment configurations
- Debug load balancer and reverse proxy configurations
- Monitor request routing and header manipulation

> [!IMPORTANT]
> **Security Notice**: This is a debugging and diagnostic tool. It exposes sensitive environment information and should **never** be deployed in production environments with public access. Use only in development, testing, or secure internal environments.

## 🚀 Features

- **Environment Information Display**
  - System environment variables
  - Application configuration
  - Runtime information (.NET version, OS details)
  - Performance counters and system metrics

- **Client Request Analysis**
  - HTTP headers inspection
  - Client IP address detection (supports X-Forwarded-For)
  - Request metadata and routing information
  - Browser and user agent details

- **Container-Ready**
  - Docker support with included Dockerfile
  - Lightweight and optimized for containerized deployments
  - Cloud platform compatibility (Azure, AWS, etc.)

## 📋 Prerequisites

To run Server Info locally, ensure you have the following installed:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/downloads/) or later
- [Git](https://git-scm.com/downloads)
- **Optional for development:**
  - [Visual Studio Code](https://code.visualstudio.com/Download) with [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
  - [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) (17.8 or later)
- **Optional for deployment:**
  - [Docker](https://www.docker.com/get-started) for containerized deployment
  - [Azure Developer CLI (azd)](https://aka.ms/install-azd) for Azure deployment

## 🛠️ Getting Started

### Local Development

1. **Clone the repository:**
   ```bash
   git clone https://github.com/microsoft/Server-Info.git
   cd Server-Info
   ```

2. **Navigate to the project directory:**
   ```bash
   cd Server-Info/Server-Info
   ```

3. **Run the application:**
   ```bash
   dotnet run
   ```

4. **Access the application:**
   - Open your browser and navigate to `https://localhost:7028` (or the URL displayed in the terminal)
   - The application will display server and environment information

### Docker Deployment

1. **Build the Docker image:**
   ```bash
   docker build -t server-info .
   ```

2. **Run the container:**
   ```bash
   docker run -p 8080:8080 server-info
   ```

3. **Access the application:**
   - Navigate to `http://localhost:8080`


## 🖥️ User Interface

### Server Information Dashboard
The main dashboard provides a comprehensive view of your server environment in an organized, easy-to-read format.

![Server Information Dashboard](images/server-info-1.png)

**Features displayed:**
- Environment variables and their values
- System information (OS, .NET version, etc.)
- Application configuration settings
- Performance metrics and runtime details

### Client Information Analysis
The Client tab reveals detailed information about incoming HTTP requests and client characteristics.

![Client Information](images/server-info-2.png)

**Information includes:**
- Browser and user agent details
- Client IP address (with proxy detection)
- Referrer and request metadata
- Session and authentication information

### HTTP Headers Inspector
Scroll down on the Client page to view comprehensive HTTP header information from the current request.

![HTTP Headers](images/server-info-3.png)

**Header details:**
- All HTTP request headers
- Custom headers and their values
- Forwarded headers (X-Forwarded-For, X-Real-IP, etc.)
- Security and caching headers

## ⚙️ Configuration

### Environment Variables
The application automatically detects and displays environment variables. You can control visibility by setting:

## 🔒 Security Considerations

> [!WARNING]
> **Important Security Guidelines**

- **Never deploy in production** with public access
- **Restrict network access** to authorized users only
- **Use in internal/development environments** only
- **Monitor access logs** if deployed in shared environments
- **Consider authentication** for sensitive environments

### Recommended Deployment Patterns

✅ **Safe Usage:**
- Local development environments
- Internal corporate networks with restricted access
- Development and staging environments
- Containerized environments with network isolation
- Behind VPN or corporate firewall

❌ **Avoid:**
- Public internet deployment
- Production environments
- Unsecured network access
- Environments with sensitive customer data

## 🐛 Troubleshooting

### Common Issues

**Application won't start:**
- Verify .NET 9.0 is installed: `dotnet --version`
- Check port availability (default: 7028 for HTTPS, 5000 for HTTP)
- Review application logs for startup errors

**Docker container issues:**
- Ensure Docker is running: `docker version`
- Check port mapping: `-p 8080:8080`
- Verify image build: `docker images`

**Missing environment variables:**
- Variables may be filtered for security
- Check container environment variable injection
- Verify environment-specific configuration files

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft 
trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.