🍕 Talabat Food Delivery API
A comprehensive e-commerce food delivery RESTful API built with .NET 8, implementing Clean Architecture principles and industry best practices.
📋 Table of Contents

Overview
Features
Tech Stack
Architecture
Getting Started
API Documentation
Project Structure
Configuration
Contributing
License

🎯 Overview
Talabat API is a robust backend solution for a food delivery platform, providing functionality for product management, user authentication, shopping cart operations, order processing, and payment integration with Stripe.
✨ Features
Core Functionality

🔐 User Authentication & Authorization

JWT-based authentication
Role-based access control (Admin, SuperAdmin)
User registration and login
Address management


🛒 Shopping Cart

Redis-based cart storage
Real-time cart updates
Cart persistence across sessions


📦 Product Management

Product catalog with filtering and sorting
Pagination support
Product categories (brands and types)
Advanced search functionality


📋 Order Management

Order creation and tracking
Order history
Multiple delivery methods
Order status tracking


💳 Payment Integration

Stripe payment gateway
Payment intent creation
Webhook handling for payment status updates
Secure payment processing



Technical Features

🏗️ Clean Architecture with separation of concerns
📊 Repository Pattern & Unit of Work
🔄 AutoMapper for object-to-object mapping
🎯 Specification Pattern for flexible querying
🚀 Redis Caching for improved performance
🛡️ Global Exception Handling
✅ Model Validation
📝 Swagger/OpenAPI documentation

🛠️ Tech Stack
Backend

.NET 8.0
ASP.NET Core Web API
Entity Framework Core 8.0
SQL Server
Redis (StackExchange.Redis)
AutoMapper 15.1.0
Stripe.NET 50.2.0

Authentication & Security

ASP.NET Core Identity
JWT Bearer Authentication
Microsoft.IdentityModel.Tokens

Additional Libraries

Swashbuckle (Swagger)
Newtonsoft.Json

🏛️ Architecture
The project follows Clean Architecture principles with clear separation of concerns:
├── Core/
│   ├── Domain/                 # Domain entities and contracts
│   ├── Services/               # Business logic implementation
│   └── Services Abstraction/   # Service interfaces
│
├── Infrastructure/
│   ├── Persistence Layer/      # Data access and repositories
│   └── Presentation Layer/     # API controllers
│
├── Shared/                     # Shared DTOs and utilities
│
└── TalabatDemo/               # API entry point and configuration
Key Patterns Implemented

Repository Pattern: Abstraction over data access
Unit of Work: Transaction management
Specification Pattern: Flexible query composition
Dependency Injection: Loose coupling
Factory Pattern: Service creation
DTO Pattern: Data transfer objects

🚀 Getting Started
Prerequisites

.NET 8.0 SDK
SQL Server
Redis
Visual Studio 2022 or VS Code
