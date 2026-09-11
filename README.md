# StreamVibe API

A .NET 10 ASP.NET Core Web API for a streaming platform. The API exposes catalog content, genres, devices, FAQs, pricing plans, authentication, user profiles, subscriptions, and contact-message functionality.

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-10.0-6C3483)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-4169E1?logo=postgresql&logoColor=white)
![JWT Bearer](https://img.shields.io/badge/Auth-JWT%20Bearer-F7B93E)
![Swagger](https://img.shields.io/badge/API%20Docs-Swagger-85EA2D?logo=swagger&logoColor=black)

---

## Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Architecture](#architecture)
- [Solution Structure](#solution-structure)
- [Core Domain](#core-domain)
- [Authentication and Authorization](#authentication-and-authorization)
- [API Endpoints](#api-endpoints)
- [Response Format](#response-format)
- [Caching](#caching)
- [Swagger](#swagger)
- [Configuration](#configuration)
- [Getting Started](#getting-started)
- [Implementation Notes](#implementation-notes)

---

## Overview

StreamVibe is organized as a four-project solution using an Onion/Clean Architecture layout:

- **Domain** contains entities, enumerations, and domain exceptions.
- **Application** contains CQRS requests and handlers, DTOs, AutoMapper configuration, the database abstraction, and response models.
- **Infrastructure** contains Entity Framework Core, PostgreSQL configuration, entity mappings, JWT token generation, password hashing, and infrastructure dependency registration.
- **WEBApi** contains controllers, middleware, application startup, Swagger configuration, and launch settings.

The application uses MediatR to dispatch application requests from controllers to their handlers. The application layer accesses the database through `IStreamDb`, whose implementation is `StreamDb` in the Infrastructure project.

---

## Features

- Content catalog queries for general content, hero content, top-ten content, seasons, reviews, and detailed content.
- Genre, device, FAQ, and landing-page queries.
- Pricing plans with monthly/yearly pricing and plan features.
- User registration, login, refresh-token, logout, and authenticated user information.
- User profile retrieval with the active subscription when one exists.
- Subscription creation, current-subscription retrieval, and cancellation.
- Contact-message submission.
- JWT bearer authentication.
- PostgreSQL persistence through Entity Framework Core and Npgsql.
- AutoMapper mappings between domain entities and record-based DTOs.
- Five-minute FusionCache caching for configured application queries.
- Centralized exception-to-status-code handling middleware.
- Swagger UI available at `/docs` in Development.

---

## Technology Stack

| Area | Technology |
|---|---|
| Runtime | .NET 10 (`net10.0`) |
| Web framework | ASP.NET Core Web API |
| Application pattern | CQRS with MediatR |
| Mapping | AutoMapper |
| ORM | Entity Framework Core 10 |
| Database provider | Npgsql / PostgreSQL |
| Authentication | ASP.NET Core JWT Bearer authentication |
| Password handling | Project `IPasswordHasher` abstraction and Infrastructure implementation |
| Caching | ZiggyCreatures FusionCache |
| API documentation | Swashbuckle.AspNetCore and Microsoft.OpenApi |
| Serialization | System.Text.Json with `JsonStringEnumConverter` |
| Package management | Central Package Management through `Directory.Packages.props` |

The main package versions are centrally defined in `Directory.Packages.props`. The solution enables nullable reference types and implicit usings through `Directory.Build.props`.

---

## Architecture

The solution follows an Onion/Clean Architecture arrangement:

```text
WEBApi
  -> Infrastructure
	  -> Application
		  -> Domain
```

The Web API depends on Infrastructure. Infrastructure references Application abstractions and implements persistence and identity-related services. Application defines use cases, DTOs, handlers, and interfaces. Domain contains the core entity model and does not depend on the outer layers.

The project does not introduce a separate Repository pattern. Handlers use the `IStreamDb` database abstraction, which exposes Entity Framework Core `DbSet<T>` properties. `StreamDb` implements that abstraction and applies all entity configurations from the Infrastructure assembly.

---

## Solution Structure

```text
StreamVibeTask/
├── Directory.Build.props
├── Directory.Packages.props
├── StreamVibe.slnx
├── CQRS.Domain/
│   ├── Entities/
│   ├── Enums/
│   └── Exceptions/
├── CQRS.Application/
│   ├── Common/
│   │   ├── Dtos/
│   │   ├── Interfaces/
│   │   ├── Mapper/
│   │   └── Response/
│   └── Features/
│       ├── Contents/
│       ├── Faqs/
│       ├── Genres/
│       ├── Login/
│       ├── Plans/
│       ├── Profile/
│       ├── Register/
│       └── ...
├── CQRS.Infrastructure/
│   ├── Configurations/
│   ├── Data/
│   ├── Identity/
│   └── DependencyInjection.cs
└── CQRS.WEBApi/
	├── Controllers/
	├── Middleware/
	├── Swagger/
	├── Program.cs
	└── Properties/launchSettings.json
```

---

## Core Domain

The domain model includes:

- `Content`, `Seasons`, `Episodes`, and `Reviews` for streaming content.
- `Genre`, `ContentGenres`, and `ContentLanguages` for content classification.
- `People` and `ContentPeople` for cast, director, and music relationships.
- `PricingPlan` and `PlanFeatures` for subscription plans.
- `User`, `RefreshToken`, `UserSubscription`, and `SubscriptionHistory` for accounts and subscriptions.
- `Device`, `Faq`, and `ContactMessages` for landing-page and support functionality.

Most entities inherit from `BaseEntity`, which provides a `Guid Id`. Entity state is generally protected with private setters and constructor validation.

Relevant enumerations include:

- `ContentType`
- `GenreType`
- `RoleType`
- `UserBillingCycle`
- `UserSubStatus`
- `HistorySubStatus`

---

## Authentication and Authorization

JWT bearer authentication is registered by Infrastructure through `AddInfrastructure`.

The JWT configuration uses:

- Issuer validation
- Audience validation
- Signing-key validation
- Lifetime validation
- Zero clock skew
- HMAC-SHA256 token signing

Tokens include subject, email, and username claims. Authenticated controllers obtain the current user ID from `ClaimTypes.NameIdentifier`.

The following endpoints require `[Authorize]`:

- `GET /api/Auth/me`
- `POST /api/Subscriptions/subscribe`
- `GET /api/Subscriptions/my`
- `DELETE /api/Subscriptions/cancel`
- `GET /api/Users/profile`

The remaining controller actions do not declare `[Authorize]`.

---

## API Endpoints

All controller routes use the `api/[controller]` convention.

### Authentication — `/api/Auth`

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/api/Auth/register` | Register a user and return authentication data | Anonymous |
| POST | `/api/Auth/login` | Authenticate with email and password | Anonymous |
| POST | `/api/Auth/refresh-token` | Refresh authentication using a refresh token | Anonymous |
| POST | `/api/Auth/logout` | Process logout using a refresh token | Anonymous |
| GET | `/api/Auth/me` | Retrieve the authenticated user's information | Bearer JWT |

### Content — `/api/Content`

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Content` | Get filtered content by type and filter; supports `limit` | Anonymous |
| GET | `/api/Content/hero` | Get hero content | Anonymous |
| GET | `/api/Content/genres` | Get genres used by content | Anonymous |
| GET | `/api/Content/top-ten` | Get top-ten ranked content | Anonymous |
| GET | `/api/Content/{id}` | Get content details | Anonymous |
| GET | `/api/Content/{id}/seasons` | Get content seasons and episodes | Anonymous |
| GET | `/api/Content/{id}/reviews` | Get content reviews | Anonymous |

The general content query requires both `type` and `filter`. Supported filters are `trending`, `new-release`, and `must-watch`.

### Landing page — `/api/LandingPage`

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/LandingPage/devices` | Get supported devices | Anonymous |
| GET | `/api/LandingPage/genres` | Get genres | Anonymous |
| GET | `/api/LandingPage/faqs` | Get FAQs | Anonymous |
| GET | `/api/LandingPage/plans` | Get pricing plans using an optional billing string | Anonymous |

### Subscriptions — `/api/Subscriptions`

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Subscriptions/plans` | Get plans with features; billing defaults to monthly and supports monthly/yearly values | Anonymous |
| POST | `/api/Subscriptions/subscribe` | Create a subscription for the authenticated user | Bearer JWT |
| GET | `/api/Subscriptions/my` | Get the authenticated user's subscription | Bearer JWT |
| DELETE | `/api/Subscriptions/cancel` | Cancel the authenticated user's subscription | Bearer JWT |

The subscription request contains `planId`, `billingCycle`, and `isTrial`.

### Users — `/api/Users`

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Users/profile` | Get the authenticated user's profile and active subscription, if present | Bearer JWT |

### Support — `/api/Support`

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/api/Support/contact` | Submit a contact message | Anonymous |

---

## Response Format

Controllers wrap successful results in the generic `Response<T>` type. Its fields are:

```json
{
  "data": {},
  "isSuccess": true,
  "statusCode": 200,
  "errors": null
}
```

Failed requests are also represented by `Response<string>` and contain error messages in the `errors` collection.

The actual HTTP status code is set by controllers or by the global exception middleware, depending on the execution path.

---

## CQRS and Application Features

Application requests and handlers are organized by feature. Examples include:

- Content queries: all content, hero content, top-ten content, details, seasons, and reviews.
- Genre, device, FAQ, and pricing-plan queries.
- Register, login, refresh-token, and logout commands.
- Profile and subscription commands/queries.
- Contact-message command handling.

MediatR handlers receive dependencies such as `IStreamDb`, AutoMapper, and FusionCache through constructor injection.

---

## Caching

The Application dependency registration adds FusionCache with a default duration of five minutes. Several query handlers use cache keys based on their request parameters, including content, plan, and genre queries.

---

## Swagger

Swagger is configured in:

```text
CQRS.WEBApi/Swagger/SwaggerConfigurations.cs
```

In Development, the application exposes:

- Swagger JSON: `/swagger/v1/swagger.json`
- Swagger UI: `/docs`

The launch profile is configured to open the Swagger UI automatically at:

```text
http://localhost:5000/docs
```

The Swagger configuration includes a Bearer JWT security definition and XML documentation generated by the Web API project.

---

## Configuration

`CQRS.WEBApi/appsettings.json` contains logging and allowed-host configuration. Development JWT settings are located in `appsettings.Development.json`.

The Infrastructure layer expects the following configuration values:

### Connection string

The key used by the application is:

```text
ConnectionStrings:DefaultConnection
```

The application configures PostgreSQL through Npgsql and maps the domain enumerations to PostgreSQL enum types.

### JWT settings

The expected section is `Jwt`:

```json
{
  "Jwt": {
	"Secret": "your-secret",
	"Issuer": "StreamVibe",
	"Audience": "StreamVibeClient",
	"ExpirationInMinutes": 15,
	"RefreshTokenExpiresDays": 7
  }
}
```

`RefreshTokenExpiresDays` is read by the application but is not currently present in the checked-in Development settings file. Configure it through user secrets, environment variables, or another configuration source before using refresh-token functionality.

Do not commit production secrets or database credentials to source control.

---

## Getting Started

### Prerequisites

- .NET 10 SDK
- PostgreSQL instance
- A configured `ConnectionStrings:DefaultConnection` value
- A non-empty JWT secret

### Run the API

From the repository root:

```bash
dotnet run --project CQRS.WEBApi/StreamVibe.WEBApi.csproj
```

The Development launch profile uses `http://localhost:5000` and opens Swagger UI at `/docs`.

### Build the solution

```bash
dotnet build
```

The solution file is `StreamVibe.slnx`.

---

## Error Handling

`GlobalExceptionHandler` converts selected exception types into HTTP status codes:

| Exception | Status code |
|---|---:|
| `UnauthorizedAccessException` | 401 |
| `ForbiddenException` | 403 |
| `ArgumentException` | 400 |
| `KeyNotFoundException` | 404 |
| `InvalidOperationException` | 409 |
| Other exceptions | 500 |

The middleware serializes failures using `Response<string>`.

---

## Current Project Scope

The repository contains the API, domain model, application handlers, persistence configuration, identity helpers, and Swagger setup described above. No claims are made here about automated migrations, seed data, role management, file storage, background jobs, or deployment infrastructure because those features are not present in the inspected project structure.
