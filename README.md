# SmartFinance (WPF)

Simple desktop budget tracker built with WPF and .NET 8.

This is a personal learning project where I practice:
- WPF + MVVM
- Layered architecture
- EF Core with SQLite
- Clean project structure

---

## Application Preview

![App Demo](docs/demo.gif)

---

## What it does (v1.0.0)

- Add transactions (amount, category, note, type)
- Delete transactions
- Data is stored in SQLite
- Data persists after restart
- Basic layered architecture:
  - App (UI)
  - Core (Domain)
  - Infrastructure (EF Core + SQLite)
  - Application (Services / Use cases)

---

## Tech stack

- .NET 8 (WPF)
- CommunityToolkit.Mvvm
- Entity Framework Core 8
- SQLite

---

## Architecture overview

SmartFinance.App  
→ WPF UI and ViewModels  

SmartFinance.Core  
→ Domain models (Transaction, Enums) and interfaces  

SmartFinance.Infrastructure  
→ EF Core DbContext and repository implementations  

SmartFinance.Application  
→ Application services (transaction logic, validation, parsing)

---

## How to run

1. Open `SmartFinance.sln`
2. Run the WPF project
3. SQLite database will be created automatically in AppData

---

## Current status

This is the foundation version (v1.0.0).  
The goal was to build clean architecture and working persistence first.

Planned next steps:
- Categories
- Accounts
- Dashboard with analytics
- Budgets and goals

---

## Why I built this

I wanted to build a real desktop app from scratch, not just small demos.  
This project helps me understand:

- Separation of concerns
- Working with EF Core in desktop apps
- Application vs Domain logic
- Structuring medium-sized projects

---

Project is still in active development.
