# Wallet API - Prueba Técnica Backend Developer

API REST desarrollada en .NET 8 con Clean Architecture para gestionar billeteras digitales y realizar transferencias entre ellas.

---

## 🌐 Tecnologías usadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (LocalDB)
- xUnit + Moq + FluentAssertions
- JWT (Autenticación, próximamente)

---

## 📋 Requisitos funcionales implementados

- [x] Crear billetera (`POST /api/wallets`)
- [x] Obtener billetera por ID (`GET /api/wallets/{id}`)
- [x] Transferir saldo entre billeteras (`POST /api/transactions/transfer`)
- [x] Obtener historial de transacciones (`GET /api/transactions?walletId=`)
- [ ] Autenticación con JWT (en progreso)

---

## ✅ Validaciones incluidas

- Monto de transferencia debe ser mayor que 0
- No se permite transferir a la misma billetera
- Saldo insuficiente lanza error
- Nombre y documento obligatorios al crear billetera
- Manejo de errores HTTP:
  - `400 BadRequest` para entradas inválidas
  - `404 NotFound` si la billetera no existe
  - `422 UnprocessableEntity` para saldo insuficiente

---

## ⚙️ Arquitectura

- Clean Architecture
- Separación por capas:
  - `DomainLayer`: entidades puras
  - `Application`: casos de uso / servicios / validaciones
  - `Infrastructure`: EF Core / Repositorios
  - `API`: Controladores y configuración

---

## 🧪 Cómo correr el proyecto

1. Clona el repositorio:
```bash
git clone <repo-url>
```

2. Accede al proyecto:
```bash
cd WalletSolution
```

3. Restaura dependencias:
```bash
dotnet restore
```

4. Aplica migraciones:
```bash
dotnet ef database update --project Wallet.Infrastructure --startup-project Wallet.API
```

5. Corre la API:
```bash
dotnet run --project Wallet.API
```

6. Accede a Swagger:
```
http://localhost:<puerto>/swagger
```

---

## 🧪 Ejecutar los tests

```bash
dotnet test Wallet.Tests --logger "console;verbosity=detailed"
```

Se incluyen tests unitarios para:
- `TransferHandler`
- `CreateWalletHandler`
- `GetWalletHandler`

Próximamente: tests de integración completos.

---

## 🔐 Autenticación (en progreso)

Se añadirá soporte para JWT con login de usuario y protección de endpoints según roles. 
Endpoints públicos:
- `GET /transactions` (solo lectura)

Endpoints protegidos:
- `POST /wallets`, `POST /transactions/transfer`, etc.

---

## 📄 Endpoints principales

### Wallets
- `POST /api/wallets`
- `GET /api/wallets/{id}`

### Transactions
- `POST /api/transactions/transfer`
- `GET /api/transactions?walletId=1`

---

## 📌 Consideraciones

- Lógica de negocio probada y validada
- Código desacoplado y escalable
- Extensible para logging, filtros, autenticación, etc.

---

## ⏱️ Tiempo de entrega

- Entregado dentro del tiempo establecido (24h)

---

## 👨‍💻 Autor

Desarrollado por **Andrés Camilo Solano Pantoja** para la prueba técnica de Backend Developer.

Para dudas técnicas, contactar sin problema.

