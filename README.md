# Product Barcode Management System

A full-stack CRUD application for managing products with barcode functionality. Built with **C# .NET Core** backend and **Vue.js** frontend.

---

## 📋 Project Overview

This is a test project demonstrating a complete CRUD (Create, Read, Update, Delete) system for managing products with barcode support. The application consists of:

- **Backend**: RESTful API built with .NET Core
- **Frontend**: Modern single-page application built with Vue.js
- **Purpose**: Interview demonstration project

---

## 🚀 Live Demo

| Component | URL |
|-----------|-----|
| **Frontend** | [https://frontendtest1-iop8qgcv0-davidpus-projects.vercel.app/](https://frontendtest1-iop8qgcv0-davidpus-projects.vercel.app/) |
| **Backend API** | [https://backendtest1-4bao.onrender.com/swagger/index.html](https://backendtest1-4bao.onrender.com/swagger/index.html) |

---

## 🏗️ Tech Stack

### Backend
- **Language**: C# (.NET Core)
- **Framework**: ASP.NET Core
- **API Documentation**: Swagger/OpenAPI
- **Hosting**: Render

### Frontend
- **Framework**: Vue.js
- **Hosting**: Vercel
- **Deployment**: Vercel (automated from Git)

---

## ✨ Features

- ✅ **Create** - Add new products with barcode information
- ✅ **Read** - View product details and browse all products
- ✅ **Update** - Modify existing product information
- ✅ **Delete** - Remove products from the system
- ✅ **Barcode Support** - Built-in barcode management for products
- ✅ **API Documentation** - Interactive Swagger UI

---

## 📦 Getting Started

### Prerequisites

- [.NET Core 5.0+](https://dotnet.microsoft.com/download)
- [Node.js 14+](https://nodejs.org/)
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/)

### Backend Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/Enter-In-2-Darkness/backendtest1.git
   cd backendtest1
   ```

2. Install dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

4. Run the API:
   ```bash
   dotnet run
   ```

5. Access Swagger UI at: `http://localhost:5000/swagger/index.html`

### Frontend Setup

1. Clone the frontend repository (or navigate to frontend directory):
   ```bash
   git clone https://github.com/[your-frontend-repo].git
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start development server:
   ```bash
   npm run serve
   ```

4. Open browser to: `http://localhost:8080`

---

## 🔌 API Endpoints

The backend provides the following REST API endpoints:

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/products` | Get all products |
| `GET` | `/api/products/{id}` | Get product by ID |
| `POST` | `/api/products` | Create new product |
| `PUT` | `/api/products/{id}` | Update product |
| `DELETE` | `/api/products/{id}` | Delete product |

**Note**: Full API documentation available at the [Swagger UI](https://backendtest1-4bao.onrender.com/swagger/index.html)

---

## 📁 Project Structure

```
backendtest1/
├── Controllers/        # API controllers
├── Models/            # Data models
├── Services/          # Business logic
├── Properties/        # Project properties
├── appsettings.json   # Configuration
└── Startup.cs         # Application startup
```

---

## 🔧 Configuration

### Backend Configuration
- Update `appsettings.json` to configure database and API settings
- Default port: `5000`

### Frontend Configuration
- Update API base URL in `.env` or environment configuration
- API URL: `https://backendtest1-4bao.onrender.com`

---

## 📝 Usage Example

### Create a Product
```bash
curl -X POST http://localhost:5000/api/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Product Name",
    "barcode": "1234567890",
    "price": 99.99
  }'
```

### Get All Products
```bash
curl http://localhost:5000/api/products
```

---

## 🌐 Deployment

### Backend (Render)
1. Push code to GitHub
2. Connect repository to Render
3. Set environment variables
4. Deploy

### Frontend (Vercel)
1. Push code to GitHub
2. Connect repository to Vercel
3. Configure build settings
4. Automatic deployment on push

---

## 🤝 Contributing

This is a personal interview project. For contributions or improvements, please feel free to fork and submit pull requests.

---

## 📄 License

This project is open source and available under the MIT License.

---

## 👤 Author

**Enter-In-2-Darkness**
- GitHub: [@Enter-In-2-Darkness](https://github.com/Enter-In-2-Darkness)

---

## 📞 Support

For issues or questions:
- Check the [Swagger API Documentation](https://backendtest1-4bao.onrender.com/swagger/index.html)
- Review the code and inline documentation
- Open an issue on GitHub

---

**Last Updated**: May 14, 2026
