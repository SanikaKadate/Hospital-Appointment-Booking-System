# 🏥 MedFlow – Appointment Booking System using C# WinForms, WebView2 and SQL Server

## 📌 Project Overview

The **MedFlow Appointment Booking System** is a Windows desktop application built with C# WinForms and Microsoft WebView2. It hosts a modern HTML, CSS and JavaScript interface inside a desktop window and saves patient appointments into a Microsoft SQL Server LocalDB database.

The project combines a web-based user interface with a C# backend, allowing patients to book appointments and doctors to view clinic activity through role-based dashboards.

## 🎯 Project Objectives

- Provide a role-based login portal for doctors and patients.
- Allow patients to book appointments with a selected specialist.
- Capture patient name, doctor, appointment date and preferred time.
- Save booking details to SQL Server using parameterized queries.
- Display appointment records in a table with a cancel option.
- Show clinic analytics such as total, pending and cancelled appointments.
- Demonstrate communication between JavaScript and C# using WebView2.

## 🛠️ Tools and Technologies

| Tool | Purpose |
|---|---|
| C# (.NET WinForms) | Desktop application shell and backend logic |
| Microsoft WebView2 | Embedding the web interface inside the desktop app |
| HTML5, CSS3, JavaScript | User interface and client-side logic |
| Bootstrap 5.3 | Responsive layout and styling |
| Font Awesome 6 and Animate.css | Icons and animations |
| Microsoft SQL Server LocalDB | Database storage |
| Microsoft.Data.SqlClient | Database connectivity from C# |
| Visual Studio 2022 or later | Development environment |
| GitHub | Project documentation and version control |

## 🗄️ Database Design

The project uses a SQL Server LocalDB database named `Appointments`.

It contains one main table:

### AppointBooking

Stores appointment booking records.

**Columns:**
- `Id` — Primary key (auto increment)
- `PatientName` — Name of the patient
- `Physician` — Selected doctor
- `Date` — Appointment date
- `Time` — Preferred appointment time

**Available doctors:** Dr. Sharma, Dr. Patel.

### Database Setup Script

```sql
CREATE DATABASE Appointments;
GO

USE Appointments;
GO

CREATE TABLE AppointBooking (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    PatientName NVARCHAR(100) NOT NULL,
    Physician   NVARCHAR(100) NOT NULL,
    [Date]      NVARCHAR(20)  NOT NULL,
    [Time]      NVARCHAR(20)  NOT NULL
);
GO
```

### Connection String

```csharp
Server=(LocalDB)\MSSQLLocalDB;Initial Catalog=Appointments;Integrated Security=True;TrustServerCertificate=True
```

## 💻 Application Interface

The interface is built in `index.html` and loaded inside the WebView2 control.

### User Roles

| Role | Email | Password |
|---|---|---|
| Doctor | `doctor@med.com` | `MedFlow1234` |
| Patient | `patient@med.com` | `MedFlow1234` |

### Application Pages

#### 1. Portal Login
Login page with email and password validation and error messages.

#### 2. Patient Dashboard
- New Appointment
- My Health Records
- Logout

#### 3. Doctor Dashboard
- Today's Patients
- Clinic Analytics
- Logout

#### 4. Book New Visit
Form to enter:
- Patient name
- Specialist
- Date
- Preferred time

#### 5. Appointments Page
Table showing patient, doctor, date, time and a cancel action.

#### 6. Practice Overview
Statistics cards showing:
- Total Appointments
- Pending Visits
- Cancelled Appointments

## 🔗 How It Works

1. The application starts and `Form1` maximizes the window.
2. WebView2 is initialized and navigates to `index.html` in the application startup folder.
3. When the user confirms a booking, JavaScript sends a message to C#:

```javascript
window.chrome.webview.postMessage("save_booking");
```

4. C# receives the message in the `WebMessageReceived` event.
5. C# reads the form values (`pname`, `doctor`, `date`, `time`) from the page using `ExecuteScriptAsync`.
6. The values are inserted into the `AppointBooking` table using a parameterized SQL query.
7. A success or error alert is displayed in the web page.

