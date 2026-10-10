# Sistema de Gestión del Club Deportivo

Proyecto académico en C# Windows Forms y MySQL para administrar socios, cuotas y vencimientos.

## Funcionalidades
- Inicio de sesión.
- Alta y consulta de socios.
- Registro de pagos de cuotas.
- Consulta de cuotas vencidas hasta la fecha seleccionada.
- Base de datos MySQL con script de creación.

## Requisitos
- Windows.
- Visual Studio 2022 o posterior con desarrollo de escritorio .NET.
- .NET 8 SDK.
- MySQL Server 8 o compatible.

## Cómo ejecutar
1. Crear la base de datos ejecutando `database/club_deportivo.sql` en MySQL.
2. Abrir `ClubDeportivo.sln` o el archivo `src/ClubDeportivo/ClubDeportivo.csproj` en Visual Studio.
3. Ajustar la cadena de conexión en `src/ClubDeportivo/Infraestructura/Conexion.cs`.
4. Restaurar paquetes NuGet y ejecutar el proyecto.

## Acceso inicial
El script crea un usuario de demostración: **admin / admin123**. Cambiar la contraseña antes de usar datos reales.

## Estado de la entrega
La estructura inicial y la documentación están cargadas en GitHub. **Todavía no debe considerarse una entrega final lista**: falta compilar y probar en Visual Studio con MySQL real, completar las pruebas observadas y terminar los módulos pendientes que se detallan en `docs/ANALISIS_Y_DISENO.md`.

## Estructura
- `src/ClubDeportivo`: aplicación Windows Forms.
- `database`: script SQL.
- `docs`: pruebas y notas de entrega.
