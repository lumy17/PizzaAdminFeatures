# Pizza Ordering App

This project is a pizza management application that allows administrators to manage pizzas, orders, ingredients, members, and coupons. It includes a user interface as well, who can access the pizza list and create a new order.

## Features

**User Features**:
- View a list of all available pizzas with their ingredients and prices
- Place orders for pizzas
- View their order history

**Administrator Features**:
- Manage the pizza menu:
  - Create new pizzas
  - Edit existing pizzas
  - Delete pizzas
- Manage orders:
  - View a list of all orders
  - Search for orders by customer name
  - Edit or delete orders
- Manage ingredients, members, and coupons

**Account Management**:
- Users can update their username, password, and other account details

## Technologies Used

- **Framework**: ASP.NET Core running on runtime .NET Core
- **Language**: C#
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Frontend**: Razor Pages with HTML, CSS, JavaScript
- **Authentication**: Identity Framework
- **Development Environment**: Visual Studio

## Getting Started

1. **Clone the repository**:
   ```bash git clone (https://github.com/lumy17/PizzaAppRazor.git)```
2. **Configure the database**:
- Open the solution in Visual Studio
- Update the connection string in the `appsettings.json` file to point to your SQL Server instance
- Run the database migrations using the Package Manager Console:
  ```
  Add-Migration name
  Update-Database
  
  ```

3. **Build and run the application**:
- Press `F5` in Visual Studio to build and run the application
