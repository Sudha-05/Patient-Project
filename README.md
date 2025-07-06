# 🏥 Patient Management System

A full-stack Patient Management System built using **ASP.NET Core MVC 8.0** and the **Onion architecture**. This system enables secure and efficient management of patients, doctors, lab tests, appointments, and user roles for healthcare institutions.

> Developed and customized by **Sudharani** — Software Engineer with expertise in .NET, Azure, and full-stack development.

---

## 🔐 Authentication & Role-Based Access

- Secure login for all users
- Admin registration with hashed password storage
- Role-based dashboards for **Administrators** and **Assistants**

---

## 🧑‍⚕️ Functional Modules

### 🔹 User Management (Admin)
- Create, edit, delete users
- Enforced validations and password confirmations

### 🔹 Doctor Management (Admin)
- Manage doctor profiles with image uploads
- Includes name, email, phone, specialization

### 🔹 Laboratory Tests (Admin)
- Add, update, or delete lab test definitions
- Form validation to ensure test completeness

### 🔹 Patient Records (Assistant)
- Manage detailed patient profiles (DOB, contact, history)
- CRUD operations with proper form validation

### 🔹 Laboratory Results (Assistant)
- Link test results to patients
- Mark results as completed and generate reports

### 🔹 Appointment Scheduling (Assistant)
- Create and manage appointments
- Associate appointments with both patients and doctors

---

## 💻 Technologies Used

| Layer          | Technology                        |
|----------------|------------------------------------|
| Frontend       | HTML, CSS, Bootstrap, Razor Pages |
| Backend        | ASP.NET Core MVC 8.0, C#          |
| ORM/DB Access  | Entity Framework Core             |
| Database       | SQL Server                        |
| Architecture   | Onion / Clean Layered Architecture|

---

## 🚀 Getting Started

### ✅ Prerequisites
- Visual Studio 2022+ with ASP.NET Core SDK 8.0
- SQL Server (LocalDB or full instance)

### 🛠️ Setup Instructions
1. Clone this repository or download the ZIP
2. Open the `.sln` file in Visual Studio
3. Update your connection string in `appsettings.json`
4. Open **Package Manager Console** and run:

5. Run the project and open it in your browser

---

## 🙋‍♀️ Developed By

**Sudharani**  
[LinkedIn](https://www.linkedin.com/in/sudharani05/) • [GitHub](https://github.com/Sudha-05)

---

## 📌 Future Enhancements

- Azure AD / JWT Authentication
- Azure DevOps CI/CD pipeline
- PDF report generation for lab results
- Role-based access refinement with policy-based auth
- Azure App Service deployment

---



