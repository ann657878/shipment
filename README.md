# 🚚 Shipment Tracking Application

## 📌 Overview

This is an ASP.NET Core MVC application built as part of an assessment.
The application allows users to track shipments and manage shipment-related data through a structured and user-friendly interface.

---

## 🛠 Tech Stack

* ASP.NET Core MVC (.NET 8)
* Entity Framework Core
* Razor Views
* HTML, CSS, Bootstrap
* JavaScript
* SQLite / SQL Server (depending on configuration)
* xUnit (Unit Testing)

---

## 🚀 Live Deployment

The application is deployed and accessible here:

👉 https://shipment-krw6.onrender.com

---

## ⚠️ Deployment Note

This project is built using ASP.NET Core MVC and requires a backend runtime.

While the assignment mentions Netlify/Vercel, these platforms do not support .NET backend applications. Therefore, the application has been deployed on Render, which supports .NET services.

---

## ⚙️ Setup Instructions

To run the application locally:

```bash
dotnet restore
dotnet build
dotnet run
```

---

## 🧪 Running Unit Tests

To execute unit tests:

```bash
dotnet test
```

---

## 📌 Assumptions

* The application uses a local database setup (SQLite or SQL Server).
* In the deployed environment, data persistence may reset due to platform limitations.
* Basic validation and error handling are implemented.

---

## 📂 Project Structure

* `ShipmentTrackingApp/` → Main MVC application
* `ShipmentTrackingApp.Tests/` → Unit test project

---

## ✨ Features

* Shipment creation and tracking
* MVC architecture with separation of concerns
* Entity Framework for data access
* Unit testing for core functionality

---
