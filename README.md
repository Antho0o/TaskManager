
# Task Management Application

A full-stack task management application built with ASP.NET Core MVC and SQLite.

The application allows users to create, manage, organize, and track tasks through a modern dark-themed interface.

## Features

- Create tasks
- Edit existing tasks
- Delete tasks
- Mark tasks as completed
- Mark completed tasks as pending
- Set task priorities
- Add task descriptions
- Set task due dates
- Search tasks
- Filter tasks by status
- Filter tasks by priority
- Task statistics dashboard
- Completion progress tracking
- Responsive dark-themed interface
- SQLite database
- Entity Framework Core database integration

## Dashboard

The dashboard provides an overview of the user's tasks, including:

- Total tasks
- Completed tasks
- Pending tasks
- High-priority tasks
- Overall completion percentage

## Search & Filtering

Tasks can be searched and filtered using:

- Search by task title or content
- All tasks
- Pending tasks
- Completed tasks
- High priority
- Medium priority
- Low priority

## Technologies

- C#
- .NET 9
- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- HTML5
- CSS3
- JavaScript
- Git
- GitHub

## Project Structure

```text
TaskManager
│
├── Controllers
│   ├── HomeController.cs
│   └── TasksController.cs
│
├── Data
│   └── TaskManagerContext.cs
│
├── Models
│   ├── TaskItem.cs
│   └── ErrorViewModel.cs
│
├── Views
│   ├── Home
│   ├── Tasks
│   └── Shared
│
├── wwwroot
│   ├── css
│   ├── js
│   └── lib
│
├── appsettings.json
├── Program.cs
└── TaskManager.csproj
