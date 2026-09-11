```markdown
# 🍿 StreamVibe API

A production-oriented **ASP.NET Core (.NET 10) Web API** built on **Onion / Clean Architecture** — catalog content retrieval, JWT authentication, user subscription management, role-based pricing plans, and MediatR-powered CQRS work out of the box, so every new streaming project starts from decisions already made.

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4)
![EF Core](https://img.shields.io/badge/EF%20Core-10.0-6C3483)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-4169E1?logo=postgresql&logoColor=white)
![JWT Bearer](https://img.shields.io/badge/Auth-JWT%20Bearer-F7B93E)
![Swagger](https://img.shields.io/badge/API%20Docs-Swagger-85EA2D?logo=swagger&logoColor=black)
![License](https://img.shields.io/badge/License-MIT-2ea44f)

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Solution Structure](#-solution-structure)
- [Key Design Decisions](#-key-design-decisions)
- [API Endpoints](#-api-endpoints)
- [Getting Started](#-getting-started)
- [License](#-license)
- [Author](#-author)

---

## 📖 Overview

`StreamVibe` is a reference backend designed to be **cloned, run and extended**. The goal is not just a working REST API — it is a foundation where the expensive part of every new project (architecture decisions) has already been made once, deliberately:

- Dependencies flow strictly **inward** — the Domain (`CQRS.Domain`) depends on nothing.
- Application logic is isolated into explicit use cases via **MediatR (CQRS)**, not scattered across monolithic services.
- Errors are **designed**: exceptions are translated centrally by a global middleware.
- Entities protect their own invariants — no anemic models with public setters.

---

## ✨ Features

- 🎬 **Content catalog queries** — dedicated endpoints for general content, hero content, top-ten content, seasons, reviews, and detailed content views.
- 📱 **Landing page resources** — fetch supported devices, genres, FAQs, and pricing plans (with monthly/yearly options).
- 🔐 **JWT Bearer authentication** — secure user registration, login, refresh-token support, logout, and authenticated user info retrieval.
- 💳 **Subscription management** — self-service subscription creation, current-subscription retrieval, and cancellation.
- ⚡ **FusionCache integration** — 5-minute cache duration for configured application queries to reduce PostgreSQL database load.
- 🚨 **Global exception middleware** — centralized exception-to-status-code handling (`UnauthorizedAccessException` → 401, `KeyNotFoundException` → 404).
- 📦 **Unified `Response<T>` envelope** — every endpoint returns the same JSON shape (`data`, `isSuccess`, `statusCode`, `errors`).
- 🧬 **Rich domain entities** — state is protected with private setters and constructor validation (e.g., `Content`, `PricingPlan`, `UserSubscription`).

---

## 🧰 Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 10) |
| Application Pattern | CQRS with MediatR |
| Data Access | Entity Framework Core 10 · PostgreSQL (Npgsql) |
| Identity & Auth | ASP.NET Core JWT Bearer authentication · Custom `IPasswordHasher` |
| Caching | ZiggyCreatures FusionCache |
| Mapping | AutoMapper |
| API Docs | Swashbuckle.AspNetCore · Microsoft.OpenApi |

---

## 🏛 Architecture

Classic **Onion / Clean Architecture** across four projects — all dependencies point inward:

```text
CQRS.WEBApi  →  CQRS.Infrastructure
                      ↓
                CQRS.Application
                      ↓
                  CQRS.Domain

```

**Architectural principles applied:**

* **Dependency Inversion** — the Web API depends on Infrastructure. Infrastructure references Application abstractions and implements persistence and identity services.
* **Separation of Concerns** — Domain contains the core entity model (`BaseEntity` with `Guid Id`, enumerations) and does not depend on outer layers.
* **CQRS Focus** — Handlers define explicit use cases (e.g., `GetHeroContentQuery`, `CreateSubscriptionCommand`), keeping business logic isolated and testable.

---

## 🧩 Solution Structure

```text
StreamVibeTask/
├── Directory.Build.props             # Centralized build settings (nullable, implicit usings)
├── Directory.Packages.props          # Central Package Management
├── StreamVibe.slnx
├── CQRS.Domain/                      # Core entities, enums, exceptions
│   ├── Entities/
│   ├── Enums/
│   └── Exceptions/
├── CQRS.Application/                 # CQRS features, abstractions, DTOs
│   ├── Common/
│   │   ├── Dtos/
│   │   ├── Interfaces/               # IStreamDb abstraction
│   │   ├── Mapper/
│   │   └── Response/                 # Response<T> envelope
│   └── Features/                     # MediatR Handlers (Contents, Login, Plans, etc.)
├── CQRS.Infrastructure/              # EF Core, PostgreSQL, JWT, Hashing
│   ├── Configurations/
│   ├── Data/                         # StreamDb implementation
│   ├── Identity/
│   └── DependencyInjection.cs
└── CQRS.WEBApi/                      # Entry point, Controllers, Middleware
    ├── Controllers/
    ├── Middleware/                   # GlobalExceptionHandler
    ├── Swagger/
    └── Program.cs

