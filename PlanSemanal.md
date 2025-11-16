# 🧾 Plan Semanal del Proyecto – Barbería Los Hermanos

Antes de empezar a trabajar en cualquier tarea, siempre debemos leer las [instrucciones del proyecto](https://github.com/delroycampbell/BarberiaDraft/blob/master/Instrucciones_Trabajo_Equipo.md)

Si tienen dudas me pueden contactar directamente por medio de whastapp

## 🗓️ Semana 10 – Fase 1: Configuración y modelos base
### 🎯 Objetivo principal  
Crear la estructura base del proyecto y la conexión a la base de datos.

#### ✅ Tareas
- [ ] Crear la solución **ASP.NET Core MVC**.  
- [ ] Agregar el archivo `appsettings.json` con la cadena de conexión.  
- [ ] Crear la clase **AppDbContext** y registrar el servicio en `Program.cs`.  
- [ ] Implementar los modelos base: `Usuario` y `Rol`.  
- [ ] Ejecutar la primera migración con `Add-Migration Inicial`.  
- [ ] Verificar que se genere la base de datos correctamente.  

#### 🧩 Modelos involucrados
`Usuario`, `Rol`, `AppDbContext`

#### 👥 Asignación sugerida
Equipo técnico – configuración inicial y migración  
Equipo documentación – estructura del proyecto

---

## 🗓️ Semana 11 – Fase 2: Login y autenticación
### 🎯 Objetivo principal  
Implementar el sistema de registro, login y roles.

#### ✅ Tareas
- [ ] Crear el **AccountController** con métodos `Login` y `Registro`.  
- [ ] Diseñar las vistas Razor `Login.cshtml` y `Registro.cshtml`.  
- [ ] Implementar cifrado de contraseñas (Hash + Salt).  
- [ ] Configurar roles: `Administrador`, `Barbero`, `Cliente`.  
- [ ] Aplicar `[Authorize(Roles="...")]` en controladores.  
- [ ] Probar la autenticación y asignación de roles.  

#### 🧩 Modelos involucrados
`Usuario`, `Rol`

#### 👥 Asignación sugerida
Equipo backend – autenticación y seguridad  
Equipo frontend – vistas y pruebas de validación

---

## 🗓️ Semana 11 – Fase 3: Relaciones completas
### 🎯 Objetivo principal  
Definir todas las entidades y sus relaciones en la base de datos.

#### ✅ Tareas
- [ ] Crear los modelos `Cita`, `EstadoCita`, `Servicio`, `CitaServicio`, `Factura`, `MetodoPago`.  
- [ ] Establecer las relaciones:
  - [ ] `Usuario → Cita (1:N)`
  - [ ] `Cita ↔ Servicio (N:M)` usando `CitaServicio`
  - [ ] `Cita ↔ Factura (1:1)`
  - [ ] `Factura ↔ MetodoPago (N:1)`  
- [ ] Crear controladores iniciales (`CitaController`, `ServicioController`).  
- [ ] Probar integridad referencial con `Add-Migration Relaciones`.  

#### 🧩 Modelos involucrados
`Usuario`, `Cita`, `Servicio`, `Factura`, `MetodoPago`, `EstadoCita`, `CitaServicio`

#### 👥 Asignación sugerida
Equipo de base de datos – diseño y relaciones  
Equipo de lógica – controladores y pruebas CRUD

---

## 🗓️ Semana 12 – Fase 4: Lógica de Negocio
### 🎯 Objetivo principal  
Implementar la lógica funcional del sistema (CRUD y flujo de trabajo).

#### ✅ Tareas
- [ ] Crear el CRUD completo para:
  - [ ] Citas
  - [ ] Servicios
  - [ ] Facturas  
- [ ] Validar fechas de cita, disponibilidad y estado.  
- [ ] Implementar flujo completo:  
  `Cliente → Agenda cita → Barbero aplica servicio → Se genera factura`.  
- [ ] Agregar mensajes de validación visual y alertas.  

#### 🧩 Modelos involucrados
`Cita`, `Servicio`, `Factura`, `EstadoCita`

#### 👥 Asignación sugerida
Equipo de desarrollo – lógica CRUD  
Equipo QA – pruebas de flujo funcional

---

## 🗓️ Semana 12 – Fase 4: Primera Fase de Diseño Visual
### 🎯 Objetivo principal  
Construir la estructura visual del sistema.

#### ✅ Tareas
- [ ] Crear `_Layout.cshtml` (navbar, footer y contenedor principal).  
- [ ] Integrar **Bootstrap 5** en el proyecto.  
- [ ] Establecer estilo visual coherente (colores, tipografía).  
- [ ] Unificar vistas bajo una plantilla base.  

#### 🧩 Vistas involucradas
`Login`, `Registro`, `Home`, `Cita`, `Servicio`, `Factura`

#### 👥 Asignación sugerida
Equipo de diseño – maquetación y estructura  
Equipo frontend – integración visual y pruebas

---

## 🗓️ Semana 13 – Fase 5: Segunda Fase de Diseño Visual
### 🎯 Objetivo principal  
Mejorar la experiencia visual del usuario (UX/UI).

#### ✅ Tareas
- [ ] Optimizar formularios con validaciones visuales.  
- [ ] Implementar alertas dinámicas (éxito, error, advertencia).  
- [ ] Alinear tablas y botones en todas las vistas.  
- [ ] Adaptar diseño responsive (móvil y escritorio).  

#### 🧩 Vistas involucradas
Todas las vistas Razor existentes.

#### 👥 Asignación sugerida
Equipo de diseño – mejoras visuales y validaciones  
Equipo de integración – revisión responsive

---

## 🗓️ Semana 14 – Fase 6: Última Fase de Diseño Visual
### 🎯 Objetivo principal  
Finalizar el diseño visual y coherencia de todo el sistema.

#### ✅ Tareas
- [ ] Revisar colores, tipografía y márgenes.  
- [ ] Uniformar botones y formularios.  
- [ ] Aplicar formato final de interfaz.  
- [ ] Prueba general del diseño completo.  

#### 👥 Asignación sugerida
Equipo de diseño – retoques visuales  
Equipo QA – revisión del flujo completo

---

## 🗓️ Semana 14 – Fase 7: Integración Final del Proyecto (Prod)
### 🎯 Objetivo principal  
Integrar todas las ramas y preparar la versión final del sistema.

#### ✅ Tareas
- [ ] Unir todas las ramas (`develop → main`).  
- [ ] Probar flujo completo del sistema:  
  `Registro → Login → Cita → Servicio → Factura`.  
- [ ] Generar el **Manual de Usuario (PDF)**.  
- [ ] Crear **Diagrama UML actualizado**.  
- [ ] Tomar capturas y compilar documentación final.  
- [ ] Subir versión final a GitHub y probar en entorno local.  

#### 👥 Asignación sugerida
Todo el equipo – integración, pruebas y documentación
