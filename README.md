# UrlShortener Project

A modern, containerized web application for shortening URLs with integrated threat analysis using the [VirusTotal](https://www.virustotal.com/) API.

The backend is built with **ASP.NET 9.0** using **Clean Architecture** and **Minimal APIs**.  
The frontend is a **Vue.js** app built with **TypeScript** and Vite.

---

## 🌐 Project Links

- 📘 **Documentation (ReadTheDocs):** [https://sqs-project.readthedocs.io/en/latest/](https://sqs-project.readthedocs.io/en/latest/)
- 🔍 **SonarQube Analysis:** [https://sonarcloud.io/summary/overall?id=LucZZ_sqs-project](https://sonarcloud.io/summary/overall?id=LucZZ_sqs-project)

---

## 🚀 Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [Node.js 20+](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- The application integrates with the [VirusTotal API](https://www.virustotal.com/) to scan URLs.

The configuration key needs to be defined in the `.env` file:

```env
VirusTotal__ApiKey=""
```

### Running the Application

```bash
# Start the full stack with Docker
docker-compose up --build
```
The following services will start:

frontend at http://localhost:8080/register, http://localhost:8080/login (default)
(Password requirements for registration: 1 digit, 1 lowercase, 1 uppercase, 1 non alphanumeric, length >=6)

backend API at http://localhost:5000

sqlserver for persistent data

Note: the backend will start with a 30 second delay, to ensure the database is up and running, or the migrations will fail. Sql healthchecks did not work in docker-compose.yml

### Running services and tests individually

#### Backend (needs environment variables!)
```bash
dotnet run --project .\backend\src\UrlShortener.Web\UrlShortener.Web.csproj
```

#### Backend Unit Tests
```bash
dotnet test .\backend\tests\UrlShortener.UnitTests\
```

#### Backend Integration Tests
```bash
dotnet test .\backend\tests\UrlShortener.IntegrationTests\
```

#### Frontend E2E tests (needs the frontend running under http://localhost:8080)
```bash
cd .\frontend\UrlShortener.E2ETests\

dotnet build

pwsh bin/Debug/net9.0/playwright.ps1 install

dotnet test
```
