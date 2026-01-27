# 🍕 Talabat Food Delivery API

A comprehensive **e-commerce food delivery RESTful API** built with **.NET 8**, following **Clean Architecture principles** and industry best practices.

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)
- [Project Structure](#-project-structure)
- [Configuration](#-configuration)
- [Contributing](#-contributing)
- [License](#-license)

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

## 🏛️ Architecture

The project follows **Clean Architecture principles** with a clear separation of concerns:

