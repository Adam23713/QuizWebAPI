# Quiz WebAPI

## Overview

The Quiz WebAPI is a project designed to provide a RESTful API for managing quizzes. It follows an n-tier architecture, utilizing Redis for caching, MS SQL Server with Entity Framework for data storage, and Identity Server for authentication.

## Features

- **N-Tier Architecture:**
  - The project is structured using an n-tier architecture, promoting modularity and separation of concerns.

- **Redis Caching:**
  - Utilizes Redis for caching to enhance performance by storing frequently accessed data.

- **MS SQL Server with Entity Framework:**
  - Manages data storage using MS SQL Server.
  - Entity Framework is employed for a Code-First approach to define and manage the database schema.

- **Identity Server for Authentication:**
  - Implements Identity Server to handle authentication and authorization, providing a secure identity management system.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed
- [MS SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) installed
- [Redis](https://redis.io/download) installed
