# 🏋️‍♂️ FitHub – Full Gym Management System

A complete, production‑level **Gym Management System** built with **ASP.NET Core**, applying clean architecture, secure authentication, online payments, and a fully structured 3‑Tier Architecture.
FitHub makes it easy for **Admins**, **Trainers**, and **Members** to manage all gym operations through a smooth and organized workflow.

---

## 📌 Overview

FitHub is a mid‑to‑large‑scale project that focuses on:

* Clean software architecture
* Authentication & authorization best practices
* External service integrations (Google Login, PayPal, Email Service)
* Layer separation (DAL → BLL → PL)
* Scalable and maintainable backend structure

This system was built to reflect real‑world gym management needs including sessions, plans, subscriptions, payments, and controlled permissions.

---

## 🚀 Features

### 👤 Members

* Purchase **individual sessions** with specific prices.
* Subscribe to **training plans** and access all included sessions.
* Access personal dashboard (sessions, plans, subscriptions).

### 🛠 Admin

Full control over the system:

* Add trainers (only Admin can do this)
* Add/remove/manage training sessions
* Add/remove/manage training plans
* View member subscriptions
* View members subscribed to each plan/session

### 🏋️ Trainer

* Add **their own sessions and plans** only
* Cannot view or edit other trainers’ content
* Access their assigned management dashboard

### 🔐 Authentication & User Management

* Login with **Google**
* Standard registration with **Email Confirmation**
* Password reset via email
* Fully implemented **Role‑Based Authorization** (Admin, Trainer, Member)

### 💳 Online Payments

* Integrated **PayPal** payment system
* Members can securely purchase sessions or plans online

---

## 🛠️ Technologies Used

* **ASP.NET Core** (Backend)
* **Entity Framework Core** (Database ORM)
* **Identity** for Authentication & Authorization
* **Google OAuth Login**
* **Email Service** for confirmation, password reset, notifications
* **PayPal SDK** for online payments
* **HTML / CSS / JavaScript / Bootstrap** for the UI

---

## 🏗️ Architecture

FitHub follows a clean, scalable **3‑Tier Architecture**:

```
FitHub‑GYM
│
├── Gym.DAL   # Data Access Layer (Entities, DbContext, Repositories)
├── Gym.BLL   # Business Logic Layer (Services, Handlers, Core Logic)
└── Gym.PL    # Presentation Layer (Controllers, Views, UI)
```

### Why this architecture?

* Maintainability
* Clear separation of responsibilities
* Easier testing and debugging
* Suitable for mid‑size to enterprise‑level systems

---

## 🏁 Getting Started

## Installation Steps

### 1. Clone the Repository

```bash
git clone https://github.com/Youssef-Shabaan/FitHub-GYM.git
```

### 2. Open the Solution

Open `FitHub.sln` in **Visual Studio**.

### 3. Set the Database Connection String

Update your `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "YOUR_SQL_SERVER_CONNECTION_STRING"
}
```

### 4. Apply Migrations

From Package Manager Console:

```bash
Update-Database
```

### 5. Run the Project

Press **F5** to launch the system.

---



## 👥 Contributors

* **Hussein Hashiem**
* **Youssef Shaaban**

---

## 📝 Notes

FitHub was a major milestone in applying clean architecture, authentication flows, payment integrations, and advanced backend concepts. The project demonstrates real‑world system design and practical .NET experience.
