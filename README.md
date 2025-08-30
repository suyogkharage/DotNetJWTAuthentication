# JWT Authentication with ASP.NET Core API and Razor Pages Client

This repository demonstrates a complete **JWT Authentication workflow** using two separate projects:

1. **API Project (Server-Side Authentication)** – Issues JWT tokens and protects endpoints.
2. **Razor Pages Web App (Client-Side)** – Authenticates against the API and consumes protected endpoints.

---

## 🔑 Basics of JWT

A **JSON Web Token (JWT)** is a compact, URL-safe token used to securely transmit information between parties.  
It consists of **three parts**:

1. **Header** – Algorithm and token type.
2. **Payload** – Contains claims (e.g., username, role).
3. **Signature** – Verifies the token wasn’t tampered with, using a secret key.

When a client logs in with valid credentials, the server generates a JWT.  
This token is then sent with every protected API request in the `Authorization` header:

```
Authorization: Bearer <your_token_here>
```

---

## 📂 Project Structure

```
/JwtApi                → ASP.NET Core Web API (server-side)
    /Controllers       → Contains Login and Secure endpoints
    /Models            → Data models and DTOs
    /Program.cs        → Configures authentication and authorization

/WebAppWithJWT         → Razor Pages Web Application (client-side)
    /Pages             → Contains UI pages (Login, SecurePage, etc.)
    /Program.cs        → Configures Razor Pages and authentication handling
```

---

## 🔄 Project Flow

1. **User Login**
   - The user enters their credentials on the **Razor Pages Login Page**.
   - The Web App sends these credentials to the API’s `/login` endpoint.

2. **JWT Token Generation**
   - The API validates credentials.
   - If valid, it generates a JWT containing claims (username, role, etc.).
   - The token is sent back to the client (Razor Pages app).

3. **Token Storage**
   - The Razor Pages app stores the JWT in **session storage** (or cookies/local storage, depending on your choice).

4. **Accessing Protected Resources**
   - When the user navigates to a protected page (e.g., SecurePage), the Web App includes the JWT in the request headers.
   - Example: `Authorization: Bearer <token>`

5. **Authorization Check**
   - The API middleware checks if the request has a valid token.
   - If roles are specified (`[Authorize(Roles="Admin")]`), it also validates the user’s role claim.
   - If valid, the secure data is returned to the client.

---

## 🚀 How to Run

### 1. Clone the Repository


### 2. Run the API Project
- Open `/APIWithJWT` in Visual Studio or VS Code.
- Run the project.
- The base URL will look like:
  ```
  https://localhost:5001  (HTTPS)
  http://localhost:5000  (HTTP)
  ```

### 3. Run the Razor Pages Web App
- Open `/WebAppWithJWT`.
- Update the API Base URL in the HttpClient configuration (usually `Program.cs`).
- Run the project.

### 4. Test the Flow
1. Open the Razor Pages app in your browser.
2. Navigate to `/Login`.
3. Enter valid credentials.
4. On successful login, the JWT will be stored in session.
5. Navigate to `/SecurePage` → The page should display secure data from the API.

---

## ✅ Key Notes
- Without a token → API returns **401 Unauthorized**.
- With an invalid/expired token → API returns **401 Unauthorized**.
- With a valid token but missing roles → API returns **403 Forbidden**.
- With a valid token and correct roles → API allows access.

---

## 📌 Summary

This solution demonstrates a **full JWT Authentication setup** with:
- **Server (API)** → Issues and validates JWT.
- **Client (Razor Pages)** → Consumes API securely using the JWT.

You can extend this project by:
- Adding **refresh tokens** for long-lived sessions.
- Storing tokens in **cookies with HttpOnly flag** for better security.
- Implementing **role-based menus** on the UI.
