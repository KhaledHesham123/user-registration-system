 (.NET 9)
 
## Architecture & Design Patterns

### 1. Vertical Slice Architecture
The project follows **Vertical Slice Architecture** instead of traditional layering. 

### 2. CQRS & MediatR
Implemented **CQRS (Command Query Responsibility Segregation)** to separate read and write operations:
- **Commands:** (e.g., `CreateUserCommand`) for handling data changes.
- **Queries:** (e.g., `GetUserByEmailQuery`) for retrieving data.
- **Orchestration:** Used an **Orchestrator** to manage complex workflows, ensuring the system remains decoupled and logic is easy to follow.

### 3. MediatR Pipeline Behaviors (Cross-Cutting Concerns)
- **Transaction Behavior:** Automatically wraps requests in a database transaction, ensuring a full **rollback** if any part of the process fails (Atomicity).
- **Validation Behavior:** Integrated with **FluentValidation** to perform automatic request validation before reaching the logic handler.

### 4. Robust Error Handling
- **Global Exception Middleware:** A centralized middleware handles all application exceptions, logging details and returning consistent, professional JSON responses to the client.

---

##  How to Run (Zero-Touch Setup)

This project is designed for a "Plug and Play" experience.

1.  **Clone the repository.**
2.  **Database Configuration:** - Open `appsettings.json`.
    - Ensure the `DefaultConnection` string points to your local SQL Server instance.
3.  **Run & Auto-Migrate:** - Simply press **F5** or run `dotnet run`.
   
4.  **Accessing the Application:**
    - The API serves static files. The frontend will be accessible at:
      `https://localhost:7074/index.html` (or your local port).

---

##  Note for Reviewers

I focused on delivering a clean, functional, and well-architected solution that meets the specific task requirements. However, I have extensive experience in building more complex systems, including:
- **Identity & Security:** Implementing JWT Authentication, Refresh Tokens.
- **Authorization:** Advanced **Dynamic RBAC** (Role-Based Access Control) and Permission-based policies.

I would be delighted to discuss these advanced implementations or any further requirements during the interview.

---
*Developed by Khaled Hesham Hashem*
