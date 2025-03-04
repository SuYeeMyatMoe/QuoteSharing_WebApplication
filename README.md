# **Quote Sharing Blog**

## **Overview**

The **Quote Sharing Blog** is a web application built using **ASP.NET Core MVC** and **ADO.NET** for data access. Users can **add, view, edit, delete, and search quotes**. Quotes are stored in a database, and the application features a clean, blue-and-white theme. Additionally, the app includes **user authentication features**, such as **Login, Signup, Session Management, and Profile Settings**.

---

## **Features**

### **Quote Management**
- **View Quotes**: Displays a list of all quotes with the quote writer, quote text, and uploaded email.
- **Add New Quote**: Users can add new quotes including the writer's name, quote text, and uploaded email.
- **Edit Quote**: Edit details of existing quotes.
- **Delete Quote**: Remove quotes from the list.
- **Search**: A search feature to filter quotes by quote writer, text, or email.
- **Responsive Design**: The app is mobile-friendly and adapts to different screen sizes.
- **ADO.NET**: Utilizes ADO.NET for database connection and CRUD operations (Create, Read, Update, Delete).
- **MVC Architecture**: The app follows the MVC (Model-View-Controller) pattern for separation of concerns.

### **User Authentication & Session Management**
- **Signup**: New users can register an account.
- **Login**: Existing users can log in to access personalized features.
- **Session Management**: Users remain logged in after authentication.
- **Profile Settings**: Users can view and update their profile, including changing their password.
- **Logout**: Users can securely log out of their session.

---

## **Installation**

### **Prerequisites**
To run the project locally, ensure you have the following installed:
- **.NET SDK** (Core 3.1 or higher)
- **Visual Studio** or **Visual Studio Code**
- **SQL Server** or another database system

### **Database Setup**
1. Open **SQL Server Management Studio (SSMS)**.
2. Create a new database named `QuoteSharing_DB`.

```sql
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL
);

CREATE TABLE Tbl_QuoteSharing (
    QuoteID INT PRIMARY KEY IDENTITY,
    QuoteWriter NVARCHAR(100) NOT NULL,
    QuoteText NVARCHAR(MAX) NOT NULL,
    UploadedEmail NVARCHAR(255) UNIQUE NOT NULL,
    IsDeleted BIT DEFAULT 0
);
```

### **Configuration**
1. Open `Dbhelper.cs` and configure the connection string:

```json
"ConnectionString": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=QuoteSharing_DB;Trusted_Connection=True;"
}
```

Replace `YOUR_SERVER` with your actual **SQL Server instance name**.

### **Run the Application**
1. Open the project in **Visual Studio**.
2. Restore dependencies using `dotnet restore`.
3. Run the application using `dotnet run` or press `F5` in Visual Studio.

---

## **Usage**

### **Signup and Login**
1. **Signup**
   - Navigate to `/Account/Signup`.
   - Enter a **username**, **email**, and **password**.
   - Click **Sign Up** to create an account.
2. **Login**
   - Navigate to `/Account/Login`.
   - Enter your **email** and **password**.
   - Click **Login** to access your account.

### **Quote Management**
- After logging in, users can **add, edit, delete, and search** for quotes.
- The uploaded quotes are associated with the logged-in user.

### **Profile Settings**
- Navigate to `/Account/ProfileSettings`.
- Users can update their password.

### **Logout**
- Click the **Logout** button in the navigation bar to securely log out.

---

## **Technologies Used**
- **ASP.NET Core MVC**
- **ADO.NET** (for database connectivity)
- **Bootstrap** (for responsive UI)
- **SQL Server** (for data storage)
- **Session Management** (for user authentication)

---

## **Contributing**
Feel free to contribute by submitting pull requests or reporting issues.

---

## **License**
This project is licensed under the **MIT License**.

---


