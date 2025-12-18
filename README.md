# Day Planner - Visual Schedule Application

A modern, child-friendly visual schedule and day planner application designed specifically for children who need extra structure and predictability, such as pupils with autism, ADHD, or other neurodevelopmental difficulties.

## 🎯 Project Overview

This application was developed for **Statped**, a Norwegian national service that supports children and pupils with special educational needs. Statped works extensively with structure, routines, and universal design of digital tools.

The Day Planner provides:
- **Visual Schedule**: Clear, icon-based activity representation
- **Daily Planning**: Easy-to-use interface for creating and managing daily plans
- **Activity Tracking**: Track completion of activities with visual feedback
- **Completion Rewards**: Celebration and trophy display when all tasks are completed

## 🛠️ Tech Stack

### Backend
- **.NET 10.0** - Modern .NET framework
- **ASP.NET Core Blazor Server** - Interactive server-side web framework
- **Entity Framework Core 10.0.1** - ORM for database operations
- **SQLite** - Lightweight, embedded database

### Frontend
- **Blazor Razor Components** - Component-based UI framework
- **Bootstrap 5** - Responsive CSS framework for mobile-friendly design
- **CSS Custom Properties** - Modern CSS variables for consistent theming
- **JavaScript** - Smooth scrolling and interactive features

### Architecture
- **Component-Based Architecture** - Reusable Razor components
- **Service Layer Pattern** - Business logic separation via `IDayPlanService`
- **Repository Pattern** - Data access abstraction through Entity Framework
- **Dependency Injection** - Built-in .NET DI container

## 📁 Project Structure

```
dayPlanner/
├── Components/
│   ├── Layout/          # Main layout components (NavMenu, MainLayout)
│   ├── Pages/           # Page components (Index, Today, EditPlan)
│   └── Shared/          # Reusable components (ActivityCard, Forms)
├── Data/
│   ├── Entities/        # Domain models (DayPlan, Activity, Pupil)
│   ├── ApplicationDbContext.cs
│   └── DataSeeder.cs    # Database seeding
├── Services/
│   ├── IDayPlanService.cs
│   └── DayPlanService.cs # Business logic
├── wwwroot/
│   ├── app.css          # Global styles and theming
│   └── js/
│       └── smooth-scroll.js
└── Program.cs           # Application entry point
```

## ✨ Key Features

### 1. **Home Page (Landing)**
- Three-section layout: Introduction, Services, About the Project
- Smooth scrolling navigation
- Child-friendly, cheerful design
- Responsive mobile layout

### 2. **Today Page**
- Visual display of today's activities
- Current activity highlighting
- Mark activities as complete
- Completion celebration with trophy 🏆
- "Plan Your Next Day!" button

### 3. **Edit Plan Page**
- Create and edit daily plans
- Add, edit, and delete activities
- Visual activity cards with icons and colors
- Time-based activity scheduling
- Drag-and-drop ordering support

### 4. **Navigation**
- Bootstrap responsive navbar
- Mobile-friendly hamburger menu
- Smooth scroll to sections
- Fixed top navigation

## 🎨 Design Principles

- **Child-Friendly**: Cheerful tone, emojis, encouraging messages
- **Visual**: Icon-based activities, color-coded schedules
- **Accessible**: Clear structure, predictable navigation
- **Responsive**: Works seamlessly on desktop, tablet, and mobile
- **Modern**: Clean UI with custom color scheme

## 🚀 Getting Started

### Prerequisites
- .NET 10.0 SDK or later
- A code editor (Visual Studio, VS Code, or Rider)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/MNKanout/dayplanner.git
cd dayplanner
```

2. Restore dependencies:
```bash
dotnet restore
```

4. Run the application:
```bash
dotnet run
```

5. Open your browser and navigate to:
```
http://localhost:5259
```

### Database

The application uses SQLite, which is automatically created and migrated on first run. Demo data is seeded automatically via `DataSeeder`.

## 🎨 Color Scheme

- **Primary Color**: `#0EA5E9` (Sky Blue)
- **Secondary Color**: `#FF8C42` (Orange)
- **Success Color**: `#10B981` (Green)
- **Background**: White with light gray accents
- **Text**: Dark gray/black for readability

## 📱 Responsive Design

The application is fully responsive with breakpoints at:
- **Desktop**: > 768px
- **Tablet**: 481px - 768px
- **Mobile**: ≤ 480px

## 🔧 Development

### Key Components

- **NavMenu.razor**: Bootstrap-based responsive navigation
- **Today.razor**: Today's schedule display with completion tracking
- **EditPlan.razor**: Plan creation and editing interface
- **ActivityCard.razor**: Reusable activity display component
- **DayPlanService**: Business logic for day plans and activities

### Adding New Features

1. Create new Razor components in `Components/Pages/` or `Components/Shared/`
2. Add routes in `Routes.razor` if needed
3. Extend `IDayPlanService` for new business logic
4. Update database context if new entities are needed

## 📝 License

This project was developed for Statped, a Norwegian national service for special educational needs.

## 👥 Credits

Developed for **Statped** - Norwegian national service supporting children and pupils with special educational needs.

---

**Note**: This application is designed with accessibility and universal design principles in mind, making it suitable for children with various neurodevelopmental needs.
