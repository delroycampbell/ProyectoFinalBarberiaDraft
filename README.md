# 📘 Proyecto Final Barbería – ASP.NET Core MVC

## 🧩 Descripción general

Este proyecto implementa un sistema de gestión para una barbería o sala de masajes, desarrollado en ASP.NET Core MVC (versión 8) con Entity Framework Core (8.0.20) bajo el enfoque Code First.
El sistema permite administrar usuarios, roles, citas, servicios y su historial, manteniendo una arquitectura limpia y escalable basada en patrones de diseño.

## ⚙️ Tecnologías utilizadas

-Framework: .NET 8.0
-ORM: Entity Framework Core 8.0.20
-Base de datos: SQL Server
-Frontend: Bootstrap 5
-IDE: Visual Studio 2022
-Lenguaje: C#

## 🧩 Principales entidades

| Entidad          | Descripción                                                               |
| ---------------- | ------------------------------------------------------------------------- |
| **Usuario**      | Representa a los usuarios del sistema (Administrador, Barbero o Cliente). |
| **Rol**          | Define los tipos de rol asignables a los usuarios.                        |
| **Cita**         | Contiene la información de las citas agendadas.                           |
| **Servicio**     | Lista los servicios ofrecidos con su precio y descripción.                |
| **CitaServicio** | Tabla intermedia que conecta las citas con los servicios seleccionados.   |
| **Factura**      | Registra los pagos realizados por los clientes.                           |
| **MetodoPago**   | Define los métodos de pago disponibles (efectivo, tarjeta, etc.).         |


## 🧠 Relaciones principales (EF Core)
| Relación          | Descripción                                    |
| ----------------- | ---------------------------------------------- |
| Usuario ↔ Rol     | N:1 (cada usuario pertenece a un rol).         |
| Usuario ↔ Cita    | 1:N (un usuario puede tener varias citas).     |
| Cita ↔ Servicio   | N:M (una cita puede incluir varios servicios). |
| Cita ↔ EstadoCita | N:1 (cada cita tiene un estado).               |
| Factura ↔ Usuario | N:1 (una factura pertenece a un usuario).      |
| Factura ↔ Cita    | 1:1 (cada cita tiene su factura asociada).     |

## 🧰 Migraciones y Base de Datos (usando Package Manager Console)

Para generar y aplicar las migraciones con Entity Framework Core 8.0.20 directamente desde Visual Studio:

En la barra superior, selecciona:
Tools → NuGet Package Manager → Package Manager Console

En la consola que aparece en la parte inferior, ejecutar los siguientes comandos:
1.Crear Migracion
```Add-Migration PrimeraMigracion```
2. Crear/Actualizar Base de datos
```UpdateDatabase```

## 🎨 Frontend (Bootstrap 5)

El proyecto utiliza Bootstrap 5 para el diseño visual responsivo.
Se incluyen componentes como formularios, botones y tablas con clases personalizadas para un estilo limpio y moderno.

## 🧩 Patrones de diseño implementados

MVC (Model-View-Controller): Separación clara entre presentación, lógica y datos.

Repository Pattern (en planificación): Para desacoplar la lógica de acceso a datos.

Unit of Work (en planificación): Manejo eficiente de transacciones múltiples.

Observer (futuro módulo de notificaciones): Para actualizaciones automáticas en citas o cambios de estado.

## 📦 Dependencias principales
Microsoft.EntityFrameworkCore (8.0.20)
Microsoft.EntityFrameworkCore.SqlServer (8.0.20)
Microsoft.EntityFrameworkCore.Tools (8.0.20)
Bootstrap (v5.x)

## 🚀 Ejecución del proyecto

Clonar el repositorio:

git clone https://github.com/tuusuario/ProyectoFinalBarberia.git


Abrir la solución en Visual Studio 2022.

Configurar la cadena de conexión en appsettings.json.

Ejecutar los comandos de migración.

Iniciar el servidor:

dotnet run


Acceder a:

http://localhost:port

## 👤 Autores


Proyecto académico: Universidad Americana – Ingeniería de Sistemas
