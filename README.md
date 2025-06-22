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

frontend at http://localhost:5173
backend API at http://localhost:5000
sqlserver for persistent data
