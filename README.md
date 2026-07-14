# AI Helpdesk Ticket Assignment System

An AI-powered Helpdesk Ticket Assignment System built with **ASP.NET Core 9**, **Entity Framework Core**, **SQLite**, **Microsoft Power Automate**, and the **OpenAI Responses API**.

This project automates the helpdesk ticket triage process by analyzing incoming support requests, determining the ticket category and priority, assigning the most suitable engineer, and storing the results in a database.

---

## Features

- RESTful ASP.NET Core 9 Web API
- Entity Framework Core with SQLite
- AI-powered ticket categorization
- AI-powered priority detection
- AI-powered engineer assignment
- AI-generated reasoning for assignments
- CRUD operations for helpdesk tickets
- Search and filtering endpoints
- Swagger/OpenAPI documentation
- Microsoft Power Automate workflow integration
- ngrok support for external API access

---

## Technologies Used

- ASP.NET Core 9
- Entity Framework Core
- SQLite
- OpenAI Responses API
- Microsoft Power Automate
- Swagger / OpenAPI
- ngrok

---

# System Architecture

```text
Employee
    │
    ▼
Microsoft Forms
    │
    ▼
Power Automate
    │
    ▼
ASP.NET Core API
    │
    ▼
SQLite Database
    │
    ▼
OpenAI Responses API
    │
    ▼
AI Analysis
(Category • Priority • Engineer)
    │
    ▼
Update Ticket
```

---

# Workflow

1. Employee submits a helpdesk request using Microsoft Forms.
2. Power Automate is triggered automatically.
3. The workflow sends the ticket to the ASP.NET Core API.
4. The API stores the ticket in the SQLite database.
5. Power Automate sends the ticket information to the OpenAI Responses API.
6. OpenAI analyzes the ticket and returns:
   - Category
   - Priority
   - Assigned Engineer
   - AI Reasoning
7. Power Automate updates the ticket using the API.
8. The updated ticket is available through the REST API.

---

# API Endpoints

## Ticket Management

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | `/api/tickets` | Get all tickets |
| GET | `/api/tickets/{id}` | Get ticket by ID |
| POST | `/api/tickets` | Create a ticket |
| PUT | `/api/tickets/{id}` | Update a ticket |
| DELETE | `/api/tickets/{id}` | Delete a ticket |

---

## Search & Filtering

| Endpoint | Description |
|----------|-------------|
| GET `/api/tickets/highpriority` | Get high priority tickets |
| GET `/api/tickets/new` | Get new tickets |
| GET `/api/tickets/assigned` | Get assigned tickets |
| GET `/api/tickets/category/{category}` | Filter by category |
| GET `/api/tickets/engineer/{name}` | Filter by assigned engineer |

---

# AI Ticket Processing

The OpenAI Responses API analyzes each helpdesk ticket and determines:

- Ticket category
- Ticket priority
- Best engineer for the issue
- Reasoning behind the assignment

### Example

Input:

```
Title:
Unable to login to ERP

Description:
Password reset did not resolve the issue.
```

AI Response:

```json
{
  "category": "Authentication",
  "priority": "High",
  "assignedEngineer": "John",
  "reason": "John specializes in ERP authentication issues."
}
```

---

# Project Structure

```
HelpdeskAPI
│
├── Controllers/
├── Data/
├── Models/
├── Services/
├── Migrations/
├── Program.cs
├── appsettings.json
└── Helpdesk.db
```

---

# Configuration

This project requires an OpenAI API key.

For security reasons, API keys are **not included** in this repository.

Configure your API key using:

- .NET User Secrets
- Environment Variables
- Azure Key Vault (Production)


---

# What I Learned

Through this project I gained experience with:

- Building RESTful APIs using ASP.NET Core
- Entity Framework Core and database management
- AI integration using the OpenAI Responses API
- Workflow automation with Microsoft Power Automate
- HTTP requests and JSON serialization
- API testing using Swagger
- Secure API development and configuration management

---

# Future Improvements

- Email notifications
- Microsoft Teams integration
- Authentication & Authorization
- SQL Server support
- Docker containerization
- Azure deployment
- Engineer workload balancing
- SLA tracking
- Unit and integration testing

---

# Author

**Shivo**

A portfolio project demonstrating backend development, AI integration, database management, and workflow automation using the Microsoft Power Platform and ASP.NET Core.
