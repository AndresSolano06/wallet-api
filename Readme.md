# Wallet API - Prueba Técnica Backend Developer

API REST desarrollada en .NET 8 con Clean Architecture para gestionar billeteras digitales, movimientos y autenticación con JWT. Cumple con todos los requisitos establecidos en la prueba técnica.

---

## 🌐 Tecnologías usadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- xUnit + Moq + FluentAssertions
- JWT para autenticación segura
- Swagger para documentación interactiva

---

## ✅ Funcionalidades completadas

- Crear billetera (`POST /api/wallets`) 🔐 Solo Admin
- Obtener billetera por ID (`GET /api/wallets/{id}`) 🔐
- Recargar billetera (`POST /api/wallets/{id}/recharge`) 🔐 Solo Admin
- Transferir saldo entre billeteras (`POST /api/transactions/transfer`) 🔐
- Obtener historial de movimientos (`GET /api/transactions?walletId=`) 🌐 Público
- Login de usuario con JWT (`POST /api/auth/login`) 🌐 Público
- Autenticación con JWT funcional con uso de roles (`Admin`, `User`)

---

## 🔐 Seguridad con JWT

- Implementación completa de autenticación con tokens JWT firmados
- Middleware configurado para validar emisor, audiencia y firma
- Botón "Authorize" funcional en Swagger para pegar tokens JWT
- Usuarios ficticios sembrados en memoria para pruebas

### 🔐 Credenciales de prueba

```json
{
  "username": "admin",
  "password": "admin123"
}
```

### 🔒 Endpoints protegidos con `[Authorize]`
- `POST /api/wallets` (solo Admin)
- `GET /api/wallets/{id}`
- `POST /api/wallets/{id}/recharge` (solo Admin)
- `POST /api/transactions/transfer`

### 🌐 Endpoints públicos con `[AllowAnonymous]`
- `GET /api/transactions?walletId=`
- `POST /api/auth/login`

---

## 🧪 Pruebas unitarias

- Ejecutadas con `xUnit`, `Moq` y `FluentAssertions`

```bash
dotnet test Wallet.Tests --logger "console;verbosity=detailed"
```

### Handlers testeados:
- Transferencias con saldo suficiente e insuficiente
- Creación de billeteras con y sin datos válidos
- Obtención de billeteras por ID (existente / no existente)
- Recarga de billeteras (validación de monto, billetera inexistente, éxito)

Logs detallados dentro de los tests para validar estados y excepciones.

---

## 📄 Cómo ejecutar el proyecto

### 1. Clonar el repositorio
```bash
git clone https://github.com/AndresSolano06/wallet-api.git
cd WalletSolution
```

### 2. Restaurar paquetes
```bash
dotnet restore
```

### 3. Aplicar migraciones y crear la BD
```bash
dotnet ef database update --project Wallet.Infrastructure --startup-project Wallet.API
```

### 4. Ejecutar la API
```bash
dotnet run --project Wallet.API
```

### 5. Acceder a Swagger
```
http://localhost:<puerto>/swagger
```

### 6. Obtener token de autenticación
1. Ir a `POST /api/auth/login`
2. Usar las credenciales de prueba
3. Copiar el token JWT del `response`
4. Presionar 🔐 Authorize en Swagger
5. Pegar el token con el formato:
```
Bearer eyJhbGciOi...
```

---

## 📦 Estructura del proyecto

```
WalletSolution/
├── Wallet.API/              // Controladores y configuración Swagger + JWT
├── Wallet.Application/      // Casos de uso, comandos, validaciones, handlers
├── Wallet.DomainLayer/      // Entidades del dominio
├── Wallet.Infrastructure/   // EF Core, DbContext, Repositorios
├── Wallet.Tests/            // Tests unitarios
└── WalletSolution.sln       // Solución principal
```

---

## 📁 Otros archivos importantes

- `.gitignore` preparado para entorno .NET
- `README.md` con instrucciones completas
- Swagger con JWT configurado visualmente
- Semilla de usuarios embebida (`UserSeed.cs`)

---

## 📅 Estado de la entrega

✅ Entrega completada 100% conforme a los requisitos del documento oficial de la prueba técnica
✅ Todos los endpoints, validaciones y pruebas implementadas
✅ Seguridad con JWT y Swagger integrados
✅ Recarga de billeteras agregada como funcionalidad adicional

---

## 👨‍💻 Autor

Desarrollado por **Andrés Camilo Solano Pantoja**

Prueba Técnica - Backend Developer en .NET

Para cualquier duda técnica, puedes contactarme.

