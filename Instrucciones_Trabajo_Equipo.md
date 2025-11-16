# 🧠 Instrucciones para Trabajo en Equipo – Proyecto *Barbería Los Hermanos*
### (Visual Studio 2022 + GitHub)

---

## 🧩 1️⃣ Conexión al repositorio remoto

1. Abre **Visual Studio 2022**.  
2. Ve a **Git → Clone Repository**.  
3. En el campo **Repository Location**, pega el enlace del repositorio GitHub, por ejemplo:  
   ```
   https://github.com/BarberiaLosHermanos/ProyectoFinal.git
   ```
4. Elige una carpeta local (por ejemplo `D:\Barberia`) y haz clic en **Clone**.  
5. Espera a que Visual Studio descargue el proyecto completo.

---

## 🌿 2️⃣ Crear tu propia rama (branch)

Cada integrante debe crear **una rama con el nombre de la fase** que trabajará.

| Semana | Fase | Nombre sugerido de la rama |
|:--|:--|:--|
| 10 | Configuración base | `fase-1-configuracion-base` |
| 11 | Login y autenticación | `fase-2-login-autenticacion` |
| 12 | Entidades y relaciones | `fase-3-entidades-relaciones` |
| 13 | Diseño visual | `fase-4-diseno-ui` |
| 14 | Integración final | `fase-5-integracion-final` |

**Pasos:**
1. Abre la ventana **Git Changes** (Ctrl + G, luego C).  
2. En la esquina superior derecha, abre el menú desplegable de ramas.  
3. Selecciona **New Branch**.  
4. Escribe el nombre de tu rama, por ejemplo `fase-3-entidades-relaciones`.  
5. Marca **Checkout branch** (para empezar a trabajar en ella).  
6. Haz clic en **Create**.

---

## 💾 3️⃣ Realizar cambios en tu rama

Una vez dentro de tu rama:
- Modifica los archivos correspondientes (modelos, controladores o vistas).  
- Guarda tus cambios (**Ctrl + S**).  
- Verifica que el proyecto compile correctamente antes de hacer commit.

---

## 🧠 4️⃣ Hacer *commit* de tus cambios

1. Abre **View → Git Changes**.  
2. En la sección **Changes**, revisa los archivos modificados.  
3. Escribe un mensaje claro en **Message**, por ejemplo:  
   ```
   Agregado modelo Factura y relación con Cita (fase 3)
   ```
4. Haz clic en **Commit All** (guarda localmente).  
5. Luego haz clic en **Commit All and Push** (sube a GitHub).  

💡 Si solo hiciste *Commit All*, puedes subirlo manualmente después con:  
**Git → Push** (flecha hacia arriba ⬆️).

---

## ☁️ 5️⃣ Verificar el *push* en GitHub

1. Entra al repositorio desde GitHub.  
2. Haz clic en la pestaña **Branches**.  
3. Verifica que tu rama aparece (por ejemplo `fase-3-entidades-relaciones`).  
4. Comprueba que tus cambios fueron subidos correctamente.

---

## 🔁 6️⃣ Crear un Pull Request

Cuando termines tu parte:

1. Abre el repositorio en GitHub.  
2. Presiona el botón verde **Compare & Pull Request**.  
3. En el título escribe algo como:  
   ```
   Fase 3: Relaciones completas y CRUD de Factura
   ```
4. Añade comentarios si es necesario.  
5. Clic en **Create Pull Request**.

> El encargado de revisión validará tu código antes de unirlo (merge) con la rama principal.

---

## 🧭 7️⃣ Buenas prácticas del equipo

- No trabajes directamente en la rama **main**.  
- Cada integrante debe usar **su propia rama**.  
- Siempre crear **Pull Request**, no hacer *Merge manual*.  
- Antes de iniciar una nueva fase, hacer **Pull desde main**:  
  - Menú → **Git → Pull**  
  - O clic en el ícono de flecha hacia abajo (⤓) en la barra Git.

---

## ✨ Estructura sugerida

```
📦 ProyectoFinal
 ┣ 📂 Models
 ┣ 📂 Controllers
 ┣ 📂 Views
 ┣ 📄 appsettings.json
 ┗ 🌿 ramas activas
     ┣ fase-1-configuracion-base
     ┣ fase-2-login-autenticacion
     ┣ fase-3-entidades-relaciones
     ┣ fase-4-diseno-ui
     ┗ fase-5-integracion-final
```

---

## ✅ Recordatorio final

- Cada miembro trabaja en **una rama independiente**.  
- Al terminar su parte, **sube los cambios** con commit y push.  
- Crear el **Pull Request** hacia `develop` o `main`.  
- Esperar aprobación antes del merge.  
- Mantener los mensajes de commit claros y con descripción corta.

---

💡 **Consejo**: Pueden crear un tablero en GitHub Projects con columnas:  
`To Do` | `In Progress` | `Review` | `Done`  
y mover las tareas del plan semanal según el progreso.
