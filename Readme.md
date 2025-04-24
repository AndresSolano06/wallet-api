# 💰 Wallet API - Prueba Técnica Backend Developer

API REST desarrollada en .NET 8 siguiendo el patrón de arquitectura limpia. Esta aplicación permite la gestión de billeteras digitales y sus movimientos, con autenticación basada en JWT y pruebas automatizadas.

---

## 🚀 Tecnologías utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- ASP.NET Core Web API
- Entity Framework Core (SQL Server e InMemory)
- JWT (Json Web Token)
- xUnit, Moq, FluentAssertions
- Swagger (OpenAPI)
- Clean Architecture

---

## ✅ Funcionalidades

| Endpoint                                     | Método | Requiere Auth | Roles Permitidos | Estado |
|----------------------------------------------|--------|----------------|------------------|--------|
| `/api/wallets`                                | POST   | ✅              | Admin            | ✅     |
| `/api/wallets/{id}`                           | GET    | ✅              | Admin, User      | ✅     |
| `/api/wallets/{id}/recharge`                  | POST   | ✅              | Admin            | ✅     |
| `/api/transactions/transfer`                  | POST   | ✅              | Admin, User      | ✅     |
| `/api/transactions?walletId={id}`             | GET    | ❌              | Público          | ✅     |
| `/api/auth/login`                             | POST   | ❌              | Público          | ✅     |

---

## 🔐 Autenticación y Autorización

- **JWT configurado con roles (`Admin`, `User`)**
- El token se firma con una clave secreta definida en `appsettings.json`
- Swagger configurado con botón `Authorize` para pruebas

### Usuario de prueba

```json
{
  "username": "admin",
  "password": "admin123"
}
```

> El endpoint `/api/auth/login` genera el token que se debe copiar en Swagger como:  
> `Bearer <token>`

---

## 🧪 Pruebas Automatizadas

- **Unitarias**: Handlers de lógica de negocio
- **Integración**: Endpoints reales usando base de datos en memoria
- **Librerías usadas**: xUnit, Moq, FluentAssertions

### Ejecutar pruebas

```bash
dotnet test Wallet.Tests --logger "console;verbosity=detailed"
```

---

## 🧭 Instrucciones de ejecución

```bash
git clone https://github.com/AndresSolano06/wallet-api.git
cd WalletSolution
dotnet restore
dotnet ef database update --project Wallet.Infrastructure --startup-project Wallet.API
dotnet run --project Wallet.API
```

Luego accede a: [http://localhost:{puerto}/swagger](http://localhost:{puerto}/swagger)

---

## 📁 Estructura del Proyecto

```
WalletSolution/
├── Wallet.API/              # API y configuración principal
├── Wallet.Application/      # Casos de uso (handlers, comandos)
├── Wallet.DomainLayer/      # Entidades de dominio
├── Wallet.Infrastructure/   # Persistencia con EF Core
├── Wallet.Tests/            # Pruebas unitarias e integración
└── WalletSolution.sln       # Archivo de solución
```

---

## 📦 Extras

- `.gitignore` optimizado para proyectos .NET
- Swagger UI con JWT integrado
- Base de datos SQL Server (migraciones incluidas)
- Roles con políticas para endpoints protegidos
- Endpoint extra: `recharge` para asignar saldo a una billetera

---

## ✅ Estado de entrega

- ✔ Todos los requisitos del PDF cumplidos
- ✔ Funcionalidades adicionales agregadas (recarga + roles)
- ✔ Pruebas completas (unitarias + integración)
- ✔ Readme detallado y documentación Swagger incluida

---

## 👨‍💻 Autor

**Andrés Camilo Solano Pantoja**  
[GitHub](https://github.com/AndresSolano06) | [LinkedIn](https://linkedin.com)