# 🍕 Talabat Food Delivery API

A comprehensive **e-commerce food delivery RESTful API** built with **.NET 8**, following **Clean Architecture principles** and industry best practices.

---

## 📋 Table of Contents

- [Overview](#-overview) 
- [Features](#-features) 
- [Project Structure](#-Project-Structure)
- [Security Features](#-Security-Features) 
- [Tech Stack](#-Tech-Stack)
- [Getting Started](#-getting-started) 
- [Author](#-author)

---

## 🎯 Overview

Talabat API is a robust backend solution for a food delivery platform. It provides features for:

- Product management
- User authentication
- Shopping cart operations
- Order processing
- Payment integration with **Stripe**

---

## ✨ Features

### Core Functionality

#### 🔐 User Authentication & Authorization
- JWT-based authentication
- Role-based access control (**Admin**, **SuperAdmin**)
- User registration and login
- Address management

#### 🛒 Shopping Cart
- Redis-based cart storage
- Real-time cart updates
- Cart persistence across sessions

#### 📦 Product Management
- Product catalog with filtering and sorting
- Pagination support
- Product categories (brands and types)
- Advanced search functionality

#### 📋 Order Management
- Order creation and tracking
- Order history
- Multiple delivery methods
- Order status tracking

#### 💳 Payment Integration
- Stripe payment gateway
- Payment intent creation
- Webhook handling for payment status updates
- Secure payment processing

### Technical Features
- 🏗️ Clean Architecture with **Separation of Concerns**
- 📊 Repository Pattern & Unit of Work
- 🔄 AutoMapper for object-to-object mapping
- 🎯 Specification Pattern for flexible querying
- 🚀 Redis Caching for improved performance
- 🛡️ Global Exception Handling
- ✅ Model Validation
- 📝 Swagger/OpenAPI Documentation

---

## 🔒 Security Features

- **JWT-based Authentication**  
- **Password Hashing** with ASP.NET Core Identity  
- **Role-based Authorization**  
- **Secure Payment Processing** with Stripe  
- **Input Validation and Sanitization**  
- **Global Exception Handling**  
- **HTTPS Enforcement**  
---

## 📁 Project Structure
```
Talabat-App-Api/
│
├── Core/
│   ├── Domain/
│   │   ├── Contracts/          # Repository interfaces
│   │   ├── Entities/           # Domain models
│   │   └── Exceptions/         # Custom exceptions
│   │
│   ├── Services/
│   │   ├── MappingProfiles/    # AutoMapper profiles
│   │   ├── Service/            # Service implementations
│   │   ├── Specifications/     # Query specifications
│   │   └── ServiceManager/     # Service aggregation
│   │
│   └── Services Abstraction/
│       └── Interfaces/         # Service contracts
│
├── Infrastructure/
│   ├── Persistence Layer/
│   │   ├── Data/              # DbContext and configurations
│   │   ├── Repositories/      # Repository implementations
│   │   └── Identity/          # Identity DbContext
│   │
│   └── Presentation Layer/
│       ├── Controllers/       # API controllers
│       └── Attributes/        # Custom attributes
│
├── Shared/
│   ├── DTOs/                  # Data Transfer Objects
│   ├── Error Models/          # Error response models
│   └── Authentication/        # JWT configuration models
│
└── TalabatDemo/               # API startup project
    ├── CustomMiddleware/      # Exception handling middleware
    ├── Extensions/            # Service registration extensions
    └── Factory/               # Response factories

## Key Patterns Implemented

- **Repository Pattern:** Abstraction over data access  
- **Unit of Work:** Transaction management  
- **Specification Pattern:** Flexible query composition  
- **Dependency Injection:** Loose coupling  
- **Factory Pattern:** Service creation  
- **DTO Pattern:** Data transfer objects  
```
---

## 🛠️ Tech Stack

### Backend
- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 8.0**
- **SQL Server**
- **Redis** (`StackExchange.Redis`)
- **AutoMapper 15.1.0**
- **Stripe.NET 50.2.0**

### Authentication & Security
- **ASP.NET Core Identity**
- **JWT Bearer Authentication**
- **Microsoft.IdentityModel.Tokens**

### Additional Libraries
- **Swashbuckle (Swagger)**
- **Newtonsoft.Json**

---

## 🚀 Getting Started

### Prerequisites

Before running the project, make sure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server)  
- [Redis](https://redis.io/)  
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)  

### Installation

Clone the repository and navigate into the project directory:

```bash
git clone https://github.com/MohamedEssam-2/Talabat-App-Api.git
cd Talabat-App-Api
---
```
## Author

**Mohamed Essam**  

GitHub: [@MohamedEssam-2](https://github.com/MohamedEssam-2)