## 📁 Project Structure

```text
C_project2/
│
├── README.md
│
├── C_project2.slnx
│
├── C_project2.csproj
│
├── Program.cs
│
├── Form1.cs
│
├── Form1.Designer.cs
│
├── Form1.resx
│
└── index.html
```

**File descriptions:**

- `README.md` — Project overview, database details and setup guide.
- `C_project2.slnx` — Visual Studio solution file.
- `C_project2.csproj` — Project configuration and package references.
- `Program.cs` — Application entry point.
- `Form1.cs` — WebView2 initialization, JavaScript to C# messaging and database insert logic.
- `Form1.Designer.cs` — Designer code for the WebView2 control.
- `Form1.resx` — Form resources.
- `index.html` — Front-end user interface (login, dashboards, booking, appointments and analytics).

## 🚀 How to Run the Project

### Step 1: Clone the Repository

Open a terminal and execute:

```bash
git clone <your-github-repository-url>
```

Navigate to the project directory:

```bash
cd C_project2
```

### Step 2: Install Prerequisites

- Windows 10 or 11
- Visual Studio 2022 or later with the **.NET desktop development** workload
- WebView2 Runtime
- SQL Server Express LocalDB

### Step 3: Restore NuGet Packages

```bash
dotnet add package Microsoft.Web.WebView2
dotnet add package Microsoft.Data.SqlClient
```

You can also restore packages directly from Visual Studio.

### Step 4: Create the Database

1. Open SQL Server Management Studio (SSMS) or Visual Studio SQL Server Object Explorer.
2. Connect to `(LocalDB)\MSSQLLocalDB`.
3. Execute the database setup script given above.
4. Verify that the `Appointments` database and `AppointBooking` table are created.

### Step 5: Configure index.html

Right-click `index.html` in Solution Explorer, open **Properties** and set **Copy to Output Directory** to **Copy if newer**. This makes the file available at `Application.StartupPath`.

### Step 6: Run the Application

Press **F5** in Visual Studio to build and run the project.

### Step 7: Verify the Data

After booking an appointment, run:

```sql
USE Appointments;
GO

SELECT * FROM dbo.AppointBooking;
```

## 🔍 Data Validation

The application includes the following checks:

- Required fields (date, time and patient name) are validated before booking.
- Parameterized SQL queries are used to prevent SQL injection.
- Database errors are caught and shown to the user.
- Insert success is confirmed using the number of affected rows.

## 📈 Key Learning Outcomes

Through this project, I practiced:

- Building a Windows desktop application using C# WinForms.
- Embedding a web interface using WebView2.
- Communicating between JavaScript and C# using web messages.
- Connecting a C# application to SQL Server LocalDB.
- Writing parameterized SQL INSERT queries.
- Designing responsive pages with Bootstrap.
- Creating role-based dashboards and statistics cards.

## 🔮 Future Enhancements

Potential improvements include:

- Replacing hard-coded login with database-based authentication and hashed passwords.
- Loading appointments and analytics from SQL Server instead of browser `localStorage`.
- Adding edit and cancel appointment support that updates the database.
- Preventing duplicate or overlapping appointment slots.
- Using proper `DATE` and `TIME` column types.
- Moving the connection string to a configuration file.
- Adding more doctors and specialties from the database.
- Adding email or SMS appointment reminders.

## 👨‍💻 Project Information

**Project Name:** MedFlow – Appointment Booking System

**Domain:** Healthcare and Appointment Management

**Database:** Appointments

**Database Technology:** Microsoft SQL Server LocalDB

**Application Type:** Windows Desktop Application (WinForms + WebView2)

**Programming Languages:** C#, HTML, CSS, JavaScript and SQL

**Project Type:** Desktop Application Development
⭐ If you find this project useful, consider giving the repository a star.

**Thank you for visiting my project!**