```

---

## 🎯 Key Design Decisions

**CQRS with MediatR.** Application requests and handlers are organized strictly by feature rather than generic services. Content queries, user registration, and profile commands are isolated, allowing dependencies like `IStreamDb`, AutoMapper, and FusionCache to be injected precisely where needed.

**No hand-rolled repository.** EF Core's `DbSet` already implements the repository / unit-of-work patterns. The `IStreamDb` interface keeps the Application layer decoupled without ceremonial abstraction — knowing when *not* to apply a pattern is part of the design.

**DDD style — with honest boundaries.** No aggregates or bounded contexts are claimed at this scale. What is applied is the tactical side: private setters, guarded constructors, and validation logic. The ORM gets a parameterless constructor for hydration; application code must go through the validated path.

**Identity comes from the token, not the route.** All self-service subscription and profile endpoints resolve the user via `ClaimTypes.NameIdentifier` from the JWT. A route-supplied id is insecure; the token says who you are — you don't get to choose.

**Strict token expiry.** JWT validation uses `ClockSkew = TimeSpan.Zero` — no default 5-minute grace window; expired means expired.

**Exception-driven error semantics.** The Application layer throws standard .NET exceptions (`ArgumentException`, `KeyNotFoundException`, `UnauthorizedAccessException`). The global middleware is only a translator mapping these directly to HTTP status codes (400, 404, 401) — no HTTP concepts leak into the CQRS handlers.

---

## 🔌 API Endpoints

### Auth — `/api/Auth`

| Method | Endpoint | Description | Auth |
| --- | --- | --- | --- |
| POST | `/register` | Register a user and return authentication data | Anonymous |
| POST | `/login` | Authenticate with email and password | Anonymous |
| POST | `/refresh-token` | Refresh authentication using a refresh token | Anonymous |
| POST | `/logout` | Process logout using a refresh token | Anonymous |
| GET | `/me` | Retrieve the authenticated user's information | Bearer JWT |

### Content — `/api/Content`

| Method | Endpoint | Description | Auth |
| --- | --- | --- | --- |
| GET | `/` | Get filtered content by type and filter (supports `limit`) | Anonymous |
| GET | `/hero` | Get hero content | Anonymous |
| GET | `/genres` | Get genres used by content | Anonymous |
| GET | `/top-ten` | Get top-ten ranked content | Anonymous |
| GET | `/{id}` | Get content details | Anonymous |
| GET | `/{id}/seasons` | Get content seasons and episodes | Anonymous |
| GET | `/{id}/reviews` | Get content reviews | Anonymous |

### Landing Page & Support

| Method | Endpoint | Description | Auth |
| --- | --- | --- | --- |
| GET | `/api/LandingPage/devices` | Get supported devices | Anonymous |
| GET | `/api/LandingPage/faqs` | Get FAQs | Anonymous |
| GET | `/api/LandingPage/plans` | Get pricing plans using an optional billing string | Anonymous |
| POST | `/api/Support/contact` | Submit a contact message | Anonymous |
| GET | `/api/Users/profile` | Get the authenticated user's profile and active sub | Bearer JWT |

### Subscriptions — `/api/Subscriptions`

| Method | Endpoint | Description | Auth |
| --- | --- | --- | --- |
| GET | `/plans` | Get plans with features (supports monthly/yearly) | Anonymous |
| POST | `/subscribe` | Create a subscription for the authenticated user | Bearer JWT |
| GET | `/my` | Get the authenticated user's active subscription | Bearer JWT |
| DELETE | `/cancel` | Cancel the authenticated user's subscription | Bearer JWT |

### Response Envelope

Every response uses the same unified shape:

```json
{
  "data": { },
  "isSuccess": true,
  "statusCode": 200,
  "errors": null
}

```

---

## 🚀 Getting Started

**Prerequisites:** .NET 10 SDK · PostgreSQL instance

```bash
git clone [https://github.com/vusal016/StreamVibe.git](https://github.com/vusal016/StreamVibe.git)
cd StreamVibe
dotnet run --project CQRS.WEBApi/StreamVibe.WEBApi.csproj

```

The Development launch profile uses `http://localhost:5000` and automatically opens the Swagger UI at `/docs`.

Configuration lives in `appsettings.json` and `appsettings.Development.json`. Ensure your database connection and JWT secrets are configured properly:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=StreamVibeDb;Username=postgres;Password=yourpassword"
  },
  "Jwt": {
    "Secret": "your-secret-key-at-least-32-characters-long",
    "Issuer": "StreamVibe",
    "Audience": "StreamVibeClient",
    "ExpirationInMinutes": 15,
    "RefreshTokenExpiresDays": 7
  }
}

```

> ⚠️ **Note:** `RefreshTokenExpiresDays` is required for refresh-token functionality. Do not commit production secrets or database credentials to source control.

---

## 📄 License

MIT — free to use, modify, and build upon.

## 👤 Author

**Vusal Mammadov** — .NET Backend Developer

```

```
