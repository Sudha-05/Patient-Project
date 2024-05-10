# Patient Management System

This project is a patient management system built using ASP.NET Core MVC under the Onion architecture. It includes various functionalities for managing users, doctors, laboratory tests, patients, appointments, and laboratory results.

  
## Functionality Overview

### Login

- Users can log in using their credentials.
- New users can register as administrators with unique usernames.
- Passwords are securely hashed and stored.

### Home

- After login, users are directed to the home screen.
- The home screen contains a menu with options based on user roles (Administrator or Assistant).

### User Management (Administrator)

- Administrators can view, create, edit, and delete users.
- User creation includes validation for unique usernames, password confirmation, and required fields.
- Editing user information involves form validation and password management.

### Doctor Management (Administrator)

- Administrators can manage doctors, including creating, editing, and deleting doctor profiles.
- Doctor profiles include information such as name, email, phone number, and a photo upload option.

### Laboratory Test Management (Administrator)

- Administrators can manage laboratory tests, including creating, editing, and deleting tests.
- Tests require a name and are validated for completeness before creation.

### Patient Management (Assistant)

- Assistants can manage patients, including creating, editing, and deleting patient profiles.
- Patient profiles include information such as name, address, phone number, date of birth, and medical history.

### Laboratory Result Management (Assistant)

- Assistants can manage laboratory results, including reporting test results and marking tests as completed.
- Results are associated with patients and tests, allowing for efficient tracking.

### Appointment Management (Assistant)

- Assistants can manage appointments, including creating, editing, and deleting appointments.
- Appointments can be linked to patients and doctors, with options for scheduling and status updates.


## Technologies Used

- **Backend**
  - C# ASP.NET Core MVC (8.0)
  - Microsoft Entity Framework Core (Code First approach)

- **Frontend**
  - HTML
  - CSS
  - Bootstrap
  - ASP.NET Razor

- **ORM**
  - Entity Framework

- **Database**
  - SQL Server

## Getting Started

### Prerequisites
To run this project, you'll need:
- Visual Studio with ASP.NET Core SDK (8 onwards)
- SQL Server

### Installation
1. Clone the repository or download the project.
2. Open the project in Visual Studio.
3. Update the database connection string in `appsettings.json` to match your SQL Server setup.
4. Open Package Manager Console in Visual Studio and run `Update-Database` to apply migrations.
5. Run the project and access it in your browser.

## Developer
- [Federico A. Garcia](https://github.com/AleGxrcia)

