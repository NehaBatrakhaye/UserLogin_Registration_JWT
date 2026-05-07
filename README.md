# JWT Authentication & User Management API

A secure ASP.NET Core Web API project implementing JWT Authentication, User Registration, Login, and CRUD operations using Entity Framework Core and SQL Server.

---

## 🚀 Features

- User Registration
- User Login Authentication
- JWT Token Generation
- Authorization using JWT Bearer Token
- Protected APIs using `[Authorize]`
- CRUD Operations
- Entity Framework Core Integration
- SQL Server Database
- Clean Layered Architecture

---

## 🛠️ Technologies Used

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / Scalar API Testing

---

## 📂 Project Structure

```bash
Controllers/
Data/
Entities/
Migrations/
Model/
Services/
Program.cs


🔐 Authentication Flow
1. User registers using Register API
2. User logs in with Email & Password
3. JWT Token is generated
4. Token is passed in Authorization Header
5. Protected APIs become accessible


📌 API Endpoints
Authentication

| Method | Endpoint             | Description   |
| ------ | -------------------- | ------------- |
| POST   | `/api/Auth/register` | Register User |
| POST   | `/api/Auth/login`    | Login User    |


User CRUD

| Method | Endpoint                    | Description    |
| ------ | --------------------------- | -------------- |
| GET    | `/api/UserController1`      | Get All Users  |
| GET    | `/api/UserController1/{id}` | Get User By Id |
| PUT    | `/api/UserController1/{id}` | Update User    |
| DELETE | `/api/UserController1/{id}` | Delete User    |


🔑 JWT Configuration

Add JWT settings in appsettings.json

"Jwt": {
  "Key": "YourSecretKey",
  "Issuer": "YourIssuer",
  "Audience": "YourAudience"
}


🧪 API Testing

You can test APIs using:

Scalar




