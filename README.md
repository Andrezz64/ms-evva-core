# EvvaCore

<div align="center">
  <img src="docs/assets/logo.png" alt="EvvaCore Logo" width="200"/>
  
  [![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)
</div>

## 📋 About

EvvaCore is a robust project and workflow management platform built with .NET Core. The system offers a modular and scalable architecture with support for real-time communication through secure WebSockets.

## ✨ Features

- 🔐 Authentication and Authorization
- 📊 Project Management
- 🔄 Customizable Workflows
- 🐳 Docker Integration
- 🔒 End-to-End Encrypted WebSocket Communication
- 📱 RESTful API
- 🔍 Detailed Logging

## 🚀 Getting Started

### Prerequisites

- .NET 7.0 or higher
- SQL Server or PostgreSQL

### Installation

1. Clone the repository
```bash
git clone https://github.com/andrezz/evvacore.git
cd evvacore
```

2. Restore dependencies
```bash
dotnet restore
```

3. Configure the database
```bash
# Edit appsettings.json with your settings
```

5. Start the application
```bash
dotnet run
```

### Docker

A docker support will be implemented soon

## 🏗️ Architecture

The project follows a layered architecture:

```
ms-evva-core/
├── Controllers/     # API Controllers
├── Models/         # Data Models
├── Base/           # Reusable Base Classes
├── Security/       # Security Implementations
└── Services/       # Business Logic
```

### WebSocket

Real-time communication is implemented using WebSockets with end-to-end encryption:

```csharp
// WebSocket connection example
const ws = new WebSocket('ws://localhost:5000/api/ProjectWebSocket/ws?hash=your-hash');
```

## 🔒 Security

- TripleDES encryption for WebSocket messages
- Hash validation for connections
- JWT Authentication
- Security logging

## 🤝 Contributing

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the GNU AFFERO GENERAL PUBLIC LICENSE - see the [LICENSE](LICENSE) file for details.

## 📫 Contact

André Felipe Melo - [@felipethw](https://www.linkedin.com/in/nelipelipe/) - andref841@gmail.com

Project Link: [https://github.com/andrezz64/evvacore](https://github.com/andrezz64/evvacore)

## 🙏 Acknowledgments

- [.NET Core](https://dotnet.microsoft.com/)
- [Dapper](https://www.learndapper.com/)
- [WebSocket](https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API)

---

⭐️ From [André Oliveira](https://github.com/andrezz64)