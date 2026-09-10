# 🏢 Employee Management & Leave Tracking System

An enterprise-grade **.NET / C#** Web API and Management System designed to streamline HR operations, track employee performance, manage leave workflows with approvals, handle automated payroll notifications, and maintain organization-wide event scheduling.

---

## 👥 Role-Based Access Panels

The application provides tailored control panels for different user roles:

* **User (Employee) Panel:**
  * View personal profile, department, and designation details[cite: 4, 5, 6].
  * Submit leave applications with custom durations, descriptions, and file attachments[cite: 8].
  * Track leave request status (Pending, Approved, Rejected) in real-time[cite: 8].
  * Receive automated email notifications when salary is disbursed by the Admin.
  * Access the Monthly Event Calendar to view upcoming holidays and company events.

* **HR Panel:**
  * Review, approve, or reject employee leave requests with tracked audit trails[cite: 8, 12].
  * Manage employee directory, profiles, and contact details[cite: 6].
  * Maintain dynamic system lookup configurations (Leave Types, System Codes)[cite: 8, 9, 10, 11].
  * Schedule and publish company-wide events on the monthly calendar.

* **Admin Panel:**
  * Complete control over system master data (Departments, Designations, Countries, Cities, Banks)[cite: 1, 2, 3, 4, 5].
  * Disburse monthly salaries and trigger automated email alerts to employees.
  * Role management and access controls for User, HR, and Admin privileges.
  * System-wide audit logs and activity tracking via `UserActivity` and `ApprovalActivity` entities[cite: 12].

---

## ✨ Key Features

* **Role-Based Access Control (RBAC):** Distinct dashboards for Users, HR, and Admins.
* **Automated Salary Email Alerts:** Real-time email notifications sent to employees as soon as salary is processed by Admin.
* **Leave Management Workflow:** Complete application cycle with multi-tier status updates and document attachment support[cite: 8].
* **Monthly Event Calendar:** Interactive calendar for company events, holidays, and schedule management.
* **Employee Profiling:** Comprehensive records including full names, contact info, departments, designations, and location mapping[cite: 6].
* **Organization Hierarchy:** Configurable master data for Departments, Designations, Banks, Countries, and Cities[cite: 1, 2, 3, 4, 5].
* **System Codes & Lookups:** Flexible lookup tables (`SystemCode` & `SystemCodeDetails`) for custom categories and options[cite: 10, 11].
* **Audit & Compliance:** Built-in tracking for creation, modification, and approval history (`CreatedBy`, `ApprovedBy`, timestamps)[cite: 12].

---

## 🛠️ Tech Stack

* **Framework:** .NET / C#
* **Architecture:** MVC / Web API (Domain-Driven Models)
* **Database:** Entity Framework Core (ORMs / Relational Models)
* **Email Service:** SMTP / Email API Integration for Salary Alerts
* **Authentication & Authorization:** Role-Based Security (User, HR, Admin)

---

## 📁 Core Domain Entities

| Entity | Description |
| :--- | :--- |
| `Employee` | Holds employee demographics, contact info, department, and designation[cite: 6]. |
| `LeaveApplication` | Tracks leave requests, attached files, durations, and approval statuses[cite: 8]. |
| `Department` & `Designation` | Defines company structure and employee roles[cite: 4, 5]. |
| `Bank`, `Country`, `City` | Master lookup structures for localized data[cite: 1, 2, 3]. |
| `SystemCode` & `Details` | Configurable system options for dynamic dropdowns and statuses[cite: 10, 11]. |
| `UserActivity` & `ApprovalActivity` | Base models providing audit fields for creation, modification, and approvals[cite: 12]. |

---

## 🚀 Getting Started

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 / 8.0 or higher)
* Visual Studio 2022 or VS Code
* SQL Server or compatible database engine

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/Sana-Noor9/AI_GetAPI.git](https://github.com/Sana-Noor9/AI_GetAPI.git)
   cd AI_GetAPI
