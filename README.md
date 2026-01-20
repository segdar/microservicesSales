# microservicesSales
This project example how to integration the most simple DDD in microservices.

Sales Microservice (.NET 8 + DDD)

Este proyecto implementa un microservicio RESTful para la gestión de **Ventas e Inventario**, diseñado bajo los principios de **Domain-Driven Design (DDD)**. 

El objetivo es demostrar cómo manejar lógica de negocio compleja, consistencia transaccional y notificaciones en tiempo real, manteniendo una arquitectura limpia, escalable y desacoplada.

---

## 🛠️ Tecnologías y Patrones

El sistema ha sido construido utilizando las últimas características del ecosistema .NET:

* **Core:** .NET 8 (C# 12)
* **Arquitectura:** N-Layer con enfoque DDD (Domain, Application, Infrastructure, API).
* **Persistencia:** Entity Framework Core (Code First).
* **Transaccionalidad:** Patrón **Unit of Work** y transacciones explícitas para integridad de datos (Atomicidad).
* **Tiempo Real:** WebSockets con **SignalR** para notificaciones de stock.
* **Inyección de Dependencias:** Contenedor nativo de .NET.
* **Containerización:** Docker & Dockerfile optimizado (Multi-stage build).

---

## 🏛️ Arquitectura del Proyecto

El código está organizado para separar responsabilidades y proteger las reglas de negocio:

```text
📂 src
├── 📂 Domain           # Núcleo: Entidades (Product, Sale) y Reglas de Negocio. Sin dependencias externas.
├── 📂 Infrastructure   # Datos: Implementación de Repositorios, EF Core DbContext, UnitOfWork.
├── 📂 Application      # Orquestación: Servicios que coordinan Dominio y Persistencia.
└── 📂 API              # Presentación: Controladores REST y Configuración de SignalR.
